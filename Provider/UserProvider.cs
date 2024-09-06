using shopping_api.General;
using shopping_api.Models;
using System.Runtime.CompilerServices;

namespace shopping_api.Provider
{
    public class UserProvider {

        public static async Task<UserModel> getUser(string id) {
            UserModel model = null!;

            MySqlConnector.execute("select * from core_user where id = " + id + ";", reader => {
                while(reader.Read()) {
                    model = new UserModel();
                    reader.mapDataToObject(model);
                }
            });

            return model;
        }

        public static async Task<UserModel> postUser(UserModel model) {
            if(model == null) return null;

            MySqlConnector.executeDirectly("insert into `core_user` (firstname, name, mail, password, created, updated) values (" +
                "'" + model.Firstname + "'," +
                "'" + model.Name + "'," +
                "'" + model.Email + "'," +
                "'" + model.Password + "'," +
                "now()," +
                "now()" +
                ");", error => {
                    Console.WriteLine("Failed to postUser: "+error.Message);
                    model = null;
                });

            if(model != null) {
                model.Id = MySqlConnector.getLastId("core_user");
            }

            return model;
        }

        public static async Task<UserModel> putUser(UserModel model) {
            if(model == null) return null;
            if(getUser(model.Id) == null) return null;

            MySqlConnector.executeDirectly("update core_user set " +
                "firstname = '" + model.Firstname + "'," +
                "name = '" + model.Name + "'," +
                "mail = '" + model.Email + "'," +
                "password = '" + model.Password + "'," +
                "updated = now() " +
                "where id = '" + model.Id + "';", error => {
                    Console.WriteLine("Failed to putUser: "+error.Message);
                    model = null;
                });

            return model;
        }

        public static async Task<bool> deleteUser(string id) { 
            if(id == null) return false;
            if(await getUser(id) == null) return false;

            MySqlConnector.executeDirectly("delete from core_user where id = '" + id + "';");

            return true;

        } 
    }
}
