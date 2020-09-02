using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Test_project
{
    public partial class Form4 : Form
    {
        Form1 form1 = new Form1();
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            string[] line_txt = File.ReadAllLines("D:\\keep text\\val_static" + " " + Form1.val_class + ".txt");

            string val_txt = File.ReadAllText("D:\\keep text\\val_static" + " " + Form1.val_class + ".txt");
            lbl_class.Text = Form1.val_class;
            string[] sp_val = new string[line_txt.Length * 8];
            string[,] list = new string[line_txt.Length, 8];
                sp_val = val_txt.Split(' ','\n');
            int index = 0;
            for (int row = 0; row < list.GetLength(0); row++)
            {
                for (int col = 0; col < list.GetLength(1); col++)
                {
                    list[row, col] = sp_val[index];
                    index++;
                }
            }

            for (int row = 0; row < list.GetLength(0); row++)
            {
                string[] grid = new string[list.GetLength(1)];
                for (int col = 0; col < list.GetLength(1); col++)
                    grid[col] = list[row, col];
                dataGridView1.Rows.Add(grid);
            }
        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btn_home_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
    }
}
