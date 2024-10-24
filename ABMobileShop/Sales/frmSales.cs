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
    public partial class frmSales : Form
    {
        public frmSales()
        {
            InitializeComponent();

            this.Load += FrmSales_Load;
            searchTxt.TextChanged += SearchTxt_TextChanged;
            btnAddNew.Click += BtnAddNew_Click;
            btnRefresh.Click += BtnRefresh_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
        }

        CrudOperations crudOperations = new CrudOperations();

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                crudOperations.dt.Clear();
                crudOperations.FetchDataInGridView("select * from vw_ViewSales", dataGridView1);
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
                frmAddSales frm = new frmAddSales();
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

        private void FrmSales_Load(object sender, EventArgs e)
        {
            try
            {
                MainClass.styleDataGridView(dataGridView1);
                crudOperations.FetchDataInGridView("select * from vw_ViewSales", dataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }
    }
}
