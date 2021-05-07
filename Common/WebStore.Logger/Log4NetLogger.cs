using log4net;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WebStore.Logger
{
    public class Log4NetLogger : ILogger 
    {
        private ILog _log;

        public Log4NetLogger(string category, XmlElement configuration)
        {
            var logger_repository = LogManager.CreateRepository(
                Assembly.GetEntryAssembly(),
                typeof(log4net.Repository.Hierarchy.Hierarchy)
                );

            _log = LogManager.GetLogger(logger_repository.Name, category);

            log4net.Config.XmlConfigurator.Configure(logger_repository, configuration);
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return new NDCDisposable(Convert.ToString(state));
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel switch {
                LogLevel.None => false,
                LogLevel.Trace => _log.IsDebugEnabled,
                LogLevel.Information => _log.IsInfoEnabled,
                LogLevel.Warning => _log.IsWarnEnabled,
                LogLevel.Error => _log.IsErrorEnabled,
                LogLevel.Critical => _log.IsFatalEnabled,
                _ => throw new ArgumentOutOfRangeException(nameof(LogLevel), logLevel, null)
            };
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter == null)
                throw new ArgumentNullException(nameof(formatter));

            if (!IsEnabled(logLevel)) return;

            var log_message = formatter(state, exception);

            if (string.IsNullOrEmpty(log_message) && exception == null) return;

            switch (logLevel) {
                default: throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
                case LogLevel.None: break;

                case LogLevel.Trace:
                case LogLevel.Debug:
                    _log.Debug(log_message);
                    break;
                case LogLevel.Information:
                    _log.Info(log_message);
                    break;
                case LogLevel.Warning:
                    _log.Warn(log_message);
                    break;
                case LogLevel.Error:
                    _log.Error(log_message, exception);
                    break;
                case LogLevel.Critical:
                    _log.Fatal(log_message, exception);
                    break;
            }
        }

        private class NDCDisposable : IDisposable
        {
            public NDCDisposable(string s) { log4net.NDC.Push(s); }
            public void Dispose() => log4net.NDC.Pop();
        }
    }
}
