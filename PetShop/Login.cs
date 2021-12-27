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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Yehia Mohamed\source\repos\PetShop\PetShop\PetshopDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if(uNameTb.Text == "" || PasswordTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                SqlDataAdapter sda = new SqlDataAdapter("Select * from EmployeeTbl where EmpName = '"+ uNameTb.Text.Trim() + "' and EmpPass = '"+PasswordTb.Text.Trim()+"'",con);  
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if(dt.Rows.Count ==1)
                {
                Products obj = new Products();
                    obj.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Wrong Username or Password");
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            uNameTb.Text = "";
            PasswordTb.Text = "";        
        }

        private void label6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
