using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeMusicApi.Logging
{
    public class ConsoleLogger : ILogger
    {
        void ILogger.Log(string str)
        {
            Console.WriteLine(str);
        }
    }
}
