using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeMusicApi.Models.Logging
{
    public class ConsoleLogger : ILogger
    {
        void ILogger.Log(string str)
        {
            Console.WriteLine(str);
        }
    }
}
