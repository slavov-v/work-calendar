using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkCalendar
{
    class Meeting
    {
        private Guid _id;
        private string _description;
        private DateTime _meetDate;

        public Guid Id { get { return _id; } }
        public string Description { get { return _description; } set { _description = value; } }
        public DateTime MeetDate { get { return _meetDate; } set { _meetDate = value; } }

        public Meeting(string desc, DateTime meetDate)
        {
            _id = Guid.NewGuid();
            Description = desc;
            MeetDate = meetDate;
        }

        public Meeting(Guid id, string desc, DateTime meetDate)
        {
            _id = id;
            Description = desc;
            MeetDate = meetDate;
        }

        public string serialize()
        {
            return string.Format("{0};{1};{2}\n", Id, Description, MeetDate.ToString());
        }

        public void Save()
        {
            string data = serialize();

            File.AppendAllText(EventManager.MeetingsFileName, data);
        }
    }
}
