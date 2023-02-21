namespace Schools.Models
{
    public class AuthModel
    {
        public string Message { get; set; }
        public bool IsAuthinticate { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public UserType UserType { get; set; }
    }
}
