
using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media.Imaging;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for InvoiceInfo.xaml
    /// </summary>
    public partial class InvoiceInfo : Window
    {
        public static string filename;
        public static BitmapImage imagesource;
        public static byte[] data;
        public InvoiceInfo()
        {
            InitializeComponent();
            string connectionstring = null;
            connectionstring = myConnection.connectionString();
            MySqlConnection cnn = new MySqlConnection(connectionstring);

            cnn.Open();
            MySqlCommand command = cnn.CreateCommand();
            command.Parameters.AddWithValue("@userid", LoginWindow.id);
            command.CommandText = "SELECT * FROM info WHERE userid=@userid";
            MySqlDataReader reader2 = command.ExecuteReader();

            if (reader2.HasRows == true) //if info exists for the current id, get the current data
            {
                GetLogo();
                GetDescrDet();
            }
            cnn.Close();
        }
        public void GetLogo()
        {
            string connectionstring = null;
            connectionstring = myConnection.connectionString();
            MySqlConnection cnn = new MySqlConnection(connectionstring);

            cnn.Open();
            MySqlCommand command = cnn.CreateCommand();
            command.Parameters.AddWithValue("@userid", LoginWindow.id);
            command.CommandText = "SELECT logo,tax FROM info WHERE userid=@userid";
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                data = (byte[])reader["logo"]; // 0 is okay if you only selecting one column

                using (MemoryStream ms = new MemoryStream(data))
                {

                    var imageSource = new BitmapImage();
                    imageSource.BeginInit();
                    imageSource.StreamSource = ms;
                    imageSource.CacheOption = BitmapCacheOption.OnLoad;
                    imageSource.EndInit();
                    //System.Drawing.ImageConverter converter = new System.Drawing.ImageConverter();
                    //Image img = Image.FromStream(data);

                    // Assign the Source property of your image
                   
                    logo.Source = imageSource;
                    imagesource = imageSource;
                }
                taxtext.Text = reader.GetFloat("tax").ToString();

            }


        }

        public void GetDescrDet()
        {
            string connectionstring = null;
            connectionstring = myConnection.connectionString();
            MySqlConnection cnn = new MySqlConnection(connectionstring);

            cnn.Open();
            MySqlCommand command = cnn.CreateCommand();
            command.Parameters.AddWithValue("@userid", LoginWindow.id);
            command.CommandText = "SELECT * FROM info WHERE userid=@userid";
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                TextRange tr = new TextRange(descrField.Document.ContentStart, descrField.Document.ContentEnd);
                // convert string to stream
                var contents = reader.GetString("description");
                byte[] byteArray = Encoding.UTF8.GetBytes(contents);
                MemoryStream stream = new MemoryStream(byteArray);
                StreamReader readerstr = new StreamReader(stream);
                string text = readerstr.ReadToEnd();
                tr.Load(stream, DataFormats.Rtf);
                detailsField.Text = reader.GetString("details");
                bank1Field.Text = reader.GetString("bank1");
                bank2Field.Text = reader.GetString("bank2");
                bank3Field.Text = reader.GetString("bank3");
                bank4Field.Text = reader.GetString("bank4");
                banknum1Field.Text = reader.GetString("banknum1");
                banknum2Field.Text = reader.GetString("banknum2");
                banknum3Field.Text = reader.GetString("banknum3");
                banknum4Field.Text = reader.GetString("banknum4");
                iban1Field.Text = reader.GetString("iban1");
                iban2Field.Text = reader.GetString("iban2");
                iban3Field.Text = reader.GetString("iban3");
                iban4Field.Text = reader.GetString("iban4");
            }


        }


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".png";
            dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                 filename = dlg.FileName;
                FileNameTextBox.Text = filename;
                
            }

        }

        private void detailsButton_Click(object sender, RoutedEventArgs e){
            byte[] file = null;
            if (!string.IsNullOrEmpty(filename))
            {
                using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = new BinaryReader(stream))
                    {
                        file = reader.ReadBytes((int)stream.Length);
                    }
                }
            }
            

            string connectionstring = null;
            connectionstring = myConnection.connectionString();
            MySqlConnection cnn = new MySqlConnection(connectionstring);

            cnn.Open();
            MySqlCommand command = cnn.CreateCommand();
            command.Parameters.AddWithValue("@userid", LoginWindow.id);
            command.CommandText = "SELECT * FROM info WHERE userid=@userid";
            MySqlDataReader reader2 = command.ExecuteReader();
        
            if (reader2.HasRows == true) //if info exists for the current id, execute update
            {
                cnn.Close();
                cnn.Open();
                command = cnn.CreateCommand();
                command.Parameters.AddWithValue("@userid", LoginWindow.id);
               
                if (file == null) { file = data; }
                command.Parameters.Add("@File", MySqlDbType.VarBinary, file.Length).Value = file;
                command.Parameters.AddWithValue("@logo", file);
                TextRange tr = new TextRange(descrField.Document.ContentStart, descrField.Document.ContentEnd);
                MemoryStream ms = new MemoryStream();
                tr.Save(ms, DataFormats.Rtf);
                command.Parameters.AddWithValue("@description", ASCIIEncoding.Default.GetString(ms.ToArray()));
                command.Parameters.AddWithValue("@details", detailsField.Text);
                command.Parameters.AddWithValue("@tax", taxtext.Text);
                command.Parameters.AddWithValue("@bank1", bank1Field.Text);
                command.Parameters.AddWithValue("@bank2", bank2Field.Text);
                command.Parameters.AddWithValue("@bank3", bank3Field.Text);
                command.Parameters.AddWithValue("@bank4", bank4Field.Text);
                command.Parameters.AddWithValue("@banknum1", banknum1Field.Text);
                command.Parameters.AddWithValue("@banknum2", banknum2Field.Text);
                command.Parameters.AddWithValue("@banknum3", banknum3Field.Text);
                command.Parameters.AddWithValue("@banknum4", banknum4Field.Text);
                command.Parameters.AddWithValue("@iban1", iban1Field.Text);
                command.Parameters.AddWithValue("@iban2", iban2Field.Text);
                command.Parameters.AddWithValue("@iban3", iban3Field.Text);
                command.Parameters.AddWithValue("@iban4", iban4Field.Text);
                command.CommandText = "UPDATE info SET userid=@userid, logo=@logo, description=@description, details=@details, tax=@tax, bank1=@bank1, bank2=@bank2, " +
                    "bank3=@bank3, bank4=@bank4, banknum1=@banknum1, banknum2=@banknum2, banknum3=@banknum3, banknum4=@banknum4, iban1=@iban1, iban2=@iban2, iban3=@iban3, iban4=@iban4";

                if (command.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Invoice Informations were successfuly updated.");
                    InvoiceInfo ii = new InvoiceInfo();
                    ii.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Record was not added!");
                }
                cnn.Close();



            }
            else //if there is no info already, execute insert
            {
                connectionstring = myConnection.connectionString();
                cnn = new MySqlConnection(connectionstring);
                cnn.Open();
                command = cnn.CreateCommand();
                command.Parameters.AddWithValue("@userid", LoginWindow.id);
                command.Parameters.Add("@File", MySqlDbType.VarBinary, file.Length).Value = file;
                command.Parameters.AddWithValue("@logo", file);
                string richText = new TextRange(descrField.Document.ContentStart, descrField.Document.ContentEnd).Text;
                command.Parameters.AddWithValue("@description", richText);
                command.Parameters.AddWithValue("@details", detailsField.Text);
                command.Parameters.AddWithValue("@tax", taxtext.Text);
                command.Parameters.AddWithValue("@bank1", bank1Field.Text);
                command.Parameters.AddWithValue("@bank2", bank2Field.Text);
                command.Parameters.AddWithValue("@bank3", bank3Field.Text);
                command.Parameters.AddWithValue("@bank4", bank4Field.Text);
                command.Parameters.AddWithValue("@banknum1", banknum1Field.Text);
                command.Parameters.AddWithValue("@banknum2", banknum2Field.Text);
                command.Parameters.AddWithValue("@banknum3", banknum3Field.Text);
                command.Parameters.AddWithValue("@banknum4", banknum4Field.Text);
                command.Parameters.AddWithValue("@iban1", iban1Field.Text);
                command.Parameters.AddWithValue("@iban2", iban2Field.Text);
                command.Parameters.AddWithValue("@iban3", iban3Field.Text);
                command.Parameters.AddWithValue("@iban4", iban4Field.Text);
                command.CommandText = "INSERT INTO info (userid, logo, description, details, tax, bank1, bank2, bank3, bank4, banknum1, banknum2, banknum3, banknum4, iban1, iban2, iban3, iban4) VALUES (@userid, @logo, @description, @details, @tax, @bank1, @bank2, @bank3, @bank4, @banknum1, @banknum2, @banknum3, @banknum4, @iban1, @iban2, @iban3, @iban4)";

                if (command.ExecuteNonQuery() >= 0)
                {
                    MessageBox.Show("Invoice Informations were successfuly saved.");
                    InvoiceInfo ii = new InvoiceInfo();
                    ii.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Record was not added!");
                }
                cnn.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddClient ac = new AddClient();
            ac.Show();
            this.Hide();
        }

      
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            InvoiceInfo ii = new InvoiceInfo();
            ii.Show();
            this.Hide();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            AddProduct ap = new AddProduct();
            ap.Show();
            this.Hide();
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

