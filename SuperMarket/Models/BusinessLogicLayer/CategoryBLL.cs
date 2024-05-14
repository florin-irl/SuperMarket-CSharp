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

        private CategoryDAL _categoryDAL = new CategoryDAL();

        public ObservableCollection<Category> GetAllCategories()
        {
            return _categoryDAL.GetAllCategories();
        }

    }
}
