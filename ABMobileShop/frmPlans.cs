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
    public partial class frmPlans : Form
    {
        public frmPlans()
        {
            InitializeComponent();

            this.Load += FrmPlans_Load;
            btnSave.Click += BtnSave_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnRefresh.Click += BtnRefresh_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
        }

        CrudOperations crudOperations = new CrudOperations();

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to permanently delete " + dataGridView1.CurrentRow.Cells["Name"].Value.ToString(), "Delete Plan", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    crudOperations.DeleteData("update tbl_Plans set planStatus = 0 where planID = '" + dataGridView1.CurrentRow.Cells["ID"].Value.ToString() + "'");
                    resetData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                idTxt.Text = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
                nameTxt.Text = dataGridView1.CurrentRow.Cells["Plan Name"].Value.ToString();
                 percentageTxt.Text = dataGridView1.CurrentRow.Cells["Percentage"].Value.ToString();
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
                resetData();
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
                crudOperations.UpdateData("update tbl_Categories set planName = '" + nameTxt.Text + "', planPercentage = '" + percentageTxt.Text + "' where planID = '" + idTxt.Text + "'");
                resetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                crudOperations.InsertData("insert into tbl_Plans values ('" + idTxt.Text + "', '" + nameTxt.Text + "', '" + percentageTxt.Text + "', 1)");
                resetData();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }

        private void FrmPlans_Load(object sender, EventArgs e)
        {
            try
            {
                MainClass.styleDataGridView(dataGridView1);
                crudOperations.FetchAccountID("select Count(planID)+1 as 'ID' from tbl_Plans", idTxt);
                crudOperations.FetchDataInGridView("select * from vw_ViewPlans", dataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }

        public void resetData()
        {
            crudOperations.FetchAccountID("select Count(planID)+1 as 'ID' from tbl_Plans", idTxt);
            crudOperations.dt.Clear();
            crudOperations.FetchDataInGridView("select * from vw_ViewPlans", dataGridView1);
            nameTxt.Text = percentageTxt.Text = "";
        }
    }
}
