using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ChromeRigs.MVC.Models.Orders
{
    public class CreateUpdateOrderViewModel
    {
        public int Id { get; set; }
        public string? Notes { get; set; }

        [Microsoft.Build.Framework.Required]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [Display(Name = "PCs")]

        [Microsoft.Build.Framework.Required]
        public List<int> PCIds { get; set; } = [];

        //########## Lookups

        [ValidateNever]
        public SelectList CustomerLookup { get; set; }

        [ValidateNever]
        public MultiSelectList PCLookUp { get; set; }

    }
}
