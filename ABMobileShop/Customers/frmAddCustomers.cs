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
    public partial class frmAddCustomers : Form
    {
        public frmAddCustomers()
        {
            InitializeComponent();

            this.Load += FrmAddCustomers_Load;
            btnSave.Click += BtnSave_Click;
            btnUpdate.Click += BtnUpdate_Click;
        }

        CrudOperations crudOperations = new CrudOperations();

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                crudOperations.UpdateData("update tbl_Customers set customerName = '" + nameTxt.Text + "', customerCareof = '" + careofTxt.Text + "', customerPhone = '" + phoneTxt.Text + "', customerAddress = '" + addressTxt.Text + "', openingBalance = '" + openingBalanceTxt.Text + "', maxCredit = '" + maxCreditTxt.Text + "' where customerID = '" + idTxt.Text + "'");
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
                crudOperations.InsertData("insert into tbl_Customers values ('" + idTxt.Text + "', '" + nameTxt.Text + "', '" + careofTxt.Text + "', '" + phoneTxt.Text + "', '" + addressTxt.Text + "', '" + openingBalanceTxt.Text + "', '" + maxCreditTxt.Text + "', 1)");
                nameTxt.Text = phoneTxt.Text = openingBalanceTxt.Text = maxCreditTxt.Text = careofTxt.Text = addressTxt.Text = "";
                crudOperations.FetchAccountID("select count(customerID)+1 as ID from tbl_Customers", idTxt);
                nameTxt.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainClass.dbConnection.Close();
            }
        }

        private void FrmAddCustomers_Load(object sender, EventArgs e)
        {
            try
            {
                if (nameTxt.Text == "")
                {
                    crudOperations.FetchAccountID("select count(customerID)+1 as ID from tbl_Customers", idTxt);
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
