using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 高主动性的todo清单
{
    class SQLiteConnectionPool
    {
        private static string prifix = "Data Source=" + AppDomain.CurrentDomain.BaseDirectory;
        private static string dataBaseName = @"todolist.db";
        private static SQLiteConnection connection = null;

        public static SQLiteConnection getConnection() {
            if (connection == null) {
                connection = new SQLiteConnection(prifix + dataBaseName);
                connection.Open();
            }
            return connection;
        }

    }
}
