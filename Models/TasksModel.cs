﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeworkTaker.Notifications;

namespace HomeworkTaker.Models
{
    public class TasksModel
    {
        public List<Models.TaskModel> TasksList { get; set; }

        public TasksModel()
        {
            TasksList = Data.TasksDAL.GetTasks().OrderBy(t => t.Deadline).ToList();
        }

        public void AddTask(Models.TaskModel newTask)
        {
            newTask.Id = getNextFreeId();
            TasksList.Add(newTask);
            TasksList = TasksList.OrderBy(t => t.Deadline).ToList();
            Data.TasksDAL.StoreTasks(TasksList);
            NotificationsManager.AddTaskNotification(newTask);
        }

        public void DeleteTask(Models.TaskModel task)
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
            NotificationsManager.RemoveTaskNotification(task);
        }

        public void StoreTasks()
        {
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
