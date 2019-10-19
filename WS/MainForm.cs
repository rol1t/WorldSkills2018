using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WS
{
    public partial class MainForm : Form
    {
        public string connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=session1.mdf";
        public MainForm()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM User"; // запрос
            OleDbDataAdapter dataAdapter;
            dataAdapter = new OleDbDataAdapter(sql, connStr);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);

            dataGridView1.DataSource = dt;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}
