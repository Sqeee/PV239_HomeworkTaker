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

namespace HomeworkTaker.Pages.Settings
{
    // Page used to set up schedule
    public sealed partial class ScheduleManagementPage : Page
    {
        private ViewModels.SubjectsViewModel subjects { get; set; }
        private ViewModels.ScheduleViewModel schedule { get; set; }
        public ScheduleManagementPage()
        {
            this.InitializeComponent();
            subjects = new ViewModels.SubjectsViewModel();
            schedule = new ViewModels.ScheduleViewModel();
            resetSchedule();
        }

        private void deleteSchedule()
        {
            scheduleGrid.Children.Clear();
            scheduleGrid.ColumnDefinitions.Clear();
            scheduleGrid.RowDefinitions.Clear();
        }

        private void createScheduleSkeleton()
        {
            for (int rows=0; rows<8; rows++)
            {
                RowDefinition rd = new RowDefinition();
                if(rows==7)
                {
                    rd.Height = Windows.UI.Xaml.GridLength.Auto;
                }
                scheduleGrid.RowDefinitions.Add(rd);
            }
            for (int cols = 0; cols < 2; cols++)
            {
                scheduleGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            TextBlock txt = new TextBlock();
            txt.Text = "Max hours: ";
            txt.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            txt.SetValue(Grid.ColumnProperty, 0);
            txt.SetValue(Grid.RowProperty, 0);
            scheduleGrid.Children.Add(txt);
            TextBox txtBox = new TextBox();
            txtBox.Name = "MaxHoursTextBox";
            txtBox.LostFocus += onMaxHoursChanged;
            txtBox.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            txtBox.SetValue(Grid.ColumnProperty, 1);
            txtBox.SetValue(Grid.RowProperty, 0);
            scheduleGrid.Children.Add(txtBox);
            for(int i=1;i<7;i++)
            {
                string text = string.Empty;
                switch(i)
                {
                    case 1: text = "Schedule"; break;
                    case 2: text = "Monday"; break;
                    case 3: text = "Tuesday"; break;
                    case 4: text = "Wednesday"; break;
                    case 5: text = "Thursday"; break;
                    case 6: text = "Friday"; break;
                }
                TextBlock tb = new TextBlock();
                tb.Text = text;
                tb.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
                tb.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
                tb.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                Border b = new Border();
                b.Child = tb;
                b.Background = new SolidColorBrush(Windows.UI.Colors.Gray);
                b.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Black);
                b.BorderThickness = new Thickness(1);
                b.SetValue(Grid.ColumnProperty, 0);
                b.SetValue(Grid.RowProperty, i);
                scheduleGrid.Children.Add(b);                
                AppBarButton acceptBtn = new AppBarButton();
                acceptBtn.Icon = new SymbolIcon(Symbol.Accept);
                acceptBtn.Label = "Accept";
                acceptBtn.Click += onAcceptBtnClick;
                AppBarButton backBtn = new AppBarButton();
                backBtn.Icon = new SymbolIcon(Symbol.Back);
                backBtn.Label = "Back";
                backBtn.Click += onBackBtnClick;
                CommandBar cb = new CommandBar();
                cb.Name = "commandBar";
                cb.VerticalAlignment = VerticalAlignment.Bottom;
                cb.PrimaryCommands.Add(backBtn);
                cb.PrimaryCommands.Add(acceptBtn);                
                cb.SetValue(Grid.RowProperty, 7);
                cb.SetValue(Grid.ColumnProperty, 0);
                cb.SetValue(Grid.ColumnSpanProperty, scheduleGrid.ColumnDefinitions.Count);
                
                scheduleGrid.Children.Add(cb);          
            }                                          
        }

        private void createSchedule()
        {                        
            //create columns
            for (int c = 2; c <= schedule.GetSchedule().MaxHours; c++)
            {
                scheduleGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            // create header
            for (int i = 1; i <= schedule.GetSchedule().MaxHours; i++)
            {                
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
                Grid.SetRow(b, 1);
                Grid.SetColumn(b, i);
            }
            // create rest of schedule
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < schedule.GetSchedule().MaxHours; j++)
                {
                    ComboBox cb = new ComboBox();
                    cb.Name = i.ToString() + "," + j.ToString();
                    cb.Items.Add("");
                    for (int s=0;s<subjects.Subjects.Count;s++)
                    {
                        cb.Items.Add(subjects.Subjects[s].Title);
                    }                                      
                    cb.VerticalAlignment = VerticalAlignment.Stretch;
                    cb.HorizontalAlignment = HorizontalAlignment.Stretch;
                    cb.SelectedItem = schedule.GetSchedule().Schedule[i][j];
                    cb.SelectionChanged += onSubjectChanged;
                    Border b = new Border();
                    b.Child = cb;
                    b.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Black);
                    b.BorderThickness = new Thickness(1);
                    scheduleGrid.Children.Add(b);
                    Grid.SetRow(b, i + 2);
                    Grid.SetColumn(b, j + 1);
                }
            }
            //adjust command bar
            for(int i=0;i<scheduleGrid.Children.Count;i++)
            {
                if(scheduleGrid.Children[i].GetType() == typeof(CommandBar))
                {
                    ((CommandBar)scheduleGrid.Children[i]).SetValue(Grid.ColumnSpanProperty, scheduleGrid.ColumnDefinitions.Count);
                }
            }
            
        }

        private void resetSchedule()
        {
            deleteSchedule();
            createScheduleSkeleton();
            createSchedule();
        }

        private void onMaxHoursChanged(object sender, RoutedEventArgs e)
        {
            int maxHours = 0;
            try
            {
                maxHours = Convert.ToInt32(((TextBox)sender).Text);
            }
            catch (Exception ex)
            {
                maxHours = 0;
            }
            schedule.GetSchedule().MaxHours = maxHours;
            for (int i = 0; i < 5; i++)
            {
                schedule.GetSchedule().Schedule[i].Clear();
                for (int j = 0; j < schedule.GetSchedule().MaxHours; j++)
                {
                    schedule.GetSchedule().Schedule[i].Add(string.Empty);
                }
            }
            resetSchedule();
        }

        private void onSubjectChanged(object sender, RoutedEventArgs e)
        {
            string name = ((ComboBox)sender).Name;
            int day = Convert.ToInt32(name.Substring(0,name.IndexOf(',')));
            int hour = Convert.ToInt32(name.Substring(name.IndexOf(',')+1));

            schedule.GetSchedule().Schedule[day][hour] = ((ComboBox)sender).SelectedItem.ToString();
        }

        private void onAcceptBtnClick(object sender, RoutedEventArgs e)
        {
            Data.ScheduleDAL.StoreSchedule(schedule.GetSchedule());
            this.Frame.Navigate(typeof(Pages.Settings.MainSettingsPage));
        }

        private void onBackBtnClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.Settings.MainSettingsPage));
        }


    }
}
