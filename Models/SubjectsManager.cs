using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTaker.Models
{
    class SubjectsManager : ISubjectManager
    {
        private List<Subject> subjects = new List<Subject>();

        public SubjectsManager()
        {

        }

        public List<Subject> GetSubjects()
        {
            return subjects;
        }

        public void InsertSubject(Subject subject)
        {
            subjects.Add(subject);
        }

        public void UpdateSubject(Subject subject)
        {

        }

        public void DeleteSubject(Subject subject)
        {
            subjects.Remove(subject);
        }
    }
}
