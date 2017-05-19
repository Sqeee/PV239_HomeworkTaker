using HomeworkTaker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTaker.ViewModels
{
    public class SubjectViewModel : ViewModelBase
    {
        Subject subject;
        public bool IsDirty { get; set; }

        public SubjectViewModel(Subject subject)
        {
            this.subject = subject;
            this.PropertyChanged += SubjectViewModel_PropertyChanged;

        }

        void SubjectViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            IsDirty = true;
        }

        public Subject Subject
        {
            get { return subject; }
        }

        public string Title
        {
            get
            {
                return subject.Title;
            }
            set
            {
                if (subject.Title !=  value)
                {
                    subject.Title = value;
                    NotifyPropertyChanged();
                }
            }

        }

        public string Description
        {
            get
            {
                return subject.Description;
            }
            set
            {
                if (subject.Description != value)
                {
                    subject.Description = value;
                    NotifyPropertyChanged();
                }
            }

        }

        public TimeSpan TimeStart
        {
            get
            {
                return subject.TimeStart;
            }
            set
            {
                if (subject.TimeStart != value)
                {
                    subject.TimeStart = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public TimeSpan TimeEnd
        {
            get
            {
                return subject.TimeEnd;
            }
            set
            {
                if (subject.TimeEnd != value)
                {
                    subject.TimeEnd = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int DayOfWeek
        {
            get
            {
                return subject.DayOfWeek;
            }
            set
            {
                if (subject.DayOfWeek !=  value)
                {
                    subject.DayOfWeek = value;
                    NotifyPropertyChanged();
                }
            }
        }

    }
}
