using ERP.SalesInvoice.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.SalesInvoice.DAL
{
    public class InvoiceDAL
    {
        private readonly string _connectionString;

        public InvoiceDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int Save(Invoice invoice)
        {
            int invoiceId = 0;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                invoice.InvoiceNumber = "INV-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string sqlHeader = @"INSERT INTO Invoices(InvoiceNumber, CustomerId, InvoiceDate, Total) 
                                             VALUES (@InvoiceNumber, @CustomerId, @InvoiceDate, @Total);
                                             SELECT CAST(SCOPE_IDENTITY() AS INT);";
                        using (SqlCommand cmd = new SqlCommand(sqlHeader, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@InvoiceNumber", invoice.InvoiceNumber);
                            cmd.Parameters.AddWithValue("@CustomerId", invoice.CustomerId);
                            cmd.Parameters.AddWithValue("@InvoiceDate", invoice.InvoiceDate);
                            cmd.Parameters.AddWithValue("@Total", invoice.Total);
                            invoiceId = (int)cmd.ExecuteScalar();
                        }

                        foreach(var detail in invoice.Details)
                        {
                            InsertInvoiceDetail(conn, transaction, invoiceId, detail);
                        }
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            return invoiceId;
        }
        public void Edit(Invoice invoice)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string sqlHeader = @"UPDATE Invoices 
                                             SET CustomerId = @CustomerId,
                                                 InvoiceDate = @InvoiceDate,
                                                 Total = @Total
                                             WHERE Id = @InvoiceId";
                        using (SqlCommand cmd = new SqlCommand(sqlHeader, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@InvoiceId", invoice.Id);
                            cmd.Parameters.AddWithValue("@CustomerId", invoice.CustomerId);
                            cmd.Parameters.AddWithValue("@InvoiceDate", invoice.InvoiceDate);
                            cmd.Parameters.AddWithValue("@Total", invoice.Total);

                            cmd.ExecuteNonQuery();
                        }

                        DeleteInvoiceDetail(conn,transaction, invoice.Id);

                        foreach (var detail in invoice.Details)
                        {
                            InsertInvoiceDetail(conn, transaction, invoice.Id, detail);
                        }
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Delete(int invoiceId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        DeleteInvoiceDetail(conn, transaction, invoiceId);
                        string deleteInvoice = "DELETE FROM Invoices WHERE Id = @InvoiceId";
                        using (SqlCommand cmd = new SqlCommand(deleteInvoice, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@InvoiceId", invoiceId);

                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public Invoice GetInvoiceByCustomerId(int customerId)
        {
            Invoice invoice = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                string sqlInvoice = @"SELECT Id, InvoiceNumber, CustomerId, InvoiceDate
                              FROM Invoices
                              WHERE CustomerId = @CustomerId";

                using (SqlCommand cmd = new SqlCommand(sqlInvoice, con))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            invoice = new Invoice
                            {
                                Id = reader.GetInt32(0),
                                InvoiceNumber = reader.GetString(1),
                                CustomerId = reader.GetInt32(2),
                                InvoiceDate = reader.GetDateTime(3),
                                Details = new List<InvoiceDetail>()
                            };
                        }
                    }
                }

                if (invoice != null)
                {
                    string sqlDetails = @"SELECT ItemId, Quantity, Price
                                  FROM InvoiceDetails
                                  WHERE InvoiceId = @InvoiceId";

                    using (SqlCommand cmd = new SqlCommand(sqlDetails, con))
                    {
                        cmd.Parameters.AddWithValue("@InvoiceId", invoice.Id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                invoice.Details.Add(new InvoiceDetail
                                {
                                    ItemId = reader.GetInt32(0),
                                    Quantity = reader.GetInt32(1),
                                    Price = reader.GetDecimal(2)
                                });
                            }
                        }
                    }
                }
            }

            return invoice;
        }
        private void InsertInvoiceDetail(SqlConnection conn, SqlTransaction transaction, int invoiceId, InvoiceDetail detail)
        {
            string sqlDetail = @"INSERT INTO InvoiceDetails (InvoiceId, ItemId, Quantity, Price, LineTotal)
                                                 VALUES (@InvoiceId, @ItemId, @Quantity, @Price, @LineTotal);";
            using (SqlCommand cmd = new SqlCommand(sqlDetail, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@InvoiceId", invoiceId);
                cmd.Parameters.AddWithValue("@ItemId", detail.ItemId);
                cmd.Parameters.AddWithValue("@Quantity", detail.Quantity);
                cmd.Parameters.AddWithValue("@Price", detail.Price);
                cmd.Parameters.AddWithValue("@LineTotal", detail.LineTotal);

                cmd.ExecuteNonQuery();
            }
        }
        private void DeleteInvoiceDetail(SqlConnection conn, SqlTransaction transaction, int invoiceId)
        {
            string deleteDetails = "DELETE FROM InvoiceDetails WHERE InvoiceId = @InvoiceId";
            using (SqlCommand cmd = new SqlCommand(deleteDetails, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@InvoiceId", invoiceId);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
