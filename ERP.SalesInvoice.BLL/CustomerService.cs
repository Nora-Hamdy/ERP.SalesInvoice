using ERP.SalesInvoice.DAL;
using ERP.SalesInvoice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.SalesInvoice.BLL
{
    public class CustomerService
    {
        private readonly CustomerDAL _customerDAL;

        public CustomerService(string connectionString)
        {
            _customerDAL = new CustomerDAL(connectionString);
        }

        public List<Customer> GetAll() => _customerDAL.GetAll();
    }
}
