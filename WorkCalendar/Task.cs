using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkCalendar
{
    class Task
    {
        private Guid _id;
        private string _description;
        private DateTime _startDate;
        private DateTime _dueDate;

        public Guid Id { get { return _id; } }
        public string Description { get { return _description; } set { _description = value; } }
        public DateTime StartDate { get { return _startDate; } set { _startDate = value;  } }
        public DateTime DueDate { get { return _dueDate; } set { _dueDate = value; } }

        public Task(string desc, DateTime startDate, DateTime dueDate)
        {
            _id = Guid.NewGuid();
            Description = desc;
            StartDate = startDate;
            DueDate = dueDate;
        }

        public Task(Guid id, string desc, DateTime startDate, DateTime dueDate)
        {
            _id = id;
            Description = desc;
            StartDate = startDate;
            DueDate = dueDate;
        }

        public string serialize()
        {
            return string.Format("{0};{1};{2};{3}\n", Id, Description, StartDate.ToString(), DueDate.ToString());
        }

        public void Save()
        {
            string data = serialize();

            File.AppendAllText(EventManager.TasksFileName, data);
        }
    }
}
