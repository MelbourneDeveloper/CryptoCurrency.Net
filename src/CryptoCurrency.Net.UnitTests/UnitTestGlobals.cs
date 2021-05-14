using Microsoft.Extensions.Logging;

#pragma warning disable IDE0059 // Unnecessary assignment of a value

namespace CryptoCurrency.Net.UnitTests
{
    public static class UnitTestGlobals
    {
        public static readonly ILoggerFactory LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder => _ = builder.AddDebug().SetMinimumLevel(LogLevel.Trace));

    }
}
