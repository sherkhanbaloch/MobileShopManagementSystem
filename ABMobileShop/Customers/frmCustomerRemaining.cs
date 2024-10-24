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
    public partial class frmCustomerRemaining : Form
    {
        public frmCustomerRemaining()
        {
            InitializeComponent();

            this.Load += FrmCustomerRemaining_Load;
            searchTxt.TextChanged += SearchTxt_TextChanged;
            btnAddNew.Click += BtnAddNew_Click;
        }

        CrudOperations crudOperations = new CrudOperations();

        private void BtnAddNew_Click(object sender, EventArgs e)
        {
            frmReceives frm = new frmReceives();
            frm.Show();
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

        private void FrmCustomerRemaining_Load(object sender, EventArgs e)
        {
            try
            {
                MainClass.styleDataGridView(dataGridView1);
                crudOperations.FetchDataInGridView("select * from vw_CustomerRemaingBalance", dataGridView1);

                int sum = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    sum += Convert.ToInt32(dataGridView1.Rows[i].Cells["Balance"].Value);
                }
                lblTotalRemaining.Text = sum.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }
    }
}
