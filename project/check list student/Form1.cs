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
    public partial class Form1 : Form
    {
        // val_class ทำหน้าที่เก็บข้อมูลจากตัวเลือกรายวิชา เพื่อนำไปใช้ใน form3
        public static string val_class;
        public static string time_etc;
        public static string val_date;
        public static string val_date_check;
        public Form1()
        {
            InitializeComponent();
        }

        /************************* ปุ่มเช็คชื่อ ******************************/
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null && (ListBox1.GetItemCheckState(0) == CheckState.Checked
                || ListBox1.GetItemCheckState(1) == CheckState.Checked || ListBox1.GetItemCheckState(2) == CheckState.Checked
                || ListBox1.GetItemCheckState(3) == CheckState.Checked || ListBox1.GetItemCheckState(4) == CheckState.Checked
                || ListBox1.GetItemCheckState(5) == CheckState.Checked || ListBox1.GetItemCheckState(6) == CheckState.Checked
                || ListBox1.GetItemCheckState(7) == CheckState.Checked || ListBox1.GetItemCheckState(8) == CheckState.Checked
                || ListBox1.GetItemCheckState(9) == CheckState.Checked || ListBox1.GetItemCheckState(10) == CheckState.Checked))
            {
                /* val_date และ val_date_check เป็นตัวแปรแบบโกลบอลเพื่อนำไปใช้ในฟอมอื่น
                 * val_date คือ วัน_เดือน_ปี
                 * val_date_check คือ วัน/เดือน/ปี ชัวโมงนาที-ชั่วโมงนาที */
                val_date = dateTimePicker1.Value.Day.ToString() + "_" + dateTimePicker1.Value.Month.ToString()
                            + "_" + dateTimePicker1.Value.Year.ToString();

                val_date_check = dateTimePicker1.Value.Day.ToString()
                   + "/" + dateTimePicker1.Value.Month.ToString() + "/" + dateTimePicker1.Value.Year.ToString()
                   + " " + period();
                // เรียกใช้ เมธอด period เพราะต้องการให้ตัวแปร time_etc เก็บค่าเวลา เพื่อนำไปใช้ใน form อื่นๆ
                period();
                val_class = comboBox1.Text;
                Form2 form2 = new Form2();
                form2.Show();
                this.Hide();
            }
            else MessageBox.Show("กรุณาเลิอกข้อมูลให้ครบ");
        }

        /************************************ เมธอด สำหรับรับค่าช่วงเวลาเรียน 0000-00000 ******************************************/
        public string period()
        {
            /* n ไว้หมุนข้อมูล ซึ้ง n จะมีค่าเท่ากับ เท่ากับ ตำแหน่งอินเด็กเวลาที่เราเลือกตัวสุดท้าย
             * time[11] คือ มีข้อมูลเวลา 11 ตัว
             * foreach นำข้อมูลใน listbox มาเก็บใน time*/
            int n = 0;
            string[] time = new string[11];
            foreach (string indexChecked in ListBox1.CheckedItems)
            {
                time[n] = indexChecked;
                n++;
            }

            /* val_time เก็บข้อมูลเวลา
             * ถ้า*/
            string val_time = "";
            if (n <= 1)
                val_time = time[n - 1];
            else
            {
                string[] start = time[0].Split('-');
                string[] end = time[n - 1].Split('-');
                val_time = start[0] + "-" + end[1];
            }
            time_etc = val_time;
            return val_time;
        }

        /*************************** เมธอด กำจัดอักขระทุกตัวที่ไม่ใช้ _ ออกจากเวลา และเชื่อมต่อเป็น 0000_0000 *****************************/
        public string time_split()
        {
            string[] temp = period().Split('.', '-');
            string time = temp[0] + temp[1] + "_" + temp[2] + temp[3];
            return time;
        }

        /********************* เมธอด(overload) กำจัดอักขระทุกตัวที่ไม่ใช้ _ ออกจากเวลา และเชื่อมต่อเป็น 0000_0000 **************************/
        public string time_split(string val)
        {
            string[] temp = val.Split('.', '-');
            string time = temp[0] + temp[1] + "_" + temp[2] + temp[3];
            return time;
        }

        /************************** เมธอด สำหรับ check รายชื่อว่ามีคนเข้าเรียน ลาป่วย ลากิจ ขาด ร่วมกิจกรรมกี่คน ******************************/
        public int[] check_stu()
        {
            /* vf_check คือ value file check ทำหน้าที่อ่านทุกบรรทัดจาก path file แล้วมาเก็บไว้
             * n_type[5] ความหมาย
             * n[0] = นับ -->เข้าเรียน<--  กี่คน
             * n[1] = นับ -->ลาป่วย<--  กี่คน
             * n[2] = นับ -->ลากิจ<--  กี่คน 
             * n[3] = นับ -->ขาด<--  กี่คน 
             * n[4] = นับ -->ร่วมกิจกกรม<--  กี่คน 
             */
            string[] vf_check = File.ReadAllLines("D:\\keep text\\val_check" + " " + comboBox1.SelectedItem.ToString() + ".txt");
            string val_txt = File.ReadAllText("D:\\keep text\\val_check" + " " + comboBox1.SelectedItem.ToString() + ".txt");
            int[] n_type = { 0, 0, 0, 0, 0 };
            int index = 0;
            string[] val_sp = val_txt.Split('>');
            string temp = "";
            foreach (string str in val_sp)
            {
                if (str.Equals(val_date + "_" + time_split(time_etc)))
                {
                    temp = val_sp[index + 1];
                    break;
                }
                index++;
            }
            string[] temp_check = temp.Split(' ');
            foreach (string s in temp_check)
            {
                if (s.Equals("เข้าเรียน"))
                    n_type[0]++;
                else if (s.Equals("ลาป่วย"))
                    n_type[1]++;
                else if (s.Equals("ลากิจ"))
                    n_type[2]++;
                else if (s.Equals("ขาด"))
                    n_type[3]++;
                else if (s.Equals("เข้าร่วมกิจกรรม"))
                    n_type[4]++;
            }
            return n_type;
        }

        private void btn_static_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                val_class = comboBox1.Text;
                Form4 form4 = new Form4();
                this.Hide();
                form4.Show();
            }
            else MessageBox.Show("กรุณาเลือกวิชา");
          
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
