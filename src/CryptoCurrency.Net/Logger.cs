using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace CryptoCurrency.Net
{
    public class Logger
    {
        public static void Log(string message, Exception ex, string section, [CallerMemberName] string callerMemberName = null)
        {
            var formattedText = $"Message: {message}\r\nTime: {DateTime.Now}\r\nSection: {section}\r\nCalling Member: {callerMemberName}\r\nError: {ex}";
            Debug.WriteLine($"--------------------------------------\r\n{formattedText}\r\n--------------------------------------");
        }
    }
}
