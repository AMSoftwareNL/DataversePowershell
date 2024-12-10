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
using Microsoft.Extensions.Logging;
using System;

namespace AMSoftware.Dataverse.PowerShell
{
    internal sealed class ServiceClientLogger : PSLoggerBase, ILogger
    {
        private static readonly ServiceClientLogger _instance = new();

        internal static ServiceClientLogger Instance
        {
            get
            {
                return _instance;
            }
        }

        private ServiceClientLogger()
        {

        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string message = formatter(state, exception);

            if (_logWriter != null)
            {
                _logWriter(new PSLogEntry()
                {
                    LogLevel = logLevel,
                    Message = message
                });
            }
        }
    }
}