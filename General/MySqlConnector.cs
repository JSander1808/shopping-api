using MySql.Data.MySqlClient;
using System.Data.Common;

namespace shopping_api.General {
    public class MySqlConnector {

        private static MySqlConnection connection = null;

        public static void setConnectionData(String server, String database, String user, String password) {
            connection = new MySqlConnection("Server="+server+";Database="+database+";Uid="+user+";Pwd="+password+";");
        }

        public static void execute(String command, Action<MySqlDataReader> reader) {
            exe(command, reader, null);
        }

        public static void execute(String command, Action<MySqlDataReader> reader, Action<Exception> error) {
            exe(command, reader, error);
        }

        public static void exe(String command, Action<MySqlDataReader> reader, Action<Exception> error) {
            if(connection == null || connection.State != System.Data.ConnectionState.Open) {
                connection.Open();
            }
            try {
                MySqlCommand cmd = new MySqlCommand(command, connection);
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                reader(mySqlDataReader);
            }catch(Exception e) {
                if(e != null && error != null) {
                    error(e);
                } else {
                    Console.WriteLine("Failed to execute mysql command: ("+command+"). "+e.Message);
                }
            }
        }

    }
}
