using System.Collections.Generic;
using System.Text;
using WindowsServiceApp.Common.Models;
using WindowsServiceApp.Infrastructure.Interfaces;

namespace WindowsServiceApp.Infrastructure
{
    public class MarkupBuilder : IMarkupBuilder
    {
        public string Build(IEnumerable<EventLogRecord> logs)
        {
            var sBuilder = new StringBuilder();
            sBuilder.Append(@"<table>
                                <thead>
                                    <th>Machine</th>
                                    <th>User</th>
                                    <th>Source</th>
                                    <th>Message</th>
                                    <th>Time Generated</th>
                                    <th>Time Written</th>
                                </thead>
                                <tbody>");

            foreach (var item in logs)
            {
                sBuilder.Append($@"<tr>
                                    <td>{item.MachineName}</td>
                                    <td>{item.User}</td>
                                    <td>{item.Source}</td>
                                    <td>{item.Message}</td>
                                    <td>{item.TimeGenerated.ToString("G")}</td>
                                    <td>{item.TimeWritten.ToString("G")}</td>
                                </tr>");
            }

            sBuilder.Append(@"</tbody></table>");

            return sBuilder.ToString();
        }
    }
}
