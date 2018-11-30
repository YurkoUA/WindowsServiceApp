using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsServiceApp.Mongo.Models;

namespace WindowsServiceApp.Sender
{
    public class MarkupBuilder
    {
        public string Build(IEnumerable<EventLogRecord> logs)
        {
            if (!logs.Any())
            {
                return "<b>There are not any new logs.</b>";
            }

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
