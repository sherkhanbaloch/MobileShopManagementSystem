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
    public partial class frmAddInvestor : Form
    {
        public frmAddInvestor()
        {
            InitializeComponent();

            this.Load += FrmAddInvestor_Load;
            btnSave.Click += BtnSave_Click;
            btnUpdate.Click += BtnUpdate_Click;
        }

        CrudOperations crudOperations = new CrudOperations();

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                crudOperations.UpdateData("update tbl_Investors set investorName = '" + nameTxt.Text + "', investorPhone = '" + phoneTxt.Text + "', investorCareof = '" + careofTxt.Text + "', investorAddress = '" + addressTxt.Text + "', openingBalance = '" + openingBalanceTxt.Text + "' where investorID = '" + idTxt.Text + "'");
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
                crudOperations.InsertData("insert into tbl_Investors values ('" + idTxt.Text + "', '" + nameTxt.Text + "', '" + phoneTxt.Text + "', '" + careofTxt.Text + "', '" + addressTxt.Text + "', '" + openingBalanceTxt.Text + "', 1)");
                nameTxt.Text = phoneTxt.Text = openingBalanceTxt.Text = careofTxt.Text = addressTxt.Text = "";
                crudOperations.FetchAccountID("select count(investorID)+1 as ID from tbl_Investors", idTxt);
                nameTxt.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }

        private void FrmAddInvestor_Load(object sender, EventArgs e)
        {
            try
            {
                if (nameTxt.Text == "")
                {
                    crudOperations.FetchAccountID("select count(investorID)+1 as ID from tbl_Investors", idTxt);
                }
                nameTxt.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }
    }
}
