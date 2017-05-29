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
                subj.SubjectID = (int)(subjectsJSON["Subjects"][i]["ID"]);
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
                subj["ID"] = subjectsList[i].SubjectID;
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
}
