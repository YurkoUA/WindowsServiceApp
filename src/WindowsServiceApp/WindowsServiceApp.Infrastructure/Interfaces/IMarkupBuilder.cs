using System.Collections.Generic;
using WindowsServiceApp.Common.Models;

namespace WindowsServiceApp.Infrastructure.Interfaces
{
    public interface IMarkupBuilder
    {
        string Build(IEnumerable<EventLogRecord> logs);
    }
}