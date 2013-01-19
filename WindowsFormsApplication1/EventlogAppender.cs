using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ServiceOverblik
{
    class EventlogAppender
    {
        public void writeWarning(string trace, string stacktrace)
        {

            if (!EventLog.SourceExists(Properties.Settings.Default.eventlogSource))
                EventLog.CreateEventSource(Properties.Settings.Default.eventlogSource, Properties.Settings.Default.eventlog);

            EventLog.WriteEntry(Properties.Settings.Default.eventlogSource, trace);
            EventLog.WriteEntry(Properties.Settings.Default.eventlogSource, trace,
                EventLogEntryType.Warning, 200);
            EventLog.WriteEntry(Properties.Settings.Default.eventlogSource, stacktrace);
            EventLog.WriteEntry(Properties.Settings.Default.eventlogSource, stacktrace,
                EventLogEntryType.Warning, 210);
        }

        public void writeError(string trace, string stacktrace)
        {

            if (!EventLog.SourceExists(Properties.Settings.Default.eventlogSource))
                EventLog.CreateEventSource(Properties.Settings.Default.eventlogSource, Properties.Settings.Default.eventlog);

            EventLog.WriteEntry(Properties.Settings.Default.eventlogSource, trace);
            EventLog.WriteEntry(Properties.Settings.Default.eventlogSource, trace,
                EventLogEntryType.Error, 100);
            EventLog.WriteEntry(Properties.Settings.Default.eventlogSource, stacktrace);
            EventLog.WriteEntry(Properties.Settings.Default.eventlogSource, stacktrace,
                EventLogEntryType.Error, 110);
        }

        public void writeInfo(string trace)
        {

            if (!EventLog.SourceExists(Properties.Settings.Default.eventlogSource))
                EventLog.CreateEventSource(Properties.Settings.Default.eventlogSource, Properties.Settings.Default.eventlog);

            EventLog.WriteEntry(Properties.Settings.Default.eventlogSource, trace);
            EventLog.WriteEntry(Properties.Settings.Default.eventlogSource, trace,
                EventLogEntryType.Information, 0);
        }
    }
}
