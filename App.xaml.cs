using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using HomeworkTaker.Models;
using HomeworkTaker.Notifications;
using HomeworkTaker.Pages;

namespace HomeworkTaker
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
            RegisterToastBackgroundTasks();
            RegisterTimeBackgroundTasks();
            ApplicationData.Current.DataChanged += RoamingDataChangeHandler;
            NotificationsManager.UpdateTaskNotifications();
        }

        private async void RegisterToastBackgroundTasks()
        {
            string taskName = "ToastBackgroundTask";

            // If background task is already registered, do nothing
            if (BackgroundTaskRegistration.AllTasks.Any(i => i.Value.Name.Equals(taskName)))
                return;

            // Otherwise request access
            BackgroundAccessStatus status = await BackgroundExecutionManager.RequestAccessAsync();

            // Create the background task
            BackgroundTaskBuilder builder = new BackgroundTaskBuilder()
            {
                Name = taskName,
            };

            // Assign the toast action trigger
            builder.SetTrigger(new ToastNotificationActionTrigger());

            // And register the task
            BackgroundTaskRegistration registration = builder.Register();
        }

        private async void RegisterTimeBackgroundTasks()
        {
            string taskName = "TimeBackgroundTask";

            // If background task is already registered, do nothing
            if (BackgroundTaskRegistration.AllTasks.Any(i => i.Value.Name.Equals(taskName)))
                return;

            // Otherwise request access
            BackgroundAccessStatus status = await BackgroundExecutionManager.RequestAccessAsync();

            // Create the background task
            BackgroundTaskBuilder builder = new BackgroundTaskBuilder()
            {
                Name = taskName,
            };

            // Assign the toast action trigger
            builder.SetTrigger(new TimeTrigger(15, false));

            // And register the task
            BackgroundTaskRegistration registration = builder.Register();
        }

        protected override async void OnBackgroundActivated(BackgroundActivatedEventArgs bgArgs)
        {
            var deferral = bgArgs.TaskInstance.GetDeferral();

            switch (bgArgs.TaskInstance.Task.Name)
            {
                case "ToastBackgroundTask":
                    var details = bgArgs.TaskInstance.TriggerDetails as ToastNotificationActionTriggerDetail;
                    if (details != null)
                    {
                        Dictionary<string, string> args = GetArgs(details.Argument);
                        TasksModel tasks = new TasksModel();
                        TaskModel task = tasks.TasksList.FirstOrDefault(t => t.Id.ToString() == args["task"]);
                        if (task == null)
                        {
                            deferral.Complete();
                            return;
                        }
                        switch (args["action"])
                        {
                            case "done":
                                tasks.DeleteTask(task);
                                break;
                            case "remindlater":
                                task.Notification = task.Notification.AddHours(1);
                                tasks.StoreTasks();
                                break;
                        }
                    }
                    break;
                case "TimeBackgroundTask":
                    if (NotificationsManager.LastUpdateTasksFile < Data.TasksDAL.LastChange())
                    {
                        NotificationsManager.UpdateTaskNotifications();
                    }
                    break;
            }

            deferral.Complete();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        protected override void OnActivated(IActivatedEventArgs e)
        {
            // Get the root frame
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (!(rootFrame.Content is MainPage))
            {
                rootFrame.Navigate(typeof(MainPage));
            }

            // Handle toast activation
            if (e is ToastNotificationActivatedEventArgs)
            {
                var toastActivationArgs = e as ToastNotificationActivatedEventArgs;

                // Parse the query string (using QueryString.NET)
                Dictionary<string, string> args = GetArgs(toastActivationArgs.Argument);

                // See what action is being requested 
                switch (args["action"])
                {
                    // Open the image
                    case "viewtasks":
                        // Navigate to view it
                        ((MainPage) rootFrame.Content).ContentNavigate(typeof(TasksPage));
                        break;
                }

                // If we're loading the app for the first time, place the main page on
                // the back stack so that user can go back after they've been
                // navigated to the specific page
                if (rootFrame.BackStack.Count == 0)
                    rootFrame.BackStack.Add(new PageStackEntry(typeof(MainPage), null, null));
            }

            // Ensure the current window is active
            Window.Current.Activate();
        }

        private Dictionary<string, string> GetArgs(string param)
        {
            return param.Replace("&amp;", "&").Split(new[] { "&" }, StringSplitOptions.None).ToDictionary(s => s.Split('=')[0], s => s.Split('=')[1]);
        }

        private void RoamingDataChangeHandler(Windows.Storage.ApplicationData appData, object o)
        {
            NotificationsManager.UpdateTaskNotifications();
        }
    }
}
