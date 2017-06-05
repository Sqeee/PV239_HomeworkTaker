using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Notifications;
using HomeworkTaker.Models;

namespace HomeworkTaker.Notifications
{
    public static class NotificationsManager
    {
        public static DateTime LastUpdateTasksFile { get; private set; }

        public static void AddTaskNotification(TaskModel task)
        {
            TileNotificationsManager.AddTileNotification(task);
            ToastNotificationsManager.AddToastNotification(task);
        }

        public static void RemoveTaskNotification(TaskModel task)
        {
            ToastNotificationManager.History.RemoveGroup(task.Id.ToString());
            ScheduledTileNotification notification = TileUpdateManager.CreateTileUpdaterForApplication().GetScheduledTileNotifications().FirstOrDefault(n => n.Id == task.Id.ToString());
            if (notification != null)
            {
                TileUpdateManager.CreateTileUpdaterForApplication().RemoveFromSchedule(notification);
                ScheduledToastNotification toast = ToastNotificationManager.CreateToastNotifier().GetScheduledToastNotifications().FirstOrDefault(n => n.Id == task.Id.ToString());
                if (toast != null)
                {
                    ToastNotificationManager.CreateToastNotifier().RemoveFromSchedule(toast);
                }
                else
                {
                    UpdateTaskNotifications();
                }
            }
            else
            {
                UpdateTaskNotifications();
            }
        }

        public static void UpdateTaskNotifications()
        {
            LastUpdateTasksFile = Data.TasksDAL.LastChange();
            CleanTaskNotifications();
            List<TaskModel> tasks = Data.TasksDAL.GetTasks();
            tasks.Where(t => t.Notification < DateTime.Now).OrderBy(t => t.Notification).Take(5).ToList().ForEach(AddTaskNotification);
            tasks.Where(t => t.Notification > DateTime.Now).ToList().ForEach(AddTaskNotification);
        }

        private static void CleanTaskNotifications()
        {
            TileUpdateManager.CreateTileUpdaterForApplication().Clear();
            foreach (ScheduledTileNotification notification in TileUpdateManager.CreateTileUpdaterForApplication().GetScheduledTileNotifications())
            {
                TileUpdateManager.CreateTileUpdaterForApplication().RemoveFromSchedule(notification);
            }
            foreach (ScheduledToastNotification notification in ToastNotificationManager.CreateToastNotifier().GetScheduledToastNotifications())
            {
                ToastNotificationManager.CreateToastNotifier().RemoveFromSchedule(notification);
            }
        }
    }
}
