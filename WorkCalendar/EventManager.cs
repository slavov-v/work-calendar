﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkCalendar
{
    static class EventManager
    {
        public static string TasksFileName = "tasks.txt";
        public static string MeetingsFileName = "meetings.txt";

        public static Meeting deserializeMeeting(string data)
        {
            String[] meetingData = data.Split(';');

            return new Meeting(new Guid(meetingData[0]), meetingData[1], DateTime.Parse(meetingData[2]));
        }

        public static Task deserializeTask(string data)
        {
            String[] meetingData = data.Split(';');

            return new Task(new Guid(meetingData[0]), meetingData[1], DateTime.Parse(meetingData[2]), DateTime.Parse(meetingData[3]));
        }

        public static List<Meeting> ListAllMeetings()
        {
            List<Meeting> result = new List<Meeting>();
            string[] readLines = File.ReadAllLines(TasksFileName);

            foreach(string line in readLines)
            {
                result.Add(deserializeMeeting(line));
            }

            return result;
        }

        public static List<Task> ListAllTasks()
        {
            List<Task> result = new List<Task>();
            string[] readLines = File.ReadAllLines(TasksFileName);

            foreach (string line in readLines)
            {
                result.Add(deserializeTask(line));
            }

            return result;
        }

        public static void RemoveMeeting(Guid meetingId)
        {
            List<Meeting> allMeetings = ListAllMeetings();
            int index_to_remove = allMeetings.FindIndex(item => item.Id == meetingId);
            allMeetings.RemoveAt(index_to_remove);

            StringBuilder data = new StringBuilder();

            foreach (Meeting item in allMeetings)
            {
                data.Append(item.serialize());
            }

            File.WriteAllText(MeetingsFileName, data.ToString());
        }

        public static void RemoveTask(Guid meetingId)
        {
            List<Task> allTasks = ListAllTasks();
            int index_to_remove = allTasks.FindIndex(item => item.Id == meetingId);
            allTasks.RemoveAt(index_to_remove);

            StringBuilder data = new StringBuilder();

            foreach (Task item in allTasks)
            {
                data.Append(item.serialize());
            }

            File.WriteAllText(TasksFileName, data.ToString());
        }
    }
}
