using ERP.SalesInvoice.BLL;
using ERP.SalesInvoice.Models;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace ERP.SalesInvoice.UI
{
    public partial class FrmSalesInvoice : Form
    {
        private readonly CustomerService _customerService;
        private readonly ItemService _itemService;
        private readonly InvoiceService _invoiceService;
      public FrmSalesInvoice()
        {
            InitializeComponent();
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json", optional: false).Build();

            string connectionString = config.GetConnectionString("ERPConnection");

            _customerService = new CustomerService(connectionString);
            _itemService = new ItemService(connectionString);
            _invoiceService = new InvoiceService(connectionString);

            LoadCustomers();
            LoadDgvItems();
            dgvItems.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dgvItems.IsCurrentCellDirty)
                {
                    dgvItems.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            };
        }

        private void LoadCustomers()
        {
            var customers = _customerService.GetAll();
            cmbCustomer.DataSource = customers;
            cmbCustomer.DisplayMember = "Name";
            cmbCustomer.ValueMember = "Id";
        }
        private void LoadDgvItems()
        {
            dgvItems.Columns.Clear();

            DataGridViewComboBoxColumn colItem = new DataGridViewComboBoxColumn();
            colItem.Name = "Item";
            colItem.HeaderText = "Item";
            var items = _itemService.GetAll();
            colItem.DataSource = items;
            colItem.DisplayMember = "Name";
            colItem.ValueMember = "Id";
            dgvItems.Columns.Add(colItem);

            DataGridViewTextBoxColumn colQty = new DataGridViewTextBoxColumn();
            colQty.Name = "Quantity";
            colQty.HeaderText = "Quantity";
            dgvItems.Columns.Add(colQty);

            DataGridViewTextBoxColumn colPrice = new DataGridViewTextBoxColumn();
            colPrice.Name = "Price";
            colPrice.HeaderText = "Price";
            colPrice.ReadOnly = true;
            dgvItems.Columns.Add(colPrice);

            DataGridViewTextBoxColumn colTotal = new DataGridViewTextBoxColumn();
            colTotal.Name = "LineTotal";
            colTotal.HeaderText = "Line Total";
            colTotal.ReadOnly = true;
            dgvItems.Columns.Add(colTotal);
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dgvItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvItems.Rows[e.RowIndex];

            if (dgvItems.Columns[e.ColumnIndex].Name == "Item")
            {
                int selectedId = 0;
                int.TryParse(row.Cells["Item"].Value?.ToString(), out selectedId);

                if (selectedId > 0)
                {
                    var selectedItem = _itemService.GetById(selectedId);

                    if (selectedItem != null)
                    {
                        row.Cells["Price"].Value = selectedItem.Price;

                        if (string.IsNullOrEmpty(row.Cells["Quantity"].Value?.ToString()))
                            row.Cells["Quantity"].Value = 1;
                    }
                }
            }

            int quantity = 0;
            decimal price = 0;

            int.TryParse(row.Cells["Quantity"].Value?.ToString(), out quantity);
            decimal.TryParse(row.Cells["Price"].Value?.ToString(), out price);

            row.Cells["LineTotal"].Value = (quantity * price).ToString("0.00");

            decimal total = 0;
            foreach (DataGridViewRow r in dgvItems.Rows)
            {
                decimal line = 0;
                decimal.TryParse(r.Cells["LineTotal"].Value?.ToString(), out line);
                total += line;
            }

            txtTotal.Text = total.ToString("0.00");
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbCustomer.SelectedValue == null)
            {
                MessageBox.Show("Please select a customer!");
                return;
            }

            if (dgvItems.Rows.Count == 0)
            {
                MessageBox.Show("Please add at least one item!");
                return;
            }

            Invoice invoice = new Invoice
            {
                CustomerId = Convert.ToInt32(cmbCustomer.SelectedValue),
                InvoiceDate = DateTime.Now,
                Details = new List<InvoiceDetail>()
            };

            foreach (DataGridViewRow row in dgvItems.Rows)
            {
                if (row.IsNewRow) continue;

                int itemId = Convert.ToInt32(row.Cells["Item"].Value);
                int qty = Convert.ToInt32(row.Cells["Quantity"].Value);
                decimal price = Convert.ToDecimal(row.Cells["Price"].Value);

                invoice.Details.Add(new InvoiceDetail
                {
                    ItemId = itemId,
                    Quantity = qty,
                    Price = price
                });
            }

            int newInvoiceId = _invoiceService.Save(invoice);

            MessageBox.Show($"Invoice saved successfully! ID = {newInvoiceId}");
            ClearForm();
        }

        private void btnEdit_Click_3(object sender, EventArgs e)
        {
            if (dgvItems.Rows.Count == 0)
            {
                MessageBox.Show("Please add at least one item!");
                return;
            }
            Customer selectedCustomer = (Customer)cmbCustomer.SelectedItem;
            int customerId = selectedCustomer.Id;

            Invoice oldInvoice = _invoiceService.GetInvoiceForCustomer(customerId);
            Invoice invoice = new Invoice
            {
                Id = oldInvoice.Id,
                CustomerId = Convert.ToInt32(cmbCustomer.SelectedValue),
                InvoiceDate = DateTime.Now,
                Details = new List<InvoiceDetail>()
            };

            foreach (DataGridViewRow row in dgvItems.Rows)
            {
                if (row.IsNewRow) continue;

                int itemId = Convert.ToInt32(row.Cells["Item"].Value);
                int qty = Convert.ToInt32(row.Cells["Quantity"].Value);
                decimal price = Convert.ToDecimal(row.Cells["Price"].Value);

                invoice.Details.Add(new InvoiceDetail
                {
                    ItemId = itemId,
                    Quantity = qty,
                    Price = price
                });
            }

            _invoiceService.Edit(invoice);

            MessageBox.Show($"Invoice updated successfully!");
            ClearForm();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Customer selectedCustomer = (Customer)cmbCustomer.SelectedItem;
            int customerId = selectedCustomer.Id;

            
            if (string.IsNullOrEmpty(cmbCustomer.SelectedValue.ToString()))
            {
                MessageBox.Show("Please select an customer to delete invoice!");
                return;
            }
            Invoice invoice = _invoiceService.GetInvoiceForCustomer(customerId);
            int invoiceId = invoice.Id;

            var confirm = MessageBox.Show("Are you sure you want to delete this invoice?",
                                          "Confirm Delete", MessageBoxButtons.YesNo);

            if (confirm == DialogResult.Yes)
            {
                _invoiceService.Delete(invoiceId);
                MessageBox.Show("Invoice deleted successfully!");
                ClearForm();
            }
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCustomer.SelectedValue == null) return;

            Customer selectedCustomer = (Customer)cmbCustomer.SelectedItem;
            int customerId = selectedCustomer.Id;

            Invoice invoice = _invoiceService.GetInvoiceForCustomer(customerId);

            if (invoice != null)
            {
                dgvItems.Rows.Clear();

                foreach (var detail in invoice.Details)
                {
                    dgvItems.Rows.Add(
                        detail.ItemId,
                        detail.Quantity,
                        detail.Price,
                        detail.Quantity * detail.Price
                    );
                }

                txtTotal.Text = invoice.Details.Sum(d => d.Quantity * d.Price).ToString("0.00");
            }
        }

        private void dgvItems_UserDeletedRow(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvItems.SelectedRows)
            {
                if (!row.IsNewRow)
                    dgvItems.Rows.Remove(row);
            }
            decimal total = 0;

            foreach (DataGridViewRow row in dgvItems.Rows)
            {
                if (row.IsNewRow) continue;

                decimal lineTotal = 0;
                decimal.TryParse(row.Cells["LineTotal"].Value?.ToString(), out lineTotal);
                total += lineTotal;
            }

            txtTotal.Text = total.ToString("0.00");
        }
        private void ClearForm()
        {
            cmbCustomer.SelectedIndex = -1;
            dgvItems.Rows.Clear();
            txtTotal.Text = "0.00";
        }
    }
}
