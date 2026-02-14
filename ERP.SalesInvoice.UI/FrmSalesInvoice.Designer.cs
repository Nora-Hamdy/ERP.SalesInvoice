namespace ERP.SalesInvoice.UI
{
    partial class FrmSalesInvoice
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cmbCustomer = new ComboBox();
            dgvItems = new DataGridView();
            Item = new DataGridViewComboBoxColumn();
            Quantity = new DataGridViewTextBoxColumn();
            Price = new DataGridViewTextBoxColumn();
            LineTotal = new DataGridViewTextBoxColumn();
            lblCustomer = new Label();
            lblItems = new Label();
            lblTotal = new Label();
            txtTotal = new TextBox();
            btnSave = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvItems).BeginInit();
            SuspendLayout();
            // 
            // cmbCustomer
            // 
            cmbCustomer.FormattingEnabled = true;
            cmbCustomer.Location = new Point(96, 6);
            cmbCustomer.Name = "cmbCustomer";
            cmbCustomer.Size = new Size(121, 23);
            cmbCustomer.TabIndex = 0;
            cmbCustomer.SelectedIndexChanged += cmbCustomer_SelectedIndexChanged;
            // 
            // dgvItems
            // 
            dgvItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvItems.Columns.AddRange(new DataGridViewColumn[] { Item, Quantity, Price, LineTotal });
            dgvItems.Location = new Point(23, 76);
            dgvItems.Name = "dgvItems";
            dgvItems.Size = new Size(493, 150);
            dgvItems.TabIndex = 1;
            dgvItems.CellValueChanged += dgvItems_CellValueChanged;
            dgvItems.UserDeletedRow += this.dgvItems_UserDeletedRow;
            // 
            // Item
            // 
            Item.HeaderText = "Item";
            Item.Name = "Item";
            // 
            // Quantity
            // 
            Quantity.HeaderText = "Quantity";
            Quantity.Name = "Quantity";
            // 
            // Price
            // 
            Price.HeaderText = "Price";
            Price.Name = "Price";
            // 
            // LineTotal
            // 
            LineTotal.HeaderText = "Line Total";
            LineTotal.Name = "LineTotal";
            LineTotal.ReadOnly = true;
            // 
            // lblCustomer
            // 
            lblCustomer.AutoSize = true;
            lblCustomer.Location = new Point(23, 9);
            lblCustomer.Name = "lblCustomer";
            lblCustomer.Size = new Size(59, 15);
            lblCustomer.TabIndex = 2;
            lblCustomer.Text = "Customer";
            // 
            // lblItems
            // 
            lblItems.AutoSize = true;
            lblItems.Location = new Point(23, 49);
            lblItems.Name = "lblItems";
            lblItems.Size = new Size(36, 15);
            lblItems.TabIndex = 3;
            lblItems.Text = "Items";
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(23, 251);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(32, 15);
            lblTotal.TabIndex = 4;
            lblTotal.Text = "Total";
            // 
            // txtTotal
            // 
            txtTotal.Location = new Point(96, 251);
            txtTotal.Name = "txtTotal";
            txtTotal.ReadOnly = true;
            txtTotal.Size = new Size(100, 23);
            txtTotal.TabIndex = 5;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(378, 289);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 6;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(475, 289);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(75, 23);
            btnEdit.TabIndex = 7;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click_3;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(574, 289);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 8;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // FrmSalesInvoice
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnDelete);
            Controls.Add(btnEdit);
            Controls.Add(btnSave);
            Controls.Add(txtTotal);
            Controls.Add(lblTotal);
            Controls.Add(lblItems);
            Controls.Add(lblCustomer);
            Controls.Add(dgvItems);
            Controls.Add(cmbCustomer);
            Name = "FrmSalesInvoice";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvItems).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbCustomer;
        private DataGridView dgvItems;
        private Label lblCustomer;
        private Label lblItems;
        private Label lblTotal;
        private TextBox txtTotal;
        private Button btnSave;
        private Button btnEdit;
        private Button btnDelete;
        private DataGridViewComboBoxColumn Item;
        private DataGridViewTextBoxColumn Quantity;
        private DataGridViewTextBoxColumn Price;
        private DataGridViewTextBoxColumn LineTotal;
    }
}
