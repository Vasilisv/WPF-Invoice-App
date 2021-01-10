using MySql.Data.MySqlClient;
using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Syncfusion.SfSkinManager;
using WpfApp1;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Payments_Management.xaml
    /// </summary>
    public partial class Payments_Management : Window
    {
        List<string> ListOfSelectedItems = new List<string>() ;
        public Payments_Management()
        {
            InitializeComponent();
       
            SfSkinManager.SetVisualStyle(dg, VisualStyles.MaterialLight);
            PMViewModel PMviewModel = new PMViewModel();
            dg.ItemsSource = PMviewModel.Orders;
            dg.AllowFiltering = true;
        }

        private void dg_SelectionChanged(object sender, GridSelectionChangedEventArgs e)
        {
            PaymentsDetails a = new PaymentsDetails(null, 0);
            a= (PaymentsDetails) dg.CurrentItem;

            if (a != null)
            {
                //ListOfSelectedItems.Add(a.Client_Name);
                nameField.Text = a.Client_Name;
            }
        }

        private void ispaid_Click(object sender, RoutedEventArgs e)
        {
            string connectionstring = null;
            
            foreach (var o in ListOfSelectedItems)
            {
                Console.WriteLine(o);
                connectionstring = myConnection.connectionString();
                MySqlConnection cnn = new MySqlConnection(connectionstring);
                MySqlCommand command = cnn.CreateCommand();
                cnn.Open();
                command.Parameters.AddWithValue("@id", LoginWindow.id);

                command.Parameters.AddWithValue("@IsPaid", o);
                command.CommandText = "SELECT IsPaid FROM SavedInvoices WHERE InvoiceId=@isPaid AND userid=@id";
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetBoolean("IsPaid").Equals(false))
                    {
                        connectionstring = myConnection.connectionString();
                        MySqlConnection cnn2 = new MySqlConnection(connectionstring);
                        cnn2.Open();
                        MySqlCommand command2 = cnn2.CreateCommand();
                        command2.Parameters.AddWithValue("@value", 1);
                        command2.Parameters.AddWithValue("@IsPaid", o);
                        
                        //command2.CommandText = "INSERT INTO SavedInvoices (isPaid) VALUES (1) WHERE InvoiceId=@IsPaid";
                        command2.CommandText= "UPDATE SavedInvoices SET IsPaid=@value WHERE InvoiceId=@IsPaid";
                        if (command2.ExecuteNonQuery() > 0)
                        {
                        }
                        else
                        {
                        }
                  
                        cnn2.Close();
                    }
                    else
                    {
                        connectionstring = myConnection.connectionString();
                        MySqlConnection cnn2 = new MySqlConnection(connectionstring);
                        cnn2.Open();
                        MySqlCommand command2 = cnn2.CreateCommand();
                        command2.Parameters.AddWithValue("@value", 0);
                        command2.Parameters.AddWithValue("@IsPaid", o);
                        command2.CommandText = "UPDATE SavedInvoices SET IsPaid=@value WHERE InvoiceId=@IsPaid";
                        if (command2.ExecuteNonQuery() > 0)
                        {
                        }
                        else
                        {
                        }
                        cnn2.Close();
                    }
                   
                }
                cnn.Close();
            }
            MessageBox.Show("Invoice Informations were successfuly updated.");
            Invoice_Management im = new Invoice_Management();
            this.Hide();
            im.Show();
        }

        private void UpdateBalance_Click(object sender, RoutedEventArgs e)
        {
            string connectionstring = null;
            connectionstring = myConnection.connectionString();
            MySqlConnection cnn2 = new MySqlConnection(connectionstring);
            cnn2.Open();
            MySqlCommand command2 = cnn2.CreateCommand();
            command2.Parameters.AddWithValue("@newbl", float.Parse(updatedBalanceField.Text));
            command2.Parameters.AddWithValue("@clientName", nameField.Text);
            command2.CommandText = "UPDATE SavedInvoices SET New_Balance=@newbl WHERE Client_Name=@clientName order by id desc limit 1";
            if (command2.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Balance was successfuly changed for "+nameField.Text +"!");

                PMViewModel PMviewModel = new PMViewModel();
                dg.ItemsSource = PMviewModel.Orders;
                dg.AllowFiltering = true;
            }
            else
            {
                MessageBox.Show("WARNING!!! [Error: Balance was NOT updated correctly.]");
            }

            cnn2.Close();
        }

        private void SfTextBoxExt_SelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            string connectionstring = null;
            string rr;
            connectionstring = myConnection.connectionString();
            MySqlConnection cnn = new MySqlConnection(connectionstring);
            cnn.Open();
            MySqlCommand command = cnn.CreateCommand();
            command.Parameters.AddWithValue("@id", LoginWindow.id);

            command.CommandText = "SELECT* FROM SavedInvoices where id in (SELECT max(id) FROM SavedInvoices GROUP BY Client_Name) && userid = @id order by id desc";
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    rr = reader.GetString("name");
                    if (rr == nameField.Text)
                    {
                        updatedBalanceField.Text = reader.GetFloat("New_Balance").ToString();
                    }
                }
            }           
            cnn.Close();
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

        private void PaymentsManagement_Click(object sender, RoutedEventArgs e)
        {
            Payments_Management pm = new Payments_Management();
            pm.Show();
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

    public class PaymentsDetails
    {
        string client_Name;
        float new_Balance;

        public string Client_Name
        {
            get { return client_Name; }
            set { client_Name = value; }
        }

        public float New_Balance
        {
            get { return new_Balance; }
            set { new_Balance = value; }
        }

        public PaymentsDetails( string client_Name, float new_Balance)
        {
            this.Client_Name = client_Name;
            this.New_Balance = new_Balance;
        }
    }

    public class PMViewModel
    {
        private List<PaymentsDetails> _orders;
        private List<Client> clients;
        public List<PaymentsDetails> Orders
        {
            get { return _orders; }
            set { _orders = value; }
        }

        public List<Client> Clients
        {
            get { return clients; }
            set { clients = value; }
        }

        public PMViewModel()
        {
            _orders = new List<PaymentsDetails>();
            clients = new List<Client>();
            this.GenerateOrders();
        }

        private void GenerateOrders()
        {
            string connectionstring = null;
            connectionstring = myConnection.connectionString();
            MySqlConnection cnn = new MySqlConnection(connectionstring);
            cnn.Open();
            MySqlCommand command = cnn.CreateCommand();
            command.Parameters.AddWithValue("@id", LoginWindow.id);
            command.CommandText = "SELECT* FROM SavedInvoices where id in (SELECT max(id) FROM SavedInvoices GROUP BY Client_Name) && userid = @id order by id desc";
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string name = reader.GetString("Client_Name");
                _orders.Add(new PaymentsDetails(name, reader.GetFloat("New_Balance")));
                clients.Add(new Client { Name = name });
            }
        }
    }
}
