using System;
using Api.Database;
using Api.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Api.Logger
{
    /// <summary>
    /// A logger that logs to the databse
    /// </summary>
    public class DatabaseLogger : ILogger
    {
        private readonly string _name;

        public DatabaseLogger(string name)
        {
            _name = name;

        }

        public IDisposable BeginScope<TState>(TState state) => default;

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            Console.WriteLine($"Log({logLevel}, '{formatter(state, exception)}')");

            //var scopeFactory = _serviceProvider.GetRequiredService<IServiceScopeFactory>();

            //_databaseContext
            //    .Set<LogEntry>()
            //    .Add(new LogEntry
            //    {
            //        MachineName = Environment.MachineName,
            //        LoggerName = _name,
            //        CreatedAt = DateTime.Now,
            //        Description = formatter(state, exception),
            //        Exception = exception.Message,
            //        LogLevel = logLevel.ToString(),
            //    });

            //_databaseContext.SaveChanges();
        }

        private class NoopDisposable : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}
