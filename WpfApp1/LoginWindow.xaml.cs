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
using MySql.Data.MySqlClient;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        
        string pass;
        string usrname;
        public static int id;

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connectionstring = null;
            connectionstring = myConnection.connectionString();
            MySqlConnection cnn = new MySqlConnection(connectionstring);
            cnn.Open();
            MySqlCommand command = cnn.CreateCommand();
            
            command.Parameters.AddWithValue("@username", usernameField.Text);
            command.Parameters.AddWithValue("@password", passwordField.Text);
            command.CommandText = "SELECT * FROM user WHERE username = @username AND pass = @password";
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                
 
                     id = reader.GetInt32(5);
                
                MessageBox.Show("Logged In ! USER ID:" + id);
                AddClient ac = new AddClient();
                ac.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Failed to Login. Check Username and/or Password. ");
            }
                
                cnn.Close();
            }
           /* catch (Exception ex)

            {
                MessageBox.Show("Can not open connection ! ");
            }*/
        }
    }

