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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();

            this.Load += FrmLogin_Load;
            btnLogin.Click += BtnLogin_Click;
            btnExit.Click += BtnExit_Click;
        }
        public static string username;
        CrudOperations crudOperations = new CrudOperations();
        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            MainClass.dbConnection.Open();
            string qry = "select * from tbl_Users where userName = '" + userNameTxt.Text + "' and userPassword = '" + passwordTxt.Text + "' and userStatus = 1";
            SqlDataAdapter da = new SqlDataAdapter(qry, MainClass.dbConnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count >= 1)
            {
                frmDashboard frm = new frmDashboard();
                this.Hide();
                username = dt.Rows[0]["userName"].ToString();
                frm.Show();
            }
            else
            {
                MessageBox.Show("User Not Found.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            MainClass.dbConnection.Close();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
