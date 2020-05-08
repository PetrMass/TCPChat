using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


namespace TCPServer
{
    public class ServerObject
    {
        TcpListener tcpListener;
        public List<ClientWorker> clients = new List<ClientWorker>(); // все подключения
        MessageViewer viewer = new MessageViewer();
        public Logger logger = new Logger();

        public void AddConnection(ClientWorker clientObject)
        {
            clients.Add(clientObject);
        }
       public void RemoveConnection(string id) // передаелать
        {
            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].Id == id)
                {
                    logger.Write(String.Format("клиент {0} удален", clients[i].userName));
                    clients.Remove(clients[i]);                    
                }
            }
        }
        // прослушивание входящих подключений
        public void Listen()
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8888);
                tcpListener.Start();
                logger.Write("Сервер запущен. Ожидание подключений...");

                while (true)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();
                    ClientWorker clientObject = new ClientWorker(tcpClient, this);                    
                    Thread clientThread = new Thread(clientObject.Process);
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                logger.Write(ex.Message);
                Disconnect();
            }
        }

        // трансляция сообщения клиентам
        public void BroadcastMessage(string message, string id, string sendId)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            if (sendId == "all")
            {
                for (int i = 0; i < clients.Count; i++)
                {
                    if (clients[i].Id != id & clients[i].userName != null) // если id клиента не равно id отправляющего
                    {                     
                        clients[i].Stream.Write(data, 0, data.Length);
                    }
                }
            }
            else if (sendId == "client") 
            {
                for (int i = 0; i < clients.Count; i++)
                {
                    if (clients[i].Id == id)
                    {                     
                        clients[i].Stream.Write(data, 0, data.Length);
                    }
                }
            }
            else
            {
                for (int i = 0; i < clients.Count; i++)
                {
                    if (clients[i].userName == sendId) // если имя клиента равно имени отправляющего
                    {
                        clients[i].Stream.Write(data, 0, data.Length); //передача данных
                    }
                }
            }
        }
        // отключение всех клиентов
        public void Disconnect()
        {
            tcpListener.Stop(); //остановка сервера

            for (int i = 0; i < clients.Count; i++)
            {
                clients[i].Close(); //отключение клиента
                logger.Write("отключение клиентов");
            }            
        }
    }
}
