using CoreNotes.AutoFac.Common.Helper;

namespace CoreNotes.AutoFac.Common.DB
{
    public class BaseDbConfig
    {
        private static readonly string SqLiteConnection = AppSettings.App("AppSettings", "SqLite", "SqLiteConnection");
        private static readonly bool IsSqLiteEnabled = (AppSettings.App("AppSettings", "SqLite", "Enabled")).ObjToBool();

        private static readonly string SqlServerConnection = AppSettings.App("AppSettings", "SqlServer", "SqlServerConnection");
        private static readonly bool IsSqlServerEnabled = (AppSettings.App("AppSettings", "SqlServer", "Enabled")).ObjToBool();

        private static readonly string MySqlConnection = AppSettings.App("AppSettings", "MySql", "MySqlConnection");
        private static readonly bool IsMySqlEnabled = (AppSettings.App("AppSettings", "MySql", "Enabled")).ObjToBool();

        private static readonly string OracleConnection = AppSettings.App("AppSettings", "Oracle", "OracleConnection");
        private static readonly bool IsOracleEnabled = (AppSettings.App("AppSettings", "Oracle", "Enabled")).ObjToBool();


        public static string ConnectionString => InitConn();
        public static DataBaseType DbType = DataBaseType.SqlServer;


        private static string InitConn()
        {
            if (IsSqLiteEnabled)
            {
                DbType = DataBaseType.SqLite;
                return SqLiteConnection;
            }

            if (IsSqlServerEnabled)
            {
                DbType = DataBaseType.SqlServer;
                return SqlServerConnection;
            }

            if (IsMySqlEnabled)
            {
                DbType = DataBaseType.MySql;
                return MySqlConnection;
            }

            if (IsOracleEnabled)
            {
                DbType = DataBaseType.Oracle;
                return OracleConnection;
            }

            return "server=127.0.0.1;uid=sa;pwd=123456;database=CoreNotes.AutoFac";

        }
    }

    public enum DataBaseType
    {
        MySql = 0,
        SqlServer = 1,
        SqLite = 2,
        Oracle = 3
    }
}
