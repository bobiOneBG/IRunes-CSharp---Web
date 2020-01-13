namespace SIS.Mvc.Framework.Logging
{
    using System;
    using System.Threading;

    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"[{DateTime.Now.ToString("yyyy/MM/dd hh:mm")}] #[{Thread.CurrentThread.ManagedThreadId}] - {message}");
        }
    }
}