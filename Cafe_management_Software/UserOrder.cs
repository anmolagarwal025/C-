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
    public partial class UserOrder : Form
    {
        public UserOrder()
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

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            ItemsForm itemsForm = new ItemsForm();
            itemsForm.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            UsersForm usersForm = new UsersForm();
            usersForm.Show();
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

        private void UserOrder_Load(object sender, EventArgs e)
        {
            populate();
            table.Columns.Add("Item Name", typeof(string));
            table.Columns.Add("Category", typeof(string));
            table.Columns.Add("Price", typeof(int)); 
            table.Columns.Add("Total Price", typeof(int));
            USellerName.Text = Form1.user;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Order Successfull..!!");
            //populate();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            populate();
        }

        //string str = "System.String";
        int sum = 0;

        private void Uordercate_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Uordercate_SelectionChangeCommitted(object sender, EventArgs e)
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

        private void USellerName_TextChanged(object sender, EventArgs e)
        {
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
