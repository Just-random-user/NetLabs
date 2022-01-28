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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfIPRawSocks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Socket CreateRawIPSock(string ip, int port)
        {
            IPAddress.TryParse(ip, out var address);
            Socket res = new Socket(SocketType.Raw, ProtocolType.IP);
            IPEndPoint endPoint = new IPEndPoint(address, port);
            res.Connect(endPoint);
            return res;
        }
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            TextBlock.Text = null;
            try
            {
                Socket sock = CreateRawIPSock(IpAddress.Text, int.Parse(Port.Text));
                await sock.SendAsync(Encoding.UTF8.GetBytes(Message.Text), SocketFlags.None);
                sock.Close();
            }
            catch (Exception exception)
            {
                TextBlock.Text = "An error has occured: " + exception.Message;
            }
        }
    }
}