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
    public partial class frmDashboard : Form
    {
        public frmDashboard()
        {
            InitializeComponent();

            this.Load += FrmDashboard_Load;

            customersPB.Click += CustomersPB_Click;
            mobilesPB.Click += MobilesPB_Click;
            investorPB.Click += InvestorPB_Click;
            viewSalePB.Click += ViewSalePB_Click;
            viewPurchasePB.Click += ViewPurchasePB_Click;
            receivePB.Click += ReceivePB_Click;
            remainingPB.Click += RemainingPB_Click;
            stockPB.Click += StockPB_Click;
            investmenttPB.Click += InvestmenttPB_Click;
            newSalePB.Click += NewSalePB_Click;
            newPurchasePB.Click += NewPurchasePB_Click;
            plansPB.Click += PlansPB_Click;
            ledgerPB.Click += LedgerPB_Click;
            cashbookPB.Click += CashbookPB_Click;
            backupPB.Click += BackupPB_Click;
            refreshPB.Click += RefreshPB_Click;
            exitPB.Click += ExitPB_Click;
        }

        private void ExitPB_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to exit?", "Exit Application", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void RefreshPB_Click(object sender, EventArgs e)
        {

        }

        private void BackupPB_Click(object sender, EventArgs e)
        {

        }

        private void CashbookPB_Click(object sender, EventArgs e)
        {
            frmCashbook frm = new frmCashbook();
            frm.Show();
        }

        private void LedgerPB_Click(object sender, EventArgs e)
        {
            frmCustomerLedger frm = new frmCustomerLedger();
            frm.Show();
        }

        private void PlansPB_Click(object sender, EventArgs e)
        {
            frmPlans frm = new frmPlans();
            frm.Show();
        }

        private void NewPurchasePB_Click(object sender, EventArgs e)
        {
            frmAddPurchase frm = new frmAddPurchase();
            frm.Show();
        }

        private void NewSalePB_Click(object sender, EventArgs e)
        {
            frmAddSales frm = new frmAddSales();
            frm.Show();
        }

        private void InvestmenttPB_Click(object sender, EventArgs e)
        {
            frmInvestment frm = new frmInvestment();
            frm.Show();
        }

        private void StockPB_Click(object sender, EventArgs e)
        {
            frmStock frm = new frmStock();
            frm.Show();
        }

        private void RemainingPB_Click(object sender, EventArgs e)
        {
            frmCustomerRemaining frm = new frmCustomerRemaining();
            frm.Show();
        }

        private void ReceivePB_Click(object sender, EventArgs e)
        {
            frmReceives frm = new frmReceives();
            frm.Show();
        }

        private void ViewPurchasePB_Click(object sender, EventArgs e)
        {
            frmPurchase frm = new frmPurchase();
            frm.Show();
        }

        private void ViewSalePB_Click(object sender, EventArgs e)
        {
            frmSales frm = new frmSales();
            frm.Show();
        }

        private void InvestorPB_Click(object sender, EventArgs e)
        {
            frmInvestors frm = new frmInvestors();
            frm.Show();
        }

        private void MobilesPB_Click(object sender, EventArgs e)
        {
            frmMobiles frm = new frmMobiles();
            frm.Show();
        }

        private void CustomersPB_Click(object sender, EventArgs e)
        {
            frmCustomers frm = new frmCustomers();
            frm.Show();
        }

        CrudOperations crudOperations = new CrudOperations();

        private void FrmDashboard_Load(object sender, EventArgs e)
        {
                MainClass.dbConnection.Close();
                lblUserName.Text = frmLogin.username;
                lblLoginTime.Text = DateTime.Now.ToString();
                MainClass.styleDataGridView(dataGridView1);
                crudOperations.FetchDataInGridView("select ID, Name, Investor, [Available Stock] from vw_MobilesRemaining where [Available Stock] > 0", dataGridView1);
                crudOperations.FetchDataInLabel("select COUNT(*) as Total from tbl_Customers where customerStatus = 1", lblCustomers);
                crudOperations.FetchDataInLabel("select COUNT(*) as Total from tbl_Mobiles where mobileStatus = 1", lblMobiles);
                crudOperations.FetchDataInLabel("select COUNT(*) as Total from tbl_Investors where investorStatus = 1", lblInvestors);
                crudOperations.FetchDataInLabel("select COUNT(*) as Total from tbl_Plans where planStatus = 1", lblPlans);
                crudOperations.FetchDataInLabel("select COUNT(*) as Total from tbl_Users where userStatus = 1", lblUsers);
            try
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }

        private void btnMobileProfit_Click(object sender, EventArgs e)
        {
            frmMobileProfit frm = new frmMobileProfit();
            frm.Show();
        }
    }
}
