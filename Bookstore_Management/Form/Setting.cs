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
    public partial class Setting : Form
    {
        DatabaseConnection databaseConnection;
        SqlConnection sqlConnection;
        private string UserID;
        public Setting()
        {
            InitializeComponent();
            databaseConnection = new DatabaseConnection();
            sqlConnection = databaseConnection.OpenConnection();
            ShowThamSo("ST01", textBox_SLNhap);
            ShowThamSo("ST02", textBox_SLTon);
            ShowThamSo("ST04", textBox_SoTienNo);
            ShowThamSo("ST05", textBoxSoLuongTonToiDa);

       
        }
        public Setting(string userid) : this()
        {
            this.UserID = userid;
        }
        private void pictureBox_Exit_Click(object sender, EventArgs e)
        {
            this.Hide();
            //bool check;
            //if (checkBox_SoTienThu.Checked)
            //{
            //    check = true;
            //    DashBoard main = new DashBoard(check, UserID);
            //    main.Show();
            //}
            //else
            //{
            //    check = false;
            //    DashBoard main = new DashBoard(check, UserID);
            //    main.Show();
            //}
        }
        private void ShowThamSo(string maThamSo, TextBox text)
        {
            string query = "SELECT GiaTri FROM THAMSO WHERE MaThamSo = @MaThamSo";
            SqlCommand command = new SqlCommand(query, sqlConnection);
            command.Parameters.AddWithValue("@MaThamSo", maThamSo);
            text.Clear();

            object giaTri = command.ExecuteScalar();
            if (giaTri != null)
            {
                text.Text = giaTri.ToString();
            }
        }
        private void button_Save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxSoLuongTonToiDa.Text) || string.IsNullOrEmpty(textBox_SLNhap.Text)
                || string.IsNullOrEmpty(textBox_SLTon.Text) || string.IsNullOrEmpty(textBox_SoTienNo.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                return;
            }

            UpdateThamSo("ST01", float.Parse(textBox_SLNhap.Text));
            UpdateThamSo("ST02", float.Parse(textBox_SLTon.Text));
            UpdateThamSo("ST04", float.Parse(textBox_SoTienNo.Text));
            UpdateThamSo("ST05", float.Parse(textBoxSoLuongTonToiDa.Text));

            bool isChecked = checkBox_SoTienThu.Checked;
            if (isChecked)
            {
                UpdateThamSo("ST03", 1);
            }
            else
            {
                UpdateThamSo("ST03", 0);
            }

            MessageBox.Show("Cập nhật giá trị thành công");


            sqlConnection.Close();

        }

        private void UpdateThamSo(string maThamSo, float giaTri)
        {
            try
            {
                string updateQuery = "UPDATE THAMSO SET GiaTri = @GiaTri WHERE MaThamSo = @MaThamSo";
                SqlCommand updateCommand = new SqlCommand(updateQuery, sqlConnection);
                updateCommand.Parameters.AddWithValue("@GiaTri", giaTri);
                updateCommand.Parameters.AddWithValue("@MaThamSo", maThamSo);
                int rowsAffected = updateCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật giá trị cho MaThamSo: " + maThamSo + "\n" + ex.Message);
            }
        }
    }
}
            