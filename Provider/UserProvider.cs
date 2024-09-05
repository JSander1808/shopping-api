using shopping_api.General;
using shopping_api.Models;

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
    }
}
