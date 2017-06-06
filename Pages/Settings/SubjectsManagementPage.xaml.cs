using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace HomeworkTaker.Pages.Settings
{
    // Page used to manage and add subjects
    public sealed partial class SubjectsManagementPage : Page
    {     
       public ViewModels.SubjectsViewModel Subjects { get; set; }

        public SubjectsManagementPage()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += onBackBtnClick;
            Subjects = new ViewModels.SubjectsViewModel();                                           
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
                this.Frame.Navigate(typeof(Pages.Settings.MainSettingsPage));
            }
            e.Handled = true;
        }

        private void onAddBtnClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.Settings.AddSubjectPage));
        }

        private void onDeleteBtnClick(object sender, RoutedEventArgs e)
        {
            Subjects.Delete();            
        }
    }
}
