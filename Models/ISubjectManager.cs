using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTaker.Models
{
    public interface ISubjectManager
    {
        List<Subject> GetSubjects();
        void InsertSubject(Subject subject);
        void UpdateSubject(Subject subject);
        void DeleteSubject(Subject subject);

    }
}
