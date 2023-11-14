namespace Arty.Dtos
{
    public class UserSignup
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
    public class UserProfile
    {
        public UserSignup User { get; set; } = new UserSignup();

    }
}
