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
    public partial class frmCashbook : Form
    {
        public frmCashbook()
        {
            InitializeComponent();

            this.Load += FrmCashbook_Load;
            searchTxt.TextChanged += SearchTxt_TextChanged;

        }

        private void SearchTxt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                crudOperations.dt.DefaultView.RowFilter = string.Format("[Account Name] LIKE '%" + searchTxt.Text + "%'", searchTxt.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }

        CrudOperations crudOperations = new CrudOperations();
        private void FrmCashbook_Load(object sender, EventArgs e)
        {
            try
            {
                MainClass.styleDataGridView(dataGridView1);
                crudOperations.FetchDataInGridView("select * from vw_Cashbook order by Date", dataGridView1);
                calculateBalance();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }

        public void calculateBalance()
        {
            double debit = 0;
            double credit = 0;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                debit += Convert.ToDouble(dataGridView1.Rows[i].Cells["Debit"].Value);
                credit += Convert.ToDouble(dataGridView1.Rows[i].Cells["Credit"].Value);
            }
            lblDebit.Text = "Debit: " + debit.ToString();
            lblCredit.Text = "Credit: " + credit.ToString();
        }
    }
}
