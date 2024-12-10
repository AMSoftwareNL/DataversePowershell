/* 
PowerShell Module for Power Platform Dataverse
Copyright(C) 2024  AMSoftwareNL

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Abstractions;
using System;

namespace AMSoftware.Dataverse.PowerShell
{
    internal sealed class MSALIdentityLogger : PSLoggerBase, IIdentityLogger
    {
        private static readonly MSALIdentityLogger _instance = new();

        internal static MSALIdentityLogger Instance
        {
            get
            {
                return _instance;
            }
        }

        private MSALIdentityLogger()
        {

        }

        public bool IsEnabled(EventLogLevel eventLogLevel)
        {
            return true;
        }

        public void Log(LogLevel level, string message, bool containsPii)
        {
            if (_logWriter != null)
            {
                switch (level)
                {
                    case LogLevel.Verbose:
                        _logWriter(new PSLogEntry()
                        {
                            LogLevel = Microsoft.Extensions.Logging.LogLevel.Trace,
                            Message = message
                        });
                        break;
                    case LogLevel.Warning:
                        _logWriter(new PSLogEntry()
                        {
                            LogLevel = Microsoft.Extensions.Logging.LogLevel.Warning,
                            Message = message
                        });
                        break;
                    case LogLevel.Always:
                        _logWriter(new PSLogEntry()
                        {
                            LogLevel = Microsoft.Extensions.Logging.LogLevel.Debug,
                            Message = message
                        });
                        break;
                }
            }
        }

        public void Log(LogEntry entry)
        {
            if (_logWriter != null)
            {
                switch (entry.EventLogLevel)
                {
                    case EventLogLevel.Verbose:
                        _logWriter(new PSLogEntry()
                        {
                            LogLevel = Microsoft.Extensions.Logging.LogLevel.Trace,
                            Message = entry.Message
                        });
                        break;
                    case EventLogLevel.Warning:
                        _logWriter(new PSLogEntry()
                        {
                            LogLevel = Microsoft.Extensions.Logging.LogLevel.Warning,
                            Message = entry.Message
                        });
                        break;
                    case EventLogLevel.LogAlways:
                        _logWriter(new PSLogEntry()
                        {
                            LogLevel = Microsoft.Extensions.Logging.LogLevel.Debug,
                            Message = entry.Message
                        });
                        break;
                }
            }
        }
    }
}