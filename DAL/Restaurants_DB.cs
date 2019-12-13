using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Configuration;
using DTO;

namespace DAL
{
    /*
     * Interface qui définit le comportement de la Restaurants_DB suivante
     */
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

        /*
         * Méthode pour supprimer un restaurant grâce à son id 
         * avec requête SQL
         */
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

        /*
         * Méthode d'ajout d'un objet restaurant dans la base de donnée
         * avec requête SQL
         */
        public Restaurant Add(Restaurant restaurant)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Restaurant(name, address, idCity, PicPath) " +
                        "VALUES(@name, @address, @idCity); SELECT SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@name", restaurant.Name);
                    cmd.Parameters.AddWithValue("@address", restaurant.Address);
                    cmd.Parameters.AddWithValue("@IdCity", restaurant.IdCity);
                    cmd.Parameters.AddWithValue("@PicPath", restaurant.PicPath);
                    
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

        /*
         * Méthode de mise à jour d'un objet restaurant dans la base de donnée
         * avec requête SQL
         */
        public int Update(Restaurant restaurant)
        {
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Restaurant SET name=@name, address=@address, IdCity=@IdCity PicPath=@PicPath WHERE idRestaurant=@id;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@name", restaurant.Name);
                    cmd.Parameters.AddWithValue("@address", restaurant.Address);
                    cmd.Parameters.AddWithValue("@IdCity", restaurant.IdCity);
                    cmd.Parameters.AddWithValue("@id", restaurant.IdRestaurant);
                    cmd.Parameters.AddWithValue("@PicPath", restaurant.PicPath);


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

        /*
         * Méthode pour récuperer un restaurant grâce à son id
         * avec requête SQL
         */
        public Restaurant GetByID(int id)
        {
            Restaurant restaurant = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Restaurant WHERE IdResto = @id";
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

        /*
         * Méthode de récuperation d'une liste de tous les restaurants
         * avec requête SQL
         */
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

        /*
         * Méthode de serialisation qui permet de transformer le résultat d'un SqlDataReader en un objet
         */
        private Restaurant serializeRestaurant(SqlDataReader dr)
        {
            // TODO: Manage to get object by id => get from manager ? 
            Restaurant restaurant = new Restaurant();

            restaurant.IdRestaurant = (int)dr["idResto"];
            restaurant.Name = (string)dr["Name"];
            restaurant.Address = (string)dr["Address"];
            restaurant.IdCity = (int)dr["IdCity"];
            restaurant.PicPath = (string)dr["PicPath"];

            return restaurant;
        }

        public static implicit operator Restaurants_DB(Cities_DB v)
        {
            throw new NotImplementedException();
        }
    }
}
