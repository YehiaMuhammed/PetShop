using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetShop
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
            timer1.Start(); 
        }
        int startP =0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            startP += 1;
            Myprogress.Value = startP;
            Percent.Text = startP + "%"; 
            if(Myprogress.Value == 100)
            {
                Myprogress.Value = 0;
                Login obj = new Login();
                obj.Show();
                this.Hide();
                timer1.Stop();
            }
        }
    }
}
