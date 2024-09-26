using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ChromeRigs.MVC.Models.PCs
{
    public class CreateUpdatePCViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name = "Components")]
        public List<int> ComponentsIds { get; set; } = new List<int>();

        // ========== Lookups
        [ValidateNever]
        public MultiSelectList ComponentLookup { get; set; }
    }
}
