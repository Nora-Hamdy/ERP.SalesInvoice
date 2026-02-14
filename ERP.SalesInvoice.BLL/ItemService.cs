using ERP.SalesInvoice.DAL;
using ERP.SalesInvoice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.SalesInvoice.BLL
{
    public class ItemService
    {
        private readonly ItemDAL _itemDAL;

        public ItemService(string connectionString)
        {
            _itemDAL = new ItemDAL(connectionString);
        }

        public List<Item> GetAll() => _itemDAL.GetAll();

        public Item GetById(int itemId) => _itemDAL.GetAll().FirstOrDefault(x => x.Id == itemId);
    }
}
