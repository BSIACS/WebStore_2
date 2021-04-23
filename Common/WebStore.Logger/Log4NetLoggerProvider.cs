using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Xml;

namespace WebStore.Logger
{
    public class Log4NetLoggerProvider : ILoggerProvider
    {
        public readonly string _configurationFile;
        public readonly ConcurrentDictionary<string, Log4NetLogger> _loggers = new ConcurrentDictionary<string, Log4NetLogger>();

        public Log4NetLoggerProvider(string configurationFile)
        {
            _configurationFile = configurationFile;
        }

        public ILogger CreateLogger(string category)
        {
            return _loggers.GetOrAdd(category, category => {
                var xml = new XmlDocument();
                xml.Load(_configurationFile);

                return new Log4NetLogger(category, xml["log4net"]);
            });
        }

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}
