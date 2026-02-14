using ERP.SalesInvoice.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.SalesInvoice.DAL
{
    public class ItemDAL
    {
        private readonly string _connectionString;

        public ItemDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Item> GetAll()
        {
            var items = new List<Item>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT Id, Name, Price FROM Items";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new Item
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Price = reader.GetDecimal(2)
                            });
                        }
                    }
                }
            }
            return items;
        }

    }
}
