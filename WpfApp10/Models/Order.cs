using System;
using System.Collections.Generic;

namespace Ykrahenia.Models
{
    public partial class Order
    {
        public Order()
        {
            ProductArticleNumbers = new HashSet<Product>();
        }

        public int OrderId { get; set; }
        public string OrderStatus { get; set; } = null!;
        public DateTime OrderDeliveryDate { get; set; }
        public string OrderPickupPoint { get; set; } = null!;

        public virtual ICollection<Product> ProductArticleNumbers { get; set; }
    }
}
