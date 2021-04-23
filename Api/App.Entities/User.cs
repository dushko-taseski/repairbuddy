using System.Collections.Generic;

namespace App.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }

        public virtual List<Customer> Customers { get; set; }
    }
}
