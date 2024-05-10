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
    public class ReceiptDAL
    {
        public ObservableCollection<Receipt> GetAllReceipts()
        {
            ObservableCollection<Receipt> receipts = new ObservableCollection<Receipt>();
            SqlConnection connection = DALHelper.Connection;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spReceiptsSelectAllActive", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Receipt receipt = new Receipt();
                    receipt.ReceiptID = reader.GetInt32(0);
                    receipt.IssueDate = reader.GetDateTime(1);
                    receipt.UserID = reader.GetInt32(2);
                    receipt.TotalPrice = reader.GetDecimal(3);
                    receipt.IsActive = reader.GetBoolean(4);
                    receipts.Add(receipt);
                }
            }
            catch
            {
                Console.WriteLine("An error occurred while getting receipts from the database.");
            }
            finally
            {
                connection.Close();
            }
            return receipts;
        }  
        
        public Receipt GetReceiptById(int receiptId)
        {
            Receipt receipt = new Receipt();
            SqlConnection connection = DALHelper.Connection;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spReceiptsSelectOneActive", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ReceiptId", receiptId);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    receipt.ReceiptID = reader.GetInt32(0);
                    receipt.IssueDate = reader.GetDateTime(1);
                    receipt.UserID = reader.GetInt32(2);
                    receipt.TotalPrice = reader.GetDecimal(3);
                    receipt.IsActive = reader.GetBoolean(4);
                }
            }
            catch
            {
                Console.WriteLine("An error occurred while getting the receipt from the database.");
            }
            finally
            {
                connection.Close();
            }
            return receipt;
        }

        public void AddReceipt(Receipt receipt)
        {
            SqlConnection connection = DALHelper.Connection;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spReceiptsInsert", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IssueDate", receipt.IssueDate);
                command.Parameters.AddWithValue("@UserID", receipt.UserID);
                command.Parameters.AddWithValue("@TotalPrice", receipt.TotalPrice);
                command.Parameters.AddWithValue("@IsActive", receipt.IsActive);
                command.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("An error occurred while adding the receipt to the database.");
            }
            finally
            {
                connection.Close();
            }
        }

        public void ModifyReceipt(Receipt receipt)
        {
            SqlConnection connection = DALHelper.Connection;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spReceiptsUpdate", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ReceiptID", receipt.ReceiptID);
                command.Parameters.AddWithValue("@IssueDate", receipt.IssueDate);
                command.Parameters.AddWithValue("@UserID", receipt.UserID);
                command.Parameters.AddWithValue("@TotalPrice", receipt.TotalPrice);
                command.Parameters.AddWithValue("@IsActive", receipt.IsActive);
                command.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("An error occurred while modifying the receipt in the database.");
            }
            finally
            {
                connection.Close();
            }
        }

        public void DeleteReceipt(Receipt receipt)
        {
            SqlConnection connection = DALHelper.Connection;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spReceiptsDelete", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ReceiptID", receipt.ReceiptID);
                command.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("An error occurred while deleting the receipt from the database.");
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
