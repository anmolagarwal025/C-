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
//using System.Data.SqlClient;

namespace Cafe_management
{
    public partial class ItemsForm : Form
    {
        public ItemsForm()
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
            UserOrder userOrder = new UserOrder();
            userOrder.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "insert into Item_Menu(Item_Number, Item_Category, Item_Name, Item_Price) values(@Item_Number, @Item_Category, @Item_Name, @Item_Price)";
            Con.Open();
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.Parameters.AddWithValue("@Item_Number", UitemNumber.Text);
            cmd.Parameters.AddWithValue("@Item_Category", Uitemcategory.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@Item_Name", UitemName.Text);
            cmd.Parameters.AddWithValue("@Item_Price", UitemPrice.Text);
            MessageBox.Show("New Item Added Successfully!!");
            cmd.ExecuteNonQuery();
            Con.Close();
            populate();

            UitemNumber.Clear();
            //Uitemcategory.SelectedItem.Clear(); 
            UitemName.Clear();
            UitemPrice.Focus();
        } 

        private void ItemsGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            UitemNumber.Text = ItemsGV.CurrentRow.Cells[0].Value.ToString();
            Uitemcategory.SelectedItem = ItemsGV.CurrentRow.Cells[1].Value.ToString();
            UitemName.Text = ItemsGV.CurrentRow.Cells[2].Value.ToString();
            UitemPrice.Text = ItemsGV.CurrentRow.Cells[3].Value.ToString();
        }

        private void ItemsForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string query = $"delete from Item_Menu where Item_Number={UitemNumber.Text}";
            Con.Open();
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Item Deleted Successfully..!!");
            Con.Close();
            populate();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string query = $"update Item_Menu set Item_Category=@Item_Category, Item_Name=@Item_Name, " +
                $"Item_Price=@Item_Price where Item_Number={UitemNumber.Text}";
            Con.Open();
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.Parameters.AddWithValue("@Item_Category", Uitemcategory.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@Item_Name", UitemName.Text);
            cmd.Parameters.AddWithValue("@Item_Price", UitemPrice.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Item Updated Successfully!!!");
            Con.Close();
            populate();
        }
    }
}
