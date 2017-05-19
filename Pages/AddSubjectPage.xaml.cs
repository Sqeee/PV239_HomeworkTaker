using HomeworkTaker.Converters;
using HomeworkTaker.Models;
using HomeworkTaker.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace HomeworkTaker
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddSubjectPage : Page
    {

        SubjectViewModel ViewModel { get; set; }

        public AddSubjectPage()
        {
            this.InitializeComponent();
            this.ViewModel = new SubjectViewModel(new Subject());
            this.DataContext = ViewModel;
        }

        private void ComboBox_Day_Selected(object sender, SelectionChangedEventArgs e)
        {
            string day = ((ComboBoxItem) e.AddedItems[0]).Content as string;
            int d = 0;
            switch (day)
            {
                case "Monday":
                    d = 0;
                    break;
                case "Tuesday":
                    d = 1;
                    break;
                case "Wednesday":
                    d = 2;
                    break;
                case "Thursday":
                    d = 3;
                    break;
                case "Friday":
                    d = 4;
                    break;
                default:
                    d = 0;
                    break;
            }
            ViewModel.DayOfWeek = d;

        }

        private void Start_Time_TimeChanged(object sender, TimePickerValueChangedEventArgs e)
        {
            ViewModel.TimeStart = e.NewTime;
        }

        private void End_Time_TimeChanged(object sender, TimePickerValueChangedEventArgs e)
        {
            ViewModel.TimeEnd = e.NewTime;
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Subject :" + ViewModel.DayOfWeek + " " + ViewModel.Title);
        }
    }
}
