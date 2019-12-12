using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Configuration;
using DTO;

namespace DAL
{

    public interface IStaffs_DB: IDB
    {

        List<Staff> GetAll();
        Staff GetByID(int id);
        int Delete(int id);
        Staff Add(Staff staff);
        int Update(Staff staff);
        Staff GetByUsernamePassword(string username, string password);
    }
    public class Staffs_DB : IStaffs_DB
    {
        public IConfiguration Configuration { get; }
        private string connectionString { get; }
        public Staffs_DB(IConfiguration conf)
        {
            Configuration = conf;
            connectionString = Configuration.GetConnectionString("DefaultConnection");
        }
       
        public Staff GetByUsernamePassword(string username, string password)
        {
            Staff staff = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Staff WHERE Username = @Username AND Password = @Password";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            staff = serializeStaff(dr);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return staff;
        }
        
        public int Delete(int id)
        {
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Staff WHERE idStaff=@id";
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
        public Staff Add(Staff staff)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Staff(Firstname, Lastname, Birthdate, Address, Username, Password, idCity) " +
                        "VALUES(@Firstname, @Lastname, @Birthdate, @Address, @Username, @Password, @idCity); SELECT SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@Firstname", staff.Firstname);
                    cmd.Parameters.AddWithValue("@Lastname", staff.Lastname);
                    cmd.Parameters.AddWithValue("@Birthdate", staff.Birthdate);
                    cmd.Parameters.AddWithValue("@Address", staff.Address);
                    cmd.Parameters.AddWithValue("@Username", staff.Username);
                    cmd.Parameters.AddWithValue("@Password", staff.Password);
                    cmd.Parameters.AddWithValue("@idCity", staff.IdCity);
                    cn.Open();

                    staff.IdStaff = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return staff;
        }
        public int Update(Staff staff)
        {
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Staff SET Firstname=@Firstname, Lastname=@Lastname, Address=@Address, " +
                        "Username=@Username, Password=@Password, Birthdate=@Birthdate, idCity=@idCity WHERE IdStaff=@id;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@Firstname", staff.Firstname);
                    cmd.Parameters.AddWithValue("@Lastname", staff.Lastname);
                    cmd.Parameters.AddWithValue("@Birthdate", staff.Birthdate);
                    cmd.Parameters.AddWithValue("@Address", staff.Address);
                    cmd.Parameters.AddWithValue("@Username", staff.Username);
                    cmd.Parameters.AddWithValue("@Password", staff.Password);
                    cmd.Parameters.AddWithValue("@idCity", staff.IdCity);
                    cmd.Parameters.AddWithValue("@id", staff.IdStaff);

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
        public Staff GetByID(int id)
        {
            Staff staff = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Staff WHERE IdStaff = @id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            staff = serializeStaff(dr);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return staff;
        }
        public List<Staff> GetAll()
        {
            List<Staff> results = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Staff";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Staff>();
                            results.Add(serializeStaff(dr));
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
        private Staff serializeStaff(SqlDataReader dr)
        {
            // TODO: Manage to get object by id => get from manager ? 
            Staff staff = new Staff();

            staff.IdStaff = (int)dr["IdStaff"];
            staff.Firstname = (string)dr["Firstname"];
            staff.Lastname = (string)dr["Lastname"];
            staff.Username = (string)dr["Username"];
            staff.Password = (string)dr["Password"];
            staff.Address = (string)dr["Address"];

            if (dr["Birthdate"] != null)
                staff.Birthdate = (string)dr["Birthdate"];

            staff.IdCity = (int)dr["IdCity"];

            return staff;
        }
    }
}
