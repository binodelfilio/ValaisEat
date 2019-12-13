using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Configuration;
using DTO;



namespace DAL
{
    /*
     * Interface qui définit le comportement de la Dishes_DB suivante
     */
    public interface IDishes_DB : IDB
    {

        List<Dish> GetAll();
        Dish GetByID(int id);
        int Delete(int id);
        Dish Add(Dish dish);
        int Update(Dish dish);
    }
    public class Dishes_DB : IDishes_DB
    {
        public IConfiguration Configuration { get; }
        private string connectionString { get; }
        public Dishes_DB(IConfiguration conf)
        {
            Configuration = conf;
            connectionString = Configuration.GetConnectionString("DefaultConnection");
        }

        /*
       * Méthode pour supprimer un objet repas grâce à son id 
       * avec requête SQL
       */
        public int Delete(int id)
        {
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Dish WHERE idDish=@id";
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
       * Méthode pour ajouter un objet repas grâce à son id 
       * avec requête SQL
       */
        public Dish Add(Dish dish)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Dish(Name, Price, TimePrepa, IdResto, PicPath) " +
                        "VALUES(@Name, @Price, @TimePrepa, @IdResto); SELECT SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@Name", dish.Name);
                    cmd.Parameters.AddWithValue("@Price", dish.Price);
                    cmd.Parameters.AddWithValue("@TimePrepa", dish.TimePrepa);
                    cmd.Parameters.AddWithValue("@IdResto", dish.IdRestaurant);
                    cmd.Parameters.AddWithValue("@PicPath", dish.PicPath);

                    cn.Open();

                    dish.IdDish = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return dish;
        }

        /*
       * Méthode pour mettre à jour un objet repas 
       * avec requête SQL
       */
        public int Update(Dish dish)
        {
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Dish SET Name=@Name, Price=@Price, TimePrepa=@TimePrepa, IdResto=@IdResto, PicPath=@PicPath WHERE idDish=@id;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@Name", dish.Name);
                    cmd.Parameters.AddWithValue("@Price", dish.Price);
                    cmd.Parameters.AddWithValue("@TimePrepa", dish.TimePrepa);
                    cmd.Parameters.AddWithValue("@IdResto", dish.IdRestaurant);
                    cmd.Parameters.AddWithValue("@id", dish.IdDish);
                    cmd.Parameters.AddWithValue("@PicPath", dish.PicPath);
                    

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
       * Méthode pour récuperer un objet repas grâce à son id 
       * avec requête SQL
       */
        public Dish GetByID(int id)
        {
            Dish dish = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Dish WHERE IdDish = @id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            dish = serializeDish(dr);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return dish;
        }

        /*
       * Méthode pour récuperer une liste de tous les repas de la base de données
       * avec requête SQL
       */
        public List<Dish> GetAll()
        {
            List<Dish> results = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Dish";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Dish>();
                            results.Add(serializeDish(dr));
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
         * Méthode de serialisation qui permet de transformer le résultat d'un SqlDataReader en un objet Dish
         */

        private Dish serializeDish(SqlDataReader dr)
        {
            // TODO: Manage to get city object => get from manager ? 
            Dish dish = new Dish();
            dish.IdDish = (int)dr["IdDish"];
            dish.Name = (string)dr["Name"];
            dish.Price = (int)dr["Price"];
            dish.TimePrepa = (int)dr["TimePrepa"];
            dish.PicPath = (string)dr["PicPath"];
            dish.IdRestaurant = (int)dr["IdResto"];

            return dish;
        }
    }
}
