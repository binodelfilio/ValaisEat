using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Configuration;
using DTO;

namespace DAL
{
    interface IOrder_DB : IDB
    {

        List<Order> GetAll();
        Order GetByID(int id);
        int Delete(int id);
        Order Add(Order order);
    }
    class Orders_DB : IOrder_DB
    {
        public IConfiguration Configuration { get; }
        private string connectionString { get; }
        public Orders_DB(IConfiguration conf)
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
                    string query = "DELETE FROM Order WHERE idOrder=@id";
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
        public Order Add(Order order)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Order(status, idCustomer, idStaff) " +
                        "VALUES(@status, @idCustomer, @idStaff); SELECT SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@status", order.Status);
                    cmd.Parameters.AddWithValue("@idCustomer", order.Customer.IdCustomer);
                    cmd.Parameters.AddWithValue("@idStaff", order.Staff.IdStaff);
                    cn.Open();

                    order.IdOrder = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return order;
        }
        public Order GetByID(int id)
        {
            Order order = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Order WHERE IdOrder = @id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                             order = serializeOrder(dr);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return order;
        }
        public List<Order> GetAll()
        {
            List<Order> results = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Order";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Order>();
                            results.Add(serializeOrder(dr));
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
        private Order serializeOrder(SqlDataReader dr)
        {
            // TODO: Manage to get object by id => get from manager ? 
            Order order = new Order();

            order.IdOrder = (int)dr["IdOrder"];
            order.Status = (string)dr["status"];
            order.Customer = null;
            order.Staff = null;

            return order;
        }
    }
}
