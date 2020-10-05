using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeMusicApi.Utils
{
    public class StaticUtils
    {
        public static List<string> StringSplit(string input, string splitStr)
        {
            List<string> res = new List<string>();

            string toProcess = input;
            while (toProcess.IndexOf(splitStr) > -1)
            {
                int index = toProcess.IndexOf(splitStr);
                string sub = toProcess.Substring(0, index);
                res.Add(sub);
                toProcess = toProcess.Remove(0, index+1);
            }

            res.Add(toProcess);

            return res;
        }
    }
}
