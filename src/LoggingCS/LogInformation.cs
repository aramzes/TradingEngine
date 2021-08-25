using System;
using System.Collections.Generic;
using System.Text;

namespace TradingEngineServer.Logging
{
    public record LogInformation(LogLevel logLevel, string Module, string Message, DateTime Now, int ThreadId, string ThreadName);
}

namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit { }
}