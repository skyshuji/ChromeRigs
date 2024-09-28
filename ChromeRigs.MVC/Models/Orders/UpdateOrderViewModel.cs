using ChromeRigs.Utils.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ChromeRigs.MVC.Models.Orders
{
    public class UpdateOrderViewModel
    {
        public int Id { get; set; }
        public string? Notes { get; set; }

        [Display(Name = "PCs")]
        [Required]
        public List<int> PCIds { get; set; } = [];


        [Required]
        [Display(Name = "Payment Method")]
        public PaymentMethod PaymentMethod { get; set; }

        [Display(Name = "Paid")]
        public bool IsPaid { get; set; }
        //########## Lookups

        [ValidateNever]
        public SelectList CustomerLookup { get; set; }

        [ValidateNever]
        public MultiSelectList PCLookUp { get; set; }

        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

    }
}
