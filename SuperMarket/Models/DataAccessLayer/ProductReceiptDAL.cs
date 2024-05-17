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
    public class ProductReceiptDAL
    {
        public ObservableCollection<ProductReceipt> GetAllProductReceipts()
        {
            ObservableCollection<ProductReceipt> productReceipts = new ObservableCollection<ProductReceipt>();
            SqlConnection sqlConnection = DALHelper.Connection;
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("spProductReceiptsSelectAllActive", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    ProductReceipt productReceipt = new ProductReceipt();
                    productReceipt.ProductId = sqlDataReader.GetInt32(0);
                    productReceipt.ReceiptId = sqlDataReader.GetInt32(1);
                    productReceipt.Quantity = sqlDataReader.GetInt32(2);
                    productReceipt.Subtotal = sqlDataReader.GetDecimal(3);
                    productReceipt.IsActive = sqlDataReader.GetBoolean(4);
                    productReceipts.Add(productReceipt);
                }
            }
            catch
            {
                Console.WriteLine("An error occurred while getting product receipts from the database.");
            }
            finally
            {
                sqlConnection.Close();
            }
            return productReceipts;
        }

        public ProductReceipt GetProductReceiptById(int productId, int receiptId)
        {
            ProductReceipt productReceipt = new ProductReceipt();
            SqlConnection sqlConnection = DALHelper.Connection;
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("spProductReceiptsSelectOneActive", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ProductId", productId);
                sqlCommand.Parameters.AddWithValue("@ReceiptId", receiptId);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    productReceipt.ProductId = sqlDataReader.GetInt32(0);
                    productReceipt.ReceiptId = sqlDataReader.GetInt32(1);
                    productReceipt.Quantity = sqlDataReader.GetInt32(2);
                    productReceipt.Subtotal = sqlDataReader.GetDecimal(3);
                    productReceipt.IsActive = sqlDataReader.GetBoolean(4);
                }
            }
            catch
            {
                Console.WriteLine("An error occurred while getting a product receipt from the database.");
            }
            finally
            {
                sqlConnection.Close();
            }
            return productReceipt;
        }

        public void AddProductReceipt(ProductReceipt productReceipt)
        {
            SqlConnection sqlConnection = DALHelper.Connection;
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("spProductReceiptsInsert", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ProductId", productReceipt.ProductId);
                sqlCommand.Parameters.AddWithValue("@ReceiptId", productReceipt.ReceiptId);
                sqlCommand.Parameters.AddWithValue("@Quantity", productReceipt.Quantity);
                sqlCommand.Parameters.AddWithValue("@Subtotal", productReceipt.Subtotal);
                sqlCommand.Parameters.AddWithValue("@IsActive", productReceipt.IsActive);
                sqlCommand.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("An error occurred while adding a product receipt to the database.");
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public void ModifyProductReceipt(ProductReceipt productReceipt)
        {
            SqlConnection sqlConnection = DALHelper.Connection;
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("spProductReceiptsUpdate", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ProductId", productReceipt.ProductId);
                sqlCommand.Parameters.AddWithValue("@ReceiptId", productReceipt.ReceiptId);
                sqlCommand.Parameters.AddWithValue("@Quantity", productReceipt.Quantity);
                sqlCommand.Parameters.AddWithValue("@Subtotal", productReceipt.Subtotal);
                sqlCommand.Parameters.AddWithValue("@IsActive", productReceipt.IsActive);
                sqlCommand.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("An error occurred while modifying a product receipt in the database.");
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public void DeleteProductReceipt(ProductReceipt productReceipt)
        {
            SqlConnection sqlConnection = DALHelper.Connection;
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("spProductReceiptsDelete", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ProductId", productReceipt.ProductId);
                sqlCommand.Parameters.AddWithValue("@ReceiptId", productReceipt.ReceiptId);
                sqlCommand.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("An error occurred while deleting a product receipt from the database.");
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public ObservableCollection<ProductReceipt> GetProductReceiptsByReceiptId(int? receiptId)
        {
            ObservableCollection<ProductReceipt> productReceipts = new ObservableCollection<ProductReceipt>();
            SqlConnection sqlConnection = DALHelper.Connection;
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("spGetProductReceiptsByReceiptId", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ReceiptId", receiptId);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    ProductReceipt productReceipt = new ProductReceipt();
                    productReceipt.ProductId = sqlDataReader.GetInt32(0);
                    productReceipt.ReceiptId = sqlDataReader.GetInt32(1);
                    productReceipt.Quantity = sqlDataReader.GetInt32(2);
                    productReceipt.Subtotal = sqlDataReader.GetDecimal(3);
                    productReceipt.IsActive = sqlDataReader.GetBoolean(4);
                    productReceipts.Add(productReceipt);
                }
            }
            catch
            {
                Console.WriteLine("An error occurred while getting product receipts by receipt ID from the database.");
            }
            finally
            {
                sqlConnection.Close();
            }
            return productReceipts;
        }
    }
}
