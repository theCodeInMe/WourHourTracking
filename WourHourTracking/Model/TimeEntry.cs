using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WourHourTracking
{
    public class TimeEntry
    {
        public int EntryID;
        public DateTime StartTime;
        public DateTime EndTime;
        public string Notes;

        public TimeEntry()
        {
        }

        public TimeEntry(string entry)
        {
            string[] data = entry.Split(',');
            EntryID = int.Parse(data[0]);
            StartTime = DateTime.Parse(data[1]);
            EndTime = DateTime.Parse(data[2]);
            Notes = data[3];
        }

        public TimeEntry(int id, DateTime start, DateTime end, string notes)
        {
            EntryID = id;
            StartTime = start;
            EndTime = end;
            Notes = notes;
        }

        public string FullEntryToString()
        {
            return $"{EntryID}, {StartTime.ToString()}, {EndTime}, {Notes}";
        }

        public static List<TimeEntry> GetTimeEntries(string sourcePath)
        {
            List<TimeEntry> result = new List<TimeEntry>();

            using (StreamReader sr = new StreamReader(sourcePath)) 
            {
               
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        TimeEntry entry = new TimeEntry(line);
                        result.Add(entry);
                    }
                
            }

            return result ;
        }
    }
}