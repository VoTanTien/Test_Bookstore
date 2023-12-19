using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bookstore_Management
{

    public partial class NhapSach : UserControl
    {
        private string connectionString;
        private SqlConnection connection;
        private SqlDataAdapter adapter;
        private DataTable dataTable;
        DatabaseConnection databaseConnection;
        public NhapSach()
        {
            InitializeComponent();
            //DatabaseConnection.Instance.GetConnection();
            databaseConnection = new DatabaseConnection();
            connection = databaseConnection.OpenConnection();

            adapter = new SqlDataAdapter();
            dataTable = new DataTable();
            LoadMaSach();
            LoadData();
        }

        private void LoadData()
        {
            string query = "SELECT * FROM PHIEUNHAPSACH";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView_NhapSach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView_NhapSach.DataSource = dataTable;
            }
        }

        private void LoadMaSach()
        {
            string query1 = "SELECT MaSach FROM THONGTINSACH";
            SqlCommand cmd1 = new SqlCommand(query1,connection);
            DataTable dataTable1 = new DataTable();
            SqlDataAdapter adapter1 = new SqlDataAdapter();
            adapter1.SelectCommand = cmd1;
            adapter1.Fill(dataTable1);
            foreach (DataRow row in dataTable1.Rows)
            {
                comboBox_MaSach.Items.Add(row[0].ToString());
            }
        }

        private void PreferenceUI()
        {
            
        }

        private void textBox_SL_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox_MaSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            string maSach = comboBox_MaSach.Text;
            string query2 = "Select TenSach,TheLoai,GiaBan FROM THONGTINSACH WHERE MaSach = @MaSach";
            SqlCommand cmd2 = new SqlCommand(query2,connection);
            cmd2.Parameters.AddWithValue("@MaSach", maSach);
            SqlDataReader reader = cmd2.ExecuteReader();
            while (reader.Read())
            {
                textBox_TenSach.Text = reader.GetString(0);
                textBox_TheLoai.Text = reader.GetString(1);
            }
            reader.Close();
            textBox_TacGia.Text = loadTenTG(loadMaTG(maSach));
            textBox_DonGia.Clear();
        }

        public string loadTenTG(string v)
        {
            string ten = "";
            string query12 = "select TenTacGia from TACGIA where MaTacGia = @MaTacGia";
            SqlCommand cmd11 = new SqlCommand(query12, connection);
            cmd11.Parameters.AddWithValue("@MaTacGia", v);
            SqlDataReader reader12 = cmd11.ExecuteReader();
            while (reader12.Read())
            {
                ten = reader12.GetString(0);
            }
            reader12.Close();
            return ten;
        }

        public string loadMaTG(string maSach)
        {
            string matg = "";
            string query11 = "select MaTacGia from CT_TACGIA where MaSach = @MaSach";
            SqlCommand cmd11 = new SqlCommand(query11,connection);
            cmd11.Parameters.AddWithValue("@MaSach", maSach);
            SqlDataReader reader = cmd11.ExecuteReader();
            while (reader.Read())
            {
                matg = reader.GetString(0);
            }
            reader.Close();
            return matg;
        }

        private void textBox_TenSach_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            String maSach = comboBox_MaSach.Text.Trim();
            String ngayNhap = dateTimePicker_NgayNhap.Value.ToShortDateString() ;
            string SL = textBox_SL.Text;
            string Gia = textBox_DonGia.Text;
            int gia = Convert.ToInt32(Gia) * 105 / 100;
            int soLuong = Convert.ToInt32(SL);
            if (!int.TryParse(textBox_SL.Text, out soLuong))
            {
                MessageBox.Show("Số lượng nhập không hợp lệ. Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //if (!CheckSoLuongNhap(soLuong))
            //{
            //    MessageBox.Show("Số lượng nhập ít nhất phải là 150. Vui lòng nhập lại.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}


            try
            {
                // Thực hiện câu lệnh SQL có chứa RAISERROR
                string query4 = "INSERT INTO PHIEUNHAPSACH(NgayNhap, MaSach, SoLuongNhap, GiaNhap) values (@NgayNhap, @MaSach, @SoLuongNhap, @GiaNhap)";
                SqlCommand command = new SqlCommand(query4, connection);
                command.Parameters.AddWithValue("@NgayNhap", ngayNhap);
                command.Parameters.AddWithValue("@MaSach", maSach);
                command.Parameters.AddWithValue("@SoLuongNhap", SL);
                command.Parameters.AddWithValue("@GiaNhap", Gia);
                var rows_affected = command.ExecuteNonQuery();
                //if (rows_affected == 0) { MessageBox.Show("Có lỗi"); }
                CapNhatSach(maSach, gia, soLuong);
                MessageBox.Show("Thành công");
                LoadData();

            }
            catch (SqlException ex)
            {
                // Bắt lỗi SqlException
                string errorMessage = ex.Message;
                MessageBox.Show(errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

                textBox_DonGia.Clear();
                textBox_SL.Text = "150";
                textBox_TacGia.Clear();
                textBox_TenSach.Clear();
                textBox_TheLoai.Clear();
                comboBox_MaSach.Text = "";
                dateTimePicker_NgayNhap.Value = DateTime.Now;
            

        }

        private bool CapNhatSach(string maSach, int gia, int soLuong)
        {
            bool rs = true;
            int soLuongSachDaNhap = soLuong;

            //connection = databaseConnection.OpenConnection();
            int SLcu = GetSLTon(maSach);
            soLuong = soLuong + SLcu;
            string query5 = " UPDATE THONGTINSACH SET SoLuong = @SoLuong,GiaBan = @GiaBan WHERE MaSach = @MaSach";
            string queryUpdate = "UPDATE BAOCAOTONKHO SET SachDaNhap = @SachDaNhap + SachDaNhap, TonCuoi = @SoLuong WHERE MaSach = @MaSach";

            SqlCommand sqlUpdate = new SqlCommand(queryUpdate, connection);
            sqlUpdate.Parameters.AddWithValue("@SachDaNhap", soLuongSachDaNhap);
            sqlUpdate.Parameters.AddWithValue("@MaSach", maSach);
            sqlUpdate.Parameters.AddWithValue("@SoLuong", soLuong);
            sqlUpdate.ExecuteNonQuery();

            SqlCommand sqlCommand = new SqlCommand(query5, connection);
            sqlCommand.Parameters.AddWithValue("@SoLuong", soLuong);
            sqlCommand.Parameters.AddWithValue("@GiaBan", gia);
            sqlCommand.Parameters.AddWithValue("@MaSach", maSach);
            var rows_affected = sqlCommand.ExecuteNonQuery();
            if (rows_affected == 0) { rs = false; }


           return rs;
        }

        private int GetSLTon(string maSach)
        {
            string query6 = "SELECT SoLuong FROM THONGTINSACH where MaSach = @MaSach";
            int result = 0;
            SqlCommand cmd = new SqlCommand(query6,connection);
            cmd.Parameters.AddWithValue("@MaSach", maSach);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result= reader.GetInt32(0);
            }
            reader.Close();
            return result;
        }

        private bool CheckSoLuongNhap(int soLuongNhap)
        {
            const int SoLuongNhapToiThieu = 150;
            return soLuongNhap >= SoLuongNhapToiThieu;
        }

        private void button_Addnewbook_Click(object sender, EventArgs e)
        {
            TaoXoaSach taosach = new TaoXoaSach();
            taosach.Show();
        }

        private void dataGridView_NhapSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = dataGridView_NhapSach.Rows[e.RowIndex];
            comboBox_MaSach.Text = Convert.ToString(row.Cells["MaSach"].Value);
            textBox_DonGia.Text = Convert.ToString(row.Cells["GiaNhap"].Value);
            textBox_SL.Text = Convert.ToString(row.Cells["SoLuongNhap"].Value);
            dateTimePicker_NgayNhap.Text = Convert.ToString(row.Cells["NgayNhap"].Value);
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            String maSach = comboBox_MaSach.Text.Trim();
            String ngayNhap = dateTimePicker_NgayNhap.Text;
            string SL = textBox_SL.Text;
            string Gia = textBox_DonGia.Text;
            int soLuong = Convert.ToInt32(SL);
            if (!int.TryParse(textBox_SL.Text, out soLuong))
            {
                MessageBox.Show("Số lượng nhập không hợp lệ. Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!CheckSoLuongNhap(soLuong))
            {
                MessageBox.Show("Số lượng nhập ít nhất phải là 150. Vui lòng nhập lại.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query5 = "UPDATE PHIEUNHAPSACH SET SoLuongNhap = @SoLuongNhap,GiaNhap = @GiaNhap WHERE NgayNhap = @NgayNhap and MaSach = @MaSach";
            SqlCommand command = new SqlCommand(query5, connection);
            command.Parameters.AddWithValue("@NgayNhap", ngayNhap);
            command.Parameters.AddWithValue("@MaSach", maSach);
            command.Parameters.AddWithValue("@SoLuongNhap", SL);
            command.Parameters.AddWithValue("@GiaNhap", Gia);
            var rows_affected = command.ExecuteNonQuery();
            if (rows_affected == 0) { MessageBox.Show("Có lỗi"); }
            else
            {
                string query6 = " UPDATE THONGTINSACH SET SoLuong = @SoLuong,GiaBan = @GiaBan WHERE MaSach = @MaSach";
                SqlCommand sqlCommand = new SqlCommand(query5, connection);
                sqlCommand.Parameters.AddWithValue("@SoLuong", soLuong);
                sqlCommand.Parameters.AddWithValue("@GiaBan", Gia);
                sqlCommand.Parameters.AddWithValue("@MaSach", maSach);
                var rows_affected1 = sqlCommand.ExecuteNonQuery();
                if (rows_affected1 == 0) { MessageBox.Show("Có lỗi"); }
                else
                {
                    MessageBox.Show("Thành công");
                    textBox_DonGia.Clear();
                    textBox_SL.Text = "150";
                    textBox_TacGia.Clear();
                    textBox_TenSach.Clear();
                    textBox_TheLoai.Clear();
                    comboBox_MaSach.Text = "";
                    dateTimePicker_NgayNhap.Value = DateTime.Now;
                }
               
            }
            LoadData();
        }
    }
}
