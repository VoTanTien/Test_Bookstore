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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Bookstore_Management
{
    public partial class TaiKhoan : Form
    {
        String maTK, tenTK, maND, matKhau, vaiTro;
        private string connectionString;
        private SqlConnection connection;
        private SqlDataAdapter adapter;
        private DataTable dataTable;
        DatabaseConnection databaseConnection;
        string Role;
        public TaiKhoan(string role)
        {
            InitializeComponent();
            databaseConnection = new DatabaseConnection();
            connection = databaseConnection.OpenConnection();
            adapter = new SqlDataAdapter();
            dataTable = new DataTable();
            ReferenceUI();
            LoadTaiKhoanToComboBox();
            Role = role;
            Load();
            TaiKhoan_Load();
        }

        private void Load()
        {
            button_Add.Enabled = false;
            button_Delete.Enabled = false;
            button_Save.Enabled = false;
            button_Add.Visible = false;
            button_Delete.Visible = false;
            button_Save.Visible = false;
            textBox_MatKhau.UseSystemPasswordChar = true;
            if (Role == "ADMIN")
            {
                button_Add.Enabled = true;
                button_Delete.Enabled = true;
                button_Save.Enabled = true;
                button_Add.Visible = true;
                button_Delete.Visible=true;
                button_Save.Visible=true;
                textBox_MatKhau.UseSystemPasswordChar =false ;
            }
            if (textBox_MatKhau.UseSystemPasswordChar)
            {
                textBox_MatKhau.PasswordChar = '*';
            }
        }

        private void TaiKhoan_Load()
        {
            UpdateGridView();
            LoadDistinctComboBox("MaNguoiDung", comboBox_MaND);
            LoadDistinctComboBox("VaiTro", comboBox_VaiTro);
        }

        private void pictureBox_Exit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            ReferenceUI();
            AddAccount(maTK, tenTK, matKhau, maND, vaiTro);
            UpdateGridView();
        }

        private void button_Save_Click(object sender, EventArgs e)
        {

            ReferenceUI();
            UpdateAccount(maTK, tenTK, matKhau, maND, vaiTro);
            UpdateGridView();
            CleanData();
            UpdateGridView();
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            ReferenceUI();
            DeleteAccount(maTK);
            UpdateGridView();
            CleanData();
            UpdateGridView();
        }


        private void ReferenceUI()
        {
            maTK = comboBox_MaTK.Text;
            tenTK = textBox_TenTK.Text;
            matKhau = textBox_MatKhau.Text;
            maND = comboBox_MaND.Text;
            vaiTro = comboBox_VaiTro.Text;
        }

        private void CleanData()
        {
            comboBox_MaTK.Text = "";
            textBox_TenTK.Text = "";
            textBox_MatKhau.Text = "";
            comboBox_MaND.Text = "";
            comboBox_VaiTro.Text = "";
        }

        private void UpdateAccount(string maTK, string tenTK, string matKhau, string maNguoiDung, string vaiTro)
        {
            if (string.IsNullOrEmpty(maTK) || string.IsNullOrEmpty(tenTK) || string.IsNullOrEmpty(matKhau) || string.IsNullOrEmpty(maNguoiDung) || string.IsNullOrEmpty(vaiTro))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (IsMaTKValid(maTK) == false)
            {
                string query = "UPDATE TAIKHOAN SET TenTK = @TenTK, MatKhau = @MatKhau, MaNguoiDung = @MaNguoiDung, VaiTro = @VaiTro WHERE MaTK = @MaTK";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TenTK", tenTK);
                    command.Parameters.AddWithValue("@MatKhau", matKhau);
                    command.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);
                    command.Parameters.AddWithValue("@VaiTro", vaiTro);
                    command.Parameters.AddWithValue("@MaTK", maTK);
                    command.ExecuteNonQuery();
                    MessageBox.Show("cập nhật tài khoản thành công");
                }
            }
            else
            {
                MessageBox.Show("MaTK không tồn tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddAccount(string maTK, string tenTK, string matKhau, string maNguoiDung, string vaiTro)
        {
            if (string.IsNullOrWhiteSpace(maTK) || string.IsNullOrWhiteSpace(tenTK) ||
                string.IsNullOrWhiteSpace(matKhau) || string.IsNullOrWhiteSpace(maNguoiDung) ||
                string.IsNullOrWhiteSpace(vaiTro))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin tài khoản.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (IsMaTKValid(maTK))
            {
                string query = "INSERT INTO TAIKHOAN (MaTK, TenTK, MatKhau, MaNguoiDung, VaiTro) " +
                           "VALUES (@MaTK, @TenTK, @MatKhau, @MaNguoiDung, @VaiTro)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaTK", maTK);
                command.Parameters.AddWithValue("@TenTK", tenTK);
                command.Parameters.AddWithValue("@MatKhau", matKhau);
                command.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);
                command.Parameters.AddWithValue("@VaiTro", vaiTro);
                command.ExecuteNonQuery();
                
                UpdateGridView();
                MessageBox.Show("Thêm mới tài khoản thành công");
            }
            else
            {
                MessageBox.Show("MaTK đã tồn tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteAccount(String maTK)
        {
            // Kiểm tra điều kiện nhập không được để trống
            if (string.IsNullOrWhiteSpace(maTK))
            {
                MessageBox.Show("Vui lòng nhập mã tài khoản cần xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (IsMaTKValid(maTK) == false)
            {
                // Thực hiện xóa tài khoản từ database
                // (sử dụng câu lệnh DELETE)
                string query = "DELETE FROM TAIKHOAN WHERE MaTK = @MaTK";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaTK", maTK);
                    command.ExecuteNonQuery();
            }
            else
            {
                MessageBox.Show("MaTK không tồn tại trong cơ sở dữ liệu. Vui lòng chọn MaTK khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView_TaiKhoan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = dataGridView_TaiKhoan.Rows[e.RowIndex];
            comboBox_MaTK.Text = Convert.ToString(row.Cells["MaTK"].Value);
            textBox_TenTK.Text = Convert.ToString(row.Cells["TenTK"].Value);
            textBox_MatKhau.Text = Convert.ToString(row.Cells["MatKhau"].Value);
            comboBox_VaiTro.Text = Convert.ToString(row.Cells["VaiTro"].Value);
            comboBox_MaND.Text = Convert.ToString(row.Cells["MaNguoiDung"].Value);
        }

        private void UpdateGridView()
        {
            string query = "SELECT MaTK, TenTK, MatKhau, MaNguoiDung, VaiTro FROM TAIKHOAN";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView_TaiKhoan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView_TaiKhoan.DataSource = dataTable;
            }
        }

        private void LoadTaiKhoanToComboBox()
        {
            string query = "SELECT MaTK, MaNguoiDung, VaiTro FROM TAIKHOAN";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string maTK = reader.GetString(0);
                string maNguoiDung = reader.GetString(1);
                string vaiTro = reader.GetString(2);

                comboBox_MaTK.Items.Add(maTK);
                comboBox_MaND.Items.Add(maNguoiDung);
                comboBox_VaiTro.Items.Add(vaiTro);
            }
            reader.Close();
        }

        private void LoadDistinctComboBox(String columnName, System.Windows.Forms.ComboBox comboBox)
        {
            string query = $"SELECT DISTINCT {columnName} FROM TAIKHOAN";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            comboBox.Items.Clear();

            while (reader.Read())
            {
                string value = reader[columnName].ToString();
                comboBox.Items.Add(value);
            }
            reader.Close();
        }

        private bool IsMaTKValid(string maTK)
        {
            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM TAIKHOAN WHERE MaTK = @MaTK", connection);
            command.Parameters.AddWithValue("@MaTK", maTK);
            int count = (int)command.ExecuteScalar();

            if (count > 0)
            {
                return false; // TK Tồn tại
            }
            else
            {
                return true; // TK k tồn tại
            }
        }
        private bool IsMaNDValid(string maND)
        {
            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM TAIKHOAN WHERE MaNguoiDung = '@MaND'", connection);
            command.Parameters.AddWithValue("@MaND", maND);
            int count = (int)command.ExecuteScalar();

            if (count > 0)
            {
                return false; // TK Tồn tại
            }
            else
            {
                return true; // TK k tồn tại
            }
        }
    }
}
