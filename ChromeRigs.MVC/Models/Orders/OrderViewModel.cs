using ChromeRigs.Entities.PCs;
using ChromeRigs.Utils.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChromeRigs.MVC.Models.Orders
{
    public class OrderViewModel
    {
        [Display(Name = "Order Number")]
        public int Id { get; set; }

        [Display(Name = "Order Time")]
        public DateTime OrderTime { get; set; }
        public string Notes { get; set; }

        [Display(Name = "Total Price")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Customer")]
        public string CustomerFullName { get; set; }

        public List<PC> PCs { get; set; } = [];

        [Display(Name = "Payment Method")]
        public PaymentMethod PaymentMethod { get; set; }

        [Display(Name = "Paid")]
        public bool IsPaid { get; set; }
    }
}
