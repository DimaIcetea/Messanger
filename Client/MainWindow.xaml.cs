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
using Client.MsgService;
using Messanger;
namespace Client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMsgServiceCallback
    {
        bool isConnected = false;
        MsgServiceClient client;
        int ID;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void MsgCallback(string msg)
        {
            lb_chat.Items.Add(msg);
        }

        void ConnectUser()
        {
            if (!isConnected)
            {
                client = new MsgServiceClient(new System.ServiceModel.InstanceContext(this));
                ID = client.Connect(tb_name.Text);
                tb_name.IsEnabled = false;
                btn_ConnDisc.Content = "Disconnect";
                isConnected = true;
            }
        }
        void DisconnectUser()
        {
            if (isConnected)
            {
                client.Disconnect(ID);
                client = null;
                tb_name.IsEnabled = true;
                btn_ConnDisc.Content = "Connect";
                isConnected = false;
            }
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectUser();
        }

        private void lb_chat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) 
            {
                if(client!=null)
                {
                    client.SendMsg(tb_message.Text, ID);
                    tb_message.Clear();
                    lb_chat.ScrollIntoView(lb_chat.Items.Count - 1);
                }
                
            }
        }
        private void ConDisClick(object sender, RoutedEventArgs e)
        {
            if (!isConnected)
                ConnectUser();
            else
                DisconnectUser();
        }

    
    }
}
