using Bookstore_Management;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab3_AutomationTest
{
    [TestClass]
    public class UpdateUserTests
    {
        [TestMethod]
        public void TestCase1()
        {
            string maTK = "TK04";
            string tenTK = "Tien";
            string mk = "TienUIT123";
            string maND = "ND14";
            string vaiTro = "ADMIN";

            // Init
            string role = "ADMIN";
            TaiKhoan tk = new TaiKhoan(role);
            ComboBox MaTK = tk.comboBox_MaTK;
            TextBox TenTK = tk.textBox_TenTK;
            TextBox MatKhau = tk.textBox_MatKhau;
            ComboBox MaND = tk.comboBox_MaND;
            ComboBox VaiTro = tk.comboBox_VaiTro;
            Button btnLuuLai = tk.button_Save;


            //Action
            MaTK.Text = maTK;
            TenTK.Text = tenTK;
            MatKhau.Text = mk;
            MaND.Text = maND;
            VaiTro.Text = vaiTro;
            btnLuuLai.PerformClick();
        }

        [TestMethod]
        public void Testcase2()
        {
            string maTK = "";
            string tenTK = "Tien";
            string mk = "TienUIT123";
            string maND = "ND14";
            string vaiTro = "ADMIN";

            // Init
            string role = "ADMIN";
            TaiKhoan tk = new TaiKhoan(role);

            ComboBox MaTK = tk.comboBox_MaTK;
            TextBox TenTK = tk.textBox_TenTK;
            TextBox MatKhau = tk.textBox_MatKhau;
            ComboBox MaND = tk.comboBox_MaND;
            ComboBox VaiTro = tk.comboBox_VaiTro;
            Button btnLuuLai = tk.button_Save;


            //Action
            MaTK.Text = maTK;
            TenTK.Text = tenTK;
            MatKhau.Text = mk;
            MaND.Text = maND;
            VaiTro.Text = vaiTro;
            btnLuuLai.PerformClick();
        }

        [TestMethod]
        public void Testcase3()
        {
            string maTK = "TK04";
            string tenTK = "Tien";
            string mk = "";
            string maND = "ND14";
            string vaiTro = "ADMIN";

            // Init
            string role = "ADMIN";
            TaiKhoan tk = new TaiKhoan(role);

            ComboBox MaTK = tk.comboBox_MaTK;
            TextBox TenTK = tk.textBox_TenTK;
            TextBox MatKhau = tk.textBox_MatKhau;
            ComboBox MaND = tk.comboBox_MaND;
            ComboBox VaiTro = tk.comboBox_VaiTro;
            Button btnLuuLai = tk.button_Save;


            //Action
            MaTK.Text = maTK;
            TenTK.Text = tenTK;
            MatKhau.Text = mk;
            MaND.Text = maND;
            VaiTro.Text = vaiTro;
            btnLuuLai.PerformClick();
        }

        [TestMethod]
        public void Testcase4()
        {
            string maTK = "TK04";
            string tenTK = "";
            string mk = "TienUIT123";
            string maND = "ND14";
            string vaiTro = "ADMIN";

            // Init
            string role = "ADMIN";
            TaiKhoan tk = new TaiKhoan(role);

            ComboBox MaTK = tk.comboBox_MaTK;
            TextBox TenTK = tk.textBox_TenTK;
            TextBox MatKhau = tk.textBox_MatKhau;
            ComboBox MaND = tk.comboBox_MaND;
            ComboBox VaiTro = tk.comboBox_VaiTro;
            Button btnLuuLai = tk.button_Save;


            //Action
            MaTK.Text = maTK;
            TenTK.Text = tenTK;
            MatKhau.Text = mk;
            MaND.Text = maND;
            VaiTro.Text = vaiTro;
            btnLuuLai.PerformClick();
        }

        [TestMethod]
        public void Testcase5()
        {
            string maTK = "TK06";
            string tenTK = "Tien";
            string mk = "TienUIT123";
            string maND = "ND14";
            string vaiTro = "ADMIN";

            // Init
            string role = "ADMIN";
            TaiKhoan tk = new TaiKhoan(role);

            ComboBox MaTK = tk.comboBox_MaTK;
            TextBox TenTK = tk.textBox_TenTK;
            TextBox MatKhau = tk.textBox_MatKhau;
            ComboBox MaND = tk.comboBox_MaND;
            ComboBox VaiTro = tk.comboBox_VaiTro;
            Button btnLuuLai = tk.button_Save;


            //Action
            MaTK.Text = maTK;
            TenTK.Text = tenTK;
            MatKhau.Text = mk;
            MaND.Text = maND;
            VaiTro.Text = vaiTro;
            btnLuuLai.PerformClick();
        }

        [TestMethod]
        public void Testcase6()
        {
            string maTK = "TK04";
            string tenTK = "Tien";
            string mk = "TienUIT123";
            string maND = "";
            string vaiTro = "ADMIN";

            // Init
            string role = "ADMIN";
            TaiKhoan tk = new TaiKhoan(role);

            ComboBox MaTK = tk.comboBox_MaTK;
            TextBox TenTK = tk.textBox_TenTK;
            TextBox MatKhau = tk.textBox_MatKhau;
            ComboBox MaND = tk.comboBox_MaND;
            ComboBox VaiTro = tk.comboBox_VaiTro;
            Button btnLuuLai = tk.button_Save;


            //Action
            MaTK.Text = maTK;
            TenTK.Text = tenTK;
            MatKhau.Text = mk;
            MaND.Text = maND;
            VaiTro.Text = vaiTro;
            btnLuuLai.PerformClick();
        }

        [TestMethod]
        public void Testcase7()
        {
            string maTK = "TK04";
            string tenTK = "Tien";
            string mk = "TienUIT123";
            string maND = "ND14";
            string vaiTro = "";

            // Init
            string role = "ADMIN";
            TaiKhoan tk = new TaiKhoan(role);

            ComboBox MaTK = tk.comboBox_MaTK;
            TextBox TenTK = tk.textBox_TenTK;
            TextBox MatKhau = tk.textBox_MatKhau;
            ComboBox MaND = tk.comboBox_MaND;
            ComboBox VaiTro = tk.comboBox_VaiTro;
            Button btnLuuLai = tk.button_Save;


            //Action
            MaTK.Text = maTK;
            TenTK.Text = tenTK;
            MatKhau.Text = mk;
            MaND.Text = maND;
            VaiTro.Text = vaiTro;
            btnLuuLai.PerformClick();
        }


    }
}
