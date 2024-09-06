using MySql.Data.MySqlClient;
using System.Data.Common;

namespace shopping_api.General {
    public class MySqlConnector {

        private static MySqlConnection connection = null;

        public static void setConnectionData(string server, string database, string user, string password) {
            connection = new MySqlConnection("Server="+server+";Database="+database+";Uid="+user+";Pwd="+password+";");
        }

        public static void executeDirectly(string command) {
            exe(command, null, null);
        }

        public static void executeDirectly(string command, Action<Exception> error) {
            exe(command, null, error);
        }

        public static void execute(string command, Action<MySqlDataReader> reader) {
            exe(command, reader, null);
        }

        public static void execute(string command, Action<MySqlDataReader> reader, Action<Exception> error) {
            exe(command, reader, error);
        }

        public static void exe(string command, Action<MySqlDataReader> reader, Action<Exception> error) {
            if(connection == null) return;
            if(connection.State != System.Data.ConnectionState.Open) {
                connection.Open();
            }
            try {
                MySqlCommand cmd = new MySqlCommand(command, connection);
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                if(reader != null) {
                    reader(mySqlDataReader);
                }
                connection.Close();
            }catch(Exception e) {
                connection.Close();
                if(e != null && error != null) {
                    error(e);
                } else {
                    Console.WriteLine("Failed to execute mysql command: ("+command+"). "+e.Message);
                }
            }
        }

        public static string getLastId(string tablename) {
            if(tablename == null) return null;
            string id = null;
            execute("select id from " + tablename + " order by id desc limit 1;", reader => {
                while(reader.Read()) {
                    id = reader.GetString(0);
                }
            });

            return id;
        }

    }
}
