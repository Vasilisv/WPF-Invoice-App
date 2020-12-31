using MySql.Data.MySqlClient;
using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Syncfusion.SfSkinManager;
using Magnum.Extensions;
using Syncfusion.UI.Xaml.Grid.Helpers;
using System.ComponentModel;
using System.Threading;
using System.Web.UI.WebControls;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Invoice_Management.xaml
    /// </summary>
    public partial class Invoice_Management : Window
    {
        List<int> ListOfSelectedItems = new List<int>() ;
        public Invoice_Management()
        {

           
            InitializeComponent();
       
            SfSkinManager.SetVisualStyle(dg, VisualStyles.MaterialLight);
            ViewModel viewModel = new ViewModel();
            dg.ItemsSource = viewModel.Orders;
            dg.AllowFiltering = true;
         
          
        }

        

        private void dg_SelectionChanged(object sender, GridSelectionChangedEventArgs e)
        {

            InvoiceDetails a = new InvoiceDetails(0, null, 0, 0, 0, 0, null, false);
               a= (InvoiceDetails) dg.CurrentItem;
            Console.WriteLine(a);
            if (a != null)
            {
                ListOfSelectedItems.Add(a.InvoiceId);
            }

        
            //List<GridCellInfo> selectedCells = this.dg.GetSelectedCells();

        }

        private void ispaid_Click(object sender, RoutedEventArgs e)
        {
            string connectionstring = null;
            
           
            foreach (int o in ListOfSelectedItems)
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

    public class InvoiceDetails
    {
        int invoiceId;
        string client_Name;
        float total_Payment;
        float total_Quantity;
        float previous_Balance;
        float new_Balance;
        string date;
        bool isPaid;

        public int InvoiceId
        {
            get { return invoiceId; }
            set { invoiceId = value; }
        }

        public string Client_Name
        {
            get { return client_Name; }
            set { client_Name = value; }
        }

        public float Total_Payment
        {
            get { return total_Payment; }
            set { total_Payment = value; }
        }

        public float Total_Quantity
        {
            get { return total_Quantity; }
            set { total_Quantity = value; }
        }

        public float Previous_Balance
        {
            get { return previous_Balance; }
            set { previous_Balance = value; }
        }

        public float New_Balance
        {
            get { return new_Balance; }
            set { new_Balance = value; }
        }

        public string Date
        {
            get { return date; }
            set { date = value; }
        }

       
        public bool IsPaid
        {
            get {return isPaid; }
            set { isPaid = value; }
        }
        public InvoiceDetails(int invoiceId, string client_Name, float total_Payment, float total_Quantity, float previous_Balance, float new_Balance, string date, bool isPaid)
        {
            this.InvoiceId = invoiceId;
            this.Client_Name = client_Name;
            this.Total_Payment = total_Payment;
            this.Total_Quantity = total_Quantity;
            this.Previous_Balance = previous_Balance;
            this.New_Balance = new_Balance;
            this.Date = date;
            this.IsPaid = isPaid;
        }
    }

    public class ViewModel
    {
        private ObservableCollection<InvoiceDetails> _orders;
        public ObservableCollection<InvoiceDetails> Orders
        {
            get { return _orders; }
            set { _orders = value; }
        }

        public ViewModel()
        {
            _orders = new ObservableCollection<InvoiceDetails>();
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
            command.CommandText = "SELECT InvoiceId,Client_Name,Total_Payment,Total_Quantity,Previous_Balance,New_Balance,Date,IsPaid FROM SavedInvoices WHERE userid=@id";
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                _orders.Add(new InvoiceDetails(reader.GetInt32("InvoiceId"), reader.GetString("Client_Name"), reader.GetFloat("Total_Payment"), reader.GetFloat("Total_Quantity"), reader.GetFloat("Previous_Balance"), reader.GetFloat("New_Balance"), reader.GetString("Date"), reader.GetBoolean("IsPaid")));
            }
           
         
        }

    

    }

}
