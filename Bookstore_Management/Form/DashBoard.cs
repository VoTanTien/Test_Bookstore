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
    public partial class DashBoard : Form
    {
        private string userID;
        private bool Tien = true;
        DatabaseConnection databaseConnection;
        SqlConnection connection;
        string Ma;
        string role;
        public DashBoard()
        {
            InitializeComponent();
            databaseConnection = new DatabaseConnection();
            connection = databaseConnection.OpenConnection();
            button_ThongKe.BackColor = Color.HotPink;
            ThongKe thongke = new ThongKe();
            AddConTrol(thongke);
        }


        public DashBoard(string UserID,string userrole,string ma) : this()
        {
            
            userID = UserID;
            label_UserName.Text = userID;
            role = userrole;
            label_RoleUser.Text = userrole;
            Ma = ma;
        }
        
        public void GetUserRole(string username)
        {
            string role = "";
            string query1 = "SELECT VaiTro FROM TAIKHOAN WHERE TenTK = @Username";
            SqlCommand cmd = new SqlCommand(query1, connection);
            cmd.Parameters.AddWithValue("@Username", username);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                label_RoleUser.Text = reader.GetString(0);
            }
            reader.Close();
        }

        ////////BUTTON CLICK///////
        private void pictureBox_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
            
        }
        private void Pick(Button c)
        {
            button_NhapSach.BackColor = button_BanSach.BackColor = button_BaoCao.BackColor = button_LichSuBan.BackColor = button_PhieuThuTien.BackColor = button_ThongKe.BackColor = button_TraCuuSach.BackColor = Color.MediumSlateBlue ;
            c.BackColor = Color.HotPink;
            
        }
        public void AddConTrol(Control c)
        {
            c.Dock = DockStyle.Fill;
            panel_UserControl.Controls.Clear();
            panel_UserControl.Controls.Add(c);
        }
        public void BaoCaoClick(object sender, EventArgs e)
        {
            BaoCaoCongNo baocao = new BaoCaoCongNo();
            AddConTrol(baocao);
        }
        private void button_ThongKe_Click(object sender, EventArgs e)
        {
            Button temp = sender as Button;
            Pick(temp);
            ThongKe thongke = new ThongKe();
            AddConTrol(thongke);
        }

        private void button_NhapSach_Click(object sender, EventArgs e)
        {
            Button temp = sender as Button;
            Pick(temp);
            NhapSach nhapsach = new NhapSach();
            AddConTrol(nhapsach);
        }

        private void button_BanSach_Click(object sender, EventArgs e)
        {
            Button temp = sender as Button;
            Pick(temp);
            BanSach bansach = new BanSach(Ma);
            AddConTrol(bansach);
        }

        private void button_TraCuuSach_Click(object sender, EventArgs e)
        {
            Button temp = sender as Button;
            Pick(temp);
            TraCuuSach tracuu = new TraCuuSach();
            AddConTrol(tracuu);
        }

        private void button_PhieuThuTien_Click(object sender, EventArgs e)
        {
            Button temp = sender as Button;
            Pick(temp);
            PhieuThuTien phieuthu = new PhieuThuTien();
            AddConTrol(phieuthu);
        }

        private void button_BaoCao_Click(object sender, EventArgs e)
        {
            Button temp = sender as Button;
            Pick(temp);
            BaoCaoTon baocao = new BaoCaoTon(this);
            AddConTrol(baocao);
        }

        private void button_LichSuBan_Click(object sender, EventArgs e)
        {
            Button temp = sender as Button;
            Pick(temp);
            LichSuBanSach lichsu = new LichSuBanSach();
            AddConTrol(lichsu);
        }

        private void pictureBox_Setting_Click(object sender, EventArgs e)
        {
            Setting setting = new Setting(userID);
            setting.Show();
            
        }

      
        private void pictureBox_TaiKhoan_Click(object sender, EventArgs e)
        {
            TaiKhoan taikhoan = new TaiKhoan(role);
            taikhoan.Show();
        }

        private void pictureBox_ThemSach_Click(object sender, EventArgs e)
        {
            TaoXoaSach taosach = new TaoXoaSach();
            taosach.Show();
        }

       
    }
}
