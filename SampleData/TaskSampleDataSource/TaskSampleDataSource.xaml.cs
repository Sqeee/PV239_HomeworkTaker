﻿//      *********    DO NOT MODIFY THIS FILE     *********
//      This file is regenerated by a design tool. Making
//      changes to this file can cause errors.
namespace Blend.SampleData.TaskSampleDataSource
{
    using System; 
    using System.ComponentModel;

// To significantly reduce the sample data footprint in your production application, you can set
// the DISABLE_SAMPLE_DATA conditional compilation constant and disable sample data at runtime.
#if DISABLE_SAMPLE_DATA
    internal class TaskSampleDataSource { }
#else

    public class TaskSampleDataSource : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public TaskSampleDataSource()
        {
            try
            {
                Uri resourceUri = new Uri("ms-appx:/SampleData/TaskSampleDataSource/TaskSampleDataSource.xaml", UriKind.RelativeOrAbsolute);
                Windows.UI.Xaml.Application.LoadComponent(this, resourceUri);
            }
            catch
            {
            }
        }

        private Task _Task = new Task();

        public Task Task
        {
            get
            {
                return this._Task;
            }
        }
    }

    public class TaskItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string _Title = string.Empty;

        public string Title
        {
            get
            {
                return this._Title;
            }

            set
            {
                if (this._Title != value)
                {
                    this._Title = value;
                    this.OnPropertyChanged("Title");
                }
            }
        }

        private string _Description = string.Empty;

        public string Description
        {
            get
            {
                return this._Description;
            }

            set
            {
                if (this._Description != value)
                {
                    this._Description = value;
                    this.OnPropertyChanged("Description");
                }
            }
        }

        private string _NotificationTime = string.Empty;

        public string NotificationTime
        {
            get
            {
                return this._NotificationTime;
            }

            set
            {
                if (this._NotificationTime != value)
                {
                    this._NotificationTime = value;
                    this.OnPropertyChanged("NotificationTime");
                }
            }
        }

        private double _SubjectId = 0;

        public double SubjectId
        {
            get
            {
                return this._SubjectId;
            }

            set
            {
                if (this._SubjectId != value)
                {
                    this._SubjectId = value;
                    this.OnPropertyChanged("SubjectId");
                }
            }
        }
    }

    public class Task : System.Collections.ObjectModel.ObservableCollection<TaskItem>
    { 
    }
#endif
}
