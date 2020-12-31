using System.Windows;
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
                if (command2.ExecuteNonQuery() > 0)
                {

                    MessageBox.Show("Client was added successfuly.");
                
                }
                else
                    MessageBox.Show("Record was not added!");
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


        //MENU 
        private void AddNewClient_Click(object sender, RoutedEventArgs e)
        {
            AddClient ac = new AddClient();
            ac.Show();
            this.Hide();
        }

        private void createinvoice_Click(object sender, RoutedEventArgs e)
        {
            InvoiceWindow iw = new InvoiceWindow();
            iw.Show();
            this.Hide();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            InvoiceInfo ii = new InvoiceInfo();
            ii.Show();
            this.Hide();
        }

        private void AddNewProduct_Click(object sender, RoutedEventArgs e)
        {
            AddProduct ap = new AddProduct();
            ap.Show();
            this.Hide();
        }

        private void InvoiceManagement_Click(object sender, RoutedEventArgs e)
        {
            Invoice_Management im = new Invoice_Management();
            im.Show();
            this.Hide();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void deliverynote_Click(object sender, RoutedEventArgs e)
        {
            DeliveryNoteWindow dn = new DeliveryNoteWindow();
            dn.Show();
            this.Hide();
        }

        private void DNInvoice_Click(object sender, RoutedEventArgs e)
        {
            InvoiceDNWindow idn = new InvoiceDNWindow();
            idn.Show();
            this.Hide();
        }
    }
}
