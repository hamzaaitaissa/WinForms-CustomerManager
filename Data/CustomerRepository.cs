using CustomerManager.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CustomerManager.Data
{
    public class CustomerRepository
    {
        private string _connectionString;

        public CustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Customer> GetAll()
        {
            var customers = new List<Customer>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                var cmd = new SqlCommand("SELECT * FROM Customers", con);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    customers.Add(new Customer
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString()
                    });
                }
            }

            return customers;
        }

        public void Add(Customer customer)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string query = "INSERT INTO Customers(Name, Email, Phone) VALUES (@Name, @Email, @Phone)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        if (
                            !string.IsNullOrWhiteSpace(customer.Name) ||
                            !string.IsNullOrWhiteSpace(customer.Email) ||
                            !string.IsNullOrWhiteSpace(customer.Phone)
                            )
                        {
                            cmd.Parameters.AddWithValue("@Name", customer.Name);
                            cmd.Parameters.AddWithValue("@Email", customer.Email);
                            cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            MessageBox.Show("You cant add empty values");
                            return;
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void Update(Customer customer)
        {
            try
            {
                using(SqlConnection  con = new SqlConnection(_connectionString))
                {
                    string query = "UPDATE Customers SET Name = @Name, Email = @Email, Phone = @Phone WHERE Id = @id";
                    using(SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Name", customer.Name);
                        cmd.Parameters.AddWithValue("@Id", customer.Id);
                        cmd.Parameters.AddWithValue("@Email", customer.Email);
                        cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }


            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                var cmd = new SqlCommand("DELETE FROM Customers WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
