using System;
using System.IO;
using Intacct.SDK.Logging;
using Microsoft.Extensions.Logging;

namespace Intacct.SDK.Tests.Logging
{
    public class MemoryStreamTestLogger : ILoggerAdapter
    {
        
        private readonly ILogger _logger;
        
        private readonly StreamWriter _streamWriter;
        
        public MemoryStreamTestLogger(MemoryStream stream, ILogger logger)
        {
            _streamWriter = new StreamWriter(stream);
            _logger = logger;
        }
        
        public void LogTrace(string message)
        {
            _streamWriter.WriteLine(message);
            _streamWriter.Flush();
            _logger.LogTrace(message);
        }
        
        public void LogDebug(string message)
        {
            _streamWriter.WriteLine(message);
            _streamWriter.Flush();
            _logger.LogDebug(message);
        }

        public void LogInformation(string message)
        {
            _streamWriter.WriteLine(message);
            _streamWriter.Flush();
            _logger.LogInformation(message);
        }

        public void LogWarning(string message)
        {
            _streamWriter.WriteLine(message);
            _streamWriter.Flush();
            _logger.LogWarning(message);
        }

        public void LogError(string message)
        {
            _streamWriter.WriteLine(message);
            _streamWriter.Flush();
            _logger.LogError(message);
        }
        public void LogError(string message, Exception exception)
        {
            _streamWriter.WriteLine(message);
            _streamWriter.Flush();
            _logger.LogError(message, exception);
        }

        public void LogCritical(string message)
        {
            _streamWriter.WriteLine(message);
            _streamWriter.Flush();
            _logger.LogCritical(message);
        }
    }
}