using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace HomeworkTaker.Data
{
    // Data access layer for subjects
    public static class SubjectsDAL
    {
        // Returns list of subjects loaded from json
        public static List<Models.SubjectModel> GetSubjects()
        {
            List<Models.SubjectModel>  subjectsList = new List<Models.SubjectModel>();
            JObject subjectsJSON;
            StorageFile subjectsFile = null;
            string content= string.Empty;

            try
            {
                var task = Task.Run(async () => { subjectsFile = await Windows.Storage.ApplicationData.Current.RoamingFolder.GetFileAsync("subjects.json"); });
                task.Wait();
                task = Task.Run(async () => { content = await Windows.Storage.FileIO.ReadTextAsync(subjectsFile); });
                task.Wait();                
                subjectsJSON = JObject.Parse(content);

            }
            catch (Exception ex)
            {
                return subjectsList;
            }

            for (int i = 0; i < ((JArray)subjectsJSON["Subjects"]).Count(); i++)
            {
                Models.SubjectModel subj = new Models.SubjectModel();
                subj.Title = (string)(subjectsJSON["Subjects"][i]["Title"]);
                subj.TitleShort = (string)(subjectsJSON["Subjects"][i]["TitleShort"]);
                subjectsList.Add(subj);
            }
            return subjectsList;
        }     

        // Store provided list of subjects into json
        public static void StoreSubjects(List<Models.SubjectModel> subjectsList)
        {
            Windows.Storage.StorageFile subjectsFile = null;
            JObject subjectsJson = new JObject();
            JArray subjects = new JArray();

            for (int i = 0; i < subjectsList.Count; i++)
            {
                JObject subj = new JObject();
                subj["Title"] = subjectsList[i].Title;
                subj["TitleShort"] = subjectsList[i].TitleShort;
                subjects.Add(subj);
            }
            subjectsJson["Subjects"] = subjects;
            try
            {
                var task = Task.Run(async () => { subjectsFile = await Windows.Storage.ApplicationData.Current.RoamingFolder.CreateFileAsync("subjects.json", Windows.Storage.CreationCollisionOption.ReplaceExisting); });
                task.Wait();
                task = Task.Run(async () => { await Windows.Storage.FileIO.WriteTextAsync(subjectsFile, subjectsJson.ToString()); });
                task.Wait();                
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }

    // Data access layer for schedule
    public static class ScheduleDAL
    {
        // Returns schedule loaded from json
        public static Models.ScheduleModel GetSchedule()
        {
            Models.ScheduleModel schedule = new Models.ScheduleModel();
            JObject scheduleJSON;
            StorageFile scheduleFile = null;
            string content = string.Empty;

            try
            {
                var task = Task.Run(async () => { scheduleFile = await Windows.Storage.ApplicationData.Current.RoamingFolder.GetFileAsync("schedule.json"); });
                task.Wait();
                task = Task.Run(async () => { content = await Windows.Storage.FileIO.ReadTextAsync(scheduleFile); });
                task.Wait();
                scheduleJSON = JObject.Parse(content);
            }
            catch (Exception ex)
            {
                return schedule;
            }
            schedule.MaxHours = (int)scheduleJSON["MaxHours"];

            for (int j = 0; j < 5; j++)
            {
                string day = string.Empty;
                switch(j)
                {
                    case 0: day = "Monday"; break;
                    case 1: day = "Tuesday"; break;
                    case 2: day = "Wednesday"; break;
                    case 3: day = "Thursday"; break;
                    case 4: day = "Friday"; break;
                }
                for (int i = 0; i < schedule.MaxHours; i++)
                {
                    schedule.Schedule[j].Add((string)(scheduleJSON[day][i]["Title"]));
                }
            }
            return schedule;
        }

        // Store provided schedule into json
        public static void StoreSchedule(Models.ScheduleModel schedule)
        {
            Windows.Storage.StorageFile scheduleFile = null;
            JObject scheduleJson = new JObject();
            scheduleJson["MaxHours"] = schedule.MaxHours;

            for (int j = 0; j < 5; j++)
            {
                JArray daySchedule = new JArray();
                string day = string.Empty;
                switch (j)
                {
                    case 0: day = "Monday"; break;
                    case 1: day = "Tuesday"; break;
                    case 2: day = "Wednesday"; break;
                    case 3: day = "Thursday"; break;
                    case 4: day = "Friday"; break;
                }
                for (int i = 0; i < schedule.MaxHours; i++)
                {
                    JObject subj = new JObject();
                    subj["Title"] = schedule.Schedule[j][i];
                    daySchedule.Add(subj);
                }
                scheduleJson[day] = daySchedule;
            }

            try
            {
                var task = Task.Run(async () => { scheduleFile = await Windows.Storage.ApplicationData.Current.RoamingFolder.CreateFileAsync("schedule.json", Windows.Storage.CreationCollisionOption.ReplaceExisting); });
                task.Wait();
                task = Task.Run(async () => { await Windows.Storage.FileIO.WriteTextAsync(scheduleFile, scheduleJson.ToString()); });
                task.Wait();
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }

    // Data access layer for tasks
    public static class TasksDAL
    {
        // Returns list of tasks loaded from json
        public static List<Models.TaskModel>GetTasks()
        {
            List<Models.TaskModel> tasks = new List<Models.TaskModel>();
            JObject tasksJSON;
            StorageFile tasksFile = null;
            string content = string.Empty;

            try
            {
                var task = Task.Run(async () => { tasksFile = await Windows.Storage.ApplicationData.Current.RoamingFolder.GetFileAsync("tasks.json"); });
                task.Wait();
                task = Task.Run(async () => { content = await Windows.Storage.FileIO.ReadTextAsync(tasksFile); });
                task.Wait();
                tasksJSON = JObject.Parse(content);
            }
            catch (Exception ex)
            {
                return tasks;
            }

            for (int i = 0; i < ((JArray)tasksJSON["Tasks"]).Count(); i++)
            {
                Models.TaskModel task = new Models.TaskModel();
                task.Id = (int)(tasksJSON["Tasks"][i]["Id"]);
                task.Description = (string)(tasksJSON["Tasks"][i]["Description"]);
                task.Subject = (string)(tasksJSON["Tasks"][i]["Subject"]);
                task.Notification = DateTime.ParseExact((string)(tasksJSON["Tasks"][i]["Notification"]), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                task.Deadline = DateTime.ParseExact((string)(tasksJSON["Tasks"][i]["Deadline"]), "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                tasks.Add(task);
            }
            return tasks;
        }

        // Store provided schedule into json
        public static void StoreTasks(List<Models.TaskModel> taskList)
        {
            Windows.Storage.StorageFile tasksFile = null;
            JObject tasksJson = new JObject();
            JArray tasks = new JArray();

            for (int i = 0; i < taskList.Count; i++)
            {
                JObject task = new JObject();
                task["Id"] = taskList[i].Id;
                task["Description"] = taskList[i].Description;
                task["Subject"] = taskList[i].Subject;
                task["Notification"] = taskList[i].Notification.ToString("yyyy-MM-dd HH:mm:ss");
                task["Deadline"] = taskList[i].Deadline.ToString("yyyy-MM-dd HH:mm:ss");
                tasks.Add(task);
            }
            tasksJson["LastChange"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            tasksJson["Tasks"] = tasks;
            try
            {
                var task = Task.Run(async () => { tasksFile = await Windows.Storage.ApplicationData.Current.RoamingFolder.CreateFileAsync("tasks.json", Windows.Storage.CreationCollisionOption.ReplaceExisting); });
                task.Wait();
                task = Task.Run(async () => { await Windows.Storage.FileIO.WriteTextAsync(tasksFile, tasksJson.ToString()); });
                task.Wait();
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public static DateTime LastChange()
        {
            JObject tasksJSON;
            StorageFile tasksFile = null;
            string content = string.Empty;

            try
            {
                var task = Task.Run(async () => { tasksFile = await ApplicationData.Current.RoamingFolder.GetFileAsync("tasks.json"); });
                task.Wait();
                task = Task.Run(async () => { content = await FileIO.ReadTextAsync(tasksFile); });
                task.Wait();
                tasksJSON = JObject.Parse(content);
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
            return DateTime.ParseExact((string)tasksJSON["LastChange"], "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
        }
    }    
}
