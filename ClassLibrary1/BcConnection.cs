using System;
using System.Data.SqlClient;

namespace SqlLibrary {
    public class BcConnection {
        //property to hold sqlconnection
        public SqlConnection Connection {get; set;}

        //create and open up connection use connection string
        public void Connect(string server, string database, string auth) {
            var connStr = $"server={server};database={database};{auth}";
            //create an instance of sqlconnection
            Connection = new SqlConnection(connStr);
            //do an open try to make connection
            Connection.Open();
            //check to see if open
            if (Connection.State != System.Data.ConnectionState.Open) {
                Console.WriteLine("Could not open the connection  check connection string");
                Connection = null;
                return;
                }
            Console.WriteLine(  "Connection open");
            }
        public void Disconnect() {
            if(Connection == null) {
                return;

            }
            if(Connection.State == System.Data.ConnectionState.Open) {
                Connection.Close();
                Connection.Dispose();
                Connection = null;
            }
        }
        
    }
}
