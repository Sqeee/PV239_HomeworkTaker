using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTaker.Models
{
    public class TasksModel
    {
        public List<Models.TaskModel> TasksList { get; set; }

        public TasksModel()
        {
            TasksList = Data.TasksDAL.GetTasks();
        }

        public void AddTask(Models.TaskModel newTask)
        {
            newTask.Id = getNextFreeId();
            TasksList.Add(newTask);
            Data.TasksDAL.StoreTasks(TasksList);
        }

        public void DeleteSubject(Models.TaskModel task)
        {
            for (int i = 0; i < TasksList.Count; i++)
            {
                if (TasksList[i].Id == task.Id)
                {
                    TasksList.RemoveAt(i);
                    break;
                }
            }
            Data.TasksDAL.StoreTasks(TasksList);
        }

        private int getNextFreeId()
        {
            List<int> usedIds = new List<int>(); 
            for(int i=0;i<TasksList.Count;i++)
            {
                usedIds.Add(TasksList[i].Id);
            }
            int result = 0;
            while(usedIds.Contains(result))
            {
                result++;
            }
            return result;
        }

    }
}
