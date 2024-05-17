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
    public class ProductDAL
    {
        public ObservableCollection<Product> GetAllProducts()
        {
            ObservableCollection<Product> products = new ObservableCollection<Product>();
            SqlConnection connection = DALHelper.Connection;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spProductsSelectAllActive", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Product product = new Product();
                    product.ProductId = reader.GetInt32(0);
                    product.Name = reader.GetString(1);
                    product.Barcode = reader.GetString(2);
                    product.CategoryId = reader.GetInt32(3);
                    product.ProducerId = reader.GetInt32(4);
                    product.IsActive = reader.GetBoolean(5);
                    product.Producer = new ProducerDAL().GetProducerById(product.ProducerId);
                    product.Category = new CategoryDAL().GetCategoryById(product.CategoryId);
                    products.Add(product);
                    Console.WriteLine(product.Name);
                }
            }
            catch
            {
                Console.WriteLine("An error occurred while getting products from the database.");
            }
            finally
            {
                connection.Close();
            }
            return products;
        }

        public Product GetProductById(int productId)
        {
            Product product = new Product();
            SqlConnection connection = DALHelper.Connection;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spProductsSelectOneActive", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductId", productId);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    product.ProductId = reader.GetInt32(0);
                    product.Name = reader.GetString(1);
                    product.Barcode = reader.GetString(2);
                    product.CategoryId = reader.GetInt32(3);
                    product.ProducerId = reader.GetInt32(4);
                    product.IsActive = reader.GetBoolean(6);
                }
            }
            catch
            {
                Console.WriteLine("An error occurred while getting the product from the database.");
            }
            finally
            {
                connection.Close();
            }
            return product;
        }

        public void AddProduct(Product product)
        {
            SqlConnection connection = DALHelper.Connection;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spProductsInsert", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Barcode", product.Barcode);
                command.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                command.Parameters.AddWithValue("@ProducerId", product.ProducerId);
                command.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("An error occurred while adding the product to the database.");
            }
            finally
            {
                connection.Close();
            }
        }

        public void ModifyProduct(Product product)
        {
            SqlConnection connection = DALHelper.Connection;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spProductsUpdate", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductId", product.ProductId);
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Barcode", product.Barcode);
                command.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                command.Parameters.AddWithValue("@ProducerId", product.ProducerId);
                command.Parameters.AddWithValue("@IsActive", product.IsActive);
                command.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("An error occurred while modifying the product in the database.");
            }
            finally
            {
                connection.Close();
            }
        }

        public void DeleteProduct(Product product)
        {
            SqlConnection connection = DALHelper.Connection;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spProductsSoftDelete", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductId", product.ProductId);
                command.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("An error occurred while deleting the product from the database.");
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
