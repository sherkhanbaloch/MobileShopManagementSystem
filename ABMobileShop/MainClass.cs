using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing;

namespace ABMobileShop
{
    class MainClass
    {
        // Connection String of Database
        public static SqlConnection dbConnection = new SqlConnection("Data Source=" + System.Environment.MachineName + ";Initial Catalog=ABMobileShop;Persist Security Info=True;User ID=\"Afaque Buledi\";Password=afaque@awbsoftwares");

        // Styling Data Grid View
        public static void styleDataGridView(DataGridView dataGridView)
        {
            dataGridView.BackgroundColor = Color.White;
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.RowHeadersVisible = false;
            dataGridView.DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(51, 61, 73);
            dataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.DefaultCellStyle.BackColor = Color.White;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.ReadOnly = true;
        }

        public static void DataGridViewLinkColumn(DataGridView dataGridView, string LinkName, string HeaderText, string LinkText, Color LinkColor)
        {
            DataGridViewLinkColumn linkColumn = new DataGridViewLinkColumn();
            linkColumn.Name = LinkName;
            linkColumn.HeaderText = HeaderText;
            linkColumn.Text = LinkText;
            linkColumn.LinkBehavior = LinkBehavior.HoverUnderline;
            linkColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            linkColumn.LinkColor = LinkColor;
            linkColumn.VisitedLinkColor = LinkColor;
            linkColumn.UseColumnTextForLinkValue = true;
            dataGridView.Columns.Add(linkColumn);
        }
    }
}
