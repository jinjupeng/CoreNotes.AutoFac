using System;
using SqlSugar;

namespace CoreNotes.AutoFac.Repository.sugar
{
    public class DbContext
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public static string ConnectionString { get; set; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public static DbType DbType { get; set; }

        /// <summary>
        /// 数据连接对象
        /// </summary>
        public SqlSugarClient Db { get; private set; }

        /// <summary>
        /// 数据库上下文实例（自动关闭连接）
        /// </summary>
        public static DbContext Context
        {
            get
            {
                return new DbContext();
            }

        }
        /// <summary>
        /// 功能描述:构造函数
        /// </summary>
        /// <param name="blnIsAutoCloseConnection">是否自动关闭连接</param>
        private DbContext(bool blnIsAutoCloseConnection = true)
        {
            if (string.IsNullOrEmpty(ConnectionString))
                throw new ArgumentNullException("数据库连接字符串为空!");
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = ConnectionString, // 必填，数据库连接字符串
                DbType = DbType, // 必填，数据库类型
                IsAutoCloseConnection = blnIsAutoCloseConnection, // 默认false，自动关闭数据库连接，设置为true时无需使用using或Close操作
                InitKeyType = InitKeyType.Attribute, // 从实体特性中读取主键或自增列信息
                IsShardSameThread = false,
                ConfigureExternalServices = new ConfigureExternalServices()
                {
                    //DataInfoCacheService = new HttpRuntimeCache()
                },
                MoreSettings = new ConnMoreSettings() // 用于一些全局设置
                {
                    // IsWithNoLockQuery = true, // true表式查询的时候默认会加上.With(SqlWith.NoLock)，可以用With(SqlWith.Null)让全局的失效
                    IsAutoRemoveDataCache = true // true表示可以自动删除二级缓存
                }
            });
        }

        #region 实例方法
        /// <summary>
        /// 功能描述:获取数据库处理对象
        /// </summary>
        /// <returns>返回值</returns>
        public SimpleClient<T> GetDb<T>() where T : class, new()
        {
            return new SimpleClient<T>(Db);
        }
        /// <summary>
        /// 功能描述:获取数据库处理对象
        /// </summary>
        /// <param name="db">db</param>
        /// <returns>返回值</returns>
        public SimpleClient<T> GetDb<T>(SqlSugarClient db) where T : class, new()
        {
            return new SimpleClient<T>(db);
        }
        #endregion

        #region 根据实体类生成数据库表
        /// <summary>
        /// 功能描述:根据实体类生成数据库表
        /// </summary>
        /// <param name="blnBackupTable">是否备份表</param>
        /// <param name="lsTs">指定的实体</param>
        public void CreateTableByEntity<T>(bool blnBackupTable, params T[] lsTs) where T : class, new()
        {
            Type[] lstTypes = null;
            if (lsTs != null)
            {
                lstTypes = new Type[lsTs.Length];
                for (int i = 0; i < lsTs.Length; i++)
                {
                    T t = lsTs[i];
                    lstTypes[i] = typeof(T);
                }
            }
            CreateTableByEntity(blnBackupTable, lstTypes);
        }

        /// <summary>
        /// 功能描述:根据实体类生成数据库表
        /// </summary>
        /// <param name="blnBackupTable">是否备份表</param>
        /// <param name="lsTs">指定的实体</param>
        public void CreateTableByEntity(bool blnBackupTable, params Type[] lsTs)
        {
            if (blnBackupTable)
            {
                Db.CodeFirst.BackupTable().InitTables(lsTs); //change entity backupTable            
            }
            else
            {
                Db.CodeFirst.InitTables(lsTs);
            }
        }
        #endregion

        #region 静态方法

        /// <summary>
        /// 功能描述:获得一个DbContext
        /// </summary>
        /// <param name="blnIsAutoCloseConnection">是否自动关闭连接（如果为false，则使用接受时需要手动关闭Db）</param>
        /// <returns>返回值</returns>
        public static DbContext GetDbContext(bool blnIsAutoCloseConnection = true)
        {
            return new DbContext(blnIsAutoCloseConnection);
        }

        /// <summary>
        /// 功能描述:设置初始化参数
        /// </summary>
        /// <param name="strConnectionString">连接字符串</param>
        /// <param name="enmDbType">数据库类型</param>
        public static void Init(string strConnectionString, DbType enmDbType = SqlSugar.DbType.SqlServer)
        {
            ConnectionString = strConnectionString;
            DbType = enmDbType;
        }

        /// <summary>
        /// 功能描述:创建一个链接配置
        /// </summary>
        /// <param name="blnIsAutoCloseConnection">是否自动关闭连接</param>
        /// <param name="blnIsShardSameThread">是否夸类事务</param>
        /// <returns>ConnectionConfig</returns>
        public static ConnectionConfig GetConnectionConfig(bool blnIsAutoCloseConnection = true, bool blnIsShardSameThread = false)
        {
            ConnectionConfig config = new ConnectionConfig()
            {
                ConnectionString = ConnectionString,
                DbType = DbType,
                IsAutoCloseConnection = blnIsAutoCloseConnection,
                ConfigureExternalServices = new ConfigureExternalServices()
                {
                    //DataInfoCacheService = new HttpRuntimeCache()
                },
                IsShardSameThread = blnIsShardSameThread
            };
            return config;
        }

        /// <summary>
        /// 功能描述:获取一个自定义的DB
        /// </summary>
        /// <param name="config">config</param>
        /// <returns>返回值</returns>
        public static SqlSugarClient GetCustomDb(ConnectionConfig config)
        {
            return new SqlSugarClient(config);
        }
        /// <summary>
        /// 功能描述:获取一个自定义的数据库处理对象
        /// </summary>
        /// <param name="sugarClient">sugarClient</param>
        /// <returns>返回值</returns>
        public static SimpleClient<T> GetCustomEntityDb<T>(SqlSugarClient sugarClient) where T : class, new()
        {
            return new SimpleClient<T>(sugarClient);
        }
        /// <summary>
        /// 功能描述:获取一个自定义的数据库处理对象
        /// </summary>
        /// <param name="config">config</param>
        /// <returns>返回值</returns>
        public static SimpleClient<T> GetCustomEntityDb<T>(ConnectionConfig config) where T : class, new()
        {
            SqlSugarClient sugarClient = GetCustomDb(config);
            return GetCustomEntityDb<T>(sugarClient);
        }
        #endregion
    }
}
