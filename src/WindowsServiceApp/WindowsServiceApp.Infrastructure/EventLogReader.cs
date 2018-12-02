using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WindowsServiceApp.Common.Models;
using WindowsServiceApp.Infrastructure.Interfaces;

namespace WindowsServiceApp.Infrastructure
{
    public class EventLogReader : IEventLogReader
    {
        private readonly string logName;

        public EventLogReader(string logName)
        {
            this.logName = logName;
        }

        public IEnumerable<EventLogRecord> GetEventLogs(DateTime startDate)
        {
            using (var eventLog = new EventLog(logName))
            {
                var entries = eventLog.Entries.Cast<EventLogEntry>()
                    .Where(l => l.TimeWritten >= startDate)
                    .Select(l => new EventLogRecord
                    {
                        User = l.UserName,
                        MachineName = l.MachineName,
                        Message = l.Message,
                        Source = l.Source,
                        TimeGenerated = l.TimeGenerated,
                        TimeWritten = l.TimeWritten
                    }).OrderByDescending(l => l.TimeGenerated).ToList();

                return entries;
            }
        }
    }
}
