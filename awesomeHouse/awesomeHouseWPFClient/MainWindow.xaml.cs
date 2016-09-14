using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace awesomeHouseWPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ChatClient client;
        Thread threadListener;
        ConnectionWindow window;
        bool connected;
        string username;
        string password;
        string confirmPassword;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                window = new ConnectionWindow();
                window.ShowDialog();

                client = window.ReturnChatClient();
                username = window.ReturnUsername();
                password = window.ReturnPassword();
                confirmPassword = window.ReturnConfirmPassword();

                WriteMessageToChatWindow(client.ConnectToServer(username, password, confirmPassword));
                connected = true;

                threadListener = new Thread(new ThreadStart(GetMessagesFromServer));
                threadListener.Start();

                //client = new ChatClient(txtBoxServerName.Text, int.Parse(txtBoxPort.Text));
            }
            catch (Exception ex)
            {
                WriteMessageToChatWindow(ex.Message);
            }
        }

        private void btnDisconnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WriteMessageToChatWindow(client.Disconnect() + "\n");
                threadListener.Abort();
                connected = false;
            }
            catch (Exception ex)
            {
                WriteMessageToChatWindow(ex.Message);
            }
        }

        private void btnClearChat_Click(object sender, RoutedEventArgs e)
        {
            chatWindow.Text = String.Empty;
        }

        private void GetMessagesFromServer()
        {
            while (connected)
            {
                string message = client.RecieveMessageFromServer();
                this.Dispatcher.Invoke((Action)(() =>
                {
                    WriteMessageToChatWindow(message);
                }));
            }
        }

        private void txtBoxInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                SendMessageToServer();
            }
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            SendMessageToServer();
        }

        private void SendMessageToServer()
        {
            client.SendMessageToServer(txtBoxInput.Text);
            txtBoxInput.Text = String.Empty;
        }

        private void WriteMessageToChatWindow(string message)
        {
            chatWindow.AppendText(message + "\n");
            chatWindow.ScrollToEnd();
        }
    }
}
