//using Dsl;
//using Newtonsoft.Json;
//using System.Net.Sockets;
//using System.Text;
//// 64000 байт окно
//namespace Transaction
//{
//    public class TransactionLocal
//    {
//        NetworkStream networkStream;

//        public void Listen(object _client)
//        {
//            TcpClient client = (TcpClient)_client;
//            RestApi restApi = new RestApi();

//            try
//            {
//                Pakage pakge = new Pakage();
//                byte[] buffer = new byte[64000];
//                string json = string.Empty;

//                while (true)
//                {
//                    networkStream = client.GetStream(); // подключаемся к клиенту
//                    networkStream.Read(buffer, 0, 64000); // ждем пакет от клиента пока не считается весь пакет
//                    json = Encoding.UTF8.GetString(buffer);
//                    pakge = JsonConvert.DeserializeObject<Pakage>(json);

//                    restApi.Exec(pakge.Method.ToLower(), client);
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//            }

//        }
//    }
//}