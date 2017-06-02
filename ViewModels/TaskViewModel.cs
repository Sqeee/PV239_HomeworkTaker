using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTaker.ViewModels
{
    public class TaskViewModel : NotificationBase<Models.TaskModel>
    {
        public TaskViewModel(Models.TaskModel task = null) : base(task) { }
        public int Id
        {
            get { return This.Id; }
            set { SetProperty(This.Id, value, () => This.Id = value); }
        }
        public string Description
        {
            get { return This.Description; }
            set { SetProperty(This.Description, value, () => This.Description = value); }
        }
        public string Subject
        {
            get { return This.Subject; }
            set { SetProperty(This.Subject, value, () => This.Subject = value); }
        }
        public DateTime DeadLine
        {
            get { return This.Deadline; }
            set { SetProperty(This.Deadline, value, () => This.Deadline = value); }
        }        
    }
}
