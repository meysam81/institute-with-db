using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
namespace Esmat
{
    public partial class FormEsmat : Form
    {
        public FormEsmat()
        {
            InitializeComponent();
        }
        string dir = Directory.GetCurrentDirectory() + @"\EsmatDB.mdf";
        string connect;
        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter sqlDataAdapter;
        DataTable dataTable;
        private void FormEsmat_Load(object sender, EventArgs e)
        {
            connect = String.Format("{0}{1}{2}", @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=", dir, ";Integrated Security=True;Connect Timeout=30");
            connection = new SqlConnection(connect);
            command = connection.CreateCommand();
            dataTable = new DataTable();
        }
        void SaveExcel()
        {
            SqlDataAdapter s1 = new SqlDataAdapter("select * from tableesmat", connection);
            s1.Fill(dataTable);
            try
            {
                using (FileStream fs = new FileStream(@"D:\Book1.xls", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    StreamWriter wr = new StreamWriter(fs);
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        wr.Write(dataTable.Columns[i].ToString().ToUpper() + "\t");
                    }

                    wr.WriteLine();

                    //write rows to excel file
                    for (int i = 0; i < (dataTable.Rows.Count); i++)
                    {
                        for (int j = 0; j < dataTable.Columns.Count; j++)
                        {
                            if (dataTable.Rows[i][j] != null)
                            {
                                wr.Write(Convert.ToString(dataTable.Rows[i][j]) + "\t");
                            }
                            else
                            {
                                wr.Write("\t");
                            }
                        }
                        //go to next line
                        wr.WriteLine();
                    }
                    ////close file
                    //wr.Close();
                    //wr.WriteLine("Hello");
                    wr.Flush();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string name = textBoxName.Text;
            string lName = textBoxLName.Text;
            string ssn = textBoxSSN.Text;
            string fatherName = textBoxFatherName.Text;
            string phoneNumber = textBoxPhoneNumber.Text;
            string price = textBoxPrice.Text;
            string nonQuery = null;
            try
            {
                int age = int.Parse(textBoxAge.Text);
                nonQuery = String.Format("insert into TableEsmat values" +
                    "('{0}','{1}','{2}','{3}',{4},'{5}','{6}')", name, lName, ssn, fatherName, age, phoneNumber, price);
                command.CommandText = nonQuery;
                connection.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("با موفقیت ثبت گردید");
                textBoxName.Clear();
                textBoxLName.Clear();
                textBoxSSN.Clear();
                textBoxFatherName.Clear();
                textBoxAge.Clear();
                textBoxPhoneNumber.Clear();
                textBoxAge.Clear();
                textBoxPrice.Clear();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("خطا");
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);
            }
            finally
            {
                SaveExcel();
                connection.Close();
            }
        }

        private void ToolStripMenuItemList_Click(object sender, EventArgs e)
        {
            new Form2().ShowDialog();
        }

        private void ToolStripMenuItemAboutUs_Click(object sender, EventArgs e)
        {
            string show = String.Format("Developed by Meysam Azad\nEmail: MeysamAzad81@yahoo.com\nPhone Number: 0919-7050256\nTelegram:0901-0393528" +
                "\nInstagram: Meysam.Azad");
            MessageBox.Show(show, "About Us", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void textBox_Enter(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }
    }
}