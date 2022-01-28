using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IpSniffer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool initialized = false;
        private Socket sock;
        private Socket CreateRawIPSock(string ip, int port)
        {
            IPAddress.TryParse(ip, out var address);
            Socket res = new Socket(SocketType.Raw, ProtocolType.IP);
            IPEndPoint endPoint = new IPEndPoint(address, port);
            res.Bind(endPoint);
            return res;
        }

        private async Task Sniff(Socket sock)
        {
            byte[] data = new byte[1024];
            await sock.ReceiveAsync(data, SocketFlags.None);
            byte[] message = new byte[1024];
            Array.Copy(data, 20, message, 0, 1004);
            TextBlock.Text += Environment.NewLine + System.Text.Encoding.Default.GetString(message);
        }
        public MainWindow()
        {
            InitializeComponent();
            
        }
        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            TextBlock.Text = null;
            //try {sock.Close();} catch(Exception exception) {}
            try
            {
                sock = CreateRawIPSock(IpAddress.Text, int.Parse(Port.Text));
                while (true) 
                    await Sniff(sock);
                
                //sock.Close();
            }
            catch (Exception exception)
            {
                TextBlock.Text = "An error has occured: " + exception.Message;
            }

            initialized = true;
        }
    }
}