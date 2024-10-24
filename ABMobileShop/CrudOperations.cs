using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace ABMobileShop
{
    class CrudOperations
    {
        public DataTable dt = new DataTable();
        public void FetchDataInGridView(string query, DataGridView dataGridView)
        {
            MainClass.dbConnection.Open();
            SqlDataAdapter da = new SqlDataAdapter(query, MainClass.dbConnection);
            da.Fill(dt);
            dataGridView.DataSource = dt;
            MainClass.dbConnection.Close();
        }

        public void FetchAccountID(string query, TextBox textBox)
        {
            MainClass.dbConnection.Open();
            SqlDataAdapter da = new SqlDataAdapter(query, MainClass.dbConnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            textBox.Text = dt.Rows[0]["ID"].ToString();
            MainClass.dbConnection.Close();
        }

        public void FetchDataInLabel(string query, Label label)
        {
            MainClass.dbConnection.Open();
            SqlDataAdapter da = new SqlDataAdapter(query, MainClass.dbConnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            label.Text = dt.Rows[0][0].ToString();
            MainClass.dbConnection.Close();
        }

        public void InsertData(string query)
        {
            MainClass.dbConnection.Open();
            SqlCommand cmd = new SqlCommand(query, MainClass.dbConnection);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record Saved Successfully.", "Save Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MainClass.dbConnection.Close();
        }

        public void UpdateData(string query)
        {
            MainClass.dbConnection.Open();
            SqlCommand cmd = new SqlCommand(query, MainClass.dbConnection);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record Updated Successfully.", "Update Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MainClass.dbConnection.Close();
        }

        public void DeleteData(string query)
        {
            MainClass.dbConnection.Open();
            SqlCommand cmd = new SqlCommand(query, MainClass.dbConnection);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record Deleted Successfully.", "Delete Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MainClass.dbConnection.Close();
        }

        public void FetchDataInComboBox(string query, ComboBox comboBox, string displayMember, string ValueMember)
        {
            MainClass.dbConnection.Open();
            SqlDataAdapter da = new SqlDataAdapter(query, MainClass.dbConnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox.DisplayMember = displayMember;
            comboBox.ValueMember = ValueMember;
            comboBox.DataSource = dt;
            MainClass.dbConnection.Close();
        }
    }
}
