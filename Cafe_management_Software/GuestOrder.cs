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
    public partial class GuestOrder : Form
    {
        public GuestOrder()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection("Data Source=DESKTOP-33G1OK1; Initial Catalog=Caffe_management; Integrated Security=True");

        void populate()
        {
            Con.Open();
            string query = "Select * from Item_Menu";
            SqlDataAdapter adapter = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            ItemsGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        int Num = 0;
        int price, total;
        string Item, category; 

        private void ItemsGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Item = ItemsGV.CurrentRow.Cells[2].Value.ToString();
            category = ItemsGV.CurrentRow.Cells[1].Value.ToString();
            price = Convert.ToInt32(ItemsGV.CurrentRow.Cells[3].Value);
            flag = true;
        }

        DataTable table = new DataTable();
        bool flag = false;

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Con.Open();
            string query = $"Select * from Item_Menu where Item_Category = '{Uordercate.SelectedItem.ToString()}'";
            SqlDataAdapter adapter = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            ItemsGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Console.WriteLine(ItemQuantity.Text.GetType());
            if (ItemQuantity.Text == "" && int.Parse(ItemQuantity.Text) <= 0)
            {
                MessageBox.Show("Enter Valid Quantity..!!");
            }
            else if (flag == false)
            {
                MessageBox.Show("Select the Item ..!!");
            }
            else
            {
                Num = Num + 1;
                total = price * int.Parse(ItemQuantity.Text);
                table.Rows.Add(Item, category, price, total);
                OrdersGV.DataSource = table;
                flag = false;
            }
            sum = sum + total;
            OderValue.Text = Convert.ToString(sum);
        }

        private void label7_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void GuestOrder_Load(object sender, EventArgs e)
        {
            populate();
            table.Columns.Add("Item Name", typeof(string));
            table.Columns.Add("Category", typeof(string));
            table.Columns.Add("Price", typeof(int));
            table.Columns.Add("Total Price", typeof(int));
            OrdersGV.DataSource = table;
        }

        int sum = 0;
    }
}
