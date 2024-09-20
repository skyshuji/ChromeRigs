using ChromeRigs.Entities.Orders;
using ChromeRigs.Utils.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChromeRigs.Entities.Customers
{
    public class Customer
    {
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<Order> Orders { get; set; } = [];


        [NotMapped]
        public string FullName
        { 
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        [NotMapped]
        public int Age
        {
            get
            {
                return DateTime.Now.Year - DateOfBirth.Year;
            }
        }
    }
}
