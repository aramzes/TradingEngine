using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using TradingEngineServer.Logging.LoggingConfiguration;

namespace TradingEngineServer.Logging
{
    public class TextLogger : AbstractLogger, ITextLogger
    {
        private readonly LoggerConfiguration _loggerConfiguration;
        private readonly BufferBlock<LogInformation> _logQueue = new BufferBlock<LogInformation>();
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private bool _disposed = false;
        private readonly object _lock = new Object();
        public TextLogger(IOptions<LoggerConfiguration> loggerConfiguration) : base()
        {
            _loggerConfiguration = loggerConfiguration.Value ?? throw new ArgumentNullException(nameof(LoggerConfiguration));
            if (_loggerConfiguration.loggerType != LoggerType.Text)
                throw new InvalidOperationException($"type of {_loggerConfiguration.loggerType} doesn't match type of {nameof(TextLogger)}");
            var now = DateTime.Now;
            string logDirectory = Path.Combine(_loggerConfiguration.TextLoggerConfiguration.Directory, $"{now:yy-MM-dd}");
            Directory.CreateDirectory(logDirectory);
            string uniqueLogFileName = $"{_loggerConfiguration.TextLoggerConfiguration.Filename}-{now:HH-mm-ss}";
            string logFilepath = Path.ChangeExtension(uniqueLogFileName, _loggerConfiguration.TextLoggerConfiguration.FileExtension);
            string filepath = Path.Combine(logDirectory, logFilepath);
            
            Task.Run(() => LogAsync(filepath, _logQueue, _cancellationTokenSource.Token));
        }

        private static async Task LogAsync(string filepath, BufferBlock<LogInformation> logQueue, CancellationToken token)
        {
            using var fs = new FileStream(filepath, FileMode.CreateNew, FileAccess.Write, FileShare.Read);
            using var sw = new StreamWriter(fs);
            try
            {
                var logItem = await logQueue.ReceiveAsync(token).ConfigureAwait(false);
                string formattedMessage = FormatLogItem(logItem);
                await sw.WriteAsync(formattedMessage).ConfigureAwait(false);
            } catch (OperationCanceledException)
            {

            }
        }

        private static string FormatLogItem(LogInformation logItem)
        {
            return $"[{logItem.Now:yy-MM-dd HH-mm-ss.fffffff}] [{logItem.ThreadName,-30}:{logItem.ThreadId:000}] " +
                $"[{logItem.logLevel}] {logItem.Module}: {logItem.Message}";
        }

        protected override void Log(LogLevel logLevel, string module, string message)
        {
            _logQueue.Post(new LogInformation(logLevel, module, message, DateTime.Now, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.Name));
        }

        ~TextLogger()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            lock (_lock)
            {
                if (_disposed)
                    return;
                _disposed = true;
            }
     

            if (disposing)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
            }

            
        }
    }
}
