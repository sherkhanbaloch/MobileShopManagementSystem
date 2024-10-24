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
    public partial class frmPurchase : Form
    {
        public frmPurchase()
        {
            InitializeComponent();

            this.Load += FrmPurchase_Load;
            searchTxt.TextChanged += SearchTxt_TextChanged;
            btnAddNew.Click += BtnAddNew_Click;
            btnRefresh.Click += BtnRefresh_Click;
        }

        CrudOperations crudOperations = new CrudOperations();

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                crudOperations.dt.Clear();
                crudOperations.FetchDataInGridView("select * from vw_ViewPurchase", dataGridView1);
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
                frmAddPurchase frm = new frmAddPurchase();
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
                crudOperations.dt.DefaultView.RowFilter = string.Format("Mobile LIKE '%" + searchTxt.Text + "%'", searchTxt.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }

        private void FrmPurchase_Load(object sender, EventArgs e)
        {
            try
            {
                MainClass.styleDataGridView(dataGridView1);
                crudOperations.FetchDataInGridView("select * from vw_ViewPurchase", dataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }
    }
}
