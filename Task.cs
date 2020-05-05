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
        private int spendDay;
        private int spendHour;
        private int spendSecond;
        private List<SubTask> subTasks;
    }
}
