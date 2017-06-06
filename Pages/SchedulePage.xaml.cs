using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace HomeworkTaker.Pages
{
   // Page used to display schedule
    public sealed partial class SchedulePage : Page
    {   
        private ViewModels.ScheduleViewModel schedule { get; set; }
        private ViewModels.SubjectsViewModel subjects { get; }

        private bool subjectExists = false;

        public SchedulePage()
        {
            this.InitializeComponent();
            schedule = new ViewModels.ScheduleViewModel();
            subjects = new ViewModels.SubjectsViewModel();
            createSchedule();
            if (!subjectExists)
            {
                DisplayMissingSettings();
            }
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
                tb.FontWeight = Windows.UI.Text.FontWeights.SemiBold;
                Border b = new Border();
                b.Child = tb;
                b.Background = new SolidColorBrush(Windows.UI.Colors.LightGray);
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
                        btn.Name = i.ToString() + "," + j.ToString();
                        btn.VerticalAlignment = VerticalAlignment.Stretch;
                        btn.HorizontalAlignment = HorizontalAlignment.Stretch;
                        btn.Background = new SolidColorBrush(Windows.UI.Colors.White);
                        btn.Click += new RoutedEventHandler(onSubjectBtnClick);
                        b.Child = btn;
                        subjectExists = true;
                    }
                }
            }
         }

        private void onSubjectBtnClick(object sender, RoutedEventArgs e)
        {
            string name = ((Button)sender).Name as string;
            this.Frame.Navigate(typeof(Pages.CreateTaskPage),name);
        }

        private async void DisplayMissingSettings()
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "Missing subjects and schedule",
                Content = "You have not provided your subjects or prepared your schedule. Please go to settings.",
                PrimaryButtonText = "OK"
            };

            await dialog.ShowAsync();
        }
    }
}
