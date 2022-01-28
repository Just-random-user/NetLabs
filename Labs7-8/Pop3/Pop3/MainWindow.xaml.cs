using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Pop3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private class DataMessage
        {
            public string Date { get; set; }
            public string Sender { get; set; }
            public string Title { get; set; }
            public string MessageBody { get; set; }
        }
        
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void buttonSynch_Click(object sender, RoutedEventArgs e)
        {
    
            try
            {
                OpenPop.Pop3.Pop3Client client = new OpenPop.Pop3.Pop3Client();

                client.Connect("pop.gmail.com", 995, true);
                client.Authenticate(textBoxEmail.Text, textBoxPwd.Password);

                var count = client.GetMessageCount();
                for (int i = 1; i <= count; i++)
                {
                    OpenPop.Mime.Message message = client.GetMessage(i);
                    var test = message.Headers.DateSent.ToString();
                    var data = new DataMessage { Date = message.Headers.DateSent.ToString(), Sender = message.Headers.From.ToString(), Title = message.Headers.Subject, MessageBody = !message.MessagePart.IsMultiPart ? message.MessagePart.GetBodyAsText() : "data_absent" };

                    DataGridTest.Items.Add(data);
                }
                textBoxLog.Text += "\n>Synch success: login["+textBoxEmail.Text+"]";
            }
            catch (Exception ex)
            {
                textBoxLog.Text += "\n>Error while sync: " + ex.Message.ToString();
            }
            
        }
    }
}