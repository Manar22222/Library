using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{
    public partial class Intro : Form
    {
        public Intro()
        {
            InitializeComponent();
        }

   

        int startpoint = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            startpoint += 1;
            bunifuProgressBar1.Value = startpoint;
            bunifuLabel4.Text = ""+startpoint;

            if (bunifuProgressBar1.Value==100)
            {
                bunifuProgressBar1.Value = 0;
                timer1.Stop();
                UserLogin log = new UserLogin();
                log.Show();
                this.Hide();
            }

        }

        private void Intro_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
