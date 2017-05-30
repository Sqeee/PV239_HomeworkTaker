using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HomeworkTaker.Pages.Settings
{
    // Page used to add new subject
    public sealed partial class AddSubjectPage : Page
    {
        public ViewModels.SubjectsViewModel Subjects { get; set; }
        public AddSubjectPage()
        {
            this.InitializeComponent();
            Subjects = new ViewModels.SubjectsViewModel();
        }

        private void onCancelBtnClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.Settings.SubjectsManagementPage));
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
