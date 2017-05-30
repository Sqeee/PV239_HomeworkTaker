using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTaker.ViewModels
{
    public class SubjectViewModel : NotificationBase<Models.SubjectModel>
    {
        public SubjectViewModel(Models.SubjectModel subject = null) : base(subject) { }
        public string Title
        {
            get { return This.Title; }
            set { SetProperty(This.Title, value, () => This.Title = value); }
        }
        public string TitleShort
        {
            get { return This.TitleShort; }
            set { SetProperty(This.TitleShort, value, () => This.TitleShort = value); }
        }
    }
}