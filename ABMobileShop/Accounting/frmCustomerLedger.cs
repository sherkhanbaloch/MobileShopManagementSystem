using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABMobileShop
{
    public partial class frmCustomerLedger : Form
    {
        CrudOperations crudOperations = new CrudOperations();

        public frmCustomerLedger()
        {
            InitializeComponent();
            this.Load += FrmCustomerLedger_Load;
            customerNameTxt.KeyUp += CustomerNameTxt_KeyUp;
        }

        private void CustomerNameTxt_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    frmSelectCustomer frm = new frmSelectCustomer();
                    frmSelectCustomer.isLedgerForm = true;
                    frm.ShowDialog();
                    customerIDTxt.Text = frmSelectCustomer.id;
                    customerNameTxt.Text = frmSelectCustomer.name;
                    careofTxt.Text = frmSelectCustomer.careof;
                    contactTxt.Text = frmSelectCustomer.phone;
                    addressTxt.Text = frmSelectCustomer.address;
                    maxCredit.Text = frmSelectCustomer.maxcredit;
                    previousBalanceTxt.Text = frmSelectCustomer.balance;
                    fetchLedger();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }

        DataTable dt = new DataTable();

        private void FrmCustomerLedger_Load(object sender, EventArgs e)
        {
            try
            {
                MainClass.styleDataGridView(dataGridView1);

                dt.Columns.Add("Date", typeof(string));
                dt.Columns.Add("Description", typeof(string));
                dt.Columns.Add("Debit", typeof(double));
                dt.Columns.Add("Credit", typeof(double));
                dt.Columns.Add("Balance", typeof(double));

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }

        public void fetchLedger()
        {
            double balance = Convert.ToDouble(previousBalanceTxt.Text);

            MainClass.dbConnection.Open();
            string qry = "select * from vw_CustomerLedger where [Customer ID] = '" + customerIDTxt.Text + "'";
            SqlCommand cmd = new SqlCommand(qry, MainClass.dbConnection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                double debit = Convert.ToDouble(reader["Debit"]);
                double credit = Convert.ToDouble(reader["Credit"]);
                balance += debit - credit;

                dt.Rows.Add(reader["Date"], reader["Description"], reader["Debit"], reader["Credit"], balance);
            }
            MainClass.dbConnection.Close();
        }


    }
}
