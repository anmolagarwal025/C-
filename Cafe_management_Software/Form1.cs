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

namespace Cafe_management
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection("Data Source=DESKTOP-33G1OK1; Initial Catalog=Caffe_management; Integrated Security=True");
        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
         
        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            GuestOrder guest = new GuestOrder();
            guest.Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        public static string user;

        private void button1_Click(object sender, EventArgs e)
        {
            user = Uname.Text;

            Con.Open();
            string query = $"Select count(*) from UsersTbl where Uname='{Uname.Text}' and Upassword='{Upassword.Text}'";
            SqlDataAdapter adapter = new SqlDataAdapter(query, Con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                this.Hide();
                UserOrder userorder = new UserOrder();
                userorder.Show();
            }
            else
            {
                MessageBox.Show("Enter Valid Login details");
            }
            Con.Close();
        }

        private void Uname_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
