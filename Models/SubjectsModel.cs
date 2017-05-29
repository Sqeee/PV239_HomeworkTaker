using System.Collections.Generic;

namespace HomeworkTaker.Models
{
    // Class holding list of all defined subjects
    class SubjectsModel
    {

        public List<Models.SubjectModel> SubjectsList { get; set; }
        
        public SubjectsModel()
        {
            SubjectsList = Data.SubjectsDAL.GetSubjects();            
        }

        public void AddSubject(Models.SubjectModel  newSubject)
        {
            SubjectsList.Add(newSubject);
            Data.SubjectsDAL.StoreSubjects(SubjectsList);            
        }

        public void DeleteSubject(Models.SubjectModel subject)
        {
            for (int i = 0; i < SubjectsList.Count; i++)
            {
                if (SubjectsList[i].SubjectID == subject.SubjectID)
                {
                    SubjectsList.RemoveAt(i);
                    break;
                }
            }
            Data.SubjectsDAL.StoreSubjects(SubjectsList);            
        }                      
    }
}
