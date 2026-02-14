using ERP.SalesInvoice.DAL;
using ERP.SalesInvoice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.SalesInvoice.BLL
{
    public class InvoiceService
    {
        private readonly InvoiceDAL _invoiceDAL;

        public InvoiceService(string connectionString)
        {
            _invoiceDAL = new InvoiceDAL(connectionString);
        }

        public int Save(Invoice invoice)
        {
            if (invoice == null)
                throw new Exception("Invoice cannot be null");

            if(invoice.CustomerId <= 0)
                throw new Exception("Customer is required");

            if (invoice.Details == null || !invoice.Details.Any())
                throw new Exception("Invoice must contain at least one item");

            invoice.Total = invoice.Details.Sum(x => x.Quantity * x.Price);
            invoice.InvoiceDate = DateTime.Now;

            return _invoiceDAL.Save(invoice);
        }

        public void Edit(Invoice invoice)
        {
            if(invoice.Id <= 0)
                throw new Exception("Invalid invoice ID");

            if (invoice.Details == null || !invoice.Details.Any())
                throw new Exception("Invoice must contain at least one item");

            invoice.Total = invoice.Details.Sum(x => x.Quantity * x.Price);

            _invoiceDAL.Edit(invoice);
        }
        public void Delete(int invoiceId)
        {
            if (invoiceId <= 0)
                throw new Exception("Invalid invoice ID");

            _invoiceDAL.Delete(invoiceId);
        }

        public Invoice GetInvoiceForCustomer(int customerId)
        {
            return _invoiceDAL.GetInvoiceByCustomerId(customerId);
        }
    }
}
