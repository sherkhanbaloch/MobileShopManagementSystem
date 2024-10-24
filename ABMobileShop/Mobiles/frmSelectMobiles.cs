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
    public partial class frmSelectMobiles : Form
    {
        public frmSelectMobiles()
        {
            InitializeComponent();

            this.Load += FrmSelectMobiles_Load;
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
        public static string id, name, investorID, investor, purchaseRate, saleRate, stock;

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                id = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
                name = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
                investorID = dataGridView1.CurrentRow.Cells["investorID"].Value.ToString();
                investor = dataGridView1.CurrentRow.Cells["Investor"].Value.ToString();
                purchaseRate = dataGridView1.CurrentRow.Cells["Purchase Rate"].Value.ToString();
                saleRate = dataGridView1.CurrentRow.Cells["Sale Rate"].Value.ToString();
                stock = dataGridView1.CurrentRow.Cells["Available Stock"].Value.ToString();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }

        private void FrmSelectMobiles_Load(object sender, EventArgs e)
        {
            try
            {
                MainClass.styleDataGridView(dataGridView1);
                crudOperations.FetchDataInGridView("select * from vw_MobilesRemaining", dataGridView1);
                dataGridView1.Columns["investorID"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }
    }
}
