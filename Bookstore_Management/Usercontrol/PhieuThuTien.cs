using System;
using System.Collections;
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
    public partial class PhieuThuTien : UserControl
    {
        DatabaseConnection databaseConnection;
        SqlConnection connection;
        private SqlDataAdapter adapter;
        private DataTable dataTable;
        private Double Tien;
        public PhieuThuTien()
        {
            InitializeComponent();
            databaseConnection = new DatabaseConnection();
            connection = databaseConnection.OpenConnection();
            adapter = new SqlDataAdapter();
            dataTable = new DataTable();
            loadMaKH();
        }
        

        private void button_Save_Click(object sender, EventArgs e)
        {
            int tienthu = Convert.ToInt32(textBox_SoTienThu.Text);
            int tienno = Convert.ToInt32(textBox_sotienno.Text.ToString());

            string query = "SELECT GiaTri FROM THAMSO WHERE MaThamSo = 'ST03'";
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            sqlCommand.ExecuteNonQuery();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            Tien = 0;
            reader.Read();
            Tien = reader.GetDouble(0);
            reader.Close();
            if (Tien == 1)
            {
                if (tienno < tienthu) MessageBox.Show("Số tiền thu không vượt quá số tiền nợ!");
                else
                {
                    LuuPhieu();
                }    
            }
            else
            {
                LuuPhieu();
            }
        }

        private void LuuPhieu()
        {
            int maPTN = getMaPTN();
            string maKH = comboBox_MaKH.Text;
            string ngay = dateTimePicker_NgayThu.Value.ToString("yyyy/MM/dd");
            int sotienthanhtoan = Convert.ToInt32(textBox_SoTienThu.Text);
            string query4 = "INSERT INTO PHIEUTHUNO(MaPTN, MaKH, NgayThuTien, SoTienThanhToan) values (@MaPTN, @MaKH, @NgayThuTien, @SoTienThanhToan)";
            SqlCommand command = new SqlCommand(query4, connection);
            command.Parameters.AddWithValue("@MaPTN", maPTN);
            command.Parameters.AddWithValue("@MaKH", maKH);
            command.Parameters.AddWithValue("@NgayThuTien", ngay);
            command.Parameters.AddWithValue("@SoTienThanhToan", sotienthanhtoan);
            var count = command.ExecuteNonQuery();
            if (count > 0)
            {
                MessageBox.Show ("Thành công");
            }
            else { MessageBox.Show("Lỗi"); }
        }

        private int getMaPTN()
        {
            int MaPTN = 0;
            string query3 = "SELECT MAX(MaPTN) FROM PHIEUTHUNO";
            SqlCommand command = new SqlCommand(query3, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                MaPTN = reader.GetInt32(0);
            }
            reader.Close();
            return MaPTN+1;
        }

        private void loadMaKH()
        {
            string query1 = "SELECT MaKH FROM KHACHHANG";
            SqlCommand command = new SqlCommand(query1, connection);
            adapter.SelectCommand = command;
            adapter.Fill(dataTable);
            foreach (DataRow row in dataTable.Rows )
            {
                comboBox_MaKH.Items.Add(row[0].ToString());
            }
        }

        private void comboBox_MaKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            string makh = comboBox_MaKH.Text;
            string query2 = "SELECT * FROM KHACHHANG WHERE MaKH = @MaKH";
            SqlCommand command = new SqlCommand(query2, connection);
            command.Parameters.AddWithValue("@MaKH", makh);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                textBox_TenKH.Text = reader.GetString(1);
                textBox_SDT.Text = reader.GetString(2);
                textBox_Email.Text = reader.GetString(3);
                textBox_DiaChi.Text = reader.GetString(4);
                textBox_sotienno.Text = reader.GetInt32(7).ToString();
            }
            reader.Close();
        }
    }
}
