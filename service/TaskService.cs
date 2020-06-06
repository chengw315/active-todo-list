using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 高主动性的todo清单
{
    class TaskService
    {
        //视图对应的域
        private List<Task> tasks;

        private TaskMapper taskMapper = new TaskMapper();
        private SubTaskMapper subTaskMapper = new SubTaskMapper();
        private TaskScriptMapper scriptMapper = new TaskScriptMapper();

        internal List<Task> Tasks { get => tasks; set => tasks = value; }

        public void getAllTasks()
        {
            //先获取任务集合
            tasks = taskMapper.getAllTasks();
            completeTasks();

        }

        private void completeTasks()
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                //获取任务的一级子任务集合
                List<SubTask> subTasks = subTaskMapper.getSubTasks(tasks.ElementAt(i).TaskId);
                tasks.ElementAt(i).SubTasks = subTasks;

                for (int j = 0; j < subTasks.Count; j++)
                {
                    //获取子任务的脚本
                    subTasks.ElementAt(j).Script = scriptMapper.getScript4SubTask(subTasks.ElementAt(j).Id);
                    //获取一级子任务的二级子任务集合
                    subTasks.ElementAt(j).SubTasks = subTaskMapper.getSonSubTasks(subTasks.ElementAt(j).Id);
                }
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
            subTask.SubTaskState = 0;
            return subTaskMapper.addSonSubTask(subTaskId, subTask);
        }

        internal void changeDescription(int taskId, string description)
        {
            taskMapper.changeDescription(taskId, description);
        }

        internal void changePriority(int taskId, int priority)
        {
            taskMapper.changePriority(taskId, priority);
        }

        internal void changeTime(int taskId, DateTime? time)
        {
            taskMapper.changeTime(taskId, time);
        }

        internal void changeDate(int taskId, DateTime? date)
        {
            taskMapper.changeDate(taskId, date);
        }

        internal Task addNewTask()
        {
            Task task = new Task();
            task.TaskName = "新任务";
            task.Priority = 0;
            task.TaskDate = new DateTime();
            task.TaskDescription = "";
            return taskMapper.addNewTask(task);
        }

        internal void search(string searchWord)
        {
            tasks = taskMapper.selectByPartName(searchWord);
            completeTasks();
        }
    }
}
