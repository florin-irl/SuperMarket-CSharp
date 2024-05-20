using SuperMarket.Models.DataAccessLayer;
using SuperMarket.Models.EntityLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Models.BusinessLogicLayer
{
    public class ProductBLL
    {
        private ProductDAL _productDAL = new ProductDAL();
        private CategoryDAL _categoryDAL = new CategoryDAL();
        private ProducerDAL _producerDAL = new ProducerDAL();
        private StockDAL _stockDAL = new StockDAL();

        public ObservableCollection<Product> GetAllProducts()
        {
            return _productDAL.GetAllProducts();
        }

        public void AddProduct(Product product)
        {
            _productDAL.AddProduct(product);
        }

        public void UpdateProduct(Product product)
        {
            _productDAL.ModifyProduct(product);
        }

        public void DeleteProduct(Product product)
        {
            _productDAL.DeleteProduct(product);
        }

        public ObservableCollection<Product> GetProductsByName(string name)
        {
            ObservableCollection<Product> products = new ObservableCollection<Product>();
            ObservableCollection<Product> allProducts = GetAllProducts();
            ObservableCollection<Stock>  stocks = _stockDAL.GetAllStocks();
            foreach (Product product in allProducts)
            {
                   if (product.Name.Contains(name) && stocks.Any(s => s.ProductId == product.ProductId))
                {
                    products.Add(product);
                }
            }
            return products;
        }

        public ObservableCollection<Product> GetProductsByBarcode(string barcode)
        {
            ObservableCollection<Product> products = new ObservableCollection<Product>();
            ObservableCollection<Product> allProducts = GetAllProducts();
            ObservableCollection<Stock> stocks = _stockDAL.GetAllStocks();
            foreach (Product product in allProducts)
            {
                if (product.Barcode.Contains(barcode) && stocks.Any(s => s.ProductId == product.ProductId))
                {
                    products.Add(product);
                }
            }
            return products;
        }

        public ObservableCollection<Product> GetProductsByCategory(string category)
        {
            Category selectedCategory;
            ObservableCollection<Product> products = new ObservableCollection<Product>();
            ObservableCollection<Product> allProducts = GetAllProducts();
            ObservableCollection<Stock> stocks = _stockDAL.GetAllStocks();
            foreach (Product product in allProducts)
            {
                selectedCategory = _categoryDAL.GetCategoryById(product.CategoryId);
                if (selectedCategory.Name == category && stocks.Any(s => s.ProductId == product.ProductId))
                {
                    products.Add(product);
                }
            }
            return products;
        }

        public ObservableCollection<Product> GetProductsByProducer(string producer)
        {
            Producer selectedProducer;
            ObservableCollection<Product> products = new ObservableCollection<Product>();
            ObservableCollection<Product> allProducts = GetAllProducts();
            ObservableCollection<Stock> stocks = _stockDAL.GetAllStocks();
            foreach (Product product in allProducts)
            {
                selectedProducer = _producerDAL.GetProducerById(product.ProducerId);
                if (selectedProducer.Name == producer && stocks.Any(s => s.ProductId == product.ProductId))
                {
                    products.Add(product);
                }
            }
            return products;
        }

        public ObservableCollection<Product> GetProductsByExpirationDate(string expirationDate)
        {
            ObservableCollection<Product> products = new ObservableCollection<Product>();
            ObservableCollection<Product> allProducts = GetAllProducts();
            ObservableCollection<Stock> stocks = _stockDAL.GetAllStocks();
            foreach (Stock stock in stocks)
            {
                Console.WriteLine(stock.ExpirationDate);
            }
            string[] dateParts = expirationDate.Split('/');
            int day = int.Parse(dateParts[0]);
            int month = int.Parse(dateParts[1]);
            int year = int.Parse(dateParts[2]);
            DateTime date = new DateTime(year, month, day);
            Console.Write(date);


            foreach (Product product in allProducts)
            {
                bool hasMatchingStock = stocks.Any(s => s.ProductId == product.ProductId && s.ExpirationDate == date);
               
                if (hasMatchingStock)
                {
                    products.Add(product);
                }
            }
            return products;
        }
    }
}
