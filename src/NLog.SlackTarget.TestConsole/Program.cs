using System;

namespace NLog.SlackTarget.TestConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();

            logger.Info("NLog.SlackTarget Test Info");
            logger.Warn("NLog.SlackTarget Test Warn");
            logger.Error("NLog.SlackTarget Test Error");

            Console.Read();
        }
    }
}