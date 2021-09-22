using System;
using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Api.Logger
{
    /// <summary>
    /// It's task is to provide loggers when requested
    /// </summary>
    public sealed class DatabaseLoggerProvider : ILoggerProvider
    {
        private readonly ConcurrentDictionary<string, ILogger> _loggers = new ConcurrentDictionary<string, ILogger>();
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// ctor
        /// </summary>
        public DatabaseLoggerProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ILogger CreateLogger(string categoryName)
        {
            // We ignore this logger, since it's causing endless loops
            var categoryToIgnore = $"{nameof(Microsoft)}.{nameof(Microsoft.EntityFrameworkCore)}.{nameof(Microsoft.EntityFrameworkCore.Infrastructure)}";
            if (categoryName == categoryToIgnore)
            {
                return _loggers.GetOrAdd(categoryName, name => new NullLogger<DbContext>());
            }

            //using var scope = _serviceProvider.CreateScope();
            //var logger = scope.ServiceProvider.GetRequiredService<DatabaseLogger>();

            //var logger = _loggers.GetOrAdd(categoryName, name => new DatabaseLogger(categoryName, _serviceProvider));

            return _loggers.GetOrAdd(categoryName, name => new DatabaseLogger(name));
        }

        public void Dispose() => _loggers.Clear();
    }
}
