using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AutoServeJobsManager
{
    public partial class frmCustomers : Form
    {
        dbWork s = new dbWork();
        public frmCustomers()
        {
            InitializeComponent();
        }
        DataTable customersTable = new DataTable();

        private void frmCustomers_Load(object sender, EventArgs e)
        {
            if (s.InitConnection())
            {
                refreshCustomersList();   
            }
            else
            {
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
                MessageBox.Show("Error Setting up Connection with the Database\nPlease check settings.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void refreshCustomersList()
        {
            customersTable = s.GetCustomersList();
            addDataBindings();
        }

        private void addDataBindings()
        {            
            lstCustomers.DataSource = customersTable;
            lstCustomers.DisplayMember = "CustomerName";

            txtId.DataBindings.Clear();
            txtName.DataBindings.Clear();
            txtAddress.DataBindings.Clear();
            txtContactPerson.DataBindings.Clear();
            txtPhone.DataBindings.Clear();
            txtMobileNo.DataBindings.Clear();

            txtId.DataBindings.Add("Text", customersTable, "CustomerId");
            txtName.DataBindings.Add("Text", customersTable, "CustomerName");
            txtAddress.DataBindings.Add("Text", customersTable, "Address");
            txtContactPerson.DataBindings.Add("Text", customersTable, "ContactPerson");
            txtPhone.DataBindings.Add("Text", customersTable, "Phone");
            txtMobileNo.DataBindings.Add("Text", customersTable, "MobileNo");
        }
               
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure ?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                s.DeleteCustomer(txtId.Text);
                refreshCustomersList();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            s.UpdateCustomer(txtId.Text, txtName.Text, txtContactPerson.Text, txtAddress.Text, txtPhone.Text, txtMobileNo.Text);
            refreshCustomersList();
        }
    }
}
