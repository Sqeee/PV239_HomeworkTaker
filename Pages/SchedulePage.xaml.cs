using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace HomeworkTaker.Pages
{
   // Page used to display schedule
    public sealed partial class SchedulePage : Page
    {
        // TODO 
        // Implement loading schedule from json.
        // Implement editing schedule in settings.
        // Implement adding of tasks when subject is clicked
        private const int MAX_HOURS = 8;

        public SchedulePage()
        {
            this.InitializeComponent();
            createSchedule();
        }

        private void createSchedule()
        {
            // create header
            for (int i=1; i<=MAX_HOURS; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                scheduleGrid.ColumnDefinitions.Add(col);
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
            for(int i=1; i<6; i++)
            {
                for(int j=1; j<=MAX_HOURS;j++)
                {
                    Button btn = new Button();
                    btn.Content = "Subject";
                    btn.Name = i.ToString() + "." + j.ToString();
                    btn.VerticalAlignment = VerticalAlignment.Stretch;
                    btn.HorizontalAlignment = HorizontalAlignment.Stretch;
                    Border b = new Border();
                    b.Child = btn;
                    b.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Black);
                    b.BorderThickness = new Thickness(1);
                    scheduleGrid.Children.Add(b);
                    Grid.SetRow(b, i);
                    Grid.SetColumn(b, j);
                }
            }          
        }
    }
}
