using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 高主动性的todo清单.datasource
{
    class SQLiteExecutor
    {
        public static void execute(string sql) {
            SQLiteCommand sQLiteCommand = new SQLiteCommand();
            sQLiteCommand.CommandText = sql;
            sQLiteCommand.Connection = SQLiteConnectionPool.getConnection();
            Console.WriteLine($"执行成功,{sQLiteCommand.ExecuteNonQuery()}行受到影响");
        }

        public static SQLiteDataReader select(string sql)
        {
            SQLiteCommand sQLiteCommand = new SQLiteCommand();
            sQLiteCommand.CommandText = sql;
            sQLiteCommand.Connection = SQLiteConnectionPool.getConnection();
            return sQLiteCommand.ExecuteReader();
        }
    }
}
