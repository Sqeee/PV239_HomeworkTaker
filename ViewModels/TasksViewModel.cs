using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTaker.ViewModels
{
    public class TasksViewModel : ViewModels.NotificationBase
    {
        Models.TasksModel tasks;

        public TasksViewModel()
        {
            tasks = new Models.TasksModel();
            _SelectedIndex = -1;
            _Tasks = new ObservableCollection<TaskViewModel>();
            foreach (var task in tasks.TasksList)
            {
                var newTask = new TaskViewModel(task);
                _Tasks.Add(newTask);
            }
        }

        ObservableCollection<TaskViewModel> _Tasks;
        public ObservableCollection<TaskViewModel> Tasks
        {
            get { return _Tasks; }
            set { SetProperty(ref _Tasks, value); }
        }

        int _SelectedIndex;
        public int SelectedIndex
        {
            get { return _SelectedIndex; }
            set
            {
                if (SetProperty(ref _SelectedIndex, value))
                { RaisePropertyChanged(nameof(SelectedTask)); }
            }
        }

        public TaskViewModel SelectedTask
        {
            get { return (_SelectedIndex >= 0) ? _Tasks[_SelectedIndex] : null; }
        }

        public void Add(ViewModels.TaskViewModel task)
        {
            tasks.AddTask(task);
            SelectedIndex = Tasks.IndexOf(task);
        }

        public void Delete()
        {
            if (SelectedIndex != -1)
            {
                var subject = Tasks[SelectedIndex];
                Tasks.RemoveAt(SelectedIndex);
                tasks.DeleteTask(subject);
            }
        }        
    }
}
