using SuperMarket.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Models.DataAccessLayer
{
    public class CategoryDAL
    {
        public ObservableCollection<Category> GetAllCategories()
        {
            ObservableCollection<Category> categories = new ObservableCollection<Category>();
            SqlConnection connection = DALHelper.Connection;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spCategoriesSelectAllActive", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                       Category category = new Category();
                       category.CategoryId = reader.GetInt32(0);
                       category.Name = reader.GetString(1);
                       category.IsActive = reader.GetBoolean(2);
                       categories.Add(category);
                }
                
            }
            catch
            {
                Console.WriteLine("An error occurred while getting categories from the database.");
            }
            finally
            {
                connection.Close();
            }
            return categories;
        }

        public Category GetCategoryById(int categoryId)
        {
            Category category = new Category();
            SqlConnection connection = DALHelper.Connection;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spCategoriesSelectOneActive", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CategoryId", categoryId);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    category.CategoryId = reader.GetInt32(0);
                    category.Name = reader.GetString(1);
                    category.IsActive = reader.GetBoolean(2);
                }
            }
            catch
            {
                Console.WriteLine("An error occurred while getting the category from the database.");
            }
            finally
            {
                connection.Close();
            }
            return category;
        }

        public void AddCategory(Category category)
        {
            SqlConnection connection = DALHelper.Connection;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spCategoriesInsert", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name", category.Name);
                command.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("An error occurred while adding the category to the database.");
            }
            finally
            {
                connection.Close();
            }
        }

        public void ModifyCategory(Category category)
        {
            SqlConnection connection = DALHelper.Connection;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spCategoriesUpdate", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CategoryId", category.CategoryId);
                command.Parameters.AddWithValue("@Name", category.Name);
                command.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("An error occurred while modifying the category in the database.");
            }
            finally
            {
                connection.Close();
            }
        }

        public void DeleteCategory(Category category)
        {
            SqlConnection connection = DALHelper.Connection;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spCategoriesSoftDelete", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CategoryId", category.CategoryId);
                command.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("An error occurred while deleting the category from the database.");
            }
            finally
            {
                connection.Close();
            }
        }


    }
}
