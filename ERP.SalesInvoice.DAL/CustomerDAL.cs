using ERP.SalesInvoice.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ERP.SalesInvoice.DAL
{
    public class CustomerDAL
    {
        private readonly string _connectionString;

        public CustomerDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Customer> GetAll()
        {
            var customers = new List<Customer>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT Id, Name, Phone, Address FROM Customers";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customers.Add(new Customer 
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Phone = reader.IsDBNull(2) ? null : reader.GetString(2),
                                    Address = reader.IsDBNull(3) ? null : reader.GetString(3),
                                });
                        }
                    }
                }
            }
            return customers;
        }

    }
}
