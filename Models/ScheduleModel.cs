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
    }
}
