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
    public partial class frmInvestors : Form
    {
        public frmInvestors()
        {
            InitializeComponent();

            this.Load += FrmInvestors_Load;
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
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to permanently delete " + dataGridView1.CurrentRow.Cells["Name"].Value.ToString(), "Delete Investor", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    crudOperations.DeleteData("update tbl_Investors set investorStatus = 0 where investorID = '" + dataGridView1.CurrentRow.Cells["ID"].Value.ToString() + "'");
                    crudOperations.dt.Clear();
                    crudOperations.FetchDataInGridView("select * from vw_ViewInvestors", dataGridView1);
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
                frmAddInvestor addInvestor = new frmAddInvestor();
                addInvestor.Text = "Update Existing Investor";
                addInvestor.lblHeading.Text = "Update Existing Investor";
                addInvestor.idTxt.Text = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
                addInvestor.nameTxt.Text = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
                addInvestor.careofTxt.Text = dataGridView1.CurrentRow.Cells["Care of"].Value.ToString();
                addInvestor.phoneTxt.Text = dataGridView1.CurrentRow.Cells["Phone"].Value.ToString();
                addInvestor.addressTxt.Text = dataGridView1.CurrentRow.Cells["Address"].Value.ToString();
                addInvestor.openingBalanceTxt.Text = dataGridView1.CurrentRow.Cells["Opening Balance"].Value.ToString();
                addInvestor.btnUpdate.Enabled = true;
                addInvestor.btnSave.Enabled = false;
                addInvestor.ShowDialog();
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
                crudOperations.FetchDataInGridView("select * from vw_ViewInvestors", dataGridView1);
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
                frmAddInvestor frm = new frmAddInvestor();
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

        private void FrmInvestors_Load(object sender, EventArgs e)
        {
            try
            {
                MainClass.styleDataGridView(dataGridView1);
                crudOperations.FetchDataInGridView("select * from vw_ViewInvestors", dataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }
    }
}
