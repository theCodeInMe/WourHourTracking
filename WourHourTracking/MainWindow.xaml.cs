using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace WourHourTracking
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DateTime CurrentTime;
        public const string FilePath = @"\TimeTable\TimeTable.csv";
        public List<TimeEntry> TimeEntries;
        public TimeEntry CurrentEntry;
        public string absolutePath;

        public MainWindow()
        {
            absolutePath = Directory.GetCurrentDirectory() + FilePath;
            CurrentEntry = new TimeEntry();
            CurrentTime = DateTime.Now;
            CheckFileExists(absolutePath);
            TimeEntries = new List<TimeEntry>(TimeEntry.GetTimeEntries(absolutePath));
            InitializeComponent();
            header.Content = $"Hallo, Heute ist {CurrentTime.DayOfWeek} der: " +
                $"{CurrentTime.Date.ToShortDateString()}. Es ist {CurrentTime.ToShortTimeString()}";
        }

        private void CheckFileExists(string absolutePath)
        {
            string directoryPath = Path.GetDirectoryName(absolutePath);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            if (!File.Exists(absolutePath))
            {
                MessageBox.Show("File nicht vorhanden");
                File.CreateText(absolutePath);
            }
        }

        private void CreateNewTimeEntry(object sender, RoutedEventArgs e)
        {
            if (CurrentEntry.StartTime != TimeEntries[TimeEntries.Count - 1].StartTime)
            {
                if (CurrentEntry.EntryID == 0)
                {
                    CurrentEntry.EntryID = TimeEntries.Count + 1;
                    CurrentEntry.StartTime = CurrentTime;
                    createEntryButton.Content = "tracking läuft...";
                }
                else
                {
                    CurrentEntry.EndTime = DateTime.Now;
                    CurrentEntry.Notes = dayNotes.Text;
                    createEntryButton.Content = "Eintrag fertig";
                    WriteEntry(absolutePath);
                    TimeEntries.Add(CurrentEntry);
                }
            }
            else
            {
                MessageBox.Show("Eintrag wurde bereits erstellt");
            }
        }

        private void AlterEntry(object sender, RoutedEventArgs e)
        {
        }

        public void WriteEntry(string path)
        {
            using (StreamWriter WriteFile = new StreamWriter(path, true))
            {
                try
                {
                    if (CurrentEntry != null)
                    {
                        WriteFile.WriteLine(CurrentEntry.FullEntryToString());
                    }
                    else { throw new Exception(); }
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Etwas ist schief gelaufen. Versuchs nochmal");
                }
            }
        }
    }
}