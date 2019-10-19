using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WS
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CSVParser.Parser("user_data.csv");
            //Method();
        }

        private void Method()
        {
            session1DataSetTableAdapters.UsersTableAdapter adapter = new session1DataSetTableAdapters.UsersTableAdapter();
            var officeData = adapter.GetData();
            var s = (from i in officeData                    
                      select i);
            string str = "";
            foreach (var i in s)
            {
                str += i.Password + "  ";
            }
            MessageBox.Show(str);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.Show();
            this.Hide();
        }
    }
}
