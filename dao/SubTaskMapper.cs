using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 高主动性的todo清单.datasource;

namespace 高主动性的todo清单
{
    class SubTaskMapper
    {
        /**
         * 添加一级子任务
         */
        internal SubTask addParentSubTask(int taskId, SubTask subTask)
        {
            //插入
            string sql = string.Format($"INSERT INTO subtask(subtask_name, subtask_state,root_id) VALUES ('{subTask.SubTaskName}','{subTask.SubTaskState}','{taskId}')");
            return addParentSubTask(sql, subTask);
        }

        private SubTask addParentSubTask(string sql, SubTask subTask)
        {
            //插入子任务
            SQLiteExecutor.execute(sql);
            //获取刚插入的数据返回
            string sql_select = string.Format("select id from subtask order by id desc limit 1");
            SQLiteDataReader sQLiteDataReader = SQLiteExecutor.select(sql);
            if (!sQLiteDataReader.Read()) return subTask;
            subTask.Id = sQLiteDataReader.GetInt32(0);
            return subTask;
        }

        /**
         * 获取所有一级子任务
         */
        public List<SubTask> getSubTasks(int taskId)
        {
            return new List<SubTask>();
        }

        /**
         * 获取所有二级子任务
         */
        public List<SubTask> getSonSubTasks(int taskId)
        {
            return new List<SubTask>();
        }

        /**
         * 添加二级子任务
         */
        internal SubTask addSonSubTask(int subTaskId, SubTask subTask)
        {
            string sql = string.Format($"INSERT INTO subtask(subtask_name, subtask_state, parent_id) VALUES ('{subTask.SubTaskName}','{subTask.SubTaskState}','{subTaskId}'')");
            return addParentSubTask(sql,subTask);
        }
    }
}
