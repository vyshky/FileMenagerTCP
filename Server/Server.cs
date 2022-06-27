using Dsl;
using Model;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public sealed class ServerModel
    {
        static TcpClient client = new TcpClient();
              
        static TcpListener listener = new TcpListener(IPAddress.Any, 8888);
        Listener listen = new Listener();
        public ServerModel() { }

        public void Start()
        {
            while (true)
            {
                client = listener.AcceptTcpClient();  // ждет подключения клиента // Пока не подключится клиент дальше шаги выполнятся не будут
                Console.WriteLine("Подключение создано");
                Thread thread = new Thread(new ParameterizedThreadStart(listen.StartListener));
                thread.Start(client);
            }
        } 

    }
    
}
