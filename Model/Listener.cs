using Model;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Text;

namespace Model
{
    public class Listener
    {
        static NetworkStream networkStream;
        static List<TcpClient> clients = new List<TcpClient>();      
        public void StartListener(TcpClient client)
        {
            clients.Add(client);
            try
            {
                Package package = new Package();
                byte[] buffer = new byte[package.Bandwidth];
                string json = string.Empty;
                int packetLenght = 0;

                while (true)
                {
                    networkStream = client.GetStream(); // подключаемся к клиенту
                    packetLenght = networkStream.Read(buffer, 0, package.Bandwidth); // ждем пакет от клиента пока не считается весь пакет
                    json += Encoding.UTF8.GetString(buffer);


                    // TODO :: разделить большие файлы на (package.Bandwidth) куски и отправлять их пока (packetLenght < package.packetLenght)
                    package = JsonConvert.DeserializeObject<Package>(json);
                    //PackageModel.Execute(package);
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
