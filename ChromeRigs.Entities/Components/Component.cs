using ChromeRigs.Entities.PCs;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChromeRigs.Entities.Components
{
    public class Component
    {

        public int Id { get; set; }

        public string Name { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public List<PC> PCs { get; set; } = [];

    }
}
