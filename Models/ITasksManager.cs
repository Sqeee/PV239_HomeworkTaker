using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTaker.Models
{
    public interface ITasksManager
    {

        List<Task> GetTasks();
        void InsertTask(Task task);
        void UpdateTask(Task task);
        void DeleteTask(Task task);

    }
}
