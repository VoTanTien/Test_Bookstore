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
    public partial class Addtacgia : Form
    {
        SqlConnection connection;
        DatabaseConnection databaseConnection;
        public Addtacgia()
        {
            InitializeComponent();
            databaseConnection = new DatabaseConnection();
            connection = databaseConnection.OpenConnection();
        }

        private void pictureBox_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            string maTG = textBox_MTG.Text;
            string tenTG = textBox_TTG.Text;
            string ngay = dateTimePicker1.Value.ToString("yyyy/MM/dd");
            string query = "INSERT INTO TACGIA values(@MaTG, @TenTG, @Ngay)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaTG", maTG);
            command.Parameters.AddWithValue("@TenTG", tenTG);
            command.Parameters.AddWithValue("@Ngay", ngay);
            var rows_affected = command.ExecuteNonQuery();
            if (rows_affected == 0) { MessageBox.Show("Mã tác giả đã tồn tại"); }
            else
            {
                MessageBox.Show("Đã thêm thành công");
                textBox_MTG.Clear();
                textBox_TTG.Clear();
            }
        }
    }
}
