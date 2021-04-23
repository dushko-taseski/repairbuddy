namespace App.Web.Dtos.Users
{
    public class RegisterUserDto
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
