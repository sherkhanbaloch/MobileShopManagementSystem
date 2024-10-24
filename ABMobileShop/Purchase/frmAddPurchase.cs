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
    public partial class frmAddPurchase : Form
    {
        CrudOperations crudOperations = new CrudOperations();

        public frmAddPurchase()
        {
            InitializeComponent();

            this.Load += FrmAddPurchase_Load;
            mobileNameTxt.KeyUp += MobileNameTxt_KeyUp;
            qtyTxt.KeyUp += QtyTxt_KeyUp;
            amountTxt.KeyUp += AmountTxt_KeyUp;
            btnSave.Click += BtnSave_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                netAmount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            mobileIDTxt.Text = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
            mobileNameTxt.Text = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
            lblInvestorID.Text = dataGridView1.CurrentRow.Cells["Investor ID"].Value.ToString();
            investorTxt.Text = dataGridView1.CurrentRow.Cells["Investor"].Value.ToString();
            purchaseRateTxt.Text = dataGridView1.CurrentRow.Cells["Purchase Rate"].Value.ToString();
            salesRateTxt.Text = dataGridView1.CurrentRow.Cells["Sale Rate"].Value.ToString();
            qtyTxt.Text = dataGridView1.CurrentRow.Cells["Quantity"].Value.ToString();
            amountTxt.Text = dataGridView1.CurrentRow.Cells["Amount"].Value.ToString();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                crudOperations.InsertData("insert into tbl_Purchase values ('" + invoiceIDTxt.Text + "', '" + invoiceDatePicker.Value.ToString("yyy-MM-dd") + "', '" + supplierIDTxt.Text + "', '" + totalTxt.Text + "')");

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string qry2 = "insert into tbl_PurchaseDetails values ('" + invoiceIDTxt.Text + "', '" + dataGridView1.Rows[i].Cells["ID"].Value.ToString() + "', '" + lblInvestorID.Text + "', '" + dataGridView1.Rows[i].Cells["Purchase Rate"].Value.ToString() + "', '" + dataGridView1.Rows[i].Cells["Sale Rate"].Value.ToString() + "', '" + dataGridView1.Rows[i].Cells["Quantity"].Value.ToString() + "')";
                    crudOperations.InsertData(qry2);
                }
                resetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }

        private void AmountTxt_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    dt.Rows.Add(mobileIDTxt.Text, mobileNameTxt.Text, lblInvestorID.Text, investorTxt.Text, purchaseRateTxt.Text, salesRateTxt.Text, qtyTxt.Text, amountTxt.Text);
                    netAmount();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MainClass.dbConnection.Close();
                };
            }
        }

        private void QtyTxt_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double amount = Convert.ToDouble(salesRateTxt.Text) * Convert.ToDouble(qtyTxt.Text);
                amountTxt.Text = amount.ToString();
            }
        }

        private void MobileNameTxt_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    frmSelectMobiles frm = new frmSelectMobiles();
                    frm.ShowDialog();
                    mobileIDTxt.Text = frmSelectMobiles.id;
                    mobileNameTxt.Text = frmSelectMobiles.name;
                    lblInvestorID.Text = frmSelectMobiles.investorID;
                    investorTxt.Text = frmSelectMobiles.investor;
                    purchaseRateTxt.Text = frmSelectMobiles.purchaseRate;
                    salesRateTxt.Text = frmSelectMobiles.saleRate;
                    stockTxt.Text = frmSelectMobiles.stock;
                    qtyTxt.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }

        DataTable dt = new DataTable();

        private void FrmAddPurchase_Load(object sender, EventArgs e)
        {
            try
            {
                if (invoiceIDTxt.Text == "")
                {
                    MainClass.styleDataGridView(dataGridView1);

                    dt.Columns.Add("ID", typeof(int));
                    dt.Columns.Add("Name", typeof(string));
                    dt.Columns.Add("Investor ID", typeof(int));
                    dt.Columns.Add("Investor", typeof(string));
                    dt.Columns.Add("Purchase Rate", typeof(double));
                    dt.Columns.Add("Sale Rate", typeof(double));
                    dt.Columns.Add("Quantity", typeof(int));
                    dt.Columns.Add("Amount", typeof(double));

                    dataGridView1.DataSource = dt;

                    crudOperations.FetchAccountID("select count(purchaseID)+1 as ID from tbl_Purchase", invoiceIDTxt);
                    mobileNameTxt.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }

        // Methods
        public void netAmount()
        {
            int sum = 0;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                sum += Convert.ToInt32(dataGridView1.Rows[i].Cells["Amount"].Value);
            }
            totalTxt.Text = sum.ToString();
        }

        public void resetData()
        {
            crudOperations.FetchAccountID("select count(purchaseID)+1 as ID from tbl_Purchase", invoiceIDTxt);
            mobileIDTxt.Text = mobileNameTxt.Text = investorTxt.Text = lblInvestorID.Text = purchaseRateTxt.Text = salesRateTxt.Text = stockTxt.Text = qtyTxt.Text = amountTxt.Text = "";
            dt.Clear();
            totalTxt.Text = paidTxt.Text = "";
        }
    }
}
