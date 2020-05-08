using System;

namespace TCPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerObject server = new ServerObject();
            try
            {
                server = new ServerObject();
                server.Listen();
            }
            catch (Exception ex)
            {
                server.Disconnect();
                Console.WriteLine(ex.Message);
            }
        }
    }
}
