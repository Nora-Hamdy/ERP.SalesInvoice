using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.SalesInvoice.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public DateTime InvoiceDate { get; set; } = DateTime.Now;
        public decimal Total { get; set; }
        public List<InvoiceDetail> Details { get; set; } = new List<InvoiceDetail>();
    }
}
