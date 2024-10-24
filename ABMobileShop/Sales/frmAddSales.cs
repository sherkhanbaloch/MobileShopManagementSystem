using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ABMobileShop
{
    public partial class frmAddSales : Form
    {
        CrudOperations crudOperations = new CrudOperations();

        public frmAddSales()
        {
            InitializeComponent();

            this.Load += FrmAddSales_Load;
            planCmb.SelectedIndexChanged += PlanCmb_SelectedIndexChanged;
            btnSave.Click += BtnSave_Click;
            btnPrint.Click += BtnPrint_Click;
            selectCustomerTxt.KeyUp += SelectCustomerTxt_KeyUp;
            selectMobileTxt.KeyUp += SelectMobileTxt_KeyUp;
            timer1.Tick += Timer1_Tick;
            IMEITxt.KeyUp += IMEITxt_KeyUp;
            btnReset.Click += BtnReset_Click;
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            resetData();
        }

        private void IMEITxt_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                advanceTxt.Focus();
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (lblPreviousBalance.ForeColor == Color.Blue || lblAvailableStock.ForeColor == Color.Blue)
            {
                lblPreviousBalance.ForeColor = lblAvailableStock.ForeColor = Color.Red;
            }
            else if (lblPreviousBalance.ForeColor == Color.Red || lblAvailableStock.ForeColor == Color.Red)
            {
                lblPreviousBalance.ForeColor = lblAvailableStock.ForeColor = Color.Blue;
            }
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
                    purchaseRateTxt.Text = frmSelectMobiles.purchaseRate;
                    salesRateTxt.Text = frmSelectMobiles.saleRate;
                    stockTxt.Text = frmSelectMobiles.stock;
                    lblAvailableStock.Text = "Available Stock \n" + frmSelectMobiles.stock;

                    double amount = Convert.ToDouble(salesRateTxt.Text) * Convert.ToDouble(qtyTxt.Text);
                    amountTxt.Text = amount.ToString();
                    IMEITxt.Focus();
                    totalTxt.Text = netAmountTxt.Text = amountTxt.Text;

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
                    careofTxt.Text = frmSelectCustomer.careof;
                    contactTxt.Text = frmSelectCustomer.phone;
                    addressTxt.Text = frmSelectCustomer.address;
                    maxCredit.Text = frmSelectCustomer.maxcredit;
                    previousBalanceTxt.Text = frmSelectCustomer.balance;
                    lblPreviousBalance.Text = "Previous Balance \n" + frmSelectCustomer.balance;
                    selectMobileTxt.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            printReceipt();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                crudOperations.InsertData("insert into tbl_Sales values('" + invoiceIDTxt.Text + "', '" + invoiceDatePicker.Value.ToString("yyy-MM-dd") + "', '" + customerIDTxt.Text + "', '" + mobileIDTxt.Text + "', '" + lblInvestorID.Text + "', '" + IMEITxt.Text + "', '" + purchaseRateTxt.Text + "', '" + salesRateTxt.Text + "', '" + Convert.ToInt32(planCmb.SelectedValue) + "', '" + qtyTxt.Text + "', '" + netAmountTxt.Text + "', '" + advanceTxt.Text + "')");

                if (advanceTxt.Text != "" && Convert.ToInt32(advanceTxt.Text) > 0)
                {
                    crudOperations.InsertData("declare @receiveID as int = (select COUNT(receiveID)+1 from tbl_Receive) insert into tbl_Receive values (@receiveID, '" + invoiceDatePicker.Value + "', '" + customerIDTxt.Text + "', '" + mobileIDTxt.Text + "', '" + lblInvestorID.Text + "', '" + IMEITxt.Text + "', '" + advanceTxt.Text + "', 'Advance Received')");
                }
                printReceipt();
                // resetData();
                selectCustomerTxt.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }

        private void PlanCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            double percentage = Convert.ToDouble(planCmb.SelectedValue) / 100 * Convert.ToDouble(totalTxt.Text);
            double netAmount = Convert.ToDouble(totalTxt.Text) + percentage;
            netAmountTxt.Text = netAmount.ToString();
        }

        private void FrmAddSales_Load(object sender, EventArgs e)
        {
            try
            {
                if (invoiceIDTxt.Text == "")
                {
                    crudOperations.FetchAccountID("select count(saleID)+1 as ID from tbl_Sales", invoiceIDTxt);
                    selectCustomerTxt.Focus();
                    lblUserName.Text = frmLogin.username;
                }
                crudOperations.FetchDataInComboBox("select [Plan Name], Percentage  from vw_ViewPlans", planCmb, "Plan Name", "Percentage");
                planCmb.Text = "--- Select Plan ---";
                timer1.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }

        public void printReceipt()
        {
            lblDateTime.Text = DateTime.Now.ToString();
            lblSaleID.Text = invoiceIDTxt.Text;
            lblCustomerID.Text = customerIDTxt.Text;
            lblCustomerName.Text = customerNameTxt.Text;
            lblPhone.Text = contactTxt.Text;
            lblMobileID.Text = mobileIDTxt.Text;
            lblMobileName.Text = mobileNameTxt.Text;
            lblIMEI.Text = IMEITxt.Text;
            lblRate.Text = salesRateTxt.Text + " x " + qtyTxt.Text;
            lblTotal.Text = totalTxt.Text;
            if (planCmb.Text != "--- Select Plan ---")
            {
                lblInstallment.Text = planCmb.Text;
            }
            else
            {
                lblInstallment.Text = "Cash";
            }
            lblNetAmount.Text = netAmountTxt.Text;
            lblAdvance.Text = advanceTxt.Text;

            var bmp = new Bitmap(this.receiptPanel.Width, this.receiptPanel.Height);
            receiptPanel.DrawToBitmap(bmp, new Rectangle(0, 0, receiptPanel.Width, receiptPanel.Height));
            bmp.Save(@"D:\MSMS\Sale Receipts\" + invoiceIDTxt.Text + ".png", ImageFormat.Png);
        }

        public void resetData()
        {
            crudOperations.FetchAccountID("select count(saleID)+1 as ID from tbl_Sales", invoiceIDTxt);
            selectCustomerTxt.Text = customerIDTxt.Text = customerNameTxt.Text = contactTxt.Text = maxCredit.Text = careofTxt.Text = previousBalanceTxt.Text = addressTxt.Text = "";
            lblPreviousBalance.Text = "Previous Balance \n" + "0";
            selectMobileTxt.Text = mobileIDTxt.Text = mobileNameTxt.Text = investorTxt.Text = lblInvestorID.Text = purchaseRateTxt.Text = salesRateTxt.Text = stockTxt.Text = IMEITxt.Text = qtyTxt.Text = amountTxt.Text = "";
            lblAvailableStock.Text = "Previous Balance \n" + "0";
            totalTxt.Text = netAmountTxt.Text = advanceTxt.Text = remainingTxt.Text = "";
        }
    }
}
