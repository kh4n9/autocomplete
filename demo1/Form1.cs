using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace demo1
{
    public partial class Form1 : Form
    {

        SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\khang\\source\\repos\\demo1\\demo1\\Database1.mdf;Integrated Security=True");
        AutoCompleteStringCollection autoCompleteStringCollection = new AutoCompleteStringCollection();
        public Form1()
        {
            InitializeComponent();
        }

        private void ReadData()
        {
            try
            {
                conn.Open();
                SqlCommand sqlCommand = new SqlCommand("select LichSu from DuLieu", conn);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    autoCompleteStringCollection.Add(reader.GetString(0));
                }
                txtSearch.AutoCompleteCustomSource = autoCompleteStringCollection;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void WriteData()
        {
            if (txtSearch.Text.Length == 0) { return; }
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("delete from DuLieu where LichSu = N'"+txtSearch.Text+"'", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("insert into DuLieu values (N'" + txtSearch.Text + "')", conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ReadData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            WriteData();
            ReadData();
        }
    }
}
