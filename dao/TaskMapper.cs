using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 高主动性的todo清单.datasource;

namespace 高主动性的todo清单
{
    class TaskMapper
    {
        /**
         * 获取所有任务
         */
        internal List<Task> getAllTasks()
        {
            string sql_select = string.Format("select id,task_name,task_priority,task_state,task_description,task_date from task");
            SQLiteDataReader sQLiteDataReader = SQLiteExecutor.select(sql_select);
            List<Task> result = new List<Task>();
            while (sQLiteDataReader.Read()) {
                int id = sQLiteDataReader.GetInt32(0);
                string taskName = sQLiteDataReader.GetString(1);
                string desc = sQLiteDataReader.GetString(4);
                DateTime dateTime = Convert.ToDateTime(sQLiteDataReader.GetString(5));
                int priority = sQLiteDataReader.GetInt32(2);
                Task task = new Task(id, taskName,desc,dateTime,priority);
                result.Add(task);
            }
            return result;
        }
    }
}
