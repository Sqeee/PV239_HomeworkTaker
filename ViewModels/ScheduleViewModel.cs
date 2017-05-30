using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTaker.ViewModels
{
    public class ScheduleViewModel : ViewModels.NotificationBase
    {
        Models.ScheduleModel schedule;

        public ScheduleViewModel()
        {
            schedule = Data.ScheduleDAL.GetSchedule();            
        }

        public Models.ScheduleModel GetSchedule()
        {
            return schedule;
        }
    }   
     
}
