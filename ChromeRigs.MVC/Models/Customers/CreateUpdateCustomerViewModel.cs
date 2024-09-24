using ChromeRigs.Utils.Enums;
using System.ComponentModel.DataAnnotations;

namespace ChromeRigs.MVC.Models.Customers
{
    public class CreateUpdateCustomerViewModel
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public Gender Gender { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }
    }
}