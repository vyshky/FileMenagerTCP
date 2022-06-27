using Dsl;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Text;

namespace Model
{
    public class Listener
    {
        static NetworkStream networkStream;
        static List<TcpClient> clients;  
        public void StartListener(object _client)
        {
            TcpClient client = (TcpClient)_client;
            clients.Add(client);
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
                client.EndConnect();
                client?.Close();                
                Console.WriteLine(ex.Message);
            }
        }

        private void Execute(Package method)
        {
            // TODO :: сделать проверку на метод и выполнить в зависимости этого метод
        }
    }
}
