using ChromeRigs.MVC.Models.PCs;
using ChromeRigs.Utils.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChromeRigs.MVC.Models.Orders
{
    public class OrderDetailsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Order Time")]
        public DateTime OrderTime { get; set; }
        public string Notes { get; set; }

        [Display(Name = "Total Price")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Customer")]
        public string CustomerFullName { get; set; }

        [Display(Name = "Customer Address")]
        public string CustomerAddress { get; set; }

        public List<PCViewModel> PCs { get; set; } = [];


        [Display(Name = "Payment Method")]
        public PaymentMethod PaymentMethod { get; set; }

        [Display(Name = "Paid")]
        public bool IsPaid { get; set; }
    }
}
