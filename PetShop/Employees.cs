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
    public partial class Employees : Form
    {
        public Employees()
        {
            InitializeComponent();
            DisplayEmployees();
        }

      

        private void DisplayEmployees()
        {
            con.Open();
            string Query = "Select * from EmployeeTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            EmployeesDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Yehia Mohamed\source\repos\PetShop\PetShop\PetshopDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if(EmpNameTb.Text == "" || EmpAddTb.Text == "" || EmpPhoneTb.Text == "" || EmpPasswordTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Insert into EmployeeTbl (EmpName, EmpAdd, EmpPhone, EmpPass) values (@EN, @EA, @EP, @EPa)", con);
                cmd.Parameters.AddWithValue("@EN", EmpNameTb.Text);
                cmd.Parameters.AddWithValue("@EA", EmpAddTb.Text);
                cmd.Parameters.AddWithValue("@EP", EmpPhoneTb.Text);
                cmd.Parameters.AddWithValue("@EPa", EmpPasswordTb.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Employee Added");
                con.Close();
                DisplayEmployees();
                clear();
                
                
            }
        }
        private void clear()
        {
            EmpNameTb.Text = "";
            EmpAddTb.Text = "";
            EmpPhoneTb.Text = "";
            EmpPasswordTb.Text = "";
        }
        int key = 0;
        private void EmployeesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EmpNameTb.Text = EmployeesDGV.SelectedRows[0].Cells[1].Value.ToString();
            EmpAddTb.Text = EmployeesDGV.SelectedRows[0].Cells[2].Value.ToString();
            EmpPhoneTb.Text = EmployeesDGV.SelectedRows[0].Cells[3].Value.ToString();
            EmpPasswordTb.Text = EmployeesDGV.SelectedRows[0].Cells[4].Value.ToString();
            if(EmpNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(EmployeesDGV.SelectedRows[0].Cells[0].Value.ToString());
            }

        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (EmpNameTb.Text == "" || EmpAddTb.Text == "" || EmpPhoneTb.Text == "" || EmpPasswordTb.Text == "")
            {
                MessageBox.Show("Select An Employee");
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Update EmployeeTbl set EmpName = @EN, EmpAdd = @EA, EmpPhone = @EP, EmpPass = @EPa where EmpNum = @Ekey", con);
                cmd.Parameters.AddWithValue("@EN", EmpNameTb.Text);
                cmd.Parameters.AddWithValue("@EA", EmpAddTb.Text);
                cmd.Parameters.AddWithValue("@EP", EmpPhoneTb.Text);
                cmd.Parameters.AddWithValue("@EPa", EmpPasswordTb.Text);
                cmd.Parameters.AddWithValue("@Ekey", key);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Employee Updated");
                con.Close();
                DisplayEmployees();
                clear();


            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (EmpNameTb.Text == "" || EmpAddTb.Text == "" || EmpPhoneTb.Text == "" || EmpPasswordTb.Text == "")
            {
                MessageBox.Show("Select An Employee");
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Delete from EmployeeTbl where EmpNum = @Ekey", con);
                cmd.Parameters.AddWithValue("@Ekey", key);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Employee Deleted");
                con.Close();
                DisplayEmployees();
                clear();


            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();
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
