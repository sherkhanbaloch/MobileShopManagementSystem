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
    public partial class frmInvestment : Form
    {
        public frmInvestment()
        {
            InitializeComponent();

            this.Load += FrmInvestment_Load;
            selectInvestorTxt.KeyUp += SelectInvestorTxt_KeyUp;
            btnSave.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                double percentage = Convert.ToDouble(30) / 100 * Convert.ToDouble(receiveTxt.Text);
                double total = Convert.ToDouble(receiveTxt.Text) + percentage;
                crudOperations.InsertData("insert into tbl_Investment values ('" + investmentIDTxt.Text + "', '" + receiveDatePicker.Value.ToString("yyy-MM-dd") + "', '" + investorIDTxt.Text + "', '" + receiveTxt.Text + "', '" + percentage + "', '" + total + "')");
                resetData();
                selectInvestorTxt.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }

        private void SelectInvestorTxt_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    frmSelectInvestor frm = new frmSelectInvestor();
                    frm.ShowDialog();
                    investorIDTxt.Text = frmSelectInvestor.id;
                    investorNameTxt.Text = frmSelectInvestor.name;
                    contactTxt.Text = frmSelectInvestor.phone;
                    careofTxt.Text = frmSelectInvestor.careof;
                    previousBalanceTxt.Text = frmSelectInvestor.balance;
                    receiveTxt.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }

        CrudOperations crudOperations = new CrudOperations();

        private void FrmInvestment_Load(object sender, EventArgs e)
        {
            try
            {
                if (investmentIDTxt.Text == "")
                {
                    MainClass.styleDataGridView(dataGridView1);
                    crudOperations.FetchAccountID("select count(investmentID)+1 as ID from tbl_Investment", investmentIDTxt);
                    crudOperations.FetchDataInGridView("select * from vw_ViewInvestment", dataGridView1);
                    selectInvestorTxt.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }

        public void resetData()
        {
            crudOperations.FetchAccountID("select count(investmentID)+1 as ID from tbl_Investment", investmentIDTxt);
            crudOperations.FetchDataInGridView("select * from vw_ViewInvestment", dataGridView1);
            investorIDTxt.Text = investorNameTxt.Text = contactTxt.Text = careofTxt.Text = previousBalanceTxt.Text = receiveTxt.Text = "";
        }
    }
}
