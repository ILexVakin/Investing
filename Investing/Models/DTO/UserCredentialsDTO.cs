namespace Investing.Models.DTO
{
    public class UserCredentialsDTO
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
        public string Login { get; set; }

        public string Password { get; set; }

        public bool IsRememberMe { get; set; }
    }
}
