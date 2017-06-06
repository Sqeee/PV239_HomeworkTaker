using System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace HomeworkTaker.Pages.Settings
{
    // Page used to add new subject
    public sealed partial class AddSubjectPage : Page
    {
        public ViewModels.SubjectsViewModel Subjects { get; set; }
        public AddSubjectPage()
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

        private void onCancelBtnClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.Settings.SubjectsManagementPage));
        }

        private void onBackBtnClick(object sender, BackRequestedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
            else
            {
                this.Frame.Navigate(typeof(Pages.Settings.SubjectsManagementPage));
            }
            e.Handled = true;
        }

        private void onAcceptBtnClick(object sender, RoutedEventArgs e)
        {
            ViewModels.SubjectViewModel subj = new ViewModels.SubjectViewModel();
            subj.Title = titleTextBox.Text;
            subj.TitleShort = titleShortTextBox.Text;
            Subjects.Add(subj);
            this.Frame.Navigate(typeof(Pages.Settings.SubjectsManagementPage));
        }
    }
}
