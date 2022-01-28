using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace HttpClientNets
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

        private async void Get_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                HttpClient client = new HttpClient();
                string text = (await client.GetAsync(Url.Text)).Content.ReadAsStringAsync().Result;
                Content.Text = text.Replace(">", ">" + Environment.NewLine);
            }
            catch (Exception exception)
            {
                Content.Text = "An exception has occured: " + exception.Message;
            }
        }
    }
}