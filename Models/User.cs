namespace kontorExpert.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
    }
}
