using System;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using HomeworkTaker.Models;

namespace HomeworkTaker.Notifications
{
    public static class TileNotificationsManager
    {
        static TileNotificationsManager()
        {
            TileUpdateManager.CreateTileUpdaterForApplication().EnableNotificationQueue(true);
        }

        public static void AddTileNotification(TaskModel task)
        {
            string source = $@"
<tile>
    <visual branding='nameAndLogo'>
        <binding template='TileSmall'>
            <text>{task.Subject}</text>
            <text hint-style='captionSubtle' hint-wrap='true'>{task.Description}</text>
        </binding>

        <binding template='TileMedium'>
            <text>{task.Subject}</text>
            <text hint-style='captionSubtle' hint-wrap='true'>{task.Description}</text>
        </binding>
 
        <binding template='TileWide'>
            <text>{task.Subject}</text>
            <text hint-style='captionSubtle' hint-wrap='true'>{task.Description}</text>
        </binding>

        <binding template='TileLarge'>
            <text>{task.Subject}</text>
            <text hint-style='captionSubtle' hint-wrap='true'>{task.Description}</text>
        </binding>
   </visual>
</tile>";
            XmlDocument xmlTile = new XmlDocument();
            xmlTile.LoadXml(source);
            if (task.Notification < DateTime.Now)
            {
                CreateTileNotification(task, xmlTile);
            }
            else
            {
                CreateScheduledTileNotification(task, xmlTile);
            }
        }

        private static void CreateTileNotification(TaskModel task, XmlDocument xml)
        {
            TileNotification tile = new TileNotification(xml);
            tile.ExpirationTime = new DateTimeOffset(task.Deadline);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tile);
        }

        private static void CreateScheduledTileNotification(TaskModel task, XmlDocument xml)
        {
            ScheduledTileNotification tile = new ScheduledTileNotification(xml, task.Notification);
            tile.Id = task.Id.ToString();
            tile.ExpirationTime = new DateTimeOffset(task.Deadline);
            TileUpdateManager.CreateTileUpdaterForApplication().AddToSchedule(tile);
        }  
    }
}
