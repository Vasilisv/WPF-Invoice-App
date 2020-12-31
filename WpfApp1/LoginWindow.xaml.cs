using System.Net;
using System.Windows;
using MySql.Data.MySqlClient;
using AutoUpdaterDotNET;
using Syncfusion.SfSkinManager;
using System.Drawing;
using System.Windows.Media;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        
     
        public static int id;
        public static string username;
       
        public LoginWindow()
        {
            //SfSkinManager.ApplyStylesOnApplication = true;
            //SfSkinManager.SetVisualStyle(this, VisualStyles.MaterialLight);
            InitializeComponent();
       
            bool internet = CheckForInternetConnection();
            if (internet == true)
            {
                connectionNO.Visibility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("Warning! No internet connection found!");
                connectionYES.Visibility = Visibility.Hidden;
            }

            AutoUpdater.Start("http://voudourisdesign.gr/erpupdate/ERPUpdate.xml");
            
        }

        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connectionstring = null;
            connectionstring = myConnection.connectionString();
            MySqlConnection cnn = new MySqlConnection(connectionstring);
            cnn.Open();
            MySqlCommand command = cnn.CreateCommand();
            
            command.Parameters.AddWithValue("@username", usernameField.Text);
            command.Parameters.AddWithValue("@password", passwordField.Password);
            command.CommandText = "SELECT * FROM user WHERE username = @username AND pass = @password";
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                     id = reader.GetInt32("id");
                username = reader.GetString("username");
                MessageBox.Show("Logged In ! USER ID:" + id);
                MenuWindow mw = new MenuWindow();
                mw.Show();
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

