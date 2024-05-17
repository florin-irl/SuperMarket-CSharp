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
    internal class OfferDAL
    {
        public ObservableCollection<Offer> GetAllOffers()
        {
            ObservableCollection<Offer> offers = new ObservableCollection<Offer>();
            SqlConnection connection = DALHelper.Connection;
           try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spOffersSelectAllActive", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Offer offer = new Offer();
                    offer.OfferId = reader.GetInt32(0);
                    offer.ProductId = reader.GetInt32(1);
                    offer.DiscountPercentage = reader.GetInt32(2);
                    offer.StartDate = reader.GetDateTime(3);
                    offer.EndDate = reader.GetDateTime(4);
                    offer.Reason = reader.GetString(5);
                    offer.IsActive = reader.GetBoolean(6);
                    offer.Product = new ProductDAL().GetProductById(offer.ProductId);
                    offers.Add(offer);
                }

            }
            catch
            {
                Console.WriteLine("An error occurred while getting offers from the database.");
            }
            finally
            {
                connection.Close();
            }
            return offers;

        }

        public Offer GetOfferById(int offerId)
        {
            Offer offer = new Offer();
            SqlConnection connection = DALHelper.Connection;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spOffersSelectOneActive", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@OfferId", offerId);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    offer.OfferId = reader.GetInt32(0);
                    offer.ProductId = reader.GetInt32(1);
                    offer.DiscountPercentage = reader.GetInt32(2);
                    offer.StartDate = reader.GetDateTime(3);
                    offer.EndDate = reader.GetDateTime(4);
                    offer.Reason = reader.GetString(5);
                    offer.IsActive = reader.GetBoolean(6);
                }
            }
            catch
            {
                Console.WriteLine("An error occurred while getting the offer from the database.");
            }
            finally
            {
                connection.Close();
            }
            return offer;
        }

        public void AddOffer(Offer offer)
        {
            SqlConnection connection = DALHelper.Connection;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spOffersInsert", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductId", offer.ProductId);
                command.Parameters.AddWithValue("@DiscountPerc", offer.DiscountPercentage);
                command.Parameters.AddWithValue("@StartDate", offer.StartDate);
                command.Parameters.AddWithValue("@EndDate", offer.EndDate);
                command.Parameters.AddWithValue("@Reason", offer.Reason);
                command.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("An error occurred while adding the offer to the database.");
            }
            finally
            {
                connection.Close();
            }
        }

        public void ModifyOffer(Offer offer)
        {
            SqlConnection connection = DALHelper.Connection;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spOffersUpdate", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@OfferId", offer.OfferId);
                command.Parameters.AddWithValue("@ProductId", offer.ProductId);
                command.Parameters.AddWithValue("@DiscountPerc", offer.DiscountPercentage);
                command.Parameters.AddWithValue("@StartDate", offer.StartDate);
                command.Parameters.AddWithValue("@EndDate", offer.EndDate);
                command.Parameters.AddWithValue("@Reason", offer.Reason);
                command.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("An error occurred while modifying the offer in the database.");
            }
            finally
            {
                connection.Close();
            }
        }

        public void DeleteOffer(Offer offer)
        {
            SqlConnection connection = DALHelper.Connection;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spOffersSoftDelete", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@OfferId", offer.OfferId);
                command.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("An error occurred while deleting the offer from the database.");
            }
            finally
            {
                connection.Close();
            }
        }

    }
}
