using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;

namespace WebStore.Logger
{
    public static class Log4NetExtensions {
        public static ILoggerFactory AddLog4Net(this ILoggerFactory factory, string configurationFilePath = "log4net.config" )
        {
            if (configurationFilePath == null) throw new ArgumentNullException(nameof(configurationFilePath));

            if (!Path.IsPathRooted(configurationFilePath)) {
                var assembly = Assembly.GetEntryAssembly() ?? throw new InvalidOperationException("Ссылка на сборку не указывает на объект");
                var dir = Path.GetDirectoryName(assembly.Location) ?? throw new InvalidOperationException("Ссылка на путь к сборке не указывает на обект");
                configurationFilePath = Path.Combine(dir, configurationFilePath);
            }

            factory.AddProvider(new Log4NetLoggerProvider(configurationFilePath));

            return factory;
        }
    }
}
