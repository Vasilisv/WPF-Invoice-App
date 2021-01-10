
using MySql.Data.MySqlClient;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;
using TextBox = System.Windows.Controls.TextBox;
using Window = System.Windows.Window;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for InvoiceWindow.xaml
    /// </summary>
    public partial class InvoiceDNWindow : Window
    {
        public static float tax, sumpricenotax;
        public float sum;

        string newbal_copy;
        public InvoiceDNWindow()
        {
            InitializeComponent();
            GetLogo();
            GetDescrDet();
            GetInvoiceDNID();

            myDatePicker.SelectedDate = DateTime.Today;
            DataContext = new MainViewModel();
            timenow.Content = DateTime.Now.ToString("HH:mm:ss");
            MAIN.IsReadOnly = true;
            price1Field.IsReadOnly = true;
            descrField.IsReadOnly = true;
            dateborder.Visibility = Visibility.Hidden;
        }

        public float? TryParseFloat(string source)
        {
            float result;
            return float.TryParse(source, out result) ? result : (float?)null;
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
                byte[] data = (byte[])reader["logo"]; // 0 is okay if you only selecting one column

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
                }
                tax = reader.GetFloat("tax");
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dateborder.Visibility = Visibility.Visible;
            timenow.Content = DateTime.Now.ToString("HH:mm:ss");
            paymenttype.BorderThickness = new Thickness(0, 0, 0, 0);
            Vehicle_Number.BorderThickness = new Thickness(0, 0, 0, 0);
            invoiceid.BorderThickness = new Thickness(0, 0, 0, 0);
            nameField.BorderThickness = new Thickness(0, 0, 0, 0);
            product1.BorderThickness = new Thickness(0, 0, 0, 0);
            product2.BorderThickness = new Thickness(0, 0, 0, 0);
            product3.BorderThickness = new Thickness(0, 0, 0, 0);
            product4.BorderThickness = new Thickness(0, 0, 0, 0);
            product5.BorderThickness = new Thickness(0, 0, 0, 0);
            product6.BorderThickness = new Thickness(0, 0, 0, 0);
            product7.BorderThickness = new Thickness(0, 0, 0, 0);
            product8.BorderThickness = new Thickness(0, 0, 0, 0);
            product9.BorderThickness = new Thickness(0, 0, 0, 0);
            product10.BorderThickness = new Thickness(0, 0, 0, 0);
            product11.BorderThickness = new Thickness(0, 0, 0, 0);
            product12.BorderThickness = new Thickness(0, 0, 0, 0);
            product13.BorderThickness = new Thickness(0, 0, 0, 0);
            product14.BorderThickness = new Thickness(0, 0, 0, 0);
            product15.BorderThickness = new Thickness(0, 0, 0, 0);
            product16.BorderThickness = new Thickness(0, 0, 0, 0);
            product17.BorderThickness = new Thickness(0, 0, 0, 0);
            product18.BorderThickness = new Thickness(0, 0, 0, 0);
            MM1Field.BorderThickness = new Thickness(0, 0, 0, 0);
            MM2Field.BorderThickness = new Thickness(0, 0, 0, 0);
            MM3Field.BorderThickness = new Thickness(0, 0, 0, 0);
            MM4Field.BorderThickness = new Thickness(0, 0, 0, 0);
            MM5Field.BorderThickness = new Thickness(0, 0, 0, 0);
            MM6Field.BorderThickness = new Thickness(0, 0, 0, 0);
            MM7Field.BorderThickness = new Thickness(0, 0, 0, 0);
            MM8Field.BorderThickness = new Thickness(0, 0, 0, 0);
            MM9Field.BorderThickness = new Thickness(0, 0, 0, 0);
            MM10Field.BorderThickness = new Thickness(0, 0, 0, 0);
            MM11Field.BorderThickness = new Thickness(0, 0, 0, 0);
            MM12Field.BorderThickness = new Thickness(0, 0, 0, 0);
            MM13Field.BorderThickness = new Thickness(0, 0, 0, 0);
            MM14Field.BorderThickness = new Thickness(0, 0, 0, 0);
            MM15Field.BorderThickness = new Thickness(0, 0, 0, 0);
            MM16Field.BorderThickness = new Thickness(0, 0, 0, 0);
            MM17Field.BorderThickness = new Thickness(0, 0, 0, 0);
            MM18Field.BorderThickness = new Thickness(0, 0, 0, 0);
            Qu1Field.BorderThickness = new Thickness(0, 0, 0, 0);
            Qu2Field.BorderThickness = new Thickness(0, 0, 0, 0);
            Qu3Field.BorderThickness = new Thickness(0, 0, 0, 0);
            Qu4Field.BorderThickness = new Thickness(0, 0, 0, 0);
            Qu5Field.BorderThickness = new Thickness(0, 0, 0, 0);
            Qu6Field.BorderThickness = new Thickness(0, 0, 0, 0);
            Qu7Field.BorderThickness = new Thickness(0, 0, 0, 0);
            Qu8Field.BorderThickness = new Thickness(0, 0, 0, 0);
            Qu9Field.BorderThickness = new Thickness(0, 0, 0, 0);
            Qu10Field.BorderThickness = new Thickness(0, 0, 0, 0);
            Qu11Field.BorderThickness = new Thickness(0, 0, 0, 0);
            Qu12Field.BorderThickness = new Thickness(0, 0, 0, 0);
            Qu13Field.BorderThickness = new Thickness(0, 0, 0, 0);
            Qu14Field.BorderThickness = new Thickness(0, 0, 0, 0);
            Qu15Field.BorderThickness = new Thickness(0, 0, 0, 0);
            Qu16Field.BorderThickness = new Thickness(0, 0, 0, 0);
            Qu17Field.BorderThickness = new Thickness(0, 0, 0, 0);
            Qu18Field.BorderThickness = new Thickness(0, 0, 0, 0);
            price1Field.BorderThickness = new Thickness(0, 0, 0, 0);
            price2Field.BorderThickness = new Thickness(0, 0, 0, 0);
            price3Field.BorderThickness = new Thickness(0, 0, 0, 0);
            price4Field.BorderThickness = new Thickness(0, 0, 0, 0);
            price5Field.BorderThickness = new Thickness(0, 0, 0, 0);
            price6Field.BorderThickness = new Thickness(0, 0, 0, 0);
            price7Field.BorderThickness = new Thickness(0, 0, 0, 0);
            price8Field.BorderThickness = new Thickness(0, 0, 0, 0);
            price9Field.BorderThickness = new Thickness(0, 0, 0, 0);
            price10Field.BorderThickness = new Thickness(0, 0, 0, 0);
            price11Field.BorderThickness = new Thickness(0, 0, 0, 0);
            price12Field.BorderThickness = new Thickness(0, 0, 0, 0);
            price13Field.BorderThickness = new Thickness(0, 0, 0, 0);
            price14Field.BorderThickness = new Thickness(0, 0, 0, 0);
            price15Field.BorderThickness = new Thickness(0, 0, 0, 0);
            price16Field.BorderThickness = new Thickness(0, 0, 0, 0);
            price17Field.BorderThickness = new Thickness(0, 0, 0, 0);
            price18Field.BorderThickness = new Thickness(0, 0, 0, 0);
            fprice1Field.BorderThickness = new Thickness(0, 0, 0, 0);
            fprice2Field.BorderThickness = new Thickness(0, 0, 0, 0);
            fprice3Field.BorderThickness = new Thickness(0, 0, 0, 0);
            fprice4Field.BorderThickness = new Thickness(0, 0, 0, 0);
            fprice5Field.BorderThickness = new Thickness(0, 0, 0, 0);
            fprice6Field.BorderThickness = new Thickness(0, 0, 0, 0);
            fprice7Field.BorderThickness = new Thickness(0, 0, 0, 0);
            fprice8Field.BorderThickness = new Thickness(0, 0, 0, 0);
            fprice9Field.BorderThickness = new Thickness(0, 0, 0, 0);
            fprice10Field.BorderThickness = new Thickness(0, 0, 0, 0);
            fprice11Field.BorderThickness = new Thickness(0, 0, 0, 0);
            fprice12Field.BorderThickness = new Thickness(0, 0, 0, 0);
            fprice13Field.BorderThickness = new Thickness(0, 0, 0, 0);
            fprice14Field.BorderThickness = new Thickness(0, 0, 0, 0);
            fprice15Field.BorderThickness = new Thickness(0, 0, 0, 0);
            fprice16Field.BorderThickness = new Thickness(0, 0, 0, 0);
            fprice17Field.BorderThickness = new Thickness(0, 0, 0, 0);
            fprice18Field.BorderThickness = new Thickness(0, 0, 0, 0);
            fpa1Field.BorderThickness = new Thickness(0, 0, 0, 0);
            fpa2Field.BorderThickness = new Thickness(0, 0, 0, 0);
            fpa3Field.BorderThickness = new Thickness(0, 0, 0, 0);
            fpa4Field.BorderThickness = new Thickness(0, 0, 0, 0);
            fpa5Field.BorderThickness = new Thickness(0, 0, 0, 0);
            fpa6Field.BorderThickness = new Thickness(0, 0, 0, 0);
            fpa7Field.BorderThickness = new Thickness(0, 0, 0, 0);
            fpa8Field.BorderThickness = new Thickness(0, 0, 0, 0);
            fpa9Field.BorderThickness = new Thickness(0, 0, 0, 0);
            fpa10Field.BorderThickness = new Thickness(0, 0, 0, 0);
            fpa11Field.BorderThickness = new Thickness(0, 0, 0, 0);
            fpa12Field.BorderThickness = new Thickness(0, 0, 0, 0);
            fpa13Field.BorderThickness = new Thickness(0, 0, 0, 0);
            fpa14Field.BorderThickness = new Thickness(0, 0, 0, 0);
            fpa15Field.BorderThickness = new Thickness(0, 0, 0, 0);
            fpa16Field.BorderThickness = new Thickness(0, 0, 0, 0);
            fpa17Field.BorderThickness = new Thickness(0, 0, 0, 0);
            fpa18Field.BorderThickness = new Thickness(0, 0, 0, 0);
            bank1Field.BorderThickness = new Thickness(0, 0, 0, 0);
            bank2Field.BorderThickness = new Thickness(0, 0, 0, 0);
            bank3Field.BorderThickness = new Thickness(0, 0, 0, 0);
            bank4Field.BorderThickness = new Thickness(0, 0, 0, 0);
            banknum1Field.BorderThickness = new Thickness(0, 0, 0, 0);
            banknum2Field.BorderThickness = new Thickness(0, 0, 0, 0);
            banknum3Field.BorderThickness = new Thickness(0, 0, 0, 0);
            banknum4Field.BorderThickness = new Thickness(0, 0, 0, 0);
            iban1Field.BorderThickness = new Thickness(0, 0, 0, 0);
            iban2Field.BorderThickness = new Thickness(0, 0, 0, 0);
            iban3Field.BorderThickness = new Thickness(0, 0, 0, 0);
            iban4Field.BorderThickness = new Thickness(0, 0, 0, 0);
            sumField.BorderThickness = new Thickness(0, 0, 0, 0);
            sumFINAL.BorderThickness = new Thickness(0, 0, 0, 0);
            sumpriceNOTAX.BorderThickness = new Thickness(0, 0, 0, 0);
            sumTAX.BorderThickness = new Thickness(0, 0, 0, 0);
            sumFINAL.BorderThickness = new Thickness(0, 0, 0, 0);
            paySUM.BorderThickness = new Thickness(0, 0, 0, 0);
            taxaddsField.BorderThickness = new Thickness(0, 0, 0, 0);
            oldBalanceField.BorderThickness = new Thickness(0, 0, 0, 0);
            newBalanceField.BorderThickness = new Thickness(0, 0, 0, 0);
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "PDF Files|*.pdf";
            dlg.FilterIndex = 0;
            dlg.FileName = "ΤΠΔΑ-" + invoiceid.Text;
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                string temp = Path.GetTempPath();
                // string savePath = Path.GetDirectoryName(dlg.FileName);
                WriteToPDF(print, temp + @"\temp.png", dlg.FileName);
                SaveInvoiceToDatabase();
            }
        }


        public void WriteToPDF(UIElement element, string filename, string selectedPath)
        {

            var rect = new Rect(element.RenderSize);
            var visual = new DrawingVisual();

            using (var dc = visual.RenderOpen())
            {
                dc.DrawRectangle(new VisualBrush(element), null, rect);
            }

            var bitmap = new RenderTargetBitmap(
                (int)rect.Width, (int)rect.Height, 96, 96, PixelFormats.Default);
            bitmap.Render(visual);

            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));

            using (var file = File.OpenWrite(filename))
            {
                encoder.Save(file);
                file.Close();
            }

            PdfDocument doc = new PdfDocument();

            PdfPage oPage = new PdfPage();

            doc.Pages.Add(oPage);
            //oPage.Rotate = 90;
            XGraphics xgr = XGraphics.FromPdfPage(oPage);
            XImage img = XImage.FromFile(@filename);

            xgr.DrawImage(img, -6, -15, 980, 780);

            doc.Save(@selectedPath);
            doc.Close();


        }

        public void GetInvoiceDNID()
        {
            string connectionstring = null;
            connectionstring = myConnection.connectionString();
            MySqlConnection cnn = new MySqlConnection(connectionstring);
            cnn.Open();
            MySqlCommand command = cnn.CreateCommand();

            command.Parameters.AddWithValue("@userid", LoginWindow.id);
            command.CommandText = "SELECT * FROM SavedDNInvoices WHERE userid=@userid ORDER BY id DESC LIMIT 1 ";
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int x = reader.GetInt32("DNInvoiceId") + 1;
                invoiceid.Text = x.ToString();
            }

            if (string.IsNullOrEmpty(invoiceid.Text))
            {
                MessageBox.Show("Please enter a starting Delivery Note-Invoice ID!");
            }
        }
        public void SaveInvoiceToDatabase()
        {
            string connectionstring = null;
            connectionstring = myConnection.connectionString();
            MySqlConnection cnn = new MySqlConnection(connectionstring);
            cnn.Open();
            MySqlCommand command = cnn.CreateCommand();

            command.Parameters.AddWithValue("@userid", LoginWindow.id);
            command.Parameters.AddWithValue("@invoiceid", invoiceid.Text);
            command.Parameters.AddWithValue("@clientname", nameField.Text);
            string[] tokens = paySUM.Text.Split(' ');
            command.Parameters.AddWithValue("@totalpay", float.Parse(tokens[0]));
            command.Parameters.AddWithValue("@totalqu", float.Parse(sumField.Text));
            command.Parameters.AddWithValue("@prevbal", float.Parse(oldBalanceField.Text));
            command.Parameters.AddWithValue("@newbal", float.Parse(newBalanceField.Text));
            command.Parameters.AddWithValue("@date", string.Join("", DateTime.Now.ToString("dd/MM/yyyy HH:mm")));
            bool cb = (bool)cbispaid.IsChecked;
            if (cb == true) command.Parameters.AddWithValue("@ispaid", 1);
            else command.Parameters.AddWithValue("@ispaid", 0);

            command.CommandText = "INSERT INTO SavedInvoices (userid,InvoiceId,Client_Name,Total_Payment,Total_Quantity,Previous_Balance,New_Balance,Date,IsPaid) VALUES (@userid, @invoiceid, @clientname, @totalpay, @totalqu, @prevbal, @newbal, @date, @ispaid)";
            if (command.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Invoice was successfuly saved!");
                this.Hide();
                InvoiceWindow iw = new InvoiceWindow();
                iw.Show();

            }
            else
                MessageBox.Show("WARNING!!! [Error: Invoive wasn't saved in database.]");
            cnn.Close();
        }

        public void SelectedProductChanged(TextBox productField, TextBox priceField, TextBox MMField, TextBox QuField)
        {
            QuField.Text = "";
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
                    if (rr == productField.Text)
                    {
                        priceField.Text = reader.GetFloat("price").ToString("0.00") + " €";
                        MMField.Text = reader.GetString("MM");
                    }
                }

            }
            if (string.IsNullOrEmpty(productField.Text))
            {
                priceField.Text = "";
                MMField.Text = "";
                QuField.Text = "";
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

            command.CommandText = "SELECT * FROM clients WHERE userid=@id";
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    rr = reader.GetString("name");
                    if (rr == nameField.Text)
                    {
                        afmField.Text = reader.GetString("afm");
                        doyField.Text = reader.GetString("doy");
                        workField.Text = reader.GetString("work");
                        addressField.Text = reader.GetString("address");
                        townField.Text = reader.GetString("town");
                    }
                }
            }
            cnn.Close();

            //Get the old and new balance of selected client
            float prev_bal = 0;
            cnn.Open();
            MySqlCommand command2 = cnn.CreateCommand();
            command2.Parameters.AddWithValue("@id", LoginWindow.id);
            command2.Parameters.AddWithValue("@name", nameField.Text);
            command2.Parameters.AddWithValue("@value", 0);
            command2.CommandText = "SELECT New_Balance FROM SavedInvoices WHERE userid=@id AND Client_Name=@name AND IsPaid=@value ORDER BY id DESC LIMIT 1";
            MySqlDataReader reader2 = command2.ExecuteReader();
            while (reader2.Read())
            {
                prev_bal = prev_bal + reader2.GetFloat("New_Balance");
            }
            oldBalanceField.Text = prev_bal.ToString("0.00");
            newBalanceField.Text = prev_bal.ToString("0.00");
            //CASE: items have already been added to invoice before selecting client
            if (!String.IsNullOrEmpty(paySUM.Text))
            {
                string[] tokens = paySUM.Text.Split(' ');
                float nb = prev_bal + float.Parse(tokens[0]);
                newBalanceField.Text = nb.ToString("0.00");
            }

            cnn.Close();

            //CASE: There is no selected client
            if (string.IsNullOrEmpty(nameField.Text))
            {
                afmField.Text = "";
                doyField.Text = "";
                workField.Text = "";
                addressField.Text = "";
                townField.Text = "";
            }
        }

        private void productItem1_SelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SelectedProductChanged(product1, price1Field, MM1Field, Qu1Field);
        }
        private void productItem2_SelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SelectedProductChanged(product2, price2Field, MM2Field, Qu2Field);
        }
        private void productItem3_SelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SelectedProductChanged(product3, price3Field, MM3Field, Qu3Field);
        }
        private void productItem4_SelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SelectedProductChanged(product4, price4Field, MM4Field, Qu4Field);
        }
        private void productItem5_SelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SelectedProductChanged(product5, price5Field, MM5Field, Qu5Field);
        }
        private void productItem6_SelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SelectedProductChanged(product6, price6Field, MM6Field, Qu6Field);
        }
        private void productItem7_SelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SelectedProductChanged(product7, price7Field, MM7Field, Qu7Field);
        }
        private void productItem8_SelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SelectedProductChanged(product8, price8Field, MM8Field, Qu8Field);
        }
        private void productItem9_SelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SelectedProductChanged(product9, price9Field, MM9Field, Qu9Field);
        }
        private void productItem10_SelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SelectedProductChanged(product10, price10Field, MM10Field, Qu10Field);
        }
        private void productItem11_SelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SelectedProductChanged(product11, price11Field, MM11Field, Qu11Field);
        }
        private void productItem12_SelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SelectedProductChanged(product12, price12Field, MM12Field, Qu12Field);
        }
        private void productItem13_SelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SelectedProductChanged(product13, price13Field, MM13Field, Qu13Field);
        }
        private void productItem14_SelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SelectedProductChanged(product14, price14Field, MM14Field, Qu14Field);
        }
        private void productItem15_SelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SelectedProductChanged(product15, price15Field, MM15Field, Qu15Field);
        }
        private void productItem16_SelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SelectedProductChanged(product16, price16Field, MM16Field, Qu16Field);
        }
        private void productItem17_SelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SelectedProductChanged(product17, price17Field, MM17Field, Qu17Field);
        }
        private void productItem18_SelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SelectedProductChanged(product18, price18Field, MM18Field, Qu18Field);
        }

        public void QuantityItemChanged(TextBox priceField, TextBox QuField, TextBox fpriceField, TextBox fpaField)
        {
            float i1 = 0, i2 = 0, i3 = 0, i4 = 0, i5 = 0, i6 = 0, i7 = 0, i8 = 0, i9 = 0, i10 = 0, i11 = 0, i12 = 0, i13 = 0, i14 = 0, i15 = 0, i16 = 0, i17 = 0, i18 = 0;
            if (TryParseFloat(Qu1Field.Text) != null) i1 = (float)(string.IsNullOrEmpty(Qu1Field.Text) ? 0 : TryParseFloat(Qu1Field.Text)); else { i1 = 0; }
            if (TryParseFloat(Qu2Field.Text) != null) i2 = (float)(string.IsNullOrEmpty(Qu2Field.Text) ? 0 : TryParseFloat(Qu2Field.Text)); else { i2 = 0; }
            if (TryParseFloat(Qu3Field.Text) != null) i3 = (float)(string.IsNullOrEmpty(Qu3Field.Text) ? 0 : TryParseFloat(Qu3Field.Text)); else { i3 = 0; }
            if (TryParseFloat(Qu4Field.Text) != null) i4 = (float)(string.IsNullOrEmpty(Qu4Field.Text) ? 0 : TryParseFloat(Qu4Field.Text)); else { i4 = 0; }
            if (TryParseFloat(Qu5Field.Text) != null) i5 = (float)(string.IsNullOrEmpty(Qu5Field.Text) ? 0 : TryParseFloat(Qu5Field.Text)); else { i5 = 0; }
            if (TryParseFloat(Qu6Field.Text) != null) i6 = (float)(string.IsNullOrEmpty(Qu6Field.Text) ? 0 : TryParseFloat(Qu6Field.Text)); else { i6 = 0; }
            if (TryParseFloat(Qu7Field.Text) != null) i7 = (float)(string.IsNullOrEmpty(Qu7Field.Text) ? 0 : TryParseFloat(Qu7Field.Text)); else { i7 = 0; }
            if (TryParseFloat(Qu8Field.Text) != null) i8 = (float)(string.IsNullOrEmpty(Qu8Field.Text) ? 0 : TryParseFloat(Qu8Field.Text)); else { i8 = 0; }
            if (TryParseFloat(Qu9Field.Text) != null) i9 = (float)(string.IsNullOrEmpty(Qu9Field.Text) ? 0 : TryParseFloat(Qu9Field.Text)); else { i9 = 0; }
            if (TryParseFloat(Qu10Field.Text) != null) i10 = (float)(string.IsNullOrEmpty(Qu10Field.Text) ? 0 : TryParseFloat(Qu10Field.Text)); else { i10 = 0; }
            if (TryParseFloat(Qu11Field.Text) != null) i11 = (float)(string.IsNullOrEmpty(Qu11Field.Text) ? 0 : TryParseFloat(Qu11Field.Text)); else { i11 = 0; }
            if (TryParseFloat(Qu12Field.Text) != null) i12 = (float)(string.IsNullOrEmpty(Qu12Field.Text) ? 0 : TryParseFloat(Qu12Field.Text)); else { i12 = 0; }
            if (TryParseFloat(Qu13Field.Text) != null) i13 = (float)(string.IsNullOrEmpty(Qu13Field.Text) ? 0 : TryParseFloat(Qu13Field.Text)); else { i13 = 0; }
            if (TryParseFloat(Qu14Field.Text) != null) i14 = (float)(string.IsNullOrEmpty(Qu14Field.Text) ? 0 : TryParseFloat(Qu14Field.Text)); else { i14 = 0; }
            if (TryParseFloat(Qu15Field.Text) != null) i15 = (float)(string.IsNullOrEmpty(Qu15Field.Text) ? 0 : TryParseFloat(Qu15Field.Text)); else { i15 = 0; }
            if (TryParseFloat(Qu16Field.Text) != null) i16 = (float)(string.IsNullOrEmpty(Qu16Field.Text) ? 0 : TryParseFloat(Qu16Field.Text)); else { i16 = 0; }
            if (TryParseFloat(Qu17Field.Text) != null) i17 = (float)(string.IsNullOrEmpty(Qu17Field.Text) ? 0 : TryParseFloat(Qu17Field.Text)); else { i17 = 0; }
            if (TryParseFloat(Qu18Field.Text) != null) i18 = (float)(string.IsNullOrEmpty(Qu18Field.Text) ? 0 : TryParseFloat(Qu18Field.Text)); else { i18 = 0; }
            sum = i1 + i2 + i3 + i4 + i5 + i6 + i7 + i8 + i9 + i10 + i11 + i12 + i13 + i14 + i15 + i16 + i17 + i18;

            sumField.Text = sum.ToString("0.00");

            string[] tokens = fprice1Field.Text.Split(' ');
            i1 = string.IsNullOrEmpty(fprice1Field.Text) ? 0 : float.Parse(tokens[0]);
            tokens = fprice2Field.Text.Split(' ');
            i2 = string.IsNullOrEmpty(fprice2Field.Text) ? 0 : float.Parse(tokens[0]);
            tokens = fprice3Field.Text.Split(' ');
            i3 = string.IsNullOrEmpty(fprice3Field.Text) ? 0 : float.Parse(tokens[0]);
            tokens = fprice4Field.Text.Split(' ');
            i4 = string.IsNullOrEmpty(fprice4Field.Text) ? 0 : float.Parse(tokens[0]);
            tokens = fprice5Field.Text.Split(' ');
            i5 = string.IsNullOrEmpty(fprice5Field.Text) ? 0 : float.Parse(tokens[0]);
            tokens = fprice6Field.Text.Split(' ');
            i6 = string.IsNullOrEmpty(fprice6Field.Text) ? 0 : float.Parse(tokens[0]);
            tokens = fprice7Field.Text.Split(' ');
            i7 = string.IsNullOrEmpty(fprice7Field.Text) ? 0 : float.Parse(tokens[0]);
            tokens = fprice8Field.Text.Split(' ');
            i8 = string.IsNullOrEmpty(fprice8Field.Text) ? 0 : float.Parse(tokens[0]);
            tokens = fprice9Field.Text.Split(' ');
            i9 = string.IsNullOrEmpty(fprice9Field.Text) ? 0 : float.Parse(tokens[0]);
            tokens = fprice10Field.Text.Split(' ');
            i10 = string.IsNullOrEmpty(fprice10Field.Text) ? 0 : float.Parse(tokens[0]);
            tokens = fprice11Field.Text.Split(' ');
            i11 = string.IsNullOrEmpty(fprice11Field.Text) ? 0 : float.Parse(tokens[0]);
            tokens = fprice12Field.Text.Split(' ');
            i12 = string.IsNullOrEmpty(fprice12Field.Text) ? 0 : float.Parse(tokens[0]);
            tokens = fprice13Field.Text.Split(' ');
            i13 = string.IsNullOrEmpty(fprice13Field.Text) ? 0 : float.Parse(tokens[0]);
            tokens = fprice14Field.Text.Split(' ');
            i14 = string.IsNullOrEmpty(fprice14Field.Text) ? 0 : float.Parse(tokens[0]);
            tokens = fprice15Field.Text.Split(' ');
            i15 = string.IsNullOrEmpty(fprice15Field.Text) ? 0 : float.Parse(tokens[0]);
            tokens = fprice16Field.Text.Split(' ');
            i16 = string.IsNullOrEmpty(fprice16Field.Text) ? 0 : float.Parse(tokens[0]);
            tokens = fprice17Field.Text.Split(' ');
            i17 = string.IsNullOrEmpty(fprice17Field.Text) ? 0 : float.Parse(tokens[0]);
            tokens = fprice18Field.Text.Split(' ');
            i18 = string.IsNullOrEmpty(fprice18Field.Text) ? 0 : float.Parse(tokens[0]);

            sumpricenotax = i1 + i2 + i3 + i4 + i5 + i6 + i7 + i8 + i9 + i10 + i11 + i12 + i13 + i14 + i15 + i16 + i17 + i18;
            sumpriceNOTAX.Text = sumpricenotax.ToString("0.00") + " €";
            float sumtax = ((float)(sumpricenotax * tax / 100));
            sumTAX.Text = sumtax.ToString("0.00") + " €";
            sumFINAL.Text = (sumpricenotax + sumtax).ToString("0.00") + " €";
            paySUM.Text = sumFINAL.Text;
            if (!String.IsNullOrEmpty(oldBalanceField.Text))
            {
                float new_bal = float.Parse(oldBalanceField.Text) + (sumpricenotax + sumtax);
                newBalanceField.Text = new_bal.ToString("0.00");
                //if ispaid checkbox is checked do not change the new balance!
                bool cb = (bool)cbispaid.IsChecked;
                if (cb == true)
                {
                    newBalanceField.Text = oldBalanceField.Text;
                    newbal_copy = new_bal.ToString();
                }
            }

            if (!String.IsNullOrEmpty(priceField.Text) && !String.IsNullOrEmpty(QuField.Text))
            {
                tokens = priceField.Text.Split(' ');
                if (TryParseFloat(QuField.Text) != null) { fpriceField.Text = (float.Parse(QuField.Text) * float.Parse(tokens[0])).ToString("0.00") + " €"; }
                else { MessageBox.Show("Warning! Quantity must be a float number e.x 5 or 5.5!"); }
                fpaField.Text = (tax + "%").ToString();
            }
            else if (String.IsNullOrEmpty(QuField.Text))
            {
                fpriceField.Text = "";
                fpaField.Text = "";
            }
            if (String.IsNullOrEmpty(product1.Text))
            {
                fpriceField.Text = "";
                fpaField.Text = "";
            }
        }

        private void Qu1Field_TextChanged(object sender, TextChangedEventArgs e)
        {
            QuantityItemChanged(price1Field, Qu1Field, fprice1Field, fpa1Field);
        }

        private void cbispaid_Checked(object sender, RoutedEventArgs e)
        {
            newbal_copy = newBalanceField.Text;
            newBalanceField.Text = oldBalanceField.Text;
        }

        private void cbispaid_Unchecked(object sender, RoutedEventArgs e)
        {
            newBalanceField.Text = newbal_copy;
        }

        private void Qu2Field_TextChanged(object sender, TextChangedEventArgs e)
        {
            QuantityItemChanged(price2Field, Qu2Field, fprice2Field, fpa2Field);
        }
        private void Qu3Field_TextChanged(object sender, TextChangedEventArgs e)
        {
            QuantityItemChanged(price3Field, Qu3Field, fprice3Field, fpa3Field);
        }
        private void Qu4Field_TextChanged(object sender, TextChangedEventArgs e)
        {
            QuantityItemChanged(price4Field, Qu4Field, fprice4Field, fpa4Field);
        }
        private void Qu5Field_TextChanged(object sender, TextChangedEventArgs e)
        {
            QuantityItemChanged(price5Field, Qu5Field, fprice5Field, fpa5Field);
        }
        private void Qu6Field_TextChanged(object sender, TextChangedEventArgs e)
        {
            QuantityItemChanged(price6Field, Qu6Field, fprice6Field, fpa6Field);
        }
        private void Qu7Field_TextChanged(object sender, TextChangedEventArgs e)
        {
            QuantityItemChanged(price7Field, Qu7Field, fprice7Field, fpa7Field);
        }
        private void Qu8Field_TextChanged(object sender, TextChangedEventArgs e)
        {
            QuantityItemChanged(price8Field, Qu8Field, fprice8Field, fpa8Field);
        }
        private void Qu9Field_TextChanged(object sender, TextChangedEventArgs e)
        {
            QuantityItemChanged(price9Field, Qu9Field, fprice9Field, fpa9Field);
        }
        private void Qu10Field_TextChanged(object sender, TextChangedEventArgs e)
        {
            QuantityItemChanged(price10Field, Qu10Field, fprice10Field, fpa10Field);
        }
        private void Qu11Field_TextChanged(object sender, TextChangedEventArgs e)
        {
            QuantityItemChanged(price11Field, Qu11Field, fprice11Field, fpa11Field);
        }
        private void Qu12Field_TextChanged(object sender, TextChangedEventArgs e)
        {
            QuantityItemChanged(price12Field, Qu12Field, fprice12Field, fpa12Field);
        }
        private void Qu13Field_TextChanged(object sender, TextChangedEventArgs e)
        {
            QuantityItemChanged(price13Field, Qu13Field, fprice13Field, fpa13Field);
        }
        private void Qu14Field_TextChanged(object sender, TextChangedEventArgs e)
        {
            QuantityItemChanged(price14Field, Qu14Field, fprice14Field, fpa14Field);
        }
        private void Qu15Field_TextChanged(object sender, TextChangedEventArgs e)
        {
            QuantityItemChanged(price15Field, Qu15Field, fprice15Field, fpa15Field);
        }
        private void Qu16Field_TextChanged(object sender, TextChangedEventArgs e)
        {
            QuantityItemChanged(price16Field, Qu16Field, fprice16Field, fpa16Field);
        }
        private void Qu17Field_TextChanged(object sender, TextChangedEventArgs e)
        {
            QuantityItemChanged(price17Field, Qu17Field, fprice17Field, fpa17Field);
        }
        private void Qu18Field_TextChanged(object sender, TextChangedEventArgs e)
        {
            QuantityItemChanged(price18Field, Qu18Field, fprice18Field, fpa18Field);
        }

        private void Qu1Field_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {

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
}