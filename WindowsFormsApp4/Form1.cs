using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class manage : Form
    {
         SqlConnection Mycon = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = 'C:\Users\Acer\Documents\Visual Studio 2017\Projects\Database1\amval_khabgah.mdf'; Integrated Security = True; Connect Timeout = 30");
        public manage()
        {
            InitializeComponent();
            comboBox1.Items.Add(" دانشجو ");
            comboBox1.Items.Add(" کارمند ");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void Sabt_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" | textBox2.Text == "")
            {
                MessageBox.Show("کد ملی و کد مال وارد نشده", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if(comboBox1.Text=="")
                MessageBox.Show("نوع آسیب زننده انتخاب نشده!", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {

                string rquery = "insert into rhurt(fk_pcode,fk_ridnumber) values ('" + textBox1.Text + "','" + textBox2.Text + "')";
                string squery = "insert into shurt(fk_pcode,fk_sidnumber) values ('" + textBox1.Text + "','" + textBox2.Text + "')";

                try
                {

                    if (comboBox1.Text == " دانشجو ")
                    {
                        SqlCommand Mycmd = new SqlCommand(squery, Mycon);
                        Mycon.Open();
                        Mycmd.CommandType = CommandType.Text;
                        Mycmd.ExecuteNonQuery();
                        MessageBox.Show("ثبت با موفقیت انجام شد", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Mycon.Close();
                    }
                    else if (comboBox1.Text == " کارمند ")
                    {
                        SqlCommand Mycmd = new SqlCommand(rquery, Mycon);
                        Mycon.Open();
                        Mycmd.CommandType = CommandType.Text;
                        Mycmd.ExecuteNonQuery();
                        MessageBox.Show("ثبت با موفقیت انجام شد", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Mycon.Close();
                    }

                }
                catch
                {
                    MessageBox.Show("نوع آسیب زننده یا کد مال یا کد ملی اشتباه است", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Mycon.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" | textBox2.Text == "")
            {
                MessageBox.Show("کد ملی و کد مال وارد نشده", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (comboBox1.Text == "")
                MessageBox.Show("نوع آسیب زننده انتخاب نشده!", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {

                try
                {
                    string rquery = "delete from rhurt where'" + textBox1.Text + "' = fk_pcode and '" + textBox2.Text + "' = fk_ridnumber";
                    string squery = "delete from shurt where'" + textBox1.Text + "' = fk_pcode and '" + textBox2.Text + "' = fk_sidnumber";
                    if (comboBox1.Text == " دانشجو ")
                    {
                        SqlCommand Mycmd = new SqlCommand(squery, Mycon);
                        Mycon.Open();
                        Mycmd.CommandType = CommandType.Text;
                        Mycmd.ExecuteNonQuery();
                        MessageBox.Show("حذف با موفقیت انجام شد", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Mycon.Close();
                    }
                    else if (comboBox1.Text == " کارمند ")
                    {
                        SqlCommand Mycmd = new SqlCommand(rquery, Mycon);
                        Mycon.Open();
                        Mycmd.CommandType = CommandType.Text;
                        Mycmd.ExecuteNonQuery();
                        MessageBox.Show("حذف با موفقیت انجام شد", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Mycon.Close();
                    }

                }
                catch
                {
                    MessageBox.Show("نوع آسیب زننده یا کد مال \n یا کد ملی اشتباه است \nو یا آسیب زننده وجود ندارد", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Mycon.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string query = "select fname,lname,id_number,fk_pcode , pname,cost " +
                "from(((hurts join people on national_id = id_number)" +
                "join dormproperty on hurts.pcode = dormproperty.pcode)" +
                "join eternalproperty on fk_pcode = hurts.pcode )";
            SqlCommand Mycmd = new SqlCommand(query, Mycon);
            SqlDataReader Myread;
            try
            {
                string list = "";
                Mycon.Open();
                list += "خسارت      اسم مال       کد مال       کد ملی      نام خانوادگی     نام \n";
                Myread = Mycmd.ExecuteReader();
                while (Myread.Read())
                {
                    list += Myread["fname"].ToString();
                    list += "     ";
                    list += Myread["lname"].ToString();
                    list += "     ";
                    list += Myread["id_number"].ToString();
                    list += "     ";
                    list += Myread["fk_pcode"].ToString();
                    list += "     ";
                    list += Myread["pname"].ToString();
                    list += "     ";
                    list += Myread["cost"].ToString();
                    list += "\n";

                }

                MessageBox.Show(list , "لیست", MessageBoxButtons.OK, MessageBoxIcon.None);


            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            finally
            {
                if (Mycon.State == ConnectionState.Open)
                    Mycon.Close();
            }
        }
    }
}
