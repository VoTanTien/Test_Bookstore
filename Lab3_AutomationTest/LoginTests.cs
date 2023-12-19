using Bookstore_Management;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;




namespace Lab3_AutomationTest
{
    [TestClass]
    public class LoginTests
    {

        [TestMethod]
        public void CorrectAccount()
        {
            string username = "TienUIT";
            string password = "TienUIT123";

            // Init
            Login loginForm = new Login();

            TextBox txtBox_userName = loginForm.textBox_TenTK;
            TextBox txtBox_password = loginForm.textBox_MatKhau;
            Button btn_Login = loginForm.button_Login;

            //Action
            txtBox_userName.Text = username;
            txtBox_password.Text = password;
            btn_Login.PerformClick();
            Assert.AreEqual("TienUIT", txtBox_userName.Text);
            Assert.AreEqual("TienUIT123", txtBox_password.Text);

        }

        [TestMethod]
        public void EmptyUsernameAndCorrectPassword()
        {
            string username = "";
            string password = "TienUIT123";

            // Init
            Login loginForm = new Login();

            TextBox txtBox_userName = loginForm.textBox_TenTK;
            TextBox txtBox_password = loginForm.textBox_MatKhau;
            Button btn_Login = loginForm.button_Login;

            //Action
            txtBox_userName.Text = username;
            txtBox_password.Text = password;
            btn_Login.PerformClick();

        }

        [TestMethod]
        public void InCorrectUserNameAndCorrectPassword()
        {
            string username = "Tien";
            string password = "TienUIT123";

            // Init
            Login loginForm = new Login();

            TextBox txtBox_userName = loginForm.textBox_TenTK;
            TextBox txtBox_password = loginForm.textBox_MatKhau;
            Button btn_Login = loginForm.button_Login;

            //Action
            txtBox_userName.Text = username;
            txtBox_password.Text = password;
            btn_Login.PerformClick();


        }


        [TestMethod]
        public void CorrecUserNameAndEmptyPassword()
        {
            string username = "TienUIT";
            string password = "";

            // Init
            Login loginForm = new Login();

            TextBox txtBox_userName = loginForm.textBox_TenTK;
            TextBox txtBox_password = loginForm.textBox_MatKhau;
            Button btn_Login = loginForm.button_Login;

            //Action
            txtBox_userName.Text = username;
            txtBox_password.Text = password;
            btn_Login.PerformClick();


        }

        [TestMethod]
        public void CorrecUserNameAndIncorrectPassword()
        {
            string username = "TienUIT";
            string password = "TienUIT";

            // Init
            Login loginForm = new Login();

            TextBox txtBox_userName = loginForm.textBox_TenTK;
            TextBox txtBox_password = loginForm.textBox_MatKhau;
            Button btn_Login = loginForm.button_Login;

            //Action
            txtBox_userName.Text = username;
            txtBox_password.Text = password;
            btn_Login.PerformClick();

        }

       

    }
}