
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

using System.Windows;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        public static List<string> data = new List<string>();

        public AddProduct()
        {

            InitializeComponent();


            string connectionstring = null;
            connectionstring = myConnection.connectionString();
            MySqlConnection cnn = new MySqlConnection(connectionstring);
            cnn.Open();
            MySqlCommand command = cnn.CreateCommand();
            command.Parameters.AddWithValue("@id", LoginWindow.id);
            command.CommandText = "SELECT name,dimX,dimY,MM,price FROM products WHERE userid=@id";
            MySqlDataReader reader = command.ExecuteReader();
            datagrid.ItemsSource = reader;


        }



        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string connectionstring = null;
            connectionstring = myConnection.connectionString();
            MySqlConnection cnn = new MySqlConnection(connectionstring);
            cnn.Open();
            MySqlCommand command = cnn.CreateCommand();
            command.Parameters.AddWithValue("@id", LoginWindow.id);
            command.CommandText = "SELECT name,MM,price FROM products WHERE userid=@id";
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                cnn.Close();
                cnn.Open();
                MySqlCommand command2 = cnn.CreateCommand();
                command2.Parameters.AddWithValue("@userid", LoginWindow.id);
                command2.Parameters.AddWithValue("@name", nameField.Text);
                command2.Parameters.AddWithValue("@dimX", float.Parse(dimXField.Text));
                command2.Parameters.AddWithValue("@dimY", float.Parse(dimYField.Text));
                command2.Parameters.AddWithValue("@MM", MMField.Text);
                command2.Parameters.AddWithValue("@price", float.Parse(priceField.Text));
                command2.CommandText = "INSERT INTO products (userid, name, dimX, dimY, MM, price) VALUES (@userid, @name, @dimX, @dimY, @MM, @price)";
                if (command2.ExecuteNonQuery() > 0)
                {

                    MessageBox.Show("Product was added successfuly.");
                    this.Hide();
                    AddProduct ap = new AddProduct();
                    ap.Show();
                   
                }
                else
                    MessageBox.Show("Record was not added!");
            }


            cnn.Close();
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

            command.CommandText = "SELECT * FROM products WHERE userid=@id";
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    rr = String.Join("", reader.GetString("name"), reader.GetFloat("dimX"), "x", reader.GetFloat("dimY"));
                    if (rr == nameField.Text)
                    {
                        nameField.Text = reader.GetString("name");
                        dimXField.Text = reader.GetFloat("dimX").ToString();
                        dimYField.Text = reader.GetFloat("dimY").ToString();
                        priceField.Text = reader.GetFloat("price").ToString();
                        MMField.Text = reader.GetString("MM");
                    }
                }

            }
            cnn.Close();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string connectionstring = null;
            string rr = null;
            Boolean found = false;
            string OLDdimXField = null;
            string OLDdimYField = null;
            string OLDpriceField = null;
            connectionstring = myConnection.connectionString();
            MySqlConnection cnn = new MySqlConnection(connectionstring);
            cnn.Open();
            MySqlCommand command = cnn.CreateCommand();
            command.Parameters.AddWithValue("@id", LoginWindow.id);

            command.CommandText = "SELECT * FROM products WHERE userid=@id";
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    rr = reader.GetString("name");
                    if (rr == nameField.Text)
                    {
                    
                        OLDdimXField = reader.GetFloat("dimX").ToString();
                        OLDdimYField = reader.GetFloat("dimY").ToString();
                        OLDpriceField = reader.GetFloat("price").ToString();
                       
                        float a = float.Parse(OLDdimXField) * float.Parse(OLDdimYField);
                       
                        float b = float.Parse(OLDpriceField) / a;
                      
                        float new_price = b * float.Parse(dimXField.Text) * float.Parse(dimYField.Text);
                       
                        priceField.Text = new_price.ToString("0.00");
                        found = true;
                    }
                   
                }
                if (found == false)
                {
                    MessageBox.Show("This item is not in the database.Auto-Pricing can't work!");

                    reader.Close();
                    command.Cancel();
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
        public class Employee
        {
            public string Name { get; set; }
      
        }

        public class EmployeeViewModel
        {
            private List<Employee> employees;
            public List<Employee> Employees
            {
                get { return employees; }

                set { employees = value; }
            }
            public EmployeeViewModel()
            {
                string name;
                float dimX, dimY;
                string connectionstring = null;
                connectionstring = myConnection.connectionString();
                MySqlConnection cnn = new MySqlConnection(connectionstring);
                cnn.Open();
                MySqlCommand command = cnn.CreateCommand();
                command.Parameters.AddWithValue("@id", LoginWindow.id);
                command.CommandText = "SELECT name,dimX,dimY FROM products WHERE userid=@id";
                MySqlDataReader reader = command.ExecuteReader();
                Employees = new List<Employee>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        name = reader.GetString("name");
                        dimX = reader.GetFloat("dimX");
                        dimY = reader.GetFloat("dimY");

                        Employees.Add(new Employee { Name = String.Join("", name, dimX, "x", dimY) });
                    }
                }


            }
     
       
    }

    }
