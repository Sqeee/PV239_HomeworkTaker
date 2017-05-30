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
                var task = Task.Run(async () => { subjectsFile = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync("subjects.json"); });
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
                var task = Task.Run(async () => { subjectsFile = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync("subjects.json", Windows.Storage.CreationCollisionOption.ReplaceExisting); });
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
                var task = Task.Run(async () => { scheduleFile = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync("schedule.json"); });
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
                var task = Task.Run(async () => { scheduleFile = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync("schedule.json", Windows.Storage.CreationCollisionOption.ReplaceExisting); });
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
}
