using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
            welcomelbl.Content = "Welcome, " + LoginWindow.username + ".";
          
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();
            this.Hide();
        }

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
