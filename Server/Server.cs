using Dsl;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public sealed class ServerModel
    {
        static TcpClient client = new TcpClient();
        static List<TcpClient> clients;
        static NetworkStream networkStream;
        static TcpListener listener = new TcpListener(IPAddress.Any, 8888);
        public ServerModel() { }

        public void Start()
        {
            while (true)
            {
                client = listener.AcceptTcpClient();  // ждет подключения клиента // Пока не подключится клиент дальше шаги выполнятся не будут
                Console.WriteLine("Подключение создано");
                clients.Add(client);
                Thread thread = new Thread(new ParameterizedThreadStart(StartListener));
                thread.Start(client);
            }
        }

        public void StartListener(object _client)
        {
            TcpClient client = (TcpClient)_client; 
            
            try
            {
                Package package = new Package();
                byte[] buffer = new byte[package.Length];
                string json = string.Empty;

                while (true)
                {
                    networkStream = client.GetStream(); // подключаемся к клиенту
                    networkStream.Read(buffer, 0, package.Length); // ждем пакет от клиента пока не считается весь пакет
                    json = Encoding.UTF8.GetString(buffer);
                    package = JsonConvert.DeserializeObject<Package>(json);
                    Execute(package);
                }
            }
            catch (Exception ex)
            {
                clients.Remove(client);
                Console.WriteLine(ex.Message);
            }

        }

        private void Execute(Package method)
        {
            // TODO :: сделать проверку на метод и выполнить в зависимости этого метод
        }
    }
    
}
