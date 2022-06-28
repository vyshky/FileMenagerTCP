using Microsoft.Win32;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Media;
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using System.Threading;
using Model;

namespace FileMenager3
{    
    public partial class MainWindow : Window
    {
        TcpClient connectionToServer;
        NetworkStream stream;
        string ip;
        int port;
        OpenFileDialog openFile;
        string path;

        public MainWindow()
        {
            InitializeComponent();
            ip = "127.0.0.1";
            port = 8888;
        }

        //ctrl shift u
        //ctrl u
        private void FormClickButtonConnection(object sender, RoutedEventArgs e)
        {
            ip = FormIp.Text;
            port = Convert.ToInt32(FormPort.Text);
            connectionToServer = new TcpClient(ip, port);
            stream = connectionToServer.GetStream();

            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = System.Windows.Media.Color.FromArgb(255, 0, 255, 0);
            FormIndicator.Fill = mySolidColorBrush;
        }
        private void FormClickButtonDisconnection(object sender, RoutedEventArgs e)
        {
            stream.Close();
            connectionToServer.Close();
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = System.Windows.Media.Color.FromArgb(255, 255, 0, 0);
            FormIndicator.Fill = mySolidColorBrush;
        }
        private void FormClickButtonUpload(object sender, RoutedEventArgs e)
        {
            if (path == null) return;
            Package file = new Package();

            ////// Инициализируем file
            int index = path.LastIndexOf('\\');
            file.Name = path.Remove(0, index + 1); // удаляет  от 0 до индекса
            file.Data = File.ReadAllBytes(path);
            file.Method = "post";
            file.Length = file.Data.Length;
            /////////////////////////////////////////////           

            string jsonObject = file.GetJson();

            byte[] packetJson = Encoding.UTF8.GetBytes(jsonObject);

            stream.Write(packetJson, 0, packetJson.Length);
        }
        private void FormClickButtonBrowse(object sender, RoutedEventArgs e)
        {
            SelectFile();
        }

        private void FormClickButtonDownLoad(object sender, RoutedEventArgs e)
        {
            SelectFolder();
            Thread thread = new Thread(ListenerDownLoad);
            thread.Start();

            Package file = new Package();
            file.Method = "Download";
            file.Name = "";
            //file.Bandwidth = 1;
            file.Data = new byte[1] { 1 };

            string jsonObject = file.GetJson();

            byte[] packetJson = Encoding.UTF8.GetBytes(jsonObject);

            stream.Write(packetJson, 0, packetJson.Length);
        }

        void SelectFolder()
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result != CommonFileDialogResult.Ok) return;
            path = dialog.FileName;
        }
        void SelectFile()
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = false;
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result != CommonFileDialogResult.Ok) return;
            path = dialog.FileName;
        }
        void ListenerDownLoad()
        {
            int packetLenght;
            string json = string.Empty;

            while (true)
            {
                stream = connectionToServer.GetStream();
                byte[] fileBuffer = new byte[60000];
                packetLenght = stream.Read(fileBuffer, 0, 60000);

                json += Encoding.UTF8.GetString(fileBuffer);              

                if (packetLenght < fileBuffer.Length)
                {
                    break;
                }
            }

            Package file = new Package();
            file = JsonConvert.DeserializeObject<Package>(json);

            if (file == null)
            {
                MessageBox.Show("Файл передан пустой", "Error", MessageBoxButton.OK);
            }

            File.WriteAllBytes(path + "\\" + file.Name, file.Data);
            MessageBox.Show("Файл передан", "Succes", MessageBoxButton.OK);
        }

    }
}
