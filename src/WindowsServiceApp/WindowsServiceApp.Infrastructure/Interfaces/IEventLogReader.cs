using System;
using System.Collections.Generic;
using WindowsServiceApp.Common.Models;

namespace WindowsServiceApp.Infrastructure.Interfaces
{
    public interface IEventLogReader
    {
        IEnumerable<EventLogRecord> GetEventLogs(DateTime startDate);
    }
}