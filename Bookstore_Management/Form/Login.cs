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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Bookstore_Management
{
    public partial class Login : Form
    {
        DatabaseConnection databaseConnection;
        SqlConnection connection;

        public Login()
        {
            InitializeComponent();
            databaseConnection = new DatabaseConnection();
            connection = databaseConnection.OpenConnection();
        }

        private void pictureBox_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_Login_Click(object sender, EventArgs e)
        {
            Dangnhap();
            
        }
        private void Dangnhap()
        {
            string username = textBox_TenTK.Text;
            string password = textBox_MatKhau.Text;

            if (AuthenticateUser(username, password))
            {
                MessageBox.Show("Đăng nhập thành công");
                this.Hide();
                DashBoard main = new DashBoard(GetUserName(GetUserID(username)),GetUserRole(username),GetUserID(username));
                main.Show();
            }
            else if (textBox_TenTK.Text == "")
            {
                MessageBox.Show("Yêu cầu nhập tài khoản");
            }
            else if (textBox_MatKhau.Text == "")
            {
                MessageBox.Show("Yêu cầu nhập mật khẩu");
            }
            else
            {
               
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu");
                textBox_MatKhau.Clear();
                textBox_TenTK.Focus();
            }
        }
        public string GetUserRole(string username)
        {
            string role = "";
            string query1 = "SELECT VaiTro FROM TAIKHOAN WHERE TenTK = @Username";
            SqlCommand cmd = new SqlCommand(query1, connection);
            cmd.Parameters.AddWithValue("@Username", username);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                role = reader.GetString(0);
            }
            reader.Close();
            return role;
        }
        private bool AuthenticateUser(string username, string password)
        {
            bool isAuthenticated = false;
            string query = "SELECT COUNT(*) FROM TAIKHOAN WHERE TenTK = @Username AND MatKhau = @Password";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);
            int count = (int)command.ExecuteScalar();
            if (count > 0)
            {
                isAuthenticated = true;
            }
            return isAuthenticated;
        }
        public string GetUserID(string username)
        {
            string userID = "";
            string query = "SELECT MaNguoiDung FROM TAIKHOAN WHERE TenTK = @Username";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Username", username);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                userID = reader.GetString(0);
            }
            reader.Close();
            return userID;
        }
        public string GetUserName(string userID)
        {
            string userName = "";
            string query = "SELECT HoTen FROM NGUOIDUNG WHERE MaNguoiDung = @UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", userID);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                userName = reader.GetString(0);
            }
            reader.Close();
            return userName;
        }
        private void NhanNut_Enter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (sender.Equals(this.textBox_TenTK))
                    textBox_MatKhau.Focus();
                else Dangnhap();
            }
        }
    }
}
