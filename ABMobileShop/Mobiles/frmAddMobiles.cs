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
    public partial class frmAddMobiles : Form
    {
        public frmAddMobiles()
        {
            InitializeComponent();

            this.Load += FrmAddMobiles_Load;
            btnSave.Click += BtnSave_Click;
            btnUpdate.Click += BtnUpdate_Click;
        }

        CrudOperations crudOperations = new CrudOperations();

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                crudOperations.UpdateData("update tbl_Mobiles set mobileName = '" + nameTxt.Text + "', investorID = '" + Convert.ToInt32(investorCmb.SelectedValue) + "', purchaseRate = '" + purchaseRateTxt.Text + "', saleRate = '" + saleRateTxt.Text + "', openingStock = '" + openingStockTxt.Text + "' where mobileID = '" + idTxt.Text + "'");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                crudOperations.InsertData("insert into tbl_Mobiles values ('" + idTxt.Text + "', '" + nameTxt.Text + "', '" + Convert.ToInt32(investorCmb.SelectedValue) + "', '" + purchaseRateTxt.Text + "', '" + saleRateTxt.Text + "', '" + openingStockTxt.Text + "', 1)");
                nameTxt.Text = investorCmb.Text = purchaseRateTxt.Text = saleRateTxt.Text = openingStockTxt.Text = "";
                crudOperations.FetchAccountID("select count(mobileID)+1 as ID from tbl_Mobiles", idTxt);
                nameTxt.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }

        private void FrmAddMobiles_Load(object sender, EventArgs e)
        {
            try
            {
                if (nameTxt.Text == "")
                {
                    crudOperations.FetchAccountID("select count(mobileID)+1 as ID from tbl_Mobiles", idTxt);
                    nameTxt.Focus();
                }
                crudOperations.FetchDataInComboBox("select ID, Name from vw_ViewInvestors", investorCmb, "Name", "ID");
                investorCmb.Text = "--- Select Investor ---";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }

    }
}
