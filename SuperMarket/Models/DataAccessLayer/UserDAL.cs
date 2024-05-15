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
    public class UserDAL
    {
        public ObservableCollection<User> GetAllUsers()
        {
            ObservableCollection<User> users = new ObservableCollection<User>();
            SqlConnection connection = DALHelper.Connection;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spUsersSelectAllActive", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    User user = new User();
                    user.UserId = reader.GetInt32(0);
                    user.IsAdmin = reader.GetBoolean(1);
                    user.Username = reader.GetString(2);
                    user.Password = reader.GetString(3);
                    user.IsActive = reader.GetBoolean(4);
                    users.Add(user);
                }
            }
            catch
            {
                Console.WriteLine("An error occurred while getting users from the database.");
            }
            finally
            {
                connection.Close();
            }
            return users;
        }

        public User GetUserById(int userId)
        {
            User user = new User();
            SqlConnection connection = DALHelper.Connection;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spUsersSelectOneActive", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId", userId);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    user.UserId = reader.GetInt32(0);
                    user.IsAdmin = reader.GetBoolean(1);
                    user.Username = reader.GetString(2);
                    user.Password = reader.GetString(3);
                    user.IsActive = reader.GetBoolean(4);
                }
            }
            catch
            {
                Console.WriteLine("An error occurred while getting the user from the database.");
            }
            finally
            {
                connection.Close();
            }
            return user;
        }

        public void AddUser(User user)
        {
            SqlConnection connection = DALHelper.Connection;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spUsersInsert", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("An error occurred while adding the user to the database.");
            }
            finally
            {
                connection.Close();
            }
        }

        public void ModifyUser(User user)
        {
            SqlConnection connection = DALHelper.Connection;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spUsersUpdate", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId", user.UserId);
                command.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@IsActive", user.IsActive);
                command.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("An error occurred while modifying the user in the database.");
            }
            finally
            {
                connection.Close();
            }
        }

        public void DeleteUser(User user)
        {
            SqlConnection connection = DALHelper.Connection;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spUsersSoftDelete", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId", user.UserId);
                command.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("An error occurred while deleting the user from the database.");
            }
            finally
            {
                connection.Close();
            }
        }

        public ObservableCollection<User> GetCashiers()
        {
            SqlConnection connection = DALHelper.Connection;
            ObservableCollection<User> cashiers = new ObservableCollection<User>();
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spSelectAllCashiers", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    User cashier = new User();
                    cashier.UserId = reader.GetInt32(0);
                    cashier.IsAdmin = reader.GetBoolean(1);
                    cashier.Username = reader.GetString(2);
                    cashier.Password = reader.GetString(3);
                    cashier.IsActive = reader.GetBoolean(4);
                    cashiers.Add(cashier);
                }
                connection.Close();
                return cashiers;
            }
            catch
            {
                Console.WriteLine("An error occurred while getting cashiers from the database.");
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public ObservableCollection<User> GetAdmins()
        {
            SqlConnection connection = DALHelper.Connection;
            ObservableCollection<User> admins = new ObservableCollection<User>();
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spSelectAllAdmins", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    User admin = new User();
                    admin.UserId = reader.GetInt32(0);
                    admin.IsAdmin = reader.GetBoolean(1);
                    admin.Username = reader.GetString(2);
                    admin.Password = reader.GetString(3);
                    admin.IsActive = reader.GetBoolean(4);
                    admins.Add(admin);
                }
                connection.Close();
                return admins;
            }
            catch
            {
                Console.WriteLine("An error occurred while getting admins from the database.");
                return null;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
