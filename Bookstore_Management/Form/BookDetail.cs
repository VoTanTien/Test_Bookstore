using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bookstore_Management
{
    public partial class BookDetail : Form
    {
        string maSach;
        DatabaseConnection databaseConnection;
        SqlConnection connection;
        public BookDetail(string masach)
        {
            InitializeComponent();
            databaseConnection = new DatabaseConnection();
            connection = databaseConnection.OpenConnection();
            maSach = masach;
            Load();
        }

        private void Load()
        {
            string tenAnh = "";
            string query = "Select * from THONGTINSACH where MaSach = @MaSach";
            SqlCommand cmd = new SqlCommand(query,connection);
            cmd.Parameters.AddWithValue("@MaSach", maSach);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                textBox_MaSach.Text = reader.GetString(0);
                textBox_TheLoai.Text = reader.GetString(1);
                textBox_TenSach.Text = reader.GetString(2);
                textBox_SL.Text = reader.GetInt32(3).ToString();
                textBox_Gia.Text = reader.GetInt32(4).ToString();
                dateTimePicker_NPH.Text = reader.GetSqlDateTime(5).ToString();
                tenAnh = reader.GetString(6);
            }
            reader.Close();
            Image img = Image.FromFile(tenAnh +".jpg");
            pictureBox_AnhBia.BackgroundImage = img;
            getTenTG(getMaTG(textBox_MaSach.Text));
        }

        private void getTenTG(string text)
        {
            string ten = "";
            string query12 = "select TenTacGia from TACGIA where MaTacGia = @MaTacGia";
            SqlCommand cmd11 = new SqlCommand(query12, connection);
            cmd11.Parameters.AddWithValue("@MaTacGia", text);
            SqlDataReader reader12 = cmd11.ExecuteReader();
            while (reader12.Read())
            {
                ten = reader12.GetString(0);
            }
            reader12.Close();
            textBox_TacGia.Text = ten;
        }

        private string getMaTG(string text)
        {
            string matg = "";
            string query11 = "select MaTacGia from CT_TACGIA where MaSach = @MaSach";
            SqlCommand cmd11 = new SqlCommand(query11, connection);
            cmd11.Parameters.AddWithValue("@MaSach", text);
            SqlDataReader reader = cmd11.ExecuteReader();
            while (reader.Read())
            {
                matg = reader.GetString(0);
            }
            reader.Close();
            return matg;
        }

        private void pictureBox_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
