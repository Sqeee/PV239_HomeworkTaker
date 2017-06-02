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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HomeworkTaker
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            hamburgerMenuControl.ItemsSource = MenuItem.GetItems();
            hamburgerMenuControl.SelectedIndex = 0;
            contentFrame.Navigate(typeof(Pages.SchedulePage));
        }

        private void OnMenuItemClick(object sender, ItemClickEventArgs e)
        {
            var menuItem = e.ClickedItem as MenuItem;
            contentFrame.Navigate(menuItem.PageType);
        }

        public class MenuItem
        {
            public Symbol Icon { get; set; }
            public string Name { get; set; }
            public Type PageType { get; set; }

            public static List<MenuItem> GetItems()
            {
                var items = new List<MenuItem>();
                items.Add(new MenuItem()
                {
                    Icon = Symbol.Calendar,
                    Name = "Schedule",
                    PageType = typeof(Pages.SchedulePage)
                });
                items.Add(new MenuItem()
                {
                    Icon = Symbol.OutlineStar,
                    Name = "Tasks",
                    PageType = typeof(Pages.TasksPage)
                });
                items.Add(new MenuItem()
                {
                    Icon = Symbol.Setting,
                    Name = "Settings",
                    PageType = typeof(Pages.Settings.MainSettingsPage)
                });
                return items;
            }
        }
    }
}
