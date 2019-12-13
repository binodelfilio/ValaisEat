using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Configuration;
using DTO;


namespace DAL
{
    /*
     * Interface qui définit le comportement de la classe Customers_DB suivante
     */
    public interface ICustomers_DB : IDB
    {

        List<Customer> GetAll();
        Customer GetByID(int id);
        Customer Add(Customer customer);
        int Update(Customer customer);
        Customer GetByUsernamePassword(string username, string password);
    }
    public class Customers_DB : ICustomers_DB
    {
        public IConfiguration Configuration { get; }
        private string connectionString { get; }
        public Customers_DB(IConfiguration conf)
        {
            Configuration = conf;
            connectionString = Configuration.GetConnectionString("DefaultConnection");
        }


        /*
         * Méthode de récuperation de mot de passe grâce au pseudo du customer
         * avec requête SQL
         */
        public Customer GetByUsernamePassword(string username, string password)
        {
            Customer customer = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Customer WHERE Username = @Username AND Password = @Password";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            customer = serializeCustomer(dr);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return customer;
        }


        /*
       * Méthode pour ajouter un objet customer
       * avec requête SQL
       */
        public Customer Add(Customer customer)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Customer(Firstname, Lastname, Address, Username, Password, Email, idCity) " +
                        "VALUES(@Firstname, @Lastname, @Address, @Username, @Password, @Email, @idCity); SELECT SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@Firstname", customer.Firstname);
                    cmd.Parameters.AddWithValue("@Lastname", customer.Lastname);
                    cmd.Parameters.AddWithValue("@Address", customer.Address);
                    cmd.Parameters.AddWithValue("@Username", customer.Username);
                    cmd.Parameters.AddWithValue("@Password", customer.Password);
                    cmd.Parameters.AddWithValue("@Email", customer.Email);
                    cmd.Parameters.AddWithValue("@idCity", customer.IdCity);
                    cn.Open();

                    customer.IdCustomer = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return customer;
        }

        /*
       * Méthode pour recuperer un objet Customer grâce à son id 
       * avec requête SQL
       */
        public Customer GetByID(int id)
        {
            Customer customer = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Customer WHERE IdCustomer = @id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            customer = serializeCustomer(dr);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return customer;
        }

        /*
       * Méthode pour récuperer une liste de tous les customer 
       * avec requête SQL
       */
        public List<Customer> GetAll()
        {
            List<Customer> results = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Customer";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Customer>();
                            results.Add(serializeCustomer(dr));
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
       * Méthode pour mettre à jour un objet customer 
       * avec requête SQL
       */
        public int Update(Customer customer)
        {
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Customer SET Firstname=@Firstname, Lastname=@Lastname, Address=@Address, Username=@Username, " +
                        "Password=@Password, Email=@Email, idCity=@idCity WHERE idCustomer=@id;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@Firstname", customer.Firstname);
                    cmd.Parameters.AddWithValue("@Lastname", customer.Lastname);
                    cmd.Parameters.AddWithValue("@Address", customer.Address);
                    cmd.Parameters.AddWithValue("@Username", customer.Username);
                    cmd.Parameters.AddWithValue("@Password", customer.Password);
                    cmd.Parameters.AddWithValue("@Email", customer.Email);
                    cmd.Parameters.AddWithValue("@idCity", customer.IdCity);
                    cmd.Parameters.AddWithValue("@id", customer.IdCustomer);

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
         * Méthode de serialisation qui permet de transformer le résultat d'un SqlDataReader en un objet
         */
        private Customer serializeCustomer(SqlDataReader dr)
        {
            // TODO: Manage to get city object => get from manager ? 
            Customer customer = new Customer();

            customer.IdCustomer = (int)dr["IdCustomer"];
            customer.Firstname = (string)dr["Firstname"];
            customer.Lastname = (string)dr["Lastname"];
            customer.Username = (string)dr["Username"];
            customer.Password = (string)dr["Password"];
            customer.Address = (string)dr["Address"];
            if (dr["Email"] != null)
                customer.Email = (string)dr["Email"];
            customer.IdCity = (int)dr["IdCity"];

            return customer;
        }
    }
}
