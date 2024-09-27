using ChromeRigs.MVC.Models.Orders;
using ChromeRigs.Utils.Enums;
using System.ComponentModel.DataAnnotations;

namespace ChromeRigs.MVC.Models.Customers
{
    public class CustomerDetailsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string FullName { get; set; }

        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }

        public List<OrderViewModel> Orders { get; set; } = [];

    }
}
