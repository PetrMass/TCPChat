using System;
using System.Net.Sockets;
using System.Text;


namespace TCPClient
{
    public class ClientHandler
    {
        public TcpClient client;
        public NetworkStream stream;
        public const string host = "127.0.0.1";
        public const int port = 8888;

        public void SendMessage() // отправка сообщений
        {
            while (true)
            {
                string message = Console.ReadLine();
                byte[] data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }
        }

        public void ReceiveMessage() // получение сообщений
        {
            while (true)
            {
                try
                {
                    byte[] data = new byte[64];
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    string message = builder.ToString();
                    Console.WriteLine(message);
                }
                catch
                {
                    Console.WriteLine("Подключение прервано!"); //соединение было прервано
                    Console.ReadLine();
                    Disconnect();
                }
            }
        }

        public void Disconnect()
        {
            if (stream != null)
                stream.Close();//отключение потока
            if (client != null)
                client.Close();//отключение клиента
            Console.WriteLine("дисконект");
            Console.ReadKey();
        }

    }
}
