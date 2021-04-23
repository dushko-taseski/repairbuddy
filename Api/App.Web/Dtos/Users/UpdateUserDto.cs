namespace App.Web.Dtos.Users
{
    public class UpdateUserDto
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }
    }
}
