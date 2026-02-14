using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.SalesInvoice.Models
{
    public class InvoiceDetail
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public Item? Item { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal LineTotal { get; set; }
    }
}
