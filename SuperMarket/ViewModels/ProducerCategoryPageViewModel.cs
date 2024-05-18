using SuperMarket.Models.BusinessLogicLayer;
using SuperMarket.Models.EntityLayer;
using SuperMarket.ViewModels.Commands;
using SuperMarket.Views;
using SuperMarket.Views.SpecialQueries;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SuperMarket.ViewModels
{
    public class ProducerCategoryPageViewModel : BaseViewModel
    {
        public ObservableCollection<string> Producers { get; set; }
        public ObservableCollection<string> Categories { get; set; }

        public ObservableCollection<Product> Products { get; set; }

        public ObservableCollection<Product> SelectedProducts { get; set; }

        private ProducerBLL _producerBLL = new ProducerBLL();
        private CategoryBLL _categoryBLL = new CategoryBLL();
        private ProductBLL _productBLL = new ProductBLL();

        public string SelectedProducer { get; set; }
        public string SelectedCategory { get; set; }

        public ICommand ListProducts { get; set; }

        public ProducerCategoryPageViewModel()
        {
            ObservableCollection<Producer> producerss = new ObservableCollection<Producer>();
            ObservableCollection<Category> categoriess = new ObservableCollection<Category>();
            Producers = new ObservableCollection<string>();
            Categories = new ObservableCollection<string>();
            SelectedProducts = new ObservableCollection<Product>();
            producerss = _producerBLL.GetAllProducers();
            foreach(Producer prod in producerss)
            {
                Producers.Add(prod.Name);
            }
            categoriess = _categoryBLL.GetAllCategories();
            foreach(Category cat in categoriess)
            {
                Categories.Add(cat.Name);
            }
            Categories.Add("All Categories");
            Producers.Add("All Producers");
            ListProducts = new RelayCommand<object>(ListProductsMethod);
        }

        private void ListProductsMethod(object? obj)
        {
            if (obj is not ProducerCategoriesPage currentPage)
            {
                return;
            }
            // Clear the existing products
            SelectedProducts.Clear();

            // Get the filtered products
            if (SelectedProducer == "All Producers" && SelectedCategory == "All Categories")
            {
                foreach (var product in _productBLL.GetAllProducts())
                {
                    SelectedProducts.Add(product);
                }
                return;
            }
            else
                if(SelectedCategory == "All Categories")
            {
                
                var filteredProducts = from product in _productBLL.GetAllProducts()
                                       where product.Producer.Name == SelectedProducer
                                       select product;

                // Add the filtered products to the SelectedProducts collection
                foreach (var product in filteredProducts)
                {
                    SelectedProducts.Add(product);
                }
                return;
            }
            else
            {
                var filteredProducts = from product in _productBLL.GetAllProducts()
                                       where product.Producer.Name == SelectedProducer && product.Category.Name == SelectedCategory
                                       select product;

                // Add the filtered products to the SelectedProducts collection
                foreach (var product in filteredProducts)
                {
                    SelectedProducts.Add(product);
                }
            }
            
        }
    }
}
