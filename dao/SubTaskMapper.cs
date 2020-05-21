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
            string sql = string.Format($"INSERT INTO subtask(subtaskName, subtaskState,rootTaskId) VALUES ('{subTask.SubTaskName}','{subTask.SubTaskState}','{taskId}'')");
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
         * 获取所有子任务
         */
        internal List<SubTask> getSubTasks(int taskId)
        {
            throw new NotImplementedException();
        }

        /**
         * 添加二级子任务
         */
        internal SubTask addSonSubTask(int subTaskId, SubTask subTask)
        {
            string sql = string.Format($"INSERT INTO subtask(subtaskName, subtaskState, parentTaskId) VALUES ('{subTask.SubTaskName}','{subTask.SubTaskState}','{subTaskId}'')");
            return addParentSubTask(sql,subTask);
        }
    }
}
