using ChromeRigs.Entities.Customers;
using ChromeRigs.Entities.PCs;
using ChromeRigs.Utils.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChromeRigs.Entities.Orders
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; }
        public string? Notes { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPrice { get; set; }



        public PaymentMethod PaymentMethod { get; set; }

        public bool IsPaid { get; set; } // Paid => True, NotPaid => False



        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public List<PC> PCs { get; set; } = [];

    }
}
