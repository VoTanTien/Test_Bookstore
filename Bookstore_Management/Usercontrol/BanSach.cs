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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Bookstore_Management
{
    public partial class BanSach : UserControl
    {
        private string connectionString;
        private SqlConnection connection;
        private SqlDataAdapter adapter;
        private DataTable dataTable;
        DatabaseConnection databaseConnection;
        DataTable cthd;
        string Ma;
        public BanSach(string ma)
        {
            Ma = ma;
            InitializeComponent();
            databaseConnection = new DatabaseConnection();
            connection = databaseConnection.OpenConnection();

            adapter = new SqlDataAdapter();
            dataTable = new DataTable();
            LoadMaSach();
            LoadMaKH();
            LoadMaHD();
            cthd = new DataTable();
            cthd.Columns.Add("Mã Sách");
            cthd.Columns.Add("Số Lượng");
            cthd.Columns.Add("Thành Tiền");
            dataGridView_BanSach.DataSource = cthd;
        }

        

        private void LoadMaHD()
        {
            string ma ="   ";
            string query3 = "select Max(MaHD) from HOADON";
            SqlCommand hd = new SqlCommand(query3,connection);
            SqlDataReader reader = hd.ExecuteReader();
            while (reader.Read())
            {
                ma = reader.GetString(0);
            }
            reader.Close();
            string max = ma.Substring(2, ma.Length-2);
            int m = Convert.ToInt32(max);
            m++;
            textBox_MaHD.Text = "HD" +m;
        }

        private void LoadMaKH()
        {
            string query1 = "SELECT MaKH FROM KHACHHANG";
            SqlCommand cmd1 = new SqlCommand(query1, connection);
            DataTable dataTable1 = new DataTable();
            SqlDataAdapter adapter1 = new SqlDataAdapter();
            adapter1.SelectCommand = cmd1;
            adapter1.Fill(dataTable1);
            foreach (DataRow row in dataTable1.Rows)
            {
                comboBox_MaKH.Items.Add(row[0].ToString());
            }
        }

        private void LoadMaSach()
        {
            string query1 = "SELECT MaSach FROM THONGTINSACH";
            SqlCommand cmd1 = new SqlCommand(query1, connection);
            DataTable dataTable1 = new DataTable();
            SqlDataAdapter adapter1 = new SqlDataAdapter();
            adapter1.SelectCommand = cmd1;
            adapter1.Fill(dataTable1);
            foreach (DataRow row in dataTable1.Rows)
            {
                comboBox_MaSach.Items.Add(row[0].ToString());
            }
        }
        private void button_Add_Click(object sender, EventArgs e)
        {
            DataRow dataRow = cthd.NewRow();
            dataRow["Mã Sách"] = comboBox_MaSach.Text;
            dataRow["Số Lượng"] = textBox_SL.Text;
            dataRow["Thành Tiền"] = textBox_TongTien.Text;
            cthd.Rows.Add(dataRow);
            int Tong = 0;
            foreach (DataRow row in cthd.Rows)
            {
                Tong = Tong + Convert.ToInt32(row[2]);
            }
            textBox_TongHD.Text = Tong.ToString();
            comboBox_MaSach.Text = "";
            textBox_TenSach.Clear();
            textBox_SL.Clear();
            textBox_TonKho.Clear();
            textBox_DonGia.Clear();
        }

        private void comboBox_MaSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            string maSach = comboBox_MaSach.Text;
            string query2 = "Select TenSach,GiaBan,SoLuong FROM THONGTINSACH WHERE MaSach = @MaSach";
            SqlCommand cmd2 = new SqlCommand(query2, connection);
            cmd2.Parameters.AddWithValue("@MaSach", maSach);
            SqlDataReader reader = cmd2.ExecuteReader();
            while (reader.Read())
            {
                textBox_TenSach.Text = reader.GetString(0);
                textBox_DonGia.Text = reader.GetInt32(1).ToString();
                textBox_TonKho.Text = reader.GetInt32(2).ToString();
            }
            reader.Close();
            
        }

        private void textBox_SL_TextChanged(object sender, EventArgs e)
        {
            if (textBox_SL.Text == "") { textBox_SL.Text = "0"; }
            int sl = Convert.ToInt32(textBox_SL.Text);
            int gia = Convert.ToInt32(textBox_DonGia.Text);
            int tong = sl * gia;
            textBox_TongTien.Text = tong.ToString();
        }

        private void comboBox_MaKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            string maKH = comboBox_MaKH.Text;
            string query2 = "Select HoTen,SDT_KH FROM KHACHHANG WHERE MaKH = @MaKH";
            SqlCommand cmd2 = new SqlCommand(query2, connection);
            cmd2.Parameters.AddWithValue("@MaKH", maKH);
            SqlDataReader reader = cmd2.ExecuteReader();
            while (reader.Read())
            {
                textBox_TenKH.Text = reader.GetString(0);
                textBox_SDT.Text = reader.GetString(1);
               
            }
            reader.Close();
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            string maHD = textBox_MaHD.Text;
            string maKH = comboBox_MaKH.Text;
            string ngay = dateTimePicker_NgayBan.Value.ToShortDateString();
            string tongHD = textBox_TongHD.Text;
            string tienTT = textBox1.Text;
            string maNguoiDung = Ma;
            try
            {
                string query3 = "INSERT INTO HOADON(MaHD, MaKH, NgayHD, TongHoaDon, SoTienDaThanhToan, MaNguoiDung) values(@MaHD,@MaKH,@Ngay,@TongHD,@TienTT,@MaNguoiDung)";
                SqlCommand cmd3 = new SqlCommand(query3, connection);
                cmd3.Parameters.AddWithValue("@MaHD", maHD);
                cmd3.Parameters.AddWithValue("@MaKH", maKH);
                cmd3.Parameters.AddWithValue("@Ngay", ngay);
                cmd3.Parameters.AddWithValue("@TongHD", tongHD);
                cmd3.Parameters.AddWithValue("@TienTT", tienTT);
                cmd3.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);
                var row_ef = cmd3.ExecuteNonQuery();
                if (row_ef > 0)
                {
                    addCTHD();
                    UpdateTonKho();
                    MessageBox.Show("Thành công");
                    comboBox_MaKH.Text = "";
                    textBox_SDT.Clear();
                    textBox1.Text = "0";
                    textBox_TenKH.Clear();
                    textBox_TongHD.Text = "0";
                    cthd = new DataTable();
                    cthd.Columns.Add("Mã Sách");
                    cthd.Columns.Add("Số Lượng");
                    cthd.Columns.Add("Thành Tiền");
                    dataGridView_BanSach.DataSource = cthd;
                    LoadMaHD();
                }
                //else { MessageBox.Show("Lỗi"); }
            }
            catch(SqlException ex)
            {
                string errorMessage = ex.Message;
                MessageBox.Show(errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void UpdateTonKho()
        {
            int d = 0;
            string maHD = textBox_MaHD.Text;
            string maSach;
            string sl;
            
            foreach (DataRow row in cthd.Rows)
            {
                maSach = row[0].ToString();
                sl = row[1].ToString();
                int slban = Convert.ToInt32(sl);
                int slton = GetSLTon(maSach);
                int SlMoi = slton - slban;
                string query = "UPDATE THONGTINSACH SET SoLuong = @SoLuong WHERE MaSach = @MaSach";    
                SqlCommand sqlCommand = new SqlCommand(query, connection);       
                sqlCommand.Parameters.AddWithValue("@SoLuong", SlMoi);       
                sqlCommand.Parameters.AddWithValue("@MaSach", maSach);        
                var rows_affected = sqlCommand.ExecuteNonQuery();
                if (rows_affected > 0) d++;               
            }
        }
        private int GetSLTon(string maSach)
        {
            string query6 = "SELECT SoLuong FROM THONGTINSACH where MaSach = @MaSach";
            int result = 0;
            SqlCommand cmd = new SqlCommand(query6, connection);
            cmd.Parameters.AddWithValue("@MaSach", maSach);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result = reader.GetInt32(0);
            }
            reader.Close();
            return result;
        }

        private void addCTHD()
        {
            int d = 0;
            string maHD = textBox_MaHD.Text;
            string maSach;
            string sl;
            foreach (DataRow row in cthd.Rows)
            {
                maSach = row[0].ToString();
                sl = row[1].ToString();
                string query = " INSERT INTO CTHD(MaHD, MaSach, SL_HD) values (@MaHD, @MaSach, @SL)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@MaHD", maHD);
                cmd.Parameters.AddWithValue("@MaSach", maSach);
                cmd.Parameters.AddWithValue("@SL", sl);
                var row_ef = cmd.ExecuteNonQuery();
                if (row_ef > 0) d++;
            }
        }
    }
}
