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
using System.Windows.Shapes;

namespace awesomeHouseWPFClient
{
    /// <summary>
    /// Interaction logic for ConnectionWindow.xaml
    /// </summary>
    public partial class ConnectionWindow : Window
    {
        string username;
        string password;
        string confirmPassword;
        ChatClient client;
        public ConnectionWindow()
        {
            InitializeComponent();
        }

        private void CreateChatClient()
        {
            try
            {
                client = new ChatClient(txtBoxServerName.Text, int.Parse(txtBoxPort.Text));
                username = txtBoxUsername.Text;
                password = passBoxPassword.Password;
                confirmPassword = passBoxConfirmPassword.Password;
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid IP or Port");
            }
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            CreateChatClient();
        }

        public ChatClient ReturnChatClient()
        {
            return client;
        }

        public string ReturnUsername()
        {
            return username;
        }

        public string ReturnPassword()
        {
            return password;
        }

        public string ReturnConfirmPassword()
        {
            return confirmPassword;
        }

        // GUI Gold plating
        private void txtBoxInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                CreateChatClient();
            }
        }
    }
}
