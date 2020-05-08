using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPServer
{
    public class Logger
    {
        public void Write(string line)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(line);
            Console.ResetColor();
        }
    }
}
