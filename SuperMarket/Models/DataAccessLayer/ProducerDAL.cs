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
    public class ProducerDAL
    {
        public ObservableCollection<Producer> GetAllProducers()
        {
            ObservableCollection<Producer> producers = new ObservableCollection<Producer>();
            SqlConnection sqlConnection = DALHelper.Connection;
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("spProducersSelectAllActive", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    Producer producer = new Producer();
                    producer.ProducerId = sqlDataReader.GetInt32(0);
                    producer.Name = sqlDataReader.GetString(1);
                    producer.Country = sqlDataReader.GetString(2);
                    producer.IsActive = sqlDataReader.GetBoolean(3);
                    producers.Add(producer);
                }
            }
            catch
            {
                Console.WriteLine("An error occurred while getting producers from the database.");
            }
            finally
            {
                sqlConnection.Close();
            }
            return producers;
        }

        public Producer GetProducerById(int producerId)
        {
            Producer producer = new Producer();
            SqlConnection sqlConnection = DALHelper.Connection;
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("spProducersSelectOneActive", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ProducerId", producerId);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    producer.ProducerId = sqlDataReader.GetInt32(0);
                    producer.Name = sqlDataReader.GetString(1);
                    producer.Country = sqlDataReader.GetString(2);
                    producer.IsActive = sqlDataReader.GetBoolean(3);
                }
            }
            catch
            {
                Console.WriteLine("An error occurred while getting producer from the database.");
            }
            finally
            {
                sqlConnection.Close();
            }
            return producer;
        }

        public void AddProducer(Producer producer)
        {
            SqlConnection sqlConnection = DALHelper.Connection;
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("spProducersInsert", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Name", producer.Name);
                sqlCommand.Parameters.AddWithValue("@Country", producer.Country);
                sqlCommand.Parameters.AddWithValue("@IsActive", producer.IsActive);
                sqlCommand.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("An error occurred while adding producer to the database.");
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public void ModifyProducer(Producer producer)
        {
            SqlConnection sqlConnection = DALHelper.Connection;
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("spProducersUpdate", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ProducerId", producer.ProducerId);
                sqlCommand.Parameters.AddWithValue("@Name", producer.Name);
                sqlCommand.Parameters.AddWithValue("@Country", producer.Country);
                sqlCommand.Parameters.AddWithValue("@IsActive", producer.IsActive);
                sqlCommand.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("An error occurred while modifying producer in the database.");
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public void DeleteProducer(Producer producer)
        {
            SqlConnection sqlConnection = DALHelper.Connection;
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("spProducersDelete", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ProducerId", producer.ProducerId);
                sqlCommand.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("An error occurred while deleting producer from the database.");
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
