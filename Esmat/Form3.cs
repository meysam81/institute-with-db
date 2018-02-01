using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Esmat
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        string connect;
        //string currentDir = Directory.GetCurrentDirectory().ToString() + @"\Database\EsmatDB.mdf";
        string dir = Directory.GetCurrentDirectory() + @"\Database\EsmatDB.mdf";

        //Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="C:\Users\MEYSAM\Documents\Visual Studio 2015\Projects\Esmat\Esmat\bin\Debug\EsmatDB.mdf";Integrated Security=True;Connect Timeout=30
        //Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MEYSAM\AppData\Roaming\Esmat\EsmatDB.mdf;Integrated Security=True;Connect Timeout=30
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection myConnection = new SqlConnection(connect);
            //MessageBox.Show(myConnection.Database.ToString());
            SqlCommand myCommand = myConnection.CreateCommand();
            string user = textBoxUser.Text;
            string pass = textBoxPass.Text;
            string myQuery = string.Format("execute myPro @user,@pass");
            myCommand.CommandText = myQuery;
            myCommand.Parameters.Add("@user", SqlDbType.NVarChar, 50).Value = textBoxUser.Text;
            myCommand.Parameters.Add("@pass", SqlDbType.NVarChar, 50).Value = textBoxPass.Text;
            SqlDataAdapter myDataAdapter = new SqlDataAdapter(myCommand);
            DataTable myDataTable = new DataTable();
            //myCommand.ExecuteNonQuery();
            //string myQuery = string.Format("select * from Account where userid='{0}' and pass='{1}'", user, pass);
            string a, b;
            try
            {
                myConnection.Open();
                //SqlDataReader myDataReader = myCommand.ExecuteReader();
                myDataAdapter.Fill(myDataTable);
                if (myDataTable.Rows.Count == 1)
                {
                    a = myDataTable.Rows[0][0].ToString();
                    b = myDataTable.Rows[0][1].ToString();
                    if (a == textBoxUser.Text && b == textBoxPass.Text)
                    {
                        //MessageBox.Show("خوش آمدید");
                        var f = new FormEsmat();
                        f.FormClosing += (s, args) => this.Close();
                        f.Show();
                        this.Hide();
                    }
                    else
                        MessageBox.Show("نام کاربری یا رمز عبور را اشتباه وارد کردید");
                }
                else
                    MessageBox.Show("متاسفانه خطا رخ داده است", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("خطا");
                MessageBox.Show(ex.Message);
            }
            finally
            {
                myConnection.Close();
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            connect = String.Format("{0}{1}{2}", @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=", dir, ";Integrated Security=True");
        }

        private void textBox_Enter(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }
    }
}