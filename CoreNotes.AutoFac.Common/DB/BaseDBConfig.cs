using System.IO;
using CoreNotes.AutoFac.Common.Helper;

namespace CoreNotes.AutoFac.Common.DB
{
    public class BaseDbConfig
    {
        private static string sqliteConnection = AppSettings.app(new string[] { "AppSettings", "Sqlite", "SqliteConnection" });
        private static bool isSqliteEnabled = (AppSettings.app(new string[] { "AppSettings", "Sqlite", "Enabled" })).ObjToBool();

        private static string sqlServerConnection = AppSettings.app(new string[] { "AppSettings", "SqlServer", "SqlServerConnection" });
        private static bool isSqlServerEnabled = (AppSettings.app(new string[] { "AppSettings", "SqlServer", "Enabled" })).ObjToBool();

        private static string mySqlConnection = AppSettings.app(new string[] { "AppSettings", "MySql", "MySqlConnection" });
        private static bool isMySqlEnabled = (AppSettings.app(new string[] { "AppSettings", "MySql", "Enabled" })).ObjToBool();

        private static string oracleConnection = AppSettings.app(new string[] { "AppSettings", "Oracle", "OracleConnection" });
        private static bool IsOracleEnabled = (AppSettings.app(new string[] { "AppSettings", "Oracle", "Enabled" })).ObjToBool();


        public static string ConnectionString => InitConn();
        public static DataBaseType DbType = DataBaseType.SqlServer;


        private static string InitConn()
        {
            if (isSqliteEnabled)
            {
                DbType = DataBaseType.Sqlite;
                return sqliteConnection;
            }
            else if (isSqlServerEnabled)
            {
                DbType = DataBaseType.SqlServer;
                // return DifDbConnOfSecurity(@"D:\my-file\dbCountPsw1.txt", @"c:\my-file\dbCountPsw1.txt", sqlServerConnection);
                return sqlServerConnection;
            }
            else if (isMySqlEnabled)
            {
                DbType = DataBaseType.MySql;
                return DifDbConnOfSecurity(@"D:\my-file\dbCountPsw1_MySqlConn.txt", @"c:\my-file\dbCountPsw1_MySqlConn.txt", mySqlConnection);
            }
            else if (IsOracleEnabled)
            {
                DbType = DataBaseType.Oracle;
                return DifDbConnOfSecurity(@"D:\my-file\dbCountPsw1_OracleConn.txt", @"c:\my-file\dbCountPsw1_OracleConn.txt", oracleConnection);
            }
            else
            {
                return "server=127.0.0.1;uid=sa;pwd=123456;database=CoreNotes.AutoFac";
            }

        }
        private static string DifDbConnOfSecurity(params string[] conn)
        {
            foreach (var item in conn)
            {
                try
                {
                    if (File.Exists(item))
                    {
                        return File.ReadAllText(item).Trim();
                    }
                }
                catch (System.Exception) { }
            }

            return conn[conn.Length - 1];
        }

    }

    public enum DataBaseType
    {
        MySql = 0,
        SqlServer = 1,
        Sqlite = 2,
        Oracle = 3,
        PostgreSQL = 4
    }
}
