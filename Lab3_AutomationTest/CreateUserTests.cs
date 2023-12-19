using Bookstore_Management;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab3_AutomationTest
{
    [TestClass]
    public class CreateUserTests
    {
        [TestMethod]
        public void Testcase1()
        {
            string maTK = "TK05";
            string tenTK = "TinhUIT";
            string mk = "12345";
            string maND = "ND09";
            string vaiTro = "ADMIN";

            // Init
            string role = "ADMIN";
            TaiKhoan tk = new TaiKhoan(role);

            ComboBox MaTK = tk.comboBox_MaTK;
            TextBox TenTK = tk.textBox_TenTK;
            TextBox MatKhau = tk.textBox_MatKhau;
            ComboBox MaND = tk.comboBox_MaND;
            ComboBox VaiTro = tk.comboBox_VaiTro;
            Button btnThem = tk.button_Add;


            //Action
            MaTK.Text = maTK;
            TenTK.Text = tenTK;
            MatKhau.Text = mk;
            MaND.Text = maND;
            VaiTro.Text = vaiTro;
            btnThem.PerformClick();
        }

        [TestMethod]
        public void Testcase2()
        {
            string maTK = "TK05";
            string tenTK = "";
            string mk = "12345";
            string maND = "ND09";
            string vaiTro = "ADMIN";

            // Init
            string role = "ADMIN";
            TaiKhoan tk = new TaiKhoan(role);

            ComboBox MaTK = tk.comboBox_MaTK;
            TextBox TenTK = tk.textBox_TenTK;
            TextBox MatKhau = tk.textBox_MatKhau;
            ComboBox MaND = tk.comboBox_MaND;
            ComboBox VaiTro = tk.comboBox_VaiTro;
            Button btnThem = tk.button_Add;


            //Action
            MaTK.Text = maTK;
            TenTK.Text = tenTK;
            MatKhau.Text = mk;
            MaND.Text = maND;
            VaiTro.Text = vaiTro;
            btnThem.PerformClick();
        }

        [TestMethod]
        public void Testcase3()
        {
            string maTK = "TK05";
            string tenTK = "TinhUIT";
            string mk = "";
            string maND = "ND09";
            string vaiTro = "ADMIN";

            // Init
            string role = "ADMIN";
            TaiKhoan tk = new TaiKhoan(role);

            ComboBox MaTK = tk.comboBox_MaTK;
            TextBox TenTK = tk.textBox_TenTK;
            TextBox MatKhau = tk.textBox_MatKhau;
            ComboBox MaND = tk.comboBox_MaND;
            ComboBox VaiTro = tk.comboBox_VaiTro;
            Button btnThem = tk.button_Add;


            //Action
            MaTK.Text = maTK;
            TenTK.Text = tenTK;
            MatKhau.Text = mk;
            MaND.Text = maND;
            VaiTro.Text = vaiTro;
            btnThem.PerformClick();
        }

        [TestMethod]
        public void Testcase4()
        {
            string maTK = "";
            string tenTK = "TienUIT";
            string mk = "12345";
            string maND = "ND09";
            string vaiTro = "ADMIN";

            // Init
            string role = "ADMIN";
            TaiKhoan tk = new TaiKhoan(role);

            ComboBox MaTK = tk.comboBox_MaTK;
            TextBox TenTK = tk.textBox_TenTK;
            TextBox MatKhau = tk.textBox_MatKhau;
            ComboBox MaND = tk.comboBox_MaND;
            ComboBox VaiTro = tk.comboBox_VaiTro;
            Button btnThem = tk.button_Add;


            //Action
            MaTK.Text = maTK;
            TenTK.Text = tenTK;
            MatKhau.Text = mk;
            MaND.Text = maND;
            VaiTro.Text = vaiTro;
            btnThem.PerformClick();
        }

        [TestMethod]
        public void Testcase5()
        {
            string maTK = "TK05";
            string tenTK = "TinhUIT";
            string mk = "12345";
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
            Button btnThem = tk.button_Add;


            //Action
            MaTK.Text = maTK;
            TenTK.Text = tenTK;
            MatKhau.Text = mk;
            MaND.Text = maND;
            VaiTro.Text = vaiTro;
            btnThem.PerformClick();
        }

        [TestMethod]
        public void Testcase6()
        {
            string maTK = "TK05";
            string tenTK = "TinhUIT";
            string mk = "12345";
            string maND = "ND09";
            string vaiTro = "USER";

            // Init
            string role = "ADMIN";
            TaiKhoan tk = new TaiKhoan(role);

            ComboBox MaTK = tk.comboBox_MaTK;
            TextBox TenTK = tk.textBox_TenTK;
            TextBox MatKhau = tk.textBox_MatKhau;
            ComboBox MaND = tk.comboBox_MaND;
            ComboBox VaiTro = tk.comboBox_VaiTro;
            Button btnThem = tk.button_Add;


            //Action
            MaTK.Text = maTK;
            TenTK.Text = tenTK;
            MatKhau.Text = mk;
            MaND.Text = maND;
            VaiTro.Text = vaiTro;
            btnThem.PerformClick();
        }

        [TestMethod]
        public void Testcase7()
        {
            string maTK = "TK05";
            string tenTK = "TinhUIT";
            string mk = "12345";
            string maND = "ND09";
            string vaiTro = "";

            // Init
            string role = "ADMIN";
            TaiKhoan tk = new TaiKhoan(role);

            ComboBox MaTK = tk.comboBox_MaTK;
            TextBox TenTK = tk.textBox_TenTK;
            TextBox MatKhau = tk.textBox_MatKhau;
            ComboBox MaND = tk.comboBox_MaND;
            ComboBox VaiTro = tk.comboBox_VaiTro;
            Button btnThem = tk.button_Add;


            //Action
            MaTK.Text = maTK;
            TenTK.Text = tenTK;
            MatKhau.Text = mk;
            MaND.Text = maND;
            VaiTro.Text = vaiTro;
            btnThem.PerformClick();
        }

        [TestMethod]
        public void Testcase8()
        {
            string maTK = "TK04";
            string tenTK = "TinhUIT";
            string mk = "12345";
            string maND = "ND09";
            string vaiTro = "ADMIN";

            // Init
            string role = "ADMIN";
            TaiKhoan tk = new TaiKhoan(role);

            ComboBox MaTK = tk.comboBox_MaTK;
            TextBox TenTK = tk.textBox_TenTK;
            TextBox MatKhau = tk.textBox_MatKhau;
            ComboBox MaND = tk.comboBox_MaND;
            ComboBox VaiTro = tk.comboBox_VaiTro;
            Button btnThem = tk.button_Add;


            //Action
            MaTK.Text = maTK;
            TenTK.Text = tenTK;
            MatKhau.Text = mk;
            MaND.Text = maND;
            VaiTro.Text = vaiTro;
            btnThem.PerformClick();
        }

       
    }
}
