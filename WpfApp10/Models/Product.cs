using System;
using System.Collections.Generic;

namespace Ykrahenia.Models
{
    public partial class Product
    {
        private string? _productPhoto;
        public Product()
        {
            Orders = new HashSet<Order>();
        }

        public string ProductArticleNumber { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string ProductDescription { get; set; } = null!;
        public string ProductCategory { get; set; } = null!;
        public string? ProductPhoto 
        {
            get => (_productPhoto == null || _productPhoto == string.Empty) ? "\\Resources\\logo.png" : $"\\Resources\\Image\\{_productPhoto}";
            set { _productPhoto = value; }
        }
        public string ProductManufacturer { get; set; } = null!;
        public decimal ProductCost { get; set; }
        public byte? ProductDiscountAmount { get; set; }
        public int ProductQuantityInStock { get; set; }
        public string ProductStatus { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}
