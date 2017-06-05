using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using HomeworkTaker.Models;
using HomeworkTaker.Notifications;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HomeworkTaker.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateTaskPage : Page
    {        
        private int day;
        private int lesson;
        private TasksModel tasks;

        public CreateTaskPage()
        {
            tasks = new TasksModel();
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //get day and lesson
            string param = e.Parameter as string;
            day = Convert.ToInt32(param.Substring(0, param.IndexOf(',')));
            lesson = Convert.ToInt32(param.Substring(param.IndexOf(',') + 1));
        }

        private void onBackBtnClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.SchedulePage));
        }

        private void onAcceptBtnClick(object sender, RoutedEventArgs e)
        {
            // get DateTime for begining of week
            DateTime mondayDateTime = DateTime.Now;
            while (mondayDateTime.DayOfWeek != DayOfWeek.Monday)
            {
                mondayDateTime = mondayDateTime.AddDays(-1);
            }

            ScheduleModel schedule = Data.ScheduleDAL.GetSchedule();
            string subject = schedule.Schedule[day][lesson];

            // get date when task was assigned;
            DateTime assigned = mondayDateTime.AddDays(day);
            
            // get date of next lesson
            int dayOffset = day + 1;
            if (dayOffset == 5)
            {
                dayOffset = 0;
            }
            int lessonOffset = 0;
            int dayCount = 1;

            while(schedule.Schedule[dayOffset][lessonOffset]!=subject)
            {
                lessonOffset++;
                if(lessonOffset == schedule.MaxHours)
                {
                    dayOffset++;
                    lessonOffset = 0;
                    if (dayOffset == 5)
                    {
                        dayOffset = 0;
                    }
                    dayCount++;                        
                }
            }
            if(day+dayCount>=5)
            {
                dayCount += 2;
            }

            DateTime deadline = assigned.AddDays(dayCount);
            DateTime notification = deadline.Date.AddDays(-1).AddHours(17);            

            Models.TaskModel task = new Models.TaskModel();

            task.Description = descritpionTextBox.Text;
            task.Subject = subject;
            task.Assigned = assigned.Date;
            task.Notification = notification;
            task.Deadline = deadline;
            tasks.AddTask(task);
            this.Frame.Navigate(typeof(Pages.SchedulePage));
        }
    }
}
