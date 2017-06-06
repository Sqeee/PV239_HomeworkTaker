using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
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
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += onBackBtnClick;
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //get day and lesson
            string param = e.Parameter as string;
            day = Convert.ToInt32(param.Substring(0, param.IndexOf(',')));
            lesson = Convert.ToInt32(param.Substring(param.IndexOf(',') + 1));
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            SystemNavigationManager.GetForCurrentView().BackRequested -= onBackBtnClick;
        }

        private void onBackBtnClick(object sender, BackRequestedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
            else
            {
                this.Frame.Navigate(typeof(Pages.SchedulePage));
            }
            e.Handled = true;
        }

        private void onIncreaseClick(object sender, RoutedEventArgs e)
        {
            int lessons = Convert.ToInt32(numOfLessonsTextBlock.Text);
            lessons++;
            numOfLessonsTextBlock.Text = lessons.ToString();
        }

        private void onDecreaseClick(object sender, RoutedEventArgs e)
        {
            int lessons = Convert.ToInt32(numOfLessonsTextBlock.Text);
            if (lessons > 1)
            {
                lessons--;
            }
            numOfLessonsTextBlock.Text = lessons.ToString();
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
            int weekDay = day;
            int lessonOffset = 0;

            int deadlineAtLesson = Convert.ToInt32(numOfLessonsTextBlock.Text);
            DateTime deadline = assigned;
            while (deadlineAtLesson > 0)
            {
                int dayCount = 1;
                weekDay++;
                if (weekDay == 5)
                {
                    weekDay = 0;
                    dayCount += 2;
                }                
                while (schedule.Schedule[weekDay][lessonOffset] != subject)
                {
                    lessonOffset++;
                    if (lessonOffset == schedule.MaxHours)
                    {
                        lessonOffset = 0;
                        weekDay++;
                        dayCount++;
                        if(weekDay==5)
                        {
                            weekDay = 0;
                            dayCount += 2;
                        }
                    }
                }                
                deadlineAtLesson--;
                deadline = deadline.AddDays(dayCount);
            }

            
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
