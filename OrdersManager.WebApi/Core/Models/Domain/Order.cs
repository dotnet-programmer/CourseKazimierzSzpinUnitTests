using OrdersManager.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OrdersManager.Core.Models.Domain
{
    public class Order
    {
        public Order()
        {
            Products = new Collection<Product>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public string FullNumber { get; set; }

        public ApplicationUser User { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}