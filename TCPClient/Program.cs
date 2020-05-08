using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TCPClient
{
    class Program
    {
        static void Main(string[] args)
        {           
            const string host = "127.0.0.1";
            const int port = 8888;
            ClientHandler handler = new ClientHandler();
            handler.client = new TcpClient();

            try
            {
                handler.client.Connect(host, port); //подключение клиента
                handler.stream = handler.client.GetStream(); // получаем поток

                Thread receiveThread = new Thread(handler.ReceiveMessage); // запускаем новый поток для получения данных
                receiveThread.Start();
                Console.WriteLine("Соединение с сервером установлено");
                handler.SendMessage();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                handler.Disconnect();
            }
        }


    }

    
}
