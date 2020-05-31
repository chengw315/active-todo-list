using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 高主动性的todo清单
{
    class SQLiteTablesInitializer
    {
        /**
         * 初始化数据库表
         */
        public static void initTables() {
            //任务表
            initTaskTable();
            //子任务表
            initSubTaskTable();
            //脚本表
            initScriptTable();
            //打开文件脚本表
            initOpenFileScriptTable();
            //打开网页脚本表
            initOpenWebScriptTable();
            //执行用户自定义脚本表
            initUserScriptTable();
        }

        private static void initUserScriptTable()
        {
            string sql = "CREATE TABLE IF NOT EXISTS secipt_user(id int(11) NOT NULL ,script_id int(11), path varchar(255) ,sever_id int(11),PRIMARY KEY(`id`))";
            createTable(sql);
        }

        private static void initOpenWebScriptTable()
        {
            string sql = "CREATE TABLE IF NOT EXISTS script_open_web(id int(11) NOT NULL ,script_id int(11),url varchar(255), sever_id int(11), PRIMARY KEY(`id`) )";
            createTable(sql);
        }

        private static void initOpenFileScriptTable()
        {
            string sql = "CREATE TABLE IF NOT EXISTS script_open_file ( id int(11) NOT NULL ,script_id int(11),  path varchar(255) ,sever_id int(11),  PRIMARY KEY(`id`))";
            createTable(sql);
        }

        private static void initScriptTable()
        {
            string sql = "CREATE TABLE IF NOT EXISTS script(id int(11) NOT NULL,script_type int(255) NULL DEFAULT NULL,subtask_id int(11) NULL DEFAULT NULL,sever_id int(11),PRIMARY KEY(`id`))";
            createTable(sql);
        }

        private static void initSubTaskTable()
        {
            string sql = "CREATE TABLE IF NOT EXISTS subtask(id int(11) NOT NULL , subtask_name varchar(255) ,subtask_state int(1)  ,parent_id int(11) NULL DEFAULT NULL ,root_id int(11) NULL DEFAULT NULL,sever_id int(11), PRIMARY KEY(`id`))";
            createTable(sql);
        }

        private static void initTaskTable()
        {
            string sql = "CREATE TABLE IF NOT EXISTS task(id int(11) NOT NULL,task_name varchar(255),task_priority tinyint(8),task_state tinyint(1) DEFAULT 0,task_description varchar(255),  task_date datetime(0),sever_id int(11),  PRIMARY KEY(id))";
            createTable(sql);
        }

        private static void createTable(string sql)
        {
            SQLiteCommand sQLiteCommand = new SQLiteCommand();
            sQLiteCommand.Connection = SQLiteConnectionPool.getConnection();
            sQLiteCommand.CommandText = sql;
            Console.WriteLine($"建表结果: {sQLiteCommand.ExecuteNonQuery() > 0}");
        }
    }
}
