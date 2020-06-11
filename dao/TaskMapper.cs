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
            return select(sql_select);
        }

        private static List<Task> select(string sql_select)
        {
            SQLiteDataReader sQLiteDataReader = SQLiteExecutor.select(sql_select);
            List<Task> result = new List<Task>();
            while (sQLiteDataReader.Read())
            {
                int id = sQLiteDataReader.GetInt32(0);
                string taskName = sQLiteDataReader.GetString(1);
                string desc = sQLiteDataReader.GetString(4);
                string dateString = sQLiteDataReader.GetString(5);
                DateTime dateTime = DateTime.Now;
                try {
                    dateTime = Convert.ToDateTime(dateString); }
                catch (Exception e) {                
                }
                int priority = sQLiteDataReader.GetInt32(2);
                Task task = new Task(id, taskName, desc, dateTime, priority);
                result.Add(task);
            }
            return result;
        }

        public Task addNewTask(Task task) {
            string sql = string.Format($"INSERT INTO task(task_name,task_priority,task_state,task_description,task_date) VALUES ('{task.TaskName}','{task.Priority}','{task.TaskState}','{task.TaskDescription}','{task.TaskDate}')");
            //插入子任务
            SQLiteExecutor.execute(sql);
            //获取刚插入的数据返回
            string sql_select = string.Format("select id from task order by id desc limit 1");
            SQLiteDataReader sQLiteDataReader = SQLiteExecutor.select(sql);
            if (!sQLiteDataReader.Read()) return task;
            task.TaskId = sQLiteDataReader.GetInt32(0);
            return task;
        }

        internal void changeDescription(int taskId, string description)
        {
            string sql_update = string.Format($"update task set task_description =  '{description}' where id = {taskId}");
            SQLiteExecutor.execute(sql_update);
        }

        internal void changePriority(int taskId, int priority)
        {
            string sql_update = string.Format($"update task set task_priority =  {priority} where id = {taskId}");
            SQLiteExecutor.execute(sql_update);
        }

        internal List<Task> selectByPartName(string searchWord)
        {
            string sql_select = string.Format($"select id,task_name,task_priority,task_state,task_description,task_date from task where task_name like '%{searchWord.Replace('\'', ' ')}%'");
            return select(sql_select);
        }

        internal List<Task> selectById(int id)
        {
            string sql_select = string.Format($"select id,task_name,task_priority,task_state,task_description,task_date from task where id = {id}");
            return select(sql_select);
        }

        internal void changeDate(int taskId, DateTime? date)
        {
            List<Task> list = selectById(taskId);
            if (list.Count == 0)
                return;
            DateTime taskDate = list.ElementAt(0).TaskDate;
            string sql_update = string.Format($"update task set task_date =  '{date.ToString().Split(' ')[0]} {taskDate.TimeOfDay}' where id = {taskId}");
            SQLiteExecutor.execute(sql_update);
        }

        internal List<Task> selectOrderByDate()
        {
            string sql_select = string.Format($"select id,task_name,task_priority,task_state,task_description,task_date from task order by task_date");
            return select(sql_select);
        }

        internal List<Task> selectInDate(DateTime? selectedDate)
        {
            string sql_select = string.Format($"select id,task_name,task_priority,task_state,task_description,task_date from task where task_date like '%{selectedDate.ToString().Split(' ')[0]}%'");
            return select(sql_select);
        }

        internal List<Task> selectOrderByPriority()
        {
            string sql_select = string.Format($"select id,task_name,task_priority,task_state,task_description,task_date from task order by task_priority");
            return select(sql_select);
        }

        internal void changeName(int id, string newName)
        {
            string sql_update = string.Format($"update task set task_Name =  '{newName}' where id = {id}");
            SQLiteExecutor.execute(sql_update);
        }

        internal void changeTime(int taskId, DateTime? time)
        {
            List<Task> list = selectById(taskId);
            if (list.Count == 0)
                return;
            DateTime taskDate = list.ElementAt(0).TaskDate;
            string sql_update = string.Format($"update task set task_date =  '{taskDate.Date.ToString().Split(' ')[0]} {time.ToString().Split(' ')[1]}' where id = {taskId}");
            SQLiteExecutor.execute(sql_update);
        }
    }
}
