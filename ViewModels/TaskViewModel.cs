using HomeworkTaker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeworkTaker.ViewModels
{
    class TaskViewModel : ViewModelBase
    {
        Task task;

        public TaskViewModel(Task task)
        {
            this.task = task;
            this.PropertyChanged += TaskViewModel_PropertyChanged;
        }

        void TaskViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            IsDirty = true;
        }

        public Task Task
        {
            get { return task; }
        }

        public string Title
        {
            get
            {
                return task.Title;
            }
            set
            {
                if (task.Title != value)
                {
                    task.Title = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Description
        {
            get
            {
                return task.Description;
            }
            set
            {
                if (task.Description != value)
                {
                    task.Description = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Subject Subject
        {
            get
            {
                return task.Subject;
            }
            set
            {
                if (task.Subject != value)
                {
                    task.Subject = value;
                    NotifyPropertyChanged();
                }
            }

        }

        public DateTime NotificationTime
        {
            get
            {
                return task.NotificationTime;
            }
            set
            {
                if (task.NotificationTime != value)
                {
                    task.NotificationTime = value;
                    NotifyPropertyChanged();
                }
            }
        }


        public bool IsDirty { get; set; }

    }
}
