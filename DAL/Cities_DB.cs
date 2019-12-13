using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Configuration;
using DTO;

namespace DAL
{
    /*
     * Interface qui définit le comportement de la Cities_DB suivante
     */
    public interface ICities_DB : IDB
    {

        List<City> GetAll();
        City GetByID(int id);
        City Add(City city);
    }
    public class Cities_DB : ICities_DB
    {
        public IConfiguration Configuration { get; }
        private string connectionString { get; }
        public Cities_DB(IConfiguration conf)
        {
            Configuration = conf;
            connectionString = Configuration.GetConnectionString("DefaultConnection");
        }


        /*
       * Méthode pour ajouter un objet ville à la base de données
       * avec requête SQL
       */
        public City Add(City city)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO City(Name, NPA) VALUES(@Name, @NPA); SELECT SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@Name", city.Name);
                    cmd.Parameters.AddWithValue("@NPA", city.NPA);
                    cn.Open();

                    city.IdCity = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return city;
        }

        /*
       * Méthode pour récuperer la liste de toutes les villes
       * avec requête SQL
       */
        public List<City> GetAll()
        {
            List<City> results = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM City";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<City>();
                            results.Add(serializeCity(dr));
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
       * Méthode pour récuperer un objet City grâce à son ID
       * avec requête SQL
       */
        public City GetByID(int id)
        {
            City city = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM City WHERE IdCity = @id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            city = serializeCity(dr);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return city;
        }

        /*
         * Méthode de serialisation qui permet de transformer le résultat d'un SqlDataReader en un objet
         */
        private City serializeCity(SqlDataReader dr)
        {
            City city = new City();

            city.IdCity = (int)dr["IdCity"];
            city.Name = (string)dr["Name"];
            city.NPA = (string)dr["NPA"];

            return city;
        }
    }
}
