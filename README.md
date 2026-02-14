# ERP Sales Invoice – Desktop Application

## 📝 Description
This is a simple Sales Invoice screen for a Desktop ERP application, built using C# (.NET 8, WinForms) and SQL Server.
The screen allows the user to:
Create a new invoice
Select a customer
Add multiple items with quantity (Price is fetched automatically from the database)
Automatically calculate totals (LineTotal & Total)
Save, edit, and delete invoices

## ⚙️ Technologies
C# (.NET 8, WinForms)
SQL Server
Data Access Layer (DAL)
Business Logic Layer (BLL)
appsettings.json for database connection string

## 🗄 Database
Database name: ERP_SalesInvoice
Tables:
Customers
Items
Invoices
InvoiceDetails
SQL script is provided to create tables and insert sample data.
## 🚀 How to Run Project
Open the solution in Visual Studio 2022 or later.
Restore NuGet packages if needed (Microsoft.Extensions.Configuration.Json).
Create the database by running the SQL script in SQL Server (SSMS)

Update the connection string in appsettings.json:
Json
Copy code
{
  "ConnectionStrings": {
    "ERPConnection": "Server=.\\SQLEXPRESS;Database=ERP_SalesInvoice;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
Build the project.

Run the project. The Sales Invoice form will open:
Select a customer from the ComboBox
Add items in the DataGridView (Quantity only; Price is fetched automatically)
Totals are calculated automatically
Use Save, Edit, and Delete buttons to manage invoices

## 📁 Project Structure

/DAL         → Data Access Layer (SQL operations)
/BLL         → Business Logic Layer (validation, calculations)
/Models      → Customer, Item, Invoice, InvoiceDetail classes
/Forms       → WinForms UI (FrmSalesInvoice)
/appsettings.json → Connection string configuration

## ✅ Notes
Invoice number is generated automatically when saving a new invoice
Customer selection is required
At least one item must be added before saving
LineTotal and Total are calculated automatically
Save/Edit/Delete operate directly on the invoice selected
