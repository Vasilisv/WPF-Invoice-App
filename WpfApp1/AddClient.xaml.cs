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
    /// Interaction logic for AddClient.xaml
    /// </summary>
    public partial class AddClient : Window
    {
        public AddClient()
        {
            InitializeComponent();
            string connectionstring = null;
            connectionstring = myConnection.connectionString();
            MySqlConnection cnn = new MySqlConnection(connectionstring);
            cnn.Open();
            MySqlCommand command = cnn.CreateCommand();
            command.Parameters.AddWithValue("@id", LoginWindow.id);
            command.CommandText = "SELECT name,afm,doy,work,town,address FROM clients WHERE userid=@id";
            MySqlDataReader reader = command.ExecuteReader();
            datagrid.ItemsSource = reader;
        }

        private void addclientbutton_Click(object sender, RoutedEventArgs e)
        {
            string connectionstring = null;
            connectionstring = myConnection.connectionString();
            MySqlConnection cnn = new MySqlConnection(connectionstring);
            cnn.Open();
            MySqlCommand command = cnn.CreateCommand();
            command.Parameters.AddWithValue("@id", LoginWindow.id);
            command.CommandText = "SELECT * FROM user WHERE id=@id";
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                cnn.Close();
                cnn.Open();
                MySqlCommand command2 = cnn.CreateCommand();
                command2.Parameters.AddWithValue("@userid", LoginWindow.id);
                command2.Parameters.AddWithValue("@name", nameField.Text);
                command2.Parameters.AddWithValue("@afm", afmField.Text);
                command2.Parameters.AddWithValue("@doy", doyField.Text);
                command2.Parameters.AddWithValue("@work", workField.Text);
                command2.Parameters.AddWithValue("@address", addressField.Text);
                command2.Parameters.AddWithValue("@town", townField.Text);
                command2.CommandText = "INSERT INTO clients (userid, name, afm, doy, work, address, town) VALUES (@userid, @name, @afm, @doy, @work, @address, @town)";
                MySqlDataReader reader2 = command.ExecuteReader();
                Console.WriteLine("hiii");
                if (reader2.Read())
                {
                    MessageBox.Show("Client was added successfuly.");
                    AddClient ac = new AddClient();
                    ac.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Failed to create client. ");
                }
            }
            

            cnn.Close();
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            string connectionstring = null;
            connectionstring = myConnection.connectionString();
            MySqlConnection cnn = new MySqlConnection(connectionstring);
            cnn.Open();
            MySqlCommand command = cnn.CreateCommand();
            command.Parameters.AddWithValue("@id", LoginWindow.id);
            command.CommandText = "SELECT name,afm,doy,work,town,address FROM clients WHERE userid=@id";
            MySqlDataReader reader = command.ExecuteReader();
            datagrid.ItemsSource = reader;
        }
    }
}
