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
    public partial class Form3 : Form
    {
        Form1 form1 = new Form1();
        // line_txt ทำหน้าที่นับจำนวนบรรทัดในไฟล์รายชื่อนิสิต
        private string[] line_txt = File.ReadAllLines("D:\\keep text\\list_stu_Data Structures.txt");
    
        public Form3()
        {
            InitializeComponent();
        }     
        private void Load_Grid()
        {

            /* lbl_class.Text รับค่าแสดงรายวิชามาแสดงใน label
             * lbl_time.Text รับค่ามาแสดงเวลาในในคาบเรียนของวิชานั้นๆ
             * val_txt อ่านไฟล์ทั้งหมดใน text รานชื่อนิสิต
             * sp_val จะเก็บคำที่ split ที่มาจาก val_txt ซึ่งมี sp_val มีขนาด แถว*หลัก 
             * list เป็น array multi จะเก็บข้อเพื่อไปแสดงใน datagridview 
             * index ทำหน้าที่เป็นตัวหมุนข้อมูล sp_val
             */
            Form1 form1 = new Form1();
            lbl_class.Text = Form1.val_class;
            lbl_time.Text = form1.time_split(Form1.val_date_check);
            string val_txt = File.ReadAllText("D:\\keep text\\list_stu_Data Structures.txt");
            string[] sp_val = new string[line_txt.Length * 4];
            string[,] list = new string[line_txt.Length, 4];
            sp_val = val_txt.Split(' ');
            int index = 0;


             /* GetLength(0) ทำหน้าที่อ่านแถวในข้อมูล multi array
              * GetLength(1) ทำหน้าที่อ่านหลักหรือคอลัมน์ในข้อมูล multi array 
              * for loop นี้จะทำหน้าที่นำข้อมูลในไฟล์ที่ sp_val นำมาใส่ใน list ให้อยู่ในรูป array 2 มิติ*/
            for (int row = 0; row < list.GetLength(0); row++)
            {
                for (int col = 0; col < list.GetLength(1); col++)
                {
                    list[row, col] = sp_val[index];
                    index++;
                }
            }

            // for loop แสดงรายชื่อนิสิต โชว์ใน datagridview
            for (int row = 0; row < list.GetLength(0); row++)
            {
                string[] grid = new string[list.GetLength(1)];
                for (int col = 0; col < list.GetLength(1); col++)
                    grid[col] = list[row, col];
                dataGridView1.Rows.Add(grid);
            }
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            Load_Grid(); 
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

       
        /***************************ปุ่มบันทึกข้อมูล*************************************/
        private void button1_Click(object sender, EventArgs e)
        {
            /* n_type ทำหน้าที่นับว่า เข้าเรียน ลาป่วย ลากิจ ขาด ร่วมกิจกรรม มีทั้งหมดอย่างละกี่คน 
             * n_type[0] คือ จำนวนคนเข้าเรียน
             * n_type[1] คือ จำนวนคนลาป่วย
             * n_type[2] คือ จำนวนคนลากิจ
             * n_type[3] คือ จำนวนคนขาด
             * n_type[4] คือ จำนวนคนร่วมกิจกรรม
             * total ทำหน้าที่นับคนทั้งหมดในคาบนั้น
             * StreamWriter wr จะสร้างไฟล์สำหรับเช็คคนเข้าเรียน ซึ่งเป็นแบบเขียนต่อไฟล์(append)
             * เริ่มแรก wr จะเขียนไฟล์ เป็น วัน_เดือน_ปี_ชั่วโมงนาทีเริ่มต้น_ชั่วโมงนาทีจบ
             * val_check ทำหน้าที่รับข้อมูลจาก grid มา และเช็คว่าเหมือนกับ n_type
             * for loop นี้จะทำการเขียนไฟล์ต่อว่าเราได้เช็ดแบบไหนไปบ้างเช่น เข้าเรียน ลาป่วย เป็นต้น*/
            int[] n_type = new int[] { 0,0,0,0,0};
            string[] val_check = new string[line_txt.Length];
            StreamWriter wr = new StreamWriter("D:\\keep text\\val_check" + " " + Form1.val_class + ".txt",true);
            wr.Write(Form1.val_date+"_"+ form1.time_split(Form1.time_etc) + ">");   
            
            for (int i = 0; i < line_txt.Length; i++)
            {
                val_check[i] = (string)dataGridView1.Rows[i].Cells["grid_check"].Value;
                wr.Write(" "+val_check[i]);
                if (val_check[i].Equals("เข้าเรียน"))
                    n_type[0]++;
                else if (val_check[i].Equals("ลาป่วย"))
                    n_type[1]++;
                else if (val_check[i].Equals("ลากิจ"))
                    n_type[2]++;
                else if (val_check[i].Equals("ขาด"))
                    n_type[3]++;
                else if (val_check[i].Equals("เข้าร่วมกิจกรรม"))
                    n_type[4]++;

            }
            wr.WriteLine();
            wr.Close();

            /* StreamWriter wr_1 ทำหน้าที่เขียนไฟล์เก็บค่า สถิติ แบบฟอร์ม คือ
             * วัน/เดือน/ปี ชัวโมง.นาที-ชั่วโมง.นาที */
            StreamWriter wr_1 = new StreamWriter("D:\\keep text\\val_static" + " " + Form1.val_class + ".txt", true);
            wr_1.Write(Form1.val_date_check
               + " " + n_type[0] + " " + n_type[1] + " " + n_type[2]
               + " " + n_type[3] + " " + n_type[4] + " " + line_txt.Length);   
            wr_1.WriteLine();
            wr_1.Close();
            MessageBox.Show("บันทึกเรียบร้อย");
            btn_save.Visible = false;
            btn_increase.Visible = false;
            btn_delete.Visible = false;
        }

        private void btn_increase_Click(object sender, EventArgs e)
        {
            /* list อ่านไฟล์ทั้งหมดและเก็บค่าไว้
             * list_array อ่านไฟลืตามจำนวนบรรทัด
             * reserve เก็บค่าจาก list โดย split
             * list_multi เก็บรายชื่อให้อยุ่ใน array 2 มืติ แถวบวก 1 เพราะมีสมาชิกเพิ่มสมาชิกมา 1 ตัว จากการกดปุ่มนี้
             * index เอาไว้หมุนช้อมูลใน reserve
             * for loop นี้จะทำหน้าที่นำข้อมูลในไฟล์ที่ reserve นำมาใส่ใน list_multi ให้อยู่ในรูป array 2 มิติ
             * โดย list_multi.GetLength(0) - 1 เอาแค่ข้อมูลเดิมมาก่อน เพราะ ตำแหน่งสุดท้าย คือ NULL ที่พึ่งถูกสร้างขึ้นมา */
            if (!String.IsNullOrEmpty(txt_order.Text) && !String.IsNullOrEmpty(txt_pas.Text) && !String.IsNullOrEmpty(txt_fname.Text) && !String.IsNullOrEmpty(txt_lname.Text))
            {
                string list = File.ReadAllText("D:\\keep text\\list_stu_Data Structures.txt");
                string[] list_arry = File.ReadAllLines("D:\\keep text\\list_stu_Data Structures.txt");
                string[] reserve = list.Split(' ');
                string[,] list_multi = new string[list_arry.Length + 1, 4];
                int index = 0;
                for (int row = 0; row < list_multi.GetLength(0) - 1; row++)
                {
                    for (int col = 0; col < list_multi.GetLength(1); col++)
                    {
                        list_multi[row, col] = reserve[index];
                        index++;
                    }
                }

                /* tmp_insert เก็บค่าจากลำดับที่ใน textbox โดยจะลบ1 เพราะต้องการตำแหน่งอินเด็กซ์
                 * StreamWriter wr ทำหน้าที่เขียนไฟล์รายชื่อนิสิตขึ้นมาใหม่หลังจากมีการบันทึกเพิ่มนิสิต
                 * for loop สำหรับแทรกรายชื่อนิสิต
                 * idea คือ ถ้าเราต้องการเพิ่มในลำดับที่ 2(index ที่ 1) เราจะเลื่อนลำดับสุดท้ายออกไป 
                 * และเลื่อนออกไปจนถึง ลำดับที่ 2 แล้วจึงหยุด และบวกตำแหน่งที่ถูกเลือกอีก +1 เพื่อให้ถูกต้อง */
                int tmp_insert = Convert.ToInt32(txt_order.Text) - 1;
                StreamWriter wr = new StreamWriter("D:\\keep text\\list_stu_Data Structures.txt");
                for (int row = list_multi.GetLength(0) - 1; row > tmp_insert; row--)
                {
                    for (int col = 0; col < list_multi.GetLength(1); col++)
                    {
                        list_multi[row, col] = list_multi[row - 1, col];
                        if (row > tmp_insert && col == 0)
                            list_multi[row, col] = (Convert.ToInt32(list_multi[row, col]) + 1).ToString();

                    }
                }

                /* เป็นรูปแบบฟอร์ม text ที่ได้กำหนดไว้ ว่าค่าแรกในบรรทัด ลำดับจะติดกับขอบ ตามที่ wr ได้เขียน
                 * for loop นี้ทำหน้ที่เขียน text โดยเริ่มตั้งแต่บรรทัดที่ 2 */
                if (tmp_insert >= list_multi.GetLength(0) - 1)
                {
                    wr.Write(list_multi[0, 0] + " " + list_multi[0, 1] + " " + list_multi[0, 2] + " " + list_multi[0, 3]);
                    for (int row = 1; row < list_multi.GetLength(0) - 1; row++)
                    {
                        for (int col = 0; col < list_multi.GetLength(1); col++)
                            wr.Write(" " + list_multi[row, col]);
                    }
                    wr.Write("\n " + list_multi.GetLength(0) + " " + txt_pas.Text + " " + txt_fname.Text + " " + txt_lname.Text);
                    wr.Close();
                }
                else
                {
                    list_multi[tmp_insert, 0] = txt_order.Text;
                    list_multi[tmp_insert, 1] = txt_pas.Text;
                    list_multi[tmp_insert, 2] = txt_fname.Text;
                    list_multi[tmp_insert, 3] = txt_lname.Text + "\n";
                    wr.Write(list_multi[0, 0] + " " + list_multi[0, 1] + " " + list_multi[0, 2] + " " + list_multi[0, 3]);
                    for (int row = 1; row < list_multi.GetLength(0); row++)
                    {
                        for (int col = 0; col < list_multi.GetLength(1); col++)
                            wr.Write(" " + list_multi[row, col]);
                    }
                    wr.Close();

                }
                btn_update.Visible = true;
                btn_delete.Visible = false;
                btn_save.Visible = false;
                dataGridView1.Visible = false;
                MessageBox.Show("กรุณากดปุ่มอัพเดท");
            }
            else MessageBox.Show("กรุณากรอกข้อมูลให้ครบ");
        }

        /*************************ปุ่มอัพเดท**********************************/
        private void btn_list_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
            this.Hide();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {

            /* list อ่านไฟล์ทั้งหมดและเก็บค่าไว้
            * list_array อ่านไฟลืตามจำนวนบรรทัด
            * reserve เก็บค่าจาก list โดย split
            * list_multi เก็บรายชื่อให้อยุ่ใน array 2 มืติ
            * index เอาไว้หมุนช้อมูลใน reserve
            * for loop นี้จะทำหน้าที่นำข้อมูลในไฟล์ที่ reserve นำมาใส่ใน list_multi ให้อยู่ในรูป array 2 มิติ*/
            string list = File.ReadAllText("D:\\keep text\\list_stu_Data Structures.txt");
            string[] list_arry = File.ReadAllLines("D:\\keep text\\list_stu_Data Structures.txt");

            if (!String.IsNullOrEmpty(txt_order.Text))
            {
                if (Convert.ToInt32(txt_order.Text) <= list_arry.Length)
                {
                    string[] reserve = list.Split(' ');
                    string[,] list_multi = new string[list_arry.Length, 4];
                    int index = 0;
                    for (int row = 0; row < list_multi.GetLength(0); row++)
                    {
                        for (int col = 0; col < list_multi.GetLength(1); col++)
                        {
                            list_multi[row, col] = reserve[index];
                            index++;
                        }
                    }


                    /* tmp_insert เก็บค่าจากลำดับที่ใน textbox โดยจะลบ1 เพราะต้องการตำแหน่งอินเด็กซ์
                    * StreamWriter wr ทำหน้าที่เขียนไฟล์รายชื่อนิสิตขึ้นมาใหม่หลังจากมีการลบนิสิต
                    * for loop สำหรับลบรายชื่อนิสิต
                    * idea คือ ถ้าเราต้องการลบในลำดับที่ 2(index ที่ 1) เราจะเลื่อนลำดับที่3 เข้ามาในำลำดับที่ 2 
                    * และเลื่อนเข้ามาตามลำดับ จนถึงตัวสุดท้ายแล้วจุงหยุด */
                    int tmp_delete = Convert.ToInt32(txt_order.Text) - 1;
                    StreamWriter wr = new StreamWriter("D:\\keep text\\list_stu_Data Structures.txt");
                    for (int row = tmp_delete; row < list_multi.GetLength(0) - 1; row++)
                    {
                        for (int col = 0; col < list_multi.GetLength(1); col++)
                        {
                            list_multi[row, col] = list_multi[row + 1, col];
                            if (row >= tmp_delete && col == 0)
                                list_multi[row, col] = (Convert.ToInt32(list_multi[row, col]) - 1).ToString();
                        }
                    }

                    /* เป็นรูปแบบฟอร์ม text ที่ได้กำหนดไว้ ว่าค่าแรกในบรรทัด ลำดับจะติดกับขอบ ตามที่ wr ได้เขียน
                     * for loop นี้ทำหน้ที่เขียน text โดยเริ่มตั้งแต่บรรทัดที่ 2 */
                    wr.Write(list_multi[0, 0] + " " + list_multi[0, 1] + " " + list_multi[0, 2] + " " + list_multi[0, 3]);
                    for (int row = 1; row < list_multi.GetLength(0) - 1; row++)
                    {
                        for (int col = 0; col < list_multi.GetLength(1); col++)
                            wr.Write(" " + list_multi[row, col]);
                    }
                    wr.Close();
                    btn_update.Visible = true;
                    btn_delete.Visible = false;
                    btn_save.Visible = false;
                    dataGridView1.Visible = false;
                    MessageBox.Show("กรุณากดปุ่มอัพเดท");
                }
                else MessageBox.Show("ไม่มีลำดับที่ " + txt_order.Text + " ในรายชื่อ");
                //string[] reserve = list.Split(' ');
                //string[,] list_multi = new string[list_arry.Length, 4];
                //int index = 0;
                //for (int row = 0; row < list_multi.GetLength(0); row++)
                //{
                //    for (int col = 0; col < list_multi.GetLength(1); col++)
                //    {
                //        list_multi[row, col] = reserve[index];
                //        index++;
                //    }
                //}


                ///* tmp_insert เก็บค่าจากลำดับที่ใน textbox โดยจะลบ1 เพราะต้องการตำแหน่งอินเด็กซ์
                //* StreamWriter wr ทำหน้าที่เขียนไฟล์รายชื่อนิสิตขึ้นมาใหม่หลังจากมีการลบนิสิต
                //* for loop สำหรับลบรายชื่อนิสิต
                //* idea คือ ถ้าเราต้องการลบในลำดับที่ 2(index ที่ 1) เราจะเลื่อนลำดับที่3 เข้ามาในำลำดับที่ 2 
                //* และเลื่อนเข้ามาตามลำดับ จนถึงตัวสุดท้ายแล้วจุงหยุด */
                //int tmp_delete = Convert.ToInt32(txt_order.Text) - 1;
                //StreamWriter wr = new StreamWriter("D:\\keep text\\list_stu_Data Structures.txt");
                //for (int row = tmp_delete; row < list_multi.GetLength(0) - 1; row++)
                //{
                //    for (int col = 0; col < list_multi.GetLength(1); col++)
                //    {
                //        list_multi[row, col] = list_multi[row + 1, col];
                //        if (row >= tmp_delete && col == 0)
                //            list_multi[row, col] = (Convert.ToInt32(list_multi[row, col]) - 1).ToString();
                //    }
                //}

                ///* เป็นรูปแบบฟอร์ม text ที่ได้กำหนดไว้ ว่าค่าแรกในบรรทัด ลำดับจะติดกับขอบ ตามที่ wr ได้เขียน
                // * for loop นี้ทำหน้ที่เขียน text โดยเริ่มตั้งแต่บรรทัดที่ 2 */
                //wr.Write(list_multi[0, 0] + " " + list_multi[0, 1] + " " + list_multi[0, 2] + " " + list_multi[0, 3]);
                //for (int row = 1; row < list_multi.GetLength(0) - 1; row++)
                //{
                //    for (int col = 0; col < list_multi.GetLength(1); col++)
                //        wr.Write(" " + list_multi[row, col]);
                //}
                //wr.Close();
                //btn_update.Visible = true;
                //btn_delete.Visible = false;
                //btn_save.Visible = false;
                //dataGridView1.Visible = false;
                //MessageBox.Show("กรุณากดปุ่มอัพเดท");
            }
            else MessageBox.Show("กรุณากรอกข้อมูลให้ถูกต้อง");
            //for (int row = 0; row < list_multi.GetLength(0); row++)
            //{
            //    for (int col = 0; col < list_multi.GetLength(1); col++)
            //    {
            //        list_multi[row, col] = reserve[index];
            //        index++;
            //    }
            //}


            ///* tmp_insert เก็บค่าจากลำดับที่ใน textbox โดยจะลบ1 เพราะต้องการตำแหน่งอินเด็กซ์
            //* StreamWriter wr ทำหน้าที่เขียนไฟล์รายชื่อนิสิตขึ้นมาใหม่หลังจากมีการลบนิสิต
            //* for loop สำหรับลบรายชื่อนิสิต
            //* idea คือ ถ้าเราต้องการลบในลำดับที่ 2(index ที่ 1) เราจะเลื่อนลำดับที่3 เข้ามาในำลำดับที่ 2 
            //* และเลื่อนเข้ามาตามลำดับ จนถึงตัวสุดท้ายแล้วจุงหยุด */
            //int tmp_delete = Convert.ToInt32(txt_order.Text) - 1;
            //StreamWriter wr = new StreamWriter("D:\\keep text\\list_stu_Data Structures.txt");
            //for (int row = tmp_delete; row < list_multi.GetLength(0) - 1; row++)
            //{
            //    for (int col = 0; col < list_multi.GetLength(1); col++)
            //    {
            //        list_multi[row, col] = list_multi[row + 1, col];
            //        if (row >= tmp_delete && col == 0)       
            //            list_multi[row, col] = (Convert.ToInt32(list_multi[row, col]) - 1).ToString();   
            //    }
            //}

            ///* เป็นรูปแบบฟอร์ม text ที่ได้กำหนดไว้ ว่าค่าแรกในบรรทัด ลำดับจะติดกับขอบ ตามที่ wr ได้เขียน
            // * for loop นี้ทำหน้ที่เขียน text โดยเริ่มตั้งแต่บรรทัดที่ 2 */
            //wr.Write(list_multi[0, 0] + " " + list_multi[0, 1] + " " + list_multi[0, 2] + " " + list_multi[0, 3]);
            //for (int row = 1; row < list_multi.GetLength(0) - 1; row++)
            //{
            //    for (int col = 0; col < list_multi.GetLength(1); col++)           
            //        wr.Write(" " + list_multi[row, col]);
            //}
            //wr.Close();
            //btn_update.Visible = true;
            //btn_delete.Visible = false;
            //btn_save.Visible = false;
            //dataGridView1.Visible = false;
            //MessageBox.Show("กรุณากดปุ่มอัพเดท");

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
    }
}
