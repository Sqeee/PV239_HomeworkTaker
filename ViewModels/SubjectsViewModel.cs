using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTaker.ViewModels
{
    public class SubjectsViewModel : ViewModels.NotificationBase
    {
        Models.SubjectsModel subjects;

        public SubjectsViewModel()
        {
            subjects = new Models.SubjectsModel();
            _SelectedIndex = -1;
            _Subjects = new ObservableCollection<SubjectViewModel>();
            foreach (var subject in subjects.SubjectsList)
            {
                var newSubject = new SubjectViewModel(subject);
                _Subjects.Add(newSubject);
            }
        }

        ObservableCollection<SubjectViewModel> _Subjects;
        public ObservableCollection<SubjectViewModel> Subjects
        {
            get { return _Subjects; }
            set { SetProperty(ref _Subjects, value); }
        }        

        int _SelectedIndex;
        public int SelectedIndex
        {
            get { return _SelectedIndex; }
            set
            {
                if (SetProperty(ref _SelectedIndex, value))
                { RaisePropertyChanged(nameof(SelectedSubject)); }
            }
        }

        public SubjectViewModel SelectedSubject
        {
            get { return (_SelectedIndex >= 0) ? _Subjects[_SelectedIndex] : null; }
        }

        public void Add(ViewModels.SubjectViewModel subj)
        {
            subjects.AddSubject(subj);
            SelectedIndex = Subjects.IndexOf(subj);
        }

        public void Delete()
        {
            if (SelectedIndex != -1)
            {
                var subject = Subjects[SelectedIndex];
                Subjects.RemoveAt(SelectedIndex);
                subjects.DeleteSubject(subject);
            }
        }
        public List<string> GetSubjectsTitleList()
        {
            List<string> result = new List<String>();
            for(int i=0; i<Subjects.Count;i++)
            {
                result.Add(Subjects[i].Title);
            }
            return result;
        }
    }
}
