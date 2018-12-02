using System;

namespace WindowsServiceApp.Common.Models
{
    public class EventLogRecord
    {
        public string Id { get; set; }
        public string MachineName { get; set; }
        public string User { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public DateTime TimeGenerated { get; set; }
        public DateTime TimeWritten { get; set; }
    }
}
