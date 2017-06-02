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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HomeworkTaker.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateTaskPage : Page
    {
        private string subject;
        private List<Models.TaskModel> tasks;
        public CreateTaskPage()
        {
            tasks = Data.TasksDAL.GetTasks();
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            subject= e.Parameter as string;
        }

        private void onBackBtnClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.SchedulePage));
        }

        private void onAcceptBtnClick(object sender, RoutedEventArgs e)
        {
            Models.TaskModel task = new Models.TaskModel();
            task.Description = descritpionTextBox.Text;
            task.Subject = subject;
            tasks.Add(task);
            Data.TasksDAL.StoreTasks(tasks);
            this.Frame.Navigate(typeof(Pages.SchedulePage));
        }
    }
}
