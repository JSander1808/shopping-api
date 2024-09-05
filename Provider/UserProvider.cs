using shopping_api.General;

namespace shopping_api.Provider {
    public class UserProvider {

        public static async Task<UserModel> getUser(string id) {
            UserModel model = null!;

            MySqlConnector.execute("select firstname, name, mail, password, created, updated from core_user where id = " + id + ";", reader => {
                while(reader.Read()) {
                    model = new UserModel();
                    model.Id = id;
                    model.Firstname = reader.GetString(0);
                    model.Name = reader.GetString(1);
                    model.Email = reader.GetString(2);
                    model.Password = reader.GetString(3);
                    model.Created = reader.GetDateTime(4);
                    model.Updated = reader.GetDateTime(5);
                }
            });

            return model;
        }
    }
}
