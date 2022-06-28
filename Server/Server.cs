using Model;
using System.Net;
using System.Net.Sockets;

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
                Thread thread = new Thread(new ParameterizedThreadStart(Listener));
                thread.Start(client);
            }
        }
    
        public void Listener(object client)
        {
            listen.StartListener((TcpClient)client);
        }


    }
    
}
