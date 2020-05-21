using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 高主动性的todo清单
{
    class Task
    {
        private int taskId;
        private string taskName;
        private string taskDescription;
        private DateTime taskDate;
        private int priority;
        private int spendTime;
        private List<SubTask> subTasks;

        public Task(int taskId, string taskName, string taskDescription, DateTime taskDate, int priority, int spendTime, List<SubTask> subTasks)
        {
            this.TaskId = taskId;
            this.TaskName = taskName;
            this.TaskDescription = taskDescription;
            this.TaskDate = taskDate;
            this.Priority = priority;
            this.SpendTime = spendTime;
            this.SubTasks = subTasks;
        }

        public int TaskId { get => taskId; set => taskId = value; }
        public string TaskName { get => taskName; set => taskName = value; }
        public string TaskDescription { get => taskDescription; set => taskDescription = value; }
        public DateTime TaskDate { get => taskDate; set => taskDate = value; }
        public int Priority { get => priority; set => priority = value; }
        public int SpendTime { get => spendTime; set => spendTime = value; }
        internal List<SubTask> SubTasks { get => subTasks; set => subTasks = value; }
    }
}
