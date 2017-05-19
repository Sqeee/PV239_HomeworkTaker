using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization.DateTimeFormatting;
using Windows.UI.Xaml.Controls;

namespace HomeworkTaker.Models
{
    public class Subject
    {
    
        public int SubjectID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public TimeSpan TimeStart { get; set; }

        public TimeSpan TimeEnd { get; set; }

        public int DayOfWeek { get; set; }
        
    }
}
