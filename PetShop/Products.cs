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

namespace PetShop
{
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
            DisplayProduct();
        }
        private void DisplayProduct()
        {
            con.Open();
            string Query = "Select * from ProductTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProductDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Yehia Mohamed\source\repos\PetShop\PetShop\PetshopDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void label3_Click(object sender, EventArgs e)
        {
            Employees obj = new Employees();
            obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Hide();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (PrNameTb.Text == "" || CatCb.Text == "" || QtyTb.Text == "" || PriceTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Insert into ProductTbl (ProductName, ProductCat, ProductQty, ProductPrice) values (@PN, @PC, @PQ, @PP)", con);
                cmd.Parameters.AddWithValue("@PN", PrNameTb.Text);
                cmd.Parameters.AddWithValue("@PC", CatCb.Text);
                cmd.Parameters.AddWithValue("@PQ", QtyTb.Text);
                cmd.Parameters.AddWithValue("@PP", PriceTb.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Added");
                con.Close();
                DisplayProduct();
                clear();


            }
        }
        private void clear()
        {
            PrNameTb.Text = "";
            CatCb.Text = "";
            QtyTb.Text = "";
            PriceTb.Text = "";
        }
        int key = 0;
        private void ProductDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PrNameTb.Text = ProductDGV.SelectedRows[0].Cells[1].Value.ToString();
            CatCb.Text = ProductDGV.SelectedRows[0].Cells[2].Value.ToString();
            QtyTb.Text = ProductDGV.SelectedRows[0].Cells[3].Value.ToString();
            PriceTb.Text = ProductDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (PrNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(ProductDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {

            if (PrNameTb.Text == "" || CatCb.Text == "" || QtyTb.Text == "" || PriceTb.Text == "")
            {
                MessageBox.Show("Select A Product");
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Update ProductTbl set ProductName = @PN, ProductCat = @PC, ProductQty = @PQ, ProductPrice = @PP where ProductId = @Pkey", con);
                cmd.Parameters.AddWithValue("@PN", PrNameTb.Text);
                cmd.Parameters.AddWithValue("@PC", CatCb.Text);
                cmd.Parameters.AddWithValue("@PQ", QtyTb.Text);
                cmd.Parameters.AddWithValue("@PP", PriceTb.Text);
                cmd.Parameters.AddWithValue("@Pkey", key);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Updated");
                con.Close();
                DisplayProduct();
                clear();
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {

            if (PrNameTb.Text == "" || CatCb.Text == "" || QtyTb.Text == "" || PriceTb.Text == "")
            {
                MessageBox.Show("Select A Product");
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Delete From ProductTbl where ProductId = @Pkey", con);
                cmd.Parameters.AddWithValue("@Pkey", key);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Deleted");
                con.Close();
                DisplayProduct();
                clear();
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }
    }
}
