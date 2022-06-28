using Model;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Text;

namespace Model
{
    public class Listener
    {
        static NetworkStream networkStream;
        static List<TcpClient> clients;
        static PackageModel pModel;
        public void StartListener(TcpClient client)
        {            
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
                    PackageModel.Execute(package);
                }
            }
            catch (Exception ex)
            {
                if(client.Connected)
                {
                    client.Close();
                }
                clients.Remove(client);                              
                Console.WriteLine(ex.Message);
            }
        }
    }
}
