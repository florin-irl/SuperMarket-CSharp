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
    public class StockDAL
    {
        public ObservableCollection<Stock> GetAllStocks()
        {
            ObservableCollection<Stock> stocks = new ObservableCollection<Stock>();
            SqlConnection sqlConnection = DALHelper.Connection;
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("spStocksSelectAllActive", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    Stock stock = new Stock();
                    stock.StockId = sqlDataReader.GetInt32(0);
                    stock.Quantity = sqlDataReader.GetInt32(1);
                    stock.Unit = sqlDataReader.GetString(2);
                    stock.SupplyDate = sqlDataReader.GetDateTime(3);
                    stock.ExpirationDate = sqlDataReader.GetDateTime(4);
                    stock.ProductId = sqlDataReader.GetInt32(5);
                    stock.AquisitionCost = sqlDataReader.GetDecimal(6);
                    stock.IsActive = sqlDataReader.GetBoolean(7);
                    stock.Product = new ProductDAL().GetProductById(stock.ProductId);
                    stocks.Add(stock);
                }
            }
            catch
            {
                Console.WriteLine("An error occurred while getting stocks from the database.");
            }
            finally
            {
                sqlConnection.Close();
            }
            return stocks;
        }

        public Stock GetStockById(int stockId)
        {
            Stock stock = new Stock();
            SqlConnection sqlConnection = DALHelper.Connection;
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("spStocksSelectOneActive", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@StockId", stockId);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    stock.StockId = sqlDataReader.GetInt32(0);
                    stock.Quantity = sqlDataReader.GetInt32(1);
                    stock.Unit = sqlDataReader.GetString(2);
                    stock.SupplyDate = sqlDataReader.GetDateTime(3);
                    stock.ExpirationDate = sqlDataReader.GetDateTime(4);
                    stock.ProductId = sqlDataReader.GetInt32(5);
                    stock.AquisitionCost = sqlDataReader.GetDecimal(6);
                    stock.IsActive = sqlDataReader.GetBoolean(7);
                }
            }
            catch
            {
                Console.WriteLine("An error occurred while getting stock from the database.");
            }
            finally
            {
                sqlConnection.Close();
            }
            return stock;
        }

        public void AddStock(Stock stock)
        {
            SqlConnection sqlConnection = DALHelper.Connection;
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("spStocksInsert", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Quantity", stock.Quantity);
                sqlCommand.Parameters.AddWithValue("@Unit", stock.Unit);
                sqlCommand.Parameters.AddWithValue("@SupplyDate", stock.SupplyDate);
                sqlCommand.Parameters.AddWithValue("@ExpirationDate", stock.ExpirationDate);
                sqlCommand.Parameters.AddWithValue("@ProductId", stock.ProductId);
                sqlCommand.Parameters.AddWithValue("@AquisitionCost", stock.AquisitionCost);
                sqlCommand.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("An error occurred while adding stock to the database.");
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public void ModifyStock(Stock stock)
        {
            SqlConnection sqlConnection = DALHelper.Connection;
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("spStocksUpdate", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@StockId", stock.StockId);
                sqlCommand.Parameters.AddWithValue("@Quantity", stock.Quantity);
                sqlCommand.Parameters.AddWithValue("@Unit", stock.Unit);
                sqlCommand.Parameters.AddWithValue("@SupplyDate", stock.SupplyDate);
                sqlCommand.Parameters.AddWithValue("@ExpirationDate", stock.ExpirationDate);
                sqlCommand.Parameters.AddWithValue("@ProductId", stock.ProductId);
                sqlCommand.Parameters.AddWithValue("@AquisitionCost", stock.AquisitionCost);
                sqlCommand.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("An error occurred while modifying stock in the database.");
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public void DeleteStock(Stock stock)
        {
            SqlConnection sqlConnection = DALHelper.Connection;
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("spStocksSoftDelete", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@StockId", stock.StockId);
                sqlCommand.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("An error occurred while deleting stock from the database.");
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
