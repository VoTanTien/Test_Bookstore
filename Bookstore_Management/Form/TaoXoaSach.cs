using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Data.SqlClient;


namespace Bookstore_Management
{
    public partial class TaoXoaSach : Form
    {
        private string path_with_image = "";
        private string repository = "";
        SqlConnection connection;
        DatabaseConnection databaseConnection;
        String maSach, tenSach, tacGia, theLoai, gia, ngayPhatHanh,tenAnh="Chưa có";


        public TaoXoaSach()
        {
            InitializeComponent();
            databaseConnection = new DatabaseConnection();
            connection = databaseConnection.OpenConnection();
        }
        private void button_Save_Click(object sender, EventArgs e)
        {
            maSach = textBox_MaSach.Text;
            tenSach = textBox_TenSach.Text;
            tacGia = textBox_TacGia.Text;
            theLoai = textBox_TheLoai.Text;
            ngayPhatHanh = dateTimePicker_NPH.Value.ToString("yyyy/MM/dd");
            gia = textBox_Gia.Text;
            string query = "INSERT INTO THONGTINSACH(MaSach, TheLoai, TenSach,GiaBan, NgayPhatHanh, TenAnh) values (@MaSach, @TheLoai, @TenSach,@Gia,@NgayPhatHanh, @TenAnh)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MaSach", maSach);
            command.Parameters.AddWithValue("@TheLoai", theLoai);
            command.Parameters.AddWithValue("@TenSach", tenSach);
            command.Parameters.AddWithValue("@Gia", gia);
            command.Parameters.AddWithValue("@NgayPhatHanh", ngayPhatHanh);
            command.Parameters.AddWithValue("@TenAnh", tenAnh);
            var rows_affected = command.ExecuteNonQuery();
            if (rows_affected == 0) { MessageBox.Show("Mã sách đã tồn tại"); }
            else
            {
                MessageBox.Show("Đã thêm thành công");
                textBox_Gia.Clear();
                textBox_MaSach.Clear();
                textBox_TacGia.Clear();
                textBox_TenSach.Clear();
                textBox_TheLoai.Clear();
            }

        }
        private void GetImage()
        {
            Byte[] BytesOfImage;

            OpenFileDialog ofdPatient = new OpenFileDialog();

            DialogResult dgResult = ofdPatient.ShowDialog();

            if (dgResult == DialogResult.OK)
            {

                path_with_image = ofdPatient.FileName;

                try
                {
                    try
                    {
                        FileStream fsRead = new FileStream(path_with_image, FileMode.Open);
                        BytesOfImage = new Byte[fsRead.Length];
                        fsRead.Read(BytesOfImage, 0, BytesOfImage.Length);
                        fsRead.Close();
                    }
                    catch { return; }
                    tenAnh = Path.GetFileName(path_with_image);
                    Bitmap bm = Image.FromFile(path_with_image) as Bitmap;
                    bm.Save(repository + tenAnh, ImageFormat.Jpeg);
                }
                catch { }
            }
        }
        private void pictureBox_Addtacgia_Click(object sender, EventArgs e)
        {
            Addtacgia them = new Addtacgia();
            them.Show();
        }
        private void pictureBox_Exit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void pictureBox_AnhBia_Click(object sender, EventArgs e)
        {
            GetImage();
        }


    }
}
