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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        string connect;

        string dir = Directory.GetCurrentDirectory() + @"\EsmatDB.mdf";
        private void Form2_Load(object sender, EventArgs e)
        {
            connect = String.Format("{0}{1}{2}", @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=", dir, ";Integrated Security=True;Connect Timeout=30");
            SqlConnection connection = new SqlConnection(connect);
            SqlCommand command = connection.CreateCommand();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from TableEsmat", connection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            this.MaximumSize = new Size(dataGridView1.Width, dataGridView1.Height);
            //this.MinimumSize = dataGridView1.Size;
            //this.Height = dataGridView1.Height;
        }
    }
}
