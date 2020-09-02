using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test_project
{
    public partial class Form5 : Form
    {
        public byte count = 2;
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            count -= 1;
            label1.Text = Convert.ToString(count);
            if (count == 0)
            {
                timer1.Stop();
                this.Hide();
                form3.Show();
            }
           
        }

        private void Form5_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
