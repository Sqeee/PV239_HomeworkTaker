using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HomeworkTaker.Pages.Settings
{
    public sealed partial class MainSettingsPage : Page
    {
        public MainSettingsPage()
        {
            this.InitializeComponent();
        }

        private void onSubjectsManagementBtnClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.Settings.SubjectsManagementPage));
        }

        private void onScheduleManagementBtnClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.Settings.ScheduleManagementPage));
        }
    }
}
