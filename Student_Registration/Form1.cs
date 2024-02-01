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

namespace Student_Registration
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Load();
        }

        //SqlConnection con = new SqlConnection("Data Source=DESKTOP-33G1OK1; Initial Catalog=gcbd; User Id=DESKTOP-33G1OK1\\user");
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-33G1OK1; Initial Catalog=gbcd; Integrated Security=True");

        SqlCommand cmd;
        SqlDataReader read;
        string id;
        bool Mode = true;
        string sql;


        public void Load()
        {
            try
            {
                sql = "Select * from students";
                cmd = new SqlCommand(sql, con);
                con.Open();
                read = cmd.ExecuteReader();
                dataGridView1.Rows.Clear();

                while (read.Read())
                {
                    dataGridView1.Rows.Add(read[0], read[1], read[2], read[3]);
                }

                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Get_ID(string id)
        {
            sql = $"select * from students where id={id}";

            cmd = new SqlCommand (sql, con);
            con.Open();
            read = cmd.ExecuteReader();

            while (read.Read() )
            {
                textBox1.Text = read[1].ToString();
                textBox2.Text = read[2].ToString();
                textBox3.Text = read[3].ToString();
            }

            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string course = textBox2.Text;
            string fee = textBox3.Text;

            if (Mode == true)
            {
                sql = "insert into students(name, course, fee) values(@name, @course, @fee)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@fee", fee);
                MessageBox.Show("Data Added...");
                cmd.ExecuteNonQuery(); 

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox1.Focus(); 
            }
            else
            {
                //MessageBox.Show("Can't connect to server..!!");
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "update students set name = @name, course = @course, fee = @fee where id=@id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@fee", fee);
                cmd.Parameters.AddWithValue("@id", id);
                MessageBox.Show("Data Updated");
                cmd.ExecuteNonQuery();

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox1.Focus();

                button1.Text = "Save";
                Mode = true;

            }

            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Column5"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                Get_ID(id);

                button1.Text = "Update";
            }

            else if (e.ColumnIndex == dataGridView1.Columns["Column6"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = $"delete from students where id={id}";
                con.Open();
                cmd = new SqlCommand(sql, con);
                //cmd.Parameters.AddwithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted");
                con.Close() ;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Load();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}