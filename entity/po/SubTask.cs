using System.Collections.Generic;

namespace 高主动性的todo清单
{
    class SubTask
    {
        private int id;
        private string subTaskName;
        private int subTaskState;
        private List<SubTask> subTasks;
        private TaskScript script;

        public int Id { get => id; set => id = value; }
        public string SubTaskName { get => subTaskName; set => subTaskName = value; }
        public int SubTaskState { get => subTaskState; set => subTaskState = value; }
        internal TaskScript Script { get => script; set => script = value; }
        internal List<SubTask> SubTasks { get => subTasks; set => subTasks = value; }
    }
}