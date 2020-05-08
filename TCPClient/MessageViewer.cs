using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPClient
{
    class MessageViewer
    {
        public void Write(string line)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(line);
            Console.ResetColor();
        }
    }
}
