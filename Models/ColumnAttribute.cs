namespace shopping_api.Models {
    public class ColumnAttribute : Attribute{

        private readonly string databaseColumnName;
        public string DatabaseColumnName => databaseColumnName;

        public ColumnAttribute(string databaseColumnName) {
            this.databaseColumnName = databaseColumnName;
        }
    }
}
