using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp10;
using Ykrahenia.Models;

namespace Ykrahenia.ViewModel
{
    public class MainWindowViewModel:ViewModelBase
    {
        public static MainWindow mainWindow;
        private User _user;
        private List<Product> _products;
        private string _searchValue;
        private string _sortValue;
        private string _filtrValue;
        private int _productCount;
        private int _displayCount;
        private Product _selectedProduct;
        public User User
        {
            get { return _user; }
            set => Set(ref _user, value, nameof(User));
        }

        public List<Product> Products
        {
            get { return _products; }
            set => Set(ref _products, value, nameof(Products));
        }

        public string SearchValue
        {
            get { return _searchValue; }
            set
            {
                Set(ref _searchValue, value, nameof(SearchValue));
                DisplayingProducts();
            }
        }

        public List<string> FiltrList => new()
        {
                "Без фильтрации",
                "0-9,99%",
                "10-14,99%",
                "15%-..."
        };

        public List<string> SortList => new()
        {
                "Без сортировки",
                "По стоимости (воз.)",
                "По стоимости (уб.)"
        };

        public string SortValue
        {
            get { return _sortValue; }
            set
            {
                Set(ref _sortValue, value, nameof(SortValue));
                DisplayingProducts();
            }
        }

        public string FiltrValue
        {
            get { return _filtrValue; }
            set
            {
                Set(ref _filtrValue, value, nameof(FiltrValue));
                DisplayingProducts();
            }
        }

        public int ProductCount
        {
            get { return _productCount; }
            set => Set(ref _productCount, value, nameof(ProductCount));
        }

        public int DisplayCount
        {
            get { return _displayCount; }
            set => Set(ref _displayCount, value, nameof(DisplayCount));
        }

        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set => Set(ref _selectedProduct, value, nameof(SelectedProduct));
        } 
        public void DisplayingProducts()
        {
            Products = Sort(Search(Filtr(GetProducts())));
            ProductCount = GetProducts().Count();
            DisplayCount = Products.Count();
        }

        private List<Product> GetProducts()
        {
            using (TradeContext db = new())
            {
                return db.Products.ToList();
            }
        }

        private List<Product> Filtr(List<Product> products)
        {
            if (FiltrValue == FiltrList[1])
                return products.Where(p => p.ProductDiscountAmount < 10 && p.ProductDiscountAmount >= 0).ToList();
            else if (FiltrValue == FiltrList[2])
                return products.Where(p => p.ProductDiscountAmount < 15 && p.ProductDiscountAmount >= 10).ToList();
            else if (FiltrValue == FiltrList[3])
                return products.Where(p => p.ProductDiscountAmount >= 15).ToList();
            else
                return products;
        }

        private List<Product> Search(List<Product> products)
        {
            if (SearchValue == null || SearchValue == string.Empty)
                return products;
            else
                return products.Where(p => p.ProductName.ToLower().Contains(SearchValue.ToLower()))
                    .ToList();
        }

        private List<Product> Sort(List<Product> products)
        {
            if (SortValue == SortList[1])
                return products.OrderBy(p => p.ProductCost).ToList();
            else if (SortValue == SortList[2])
                return products.OrderByDescending(p => p.ProductCost).ToList();
            else
                return products;
        }
    }
}
