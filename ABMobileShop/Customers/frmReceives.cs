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
    public partial class frmReceives : Form
    {
        public frmReceives()
        {
            InitializeComponent();

            this.Load += FrmReceives_Load;
            IMEITxt.KeyUp += IMEITxt_KeyUp;
            receiveTxt.KeyUp += ReceiveTxt_KeyUp;
            btnSave.Click += BtnSave_Click;
            selectCustomerTxt.KeyUp += SelectCustomerTxt_KeyUp;
            selectMobileTxt.KeyUp += SelectMobileTxt_KeyUp;
            btnReset.Click += BtnReset_Click;
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            resetData();
        }

        private void SelectMobileTxt_KeyUp(object sender, KeyEventArgs e)
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
                    IMEITxt.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }

        private void SelectCustomerTxt_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    frmSelectCustomer frm = new frmSelectCustomer();
                    frm.ShowDialog();
                    customerIDTxt.Text = frmSelectCustomer.id;
                    customerNameTxt.Text = frmSelectCustomer.name;
                    contactTxt.Text = frmSelectCustomer.phone;
                    careofTxt.Text = frmSelectCustomer.careof;
                    previousBalanceTxt.Text = frmSelectCustomer.balance;
                    mobileNameTxt.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }

        private void ReceiveTxt_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int remaining = Convert.ToInt32(previousBalanceTxt.Text) - Convert.ToInt32(receiveTxt.Text);
                remainingTxt.Text = remaining.ToString();
            }
        }

        CrudOperations crudOperations = new CrudOperations();

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                crudOperations.InsertData("insert into tbl_Receive values ('" + receiveIDTxt.Text + "', '" + receiveDatePicker.Value.ToString("yyy-MM-dd") + "', '" + customerIDTxt.Text + "', '" + mobileIDTxt.Text + "', '" + lblInvestorID.Text + "', '" + IMEITxt.Text + "', '" + receiveTxt.Text + "', '" + descriptionTxt.Text + "')");
                resetData();
                selectCustomerTxt.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }


        private void IMEITxt_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                receiveTxt.Focus();
            }
        }

        private void FrmReceives_Load(object sender, EventArgs e)
        {
            try
            {
                if (receiveIDTxt.Text == "")
                {
                    MainClass.styleDataGridView(dataGridView1);
                    resetData();
                    selectCustomerTxt.Focus();
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
            crudOperations.FetchAccountID("select count(receiveID)+1 as ID from tbl_Receive", receiveIDTxt);
            crudOperations.dt.Clear();
            crudOperations.FetchDataInGridView("select * from vw_ViewReceived", dataGridView1);
            selectCustomerTxt.Text = customerIDTxt.Text = customerNameTxt.Text = careofTxt.Text = previousBalanceTxt.Text = "";
            selectMobileTxt.Text = mobileIDTxt.Text = mobileNameTxt.Text = investorTxt.Text = lblInvestorID.Text = IMEITxt.Text = "";
            receiveTxt.Text = remainingTxt.Text = descriptionTxt.Text = "";
        }
    }
}
