using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Configuration;
using DTO;

namespace DAL
{
    public interface IRestaurants_DB : IDB
    {

        List<Restaurant> GetAll();
        Restaurant GetByID(int id);
        int Delete(int id);
        Restaurant Add(Restaurant restaurant);
        int Update(Restaurant restaurant);
    }
    public class Restaurants_DB : IRestaurants_DB
    {
        public IConfiguration Configuration { get; }
        private string connectionString { get; }
        public Restaurants_DB(IConfiguration conf)
        {
            Configuration = conf;
            connectionString = Configuration.GetConnectionString("DefaultConnection");
        }
        public int Delete(int id)
        {
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Restaurant WHERE idRestaurant=@id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }
        public Restaurant Add(Restaurant restaurant)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Restaurant(name, address, idCity) " +
                        "VALUES(@name, @address, @idCity); SELECT SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@name", restaurant.Name);
                    cmd.Parameters.AddWithValue("@address", restaurant.Address);
                    cmd.Parameters.AddWithValue("@IdCity", restaurant.City.IdCity);
                    cn.Open();

                    restaurant.IdRestaurant = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return restaurant;
        }
        public int Update(Restaurant restaurant)
        {
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Restaurant SET name=@name, address=@address, IdCity=@IdCity WHERE idRestaurant=@id;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@name", restaurant.Name);
                    cmd.Parameters.AddWithValue("@address", restaurant.Address);
                    cmd.Parameters.AddWithValue("@IdCity", restaurant.City.IdCity);
                    cmd.Parameters.AddWithValue("@id", restaurant.IdRestaurant);

                    cn.Open();

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }
        public Restaurant GetByID(int id)
        {
            Restaurant restaurant = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Restaurant WHERE IdRestaurant = @id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            restaurant = serializeRestaurant(dr);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return restaurant;
        }
        public List<Restaurant> GetAll()
        {
            List<Restaurant> results = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Restaurant";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Restaurant>();
                            results.Add(serializeRestaurant(dr));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return results;
        }
        private Restaurant serializeRestaurant(SqlDataReader dr)
        {
            // TODO: Manage to get object by id => get from manager ? 
            Restaurant restaurant = new Restaurant();

            restaurant.IdRestaurant = (int)dr["IdRestaurant"];
            restaurant.Name = (string)dr["Name"];
            restaurant.Address = (string)dr["Address"]; ;
            restaurant.City = null;

            return restaurant;
        }

        public static implicit operator Restaurants_DB(Cities_DB v)
        {
            throw new NotImplementedException();
        }
    }
}
