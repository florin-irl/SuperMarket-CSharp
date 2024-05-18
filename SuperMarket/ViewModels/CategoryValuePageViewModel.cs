using SuperMarket.Models.BusinessLogicLayer;
using SuperMarket.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.ViewModels
{
    public class CategoryValuePageViewModel : BaseViewModel
    {
        public ObservableCollection<CategoryValue> CategoryValueList { get; set; }
        private CategoryBLL _categoryBLL = new CategoryBLL();
        private ProductBLL _productBLL = new ProductBLL();
        private StockBLL _stockBLL = new StockBLL();

        public CategoryValuePageViewModel()
        {
            CategoryValueList = new ObservableCollection<CategoryValue>();
            ObservableCollection<Category> categories = _categoryBLL.GetAllCategories();
            ObservableCollection<Product> products= _productBLL.GetAllProducts();
            ObservableCollection<Stock> stocks = _stockBLL.GetAllStocks();
            ObservableCollection<int?> selectedProducts = new ObservableCollection<int?>();
            foreach (Category category in categories)
            {
                CategoryValue categoryValue = new CategoryValue();
                categoryValue.CategoryName = category.Name;
                categoryValue.TotalValue = 0;
                selectedProducts.Clear();
                foreach (Product product in products)
                {
                    if (product.CategoryId == category.CategoryId)
                    {
                        selectedProducts.Add(product.ProductId);
                    }
                }
                foreach (Stock stock in stocks)
                {
                    if (selectedProducts.Contains(stock.ProductId))
                    {
                        categoryValue.TotalValue += stock.Quantity * stock.SellingPrice;
                    }
                }
                
                CategoryValueList.Add(categoryValue);
            }
        }
    }
}
