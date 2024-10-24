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
    public partial class frmSelectCustomer : Form
    {
        public frmSelectCustomer()
        {
            InitializeComponent();

            this.Load += FrmSelectCustomer_Load;
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

        public static string id, name, careof, phone, address, balance, maxcredit;

        public static bool isLedgerForm = false;

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                id = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
                name = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
                careof = dataGridView1.CurrentRow.Cells["Care of"].Value.ToString();
                phone = dataGridView1.CurrentRow.Cells["Phone"].Value.ToString();
                address = dataGridView1.CurrentRow.Cells["Address"].Value.ToString();
                maxcredit = dataGridView1.CurrentRow.Cells["Max Credit"].Value.ToString();
                balance = dataGridView1.CurrentRow.Cells["Balance"].Value.ToString();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }

        private void FrmSelectCustomer_Load(object sender, EventArgs e)
        {
            try
            {
                MainClass.styleDataGridView(dataGridView1);

                if (isLedgerForm == true)
                {
                    crudOperations.FetchDataInGridView("select * from vw_ViewCustomers", dataGridView1);
                }
                else
                {
                    crudOperations.FetchDataInGridView("select * from vw_CustomerRemaingBalance", dataGridView1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }
    }
}
