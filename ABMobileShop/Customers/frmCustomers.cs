using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABMobileShop
{
    public partial class frmCustomers : Form
    {
        public frmCustomers()
        {
            InitializeComponent();

            this.Load += FrmCustomers_Load;
            searchTxt.TextChanged += SearchTxt_TextChanged;
            btnAddNew.Click += BtnAddNew_Click;
            btnRefresh.Click += BtnRefresh_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
        }

        CrudOperations crudOperations = new CrudOperations();

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to permanently delete " + dataGridView1.CurrentRow.Cells["Name"].Value.ToString(), "Delete Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    crudOperations.DeleteData("update tbl_Customers set customerStatus = 0 where customerID = '" + dataGridView1.CurrentRow.Cells["ID"].Value.ToString() + "'");
                    crudOperations.dt.Clear();
                    crudOperations.FetchDataInGridView("select * from vw_ViewCustomers", dataGridView1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                frmAddCustomers addCustomer = new frmAddCustomers();
                addCustomer.Text = "Update Existing Customer";
                addCustomer.lblHeading.Text = "Update Existing Customer";
                addCustomer.idTxt.Text = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
                addCustomer.nameTxt.Text = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
                addCustomer.careofTxt.Text = dataGridView1.CurrentRow.Cells["Care of"].Value.ToString();
                addCustomer.phoneTxt.Text = dataGridView1.CurrentRow.Cells["Phone"].Value.ToString();
                addCustomer.addressTxt.Text = dataGridView1.CurrentRow.Cells["Address"].Value.ToString();
                addCustomer.openingBalanceTxt.Text = dataGridView1.CurrentRow.Cells["Opening Balance"].Value.ToString();
                addCustomer.maxCreditTxt.Text = dataGridView1.CurrentRow.Cells["Max Credit"].Value.ToString();
                addCustomer.btnUpdate.Enabled = true;
                addCustomer.btnSave.Enabled = false;
                addCustomer.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                crudOperations.dt.Clear();
                crudOperations.FetchDataInGridView("select * from vw_ViewCustomers", dataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }

        private void BtnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                frmAddCustomers frm = new frmAddCustomers();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }

        private void SearchTxt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                crudOperations.dt.DefaultView.RowFilter = string.Format("Name LIKE '%" + searchTxt.Text + "%'", searchTxt.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }

        private void FrmCustomers_Load(object sender, EventArgs e)
        {
            try
            {
                MainClass.styleDataGridView(dataGridView1);
                crudOperations.FetchDataInGridView("select * from vw_ViewCustomers", dataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }
    }
}
