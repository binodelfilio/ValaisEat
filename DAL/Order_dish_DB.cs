using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Configuration;
using DTO;

namespace DAL
{

    public interface IOrder_Dish_DB : IDB
    {

        List<Order_Dish> GetAll();
        Order_Dish GetByID(int id);
        int Delete(int id);
        Order_Dish Add(Order_Dish order_Dish);
    }
    public class Order_Dish_DB : IOrder_Dish_DB
    {
        public IConfiguration Configuration { get; }
        private string connectionString { get; }
        public Order_Dish_DB(IConfiguration conf)
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
                    string query = "DELETE FROM Order_Dish WHERE IdOrder_Dish=@id";
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
        public Order_Dish Add(Order_Dish order_Dish)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Order_Dish(Quantity, DateTime, IdDish, IdOrder) " +
                        "VALUES(@Quantity, @DateTime, @IdDish, @IdOrder); SELECT SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@Quantity", order_Dish.Quantity);
                    cmd.Parameters.AddWithValue("@DateTime", order_Dish.DateTime);
                    cmd.Parameters.AddWithValue("@IdDish", order_Dish.Dish.IdDish);
                    cmd.Parameters.AddWithValue("@IdOrder", order_Dish.Order.IdOrder);
                    cn.Open();

                    order_Dish.IdOrder_Dish = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return order_Dish;
        }
        public Order_Dish GetByID(int id)
        {
            Order_Dish order_Dish = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Order_Dish WHERE IdOrder_Dish = @id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            order_Dish = serializeOrderDish(dr);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return order_Dish;
        }
        public List<Order_Dish> GetAll()
        {
            List<Order_Dish> results = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Order_Dish";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Order_Dish>();
                            results.Add(serializeOrderDish(dr));
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
        private Order_Dish serializeOrderDish(SqlDataReader dr)
        {
            // TODO: Manage to get object by id => get from manager ? 
            Order_Dish order_dish = new Order_Dish();

            order_dish.IdOrder_Dish = (int)dr["IdOrder_Dish"];
            order_dish.Quantity = (int)dr["Name"]; 
            order_dish.DateTime = (DateTime)dr["Price"];
            order_dish.Dish = null;
            order_dish.Order = null;

            return order_dish;
        }
    }
}
