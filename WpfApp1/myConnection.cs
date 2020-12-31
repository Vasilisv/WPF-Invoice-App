using System;

namespace WpfApp1
{
    class myConnection
    {
      

        public static String connectionString()
        {
           string  server = "server.linux106.papaki.gr";
            string database = "t116679bil_invoice";
            string uid = "t116679bil_testapp";
            string password = "1925laos4928";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" + "CHARSET= utf8" +";" ;

            return connectionString;
        }

    }
}
