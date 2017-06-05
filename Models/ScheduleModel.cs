using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTaker.Models
{
    public class ScheduleModel
    {
        public ScheduleModel()
        {
            Schedule = new List<string>[5] {new List<string>(), new List<string>(), new List<string>(), new List<string>(), new List<string>()};
            
        }

        public int MaxHours { get; set; }
        public List<string>[] Schedule { get; set; }
        public void DeleteSubjectFromSchedule(string subject)
        {
            for(int day=0;day<5;day++)
            {
                for(int lesson=0;lesson< MaxHours;lesson++)
                {
                    if (Schedule[day][lesson] == subject)
                    {
                        Schedule[day][lesson] = String.Empty;
                    }
                }
            }
        }  
    }
}
