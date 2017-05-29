using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HomeworkTaker.Pages.Settings
{
    // Page used to manage and add subjects
    public sealed partial class SubjectsManagementPage : Page
    {     
       public ViewModels.SubjectsViewModel Subjects { get; set; }

        public SubjectsManagementPage()
        {
            this.InitializeComponent();            
            Subjects = new ViewModels.SubjectsViewModel();                                           
        }

        private void onBackBtnClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.Settings.MainSettingsPage));
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
