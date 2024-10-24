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
    public partial class frmMobiles : Form
    {
        public frmMobiles()
        {
            InitializeComponent();

            this.Load += FrmMobiles_Load;
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
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to permanently delete " + dataGridView1.CurrentRow.Cells["Name"].Value.ToString(), "Delete Mobile", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    crudOperations.DeleteData("update tbl_Mobiles set mobileStatus = 0 where mobileID = '" + dataGridView1.CurrentRow.Cells["ID"].Value.ToString() + "'");
                    crudOperations.dt.Clear();
                    crudOperations.FetchDataInGridView("select * from vw_ViewMobiles", dataGridView1);
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
                frmAddMobiles addMobiles = new frmAddMobiles();
                addMobiles.Text = "Update Existing Mobile";
                addMobiles.lblHeading.Text = "Update Existing Mobile";
                addMobiles.idTxt.Text = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
                addMobiles.nameTxt.Text = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
                addMobiles.investorCmb.Text = dataGridView1.CurrentRow.Cells["Investor"].Value.ToString();
                addMobiles.purchaseRateTxt.Text = dataGridView1.CurrentRow.Cells["Purchase Rate"].Value.ToString();
                addMobiles.saleRateTxt.Text = dataGridView1.CurrentRow.Cells["Sale Rate"].Value.ToString();
                addMobiles.openingStockTxt.Text = dataGridView1.CurrentRow.Cells["Opening Stock"].Value.ToString();
                addMobiles.btnUpdate.Enabled = true;
                addMobiles.btnSave.Enabled = false;
                addMobiles.ShowDialog();
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
                crudOperations.FetchDataInGridView("select * from vw_ViewMobiles", dataGridView1);
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
                frmAddMobiles frm = new frmAddMobiles();
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

        private void FrmMobiles_Load(object sender, EventArgs e)
        {
            try
            {
                MainClass.styleDataGridView(dataGridView1);
                crudOperations.FetchDataInGridView("select * from vw_ViewMobiles", dataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }
    }
}
