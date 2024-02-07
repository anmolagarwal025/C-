using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace Cafe_management
{
    public partial class UsersForm : Form
    {
        public UsersForm()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection("Data Source=DESKTOP-33G1OK1; Initial Catalog=Caffe_management; Integrated Security=True");

        void populate()
        {
            Con.Open();
            string query = "Select * from UsersTbl";
            SqlDataAdapter adapter = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            UsersGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserOrder userOrder = new UserOrder();
            userOrder.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*Con.Open();
            string query = "insert into UsersTbl values()";
            SqlCommand cmd = new SqlCommand(query,Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("New User Added Successfully!!"); 
            Con.Close();
*/

            string query = "insert into UsersTbl(Uname, Upassword, Uphone) values(@Uname, @Upassword, @Uphone)";
            Con.Open();
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.Parameters.AddWithValue("@Uname", Unametb.Text);
            cmd.Parameters.AddWithValue("@Upassword", Upasstb.Text);
            cmd.Parameters.AddWithValue("@Uphone", Uphonetb.Text);
            MessageBox.Show("New User Added Successfully!!");
            cmd.ExecuteNonQuery();
            Con.Close();
            populate();

            Unametb.Clear();
            Upasstb.Clear();
            Uphonetb.Clear();
            Unametb.Focus();
        }

        private void UsersForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void UsersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Unametb.Text = UsersGV.SelectedRows[0].Cells[0].Value.ToString();
            Upasstb.Text = UsersGV.SelectedRows[0].Cells[1].Value.ToString();
            Uphonetb.Text = UsersGV.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //string id = UsersGV.CurrentRow.Cells[0].Value.ToString();
            string query = $"delete from UsersTbl where Uphone='{Uphonetb.Text}'";
            Con.Open();
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Record Deleted");
            Con.Close();
            populate();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string query = $"update UsersTbl set Uname=@Uname, Upassword=@Upassword where Uphone='{Uphonetb.Text}'";
            Con.Open();
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.Parameters.AddWithValue("@Uname", Unametb.Text);
            cmd.Parameters.AddWithValue("@Upassword", Upasstb.Text);
            //cmd.Parameters.AddWithValue("@Uphone", Uphonetb.Text);
            MessageBox.Show("User Updated Successfully!!");
            cmd.ExecuteNonQuery();
            Con.Close();
            populate();

            Unametb.Clear();
            Upasstb.Clear();
            Uphonetb.Clear();
            Unametb.Focus();
        }

        private void Unametb_TextChanged(object sender, EventArgs e)
        {

        }

        private void Upasstb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
