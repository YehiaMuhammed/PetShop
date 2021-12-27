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
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
            DisplayCustomers();
        }
        private void DisplayCustomers()
        {
            con.Open();
            string Query = "Select * from CustomerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CustomerDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void clear()
        {
            CustName.Text = "";
            CustAdd.Text = "";
            CustPhone.Text = "";
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Yehia Mohamed\source\repos\PetShop\PetShop\PetshopDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (CustName.Text == "" || CustAdd.Text == "" || CustPhone.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Insert into CustomerTbl (CustName, CustAdd, CustPhone) values (@CN, @CA, @CP)", con);
                cmd.Parameters.AddWithValue("@CN", CustName.Text);
                cmd.Parameters.AddWithValue("@CA", CustAdd.Text);
                cmd.Parameters.AddWithValue("@CP", CustPhone.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer Added");
                con.Close();
                DisplayCustomers();
                clear();


            }
        }
        int key = 0;
        private void CustomerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CustName.Text = CustomerDGV.SelectedRows[0].Cells[1].Value.ToString();
            CustAdd.Text = CustomerDGV.SelectedRows[0].Cells[2].Value.ToString();
            CustPhone.Text = CustomerDGV.SelectedRows[0].Cells[3].Value.ToString();
            if (CustName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(CustomerDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (CustName.Text == "" || CustAdd.Text == "" || CustPhone.Text == "")
            {
                MessageBox.Show("Select A Customer");
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Update CustomerTbl set CustName = @CN, CustAdd = @CA, CustPhone = @CP where CustId = @Ckey", con);
                cmd.Parameters.AddWithValue("@CN", CustName.Text);
                cmd.Parameters.AddWithValue("@CA", CustAdd.Text);
                cmd.Parameters.AddWithValue("@CP", CustPhone.Text);
                cmd.Parameters.AddWithValue("@Ckey", key);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer Updated");
                con.Close();
                DisplayCustomers();
                clear();


            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (CustName.Text == "" || CustAdd.Text == "" || CustPhone.Text == "")
            {
                MessageBox.Show("Select A Customer");
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Delete from CustomerTbl where CustId = @Ckey", con);
                cmd.Parameters.AddWithValue("@Ckey", key);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer Deleted");
                con.Close();
                DisplayCustomers();
                clear();


            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Employees obj = new Employees();
            obj.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Products obj = new Products();
            obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }
    }
}
