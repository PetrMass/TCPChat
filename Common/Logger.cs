using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class MyLogger
    {
        //Logger logger = LogManager.GetCurrentClassLogger();
        public void Write(string line)
        {
            //logger.Info(line);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(line);
            Console.ResetColor();
        }
    }
}
