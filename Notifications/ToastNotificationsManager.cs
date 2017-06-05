using System;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using HomeworkTaker.Models;

namespace HomeworkTaker.Notifications
{
    public static class ToastNotificationsManager
    {
        public static void AddToastNotification(TaskModel task)
        {
            if (task.Notification < DateTime.Now)
            {
                return;
            }
            string source = $@"
<toast launch='action=viewtasks'>
    <visual>
        <binding template='ToastGeneric'>
            <text>{task.Subject}</text>
            <text>{task.Description}</text>
        </binding>  
    </visual>

    <actions>
        <action
            content='Done'
            arguments='action=done&amp;task={task.Id}'
            activationType='background'/>

        <action
            content='Remind me later'
            arguments='action=remindlater&amp;task={task.Id}'
            activationType='background'/>
    </actions>
</toast>";
            XmlDocument xmlToast = new XmlDocument();
            xmlToast.LoadXml(source);
            ScheduledToastNotification toast = new ScheduledToastNotification(xmlToast, task.Notification, TimeSpan.FromMinutes(60), 5); //TODO odhalit
            toast.Tag = task.Id.ToString();
            toast.Group = task.Id.ToString();
            toast.Id = task.Id.ToString();
            ToastNotificationManager.CreateToastNotifier().AddToSchedule(toast);
        }
    }
}
