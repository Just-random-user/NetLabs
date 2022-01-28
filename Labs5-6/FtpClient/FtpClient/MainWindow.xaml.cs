using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using Path = System.IO.Path;

namespace FtpClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Get_OnClick(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(FileName.Text) || String.IsNullOrEmpty(TxtUrl.Text))
            {
                TxtResult.Text = "Not enough info";
                return;
            }
            try
            {
                string nl = Environment.NewLine;
                TxtResult.Text += nl + "Load: " + TxtRead.Text + TxtUrl + FileName + nl;
                TxtResult.Text += "[" + DateTime.Now.ToString("HH:mm:ss.ffff") + "] Start_load\n";
                TxtResult.Text += "[" +  DateTime.Now.ToString("HH:mm:ss.ffff") + "] Connection\n";

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(TxtRead.Text + TxtUrl.Text + FileName.Text);
                        
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                TxtResult.Text += "[" + DateTime.Now.ToString("HH:mm:ss.ffff") + "] Response_from_server\n";
                Stream responseStream = response.GetResponseStream();
                TxtResult.Text += "[" + DateTime.Now.ToString("HH:mm:ss.ffff") + "] Data_from_server\n";
                       

                FileStream fs = new FileStream("$HOME\\Downloads" + Path.PathSeparator
                                                                  + FileName.Text, FileMode.Create);
                TxtResult.Text += "[" + DateTime.Now.ToString("HH:mm:ss.ffff") + "] FSream_ceated\n";
                byte[] buffer = new byte[64];
                int size = 0;
                while ((size = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                    fs.Write(buffer, 0, size);
                fs.Close();
                response.Close();
                TxtResult.Text += "[" + DateTime.Now.ToString("HH:mm:ss.ffff") + "] End_load\n";
            }
            catch (Exception ex)
            {
                TxtResult.Text += "\n#Error#\n" + ex.Message+"\n#---#\n";
            }
        }
    }
}