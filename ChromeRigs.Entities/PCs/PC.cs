using ChromeRigs.Entities.Components;
using ChromeRigs.Entities.Orders;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChromeRigs.Entities.PCs
{
    public class PC
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "decimal(4, 2)")]
        public decimal Price { get; set; }

        public List<Component> Components { get; set; } = [];

        public List<Order> Orders { get; set; } = [];

    }
}
