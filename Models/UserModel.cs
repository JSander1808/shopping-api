namespace shopping_api.Models
{
    public class UserModel : Model
    {
        [Column("firstname")]
        public string Firstname { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("mail")]
        public string Email { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("created")]
        public DateTime Created { get; set; }
        [Column("updated")]
        public DateTime Updated { get; set; }

    }
}
