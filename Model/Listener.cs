using Newtonsoft.Json;
using System.Net.Sockets;
using System.Text;

namespace Model
{
    public class Listener
    {
        static NetworkStream networkStream;
        static List<TcpClient> clients = new List<TcpClient>();
        PackageModel pModel = new PackageModel();
        public void StartListener(TcpClient client)
        {
            clients.Add(client);
            try
            {
                Package package = new Package();
                byte[] buffer = new byte[package.Bandwidth];
                string json = string.Empty;
                int packetLenght = 0;

                networkStream = client.GetStream(); // подключаемся к клиенту
                while (true)
                {                              
                    packetLenght = networkStream.Read(buffer, 0, package.Bandwidth); // ждем пакет от клиента пока не считается весь пакет

                    if (networkStream.CanRead)
                    {    
                        // TODO :: не хранить большой объем памяти в json лучше сразу дозаписавыть Data в файл
                        json += Encoding.UTF8.GetString(buffer.AsSpan(0, packetLenght));

                        var ch = json[json.Length - 1];

                        if (json[json.Length - 1] == '}')
                        {
                            package = JsonConvert.DeserializeObject<Package>(json);
                            Console.WriteLine("Размер пакета " + json.Length);
                            Console.WriteLine("Размер файла " + package.Data.Length);
                            Console.WriteLine("Загружен файл");
                            json = string.Empty;
                            pModel.Execute(package);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (client.Connected)
                {
                    client.Close();
                }
                clients.Remove(client);
                Console.WriteLine(ex.Message);
            }
        }
    }
}
