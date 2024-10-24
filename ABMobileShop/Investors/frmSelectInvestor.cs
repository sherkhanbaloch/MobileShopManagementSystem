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
    public partial class frmSelectInvestor : Form
    {
        public frmSelectInvestor()
        {
            InitializeComponent();

            this.Load += FrmSelectInvestor_Load;
            dataGridView1.CellClick += DataGridView1_CellClick;
            searchTxt.TextChanged += SearchTxt_TextChanged;
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

        CrudOperations crudOperations = new CrudOperations();
        public static string id, name, phone, careof, address, balance;

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                id = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
                name = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
                phone = dataGridView1.CurrentRow.Cells["Phone"].Value.ToString();
                careof = dataGridView1.CurrentRow.Cells["Care of"].Value.ToString();
                address = dataGridView1.CurrentRow.Cells["Address"].Value.ToString();
                balance = dataGridView1.CurrentRow.Cells["Opening Balance"].Value.ToString();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }

        private void FrmSelectInvestor_Load(object sender, EventArgs e)
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
