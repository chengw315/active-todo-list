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
            return getSubTasks(taskId, "root_id");
        }

        private static List<SubTask> getSubTasks(int taskId, string parentIdName)
        {
            List<SubTask> result = new List<SubTask>();
            string sql_select = string.Format($"select id,subtask_name,subtask_state from subtask where {parentIdName} = {taskId}");
            SQLiteDataReader sQLiteDataReader = SQLiteExecutor.select(sql_select);
            while (sQLiteDataReader.Read())
            {
                SubTask subTask = new SubTask();
                subTask.Id = sQLiteDataReader.GetInt32(0);
                subTask.SubTaskName = sQLiteDataReader.GetString(1);
                subTask.SubTaskState = sQLiteDataReader.GetInt32(2);
                result.Add(subTask);

            }
            return result;
        }

        /**
         * 获取所有二级子任务
         */
        public List<SubTask> getSonSubTasks(int taskId)
        {
            return getSubTasks(taskId, "parent_id");
        }

        /**
         * 添加二级子任务
         */
        internal SubTask addSonSubTask(int subTaskId, SubTask subTask)
        {
            string sql = string.Format($"INSERT INTO subtask(subtask_name, subtask_state, parent_id) VALUES ('{subTask.SubTaskName}','{subTask.SubTaskState}','{subTaskId}')");
            return addParentSubTask(sql,subTask);
        }

        internal void changeSubTaskName(int id, string newName)
        {
            newName.Replace("'","\'");
            string sql = string.Format($"UPDATE subtask SET subtask_name = '{newName}' WHERE id = {id}");
            SQLiteExecutor.execute(sql);
        }
    }
}
