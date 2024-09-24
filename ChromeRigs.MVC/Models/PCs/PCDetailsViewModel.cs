using System.ComponentModel.DataAnnotations.Schema;

namespace ChromeRigs.MVC.Models.PCs
{
    public class PCDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "decimal(4, 2)")]
        public decimal Price { get; set; }

        public List<Components.PCViewModel> Components { get; set; } = [];
    }
}
