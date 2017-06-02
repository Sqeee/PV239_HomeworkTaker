using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace HomeworkTaker.Pages
{
   // Page used to display schedule
    public sealed partial class SchedulePage : Page
    {
        // TODO 
        // Implement adding of tasks when subject is clicked
        private ViewModels.ScheduleViewModel schedule { get; set; }
        private ViewModels.SubjectsViewModel subjects { get; }

        public SchedulePage()
        {
            this.InitializeComponent();
            schedule = new ViewModels.ScheduleViewModel();
            subjects = new ViewModels.SubjectsViewModel();
            createSchedule();
        }

        private void createSchedule()
        {
            // create header
            for (int i = 1; i<=schedule.GetSchedule().MaxHours; i++)
            {
                scheduleGrid.ColumnDefinitions.Add(new ColumnDefinition());
                TextBlock tb = new TextBlock();
                tb.Text = i.ToString();
                tb.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
                tb.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
                tb.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                Border b = new Border();
                b.Child = tb;
                b.Background = new SolidColorBrush(Windows.UI.Colors.Gray);
                b.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Black);
                b.BorderThickness = new Thickness(1);
                scheduleGrid.Children.Add(b);
                Grid.SetRow(b, 0);
                Grid.SetColumn(b, i);
            }
            // create rest of schedule
            for(int i = 0; i<5; i++)
            {
                for(int j = 0; j< schedule.GetSchedule().MaxHours; j++)
                {
                    Border b = new Border();
                    b.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Black);
                    b.BorderThickness = new Thickness(1);
                    scheduleGrid.Children.Add(b);
                    Grid.SetRow(b, i + 1);
                    Grid.SetColumn(b, j + 1);
                    string subject = schedule.GetSchedule().Schedule[i][j];
                    if(!subjects.GetSubjectsTitleList().Contains(subject))
                    {
                        subject = string.Empty;
                        schedule.GetSchedule().Schedule[i][j] = string.Empty;
                    }

                    if (subject != string.Empty)
                    {
                        Button btn = new Button();
                        btn.Content = subject;
                        btn.VerticalAlignment = VerticalAlignment.Stretch;
                        btn.HorizontalAlignment = HorizontalAlignment.Stretch;
                        btn.Click += new RoutedEventHandler(onSubjectBtnClick);
                        b.Child = btn;
                    }
                }
            }
         }

        private void onSubjectBtnClick(object sender, RoutedEventArgs e)
        {
            string subject = ((Button)sender).Content as string;
            this.Frame.Navigate(typeof(Pages.CreateTaskPage),subject);
        }
    }
}
