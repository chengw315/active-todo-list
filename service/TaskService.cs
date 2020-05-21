using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 高主动性的todo清单.service
{
    class TaskService
    {
        //视图对应的域
        private List<Task> tasks;

        private TaskMapper taskMapper = new TaskMapper();
        private SubTaskMapper subTaskMapper = new SubTaskMapper();

        internal List<Task> Tasks { get => tasks; set => tasks = value; }

        public void getAllTasks() {
            tasks = taskMapper.getAllTasks();
            for (int i = 0; i < tasks.Count; i++) {
                tasks.ElementAt(i).SubTasks = subTaskMapper.getSubTasks(tasks.ElementAt(i).TaskId);
            }
        }

        /**
         * 添加一个一级子任务
         */
        internal SubTask addParentSubTask(int taskId)
        {
            SubTask subTask = new SubTask();
            subTask.SubTaskName = "新任务";
            return subTaskMapper.addParentSubTask(taskId,subTask);
        }

        /**
         * 添加一个二级子任务
         */
        internal SubTask addSonSubTask(int subTaskId)
        {
            SubTask subTask = new SubTask();
            subTask.SubTaskName = "新任务";
            return subTaskMapper.addSonSubTask(subTaskId, subTask);
        }

        internal void changeDescription(int taskId, string description)
        {
            throw new NotImplementedException();
        }

        internal void changePriority(int taskId, int priority)
        {
            throw new NotImplementedException();
        }

        internal void changeTime(int taskId, DateTime? time)
        {
            throw new NotImplementedException();
        }

        internal void changeDate(int tag, DateTime? selectedDate)
        {
            throw new NotImplementedException();
        }
    }
}
