using SuperMarket.Models.DataAccessLayer;
using SuperMarket.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Models.BusinessLogicLayer
{
    public class CategoryBLL
    {
        public ObservableCollection<Category> CategoryList { get; set; }
        public Category SelectedCategory { get; set; }
        CategoryDAL categoryDAL = new CategoryDAL();

        public CategoryBLL()
        {
            CategoryList = new ObservableCollection<Category>();
        }

        public ObservableCollection<Category> GetAllCategories()
        {
            CategoryList.Clear();
            CategoryList = categoryDAL.GetAllCategories();
            return categoryDAL.GetAllCategories();
        }

        public Category GetCategoryById(int categoryId)
        {
            SelectedCategory = categoryDAL.GetCategoryById(categoryId);
            var Catgory = categoryDAL.GetCategoryById(categoryId);
            return Catgory;
        }

        public void AddCategory(Category category)
        {
            if (string.IsNullOrEmpty(category.Name))
            {
                throw new Exception("Category name is required");
            }
            else
            {
                categoryDAL.AddCategory(category);
                CategoryList.Add(category);
            }

        }

        public void ModifyCategory(Category category)
        {
            if (category == null)
            {
                throw new Exception("Category is required");
            }
            if (string.IsNullOrEmpty(category.Name))
            {
                throw new Exception("Category name is required");
            }
            else
            {
                categoryDAL.ModifyCategory(category);
            }
        }

        public void RemoveCategory(Category category)
        {
            if (category == null||category.CategoryId==null)
            {
                throw new Exception("Category is required");
            }
            else
            {
                categoryDAL.DeleteCategory(category);
                CategoryList.Remove(category);
            }
        }

    }
}
