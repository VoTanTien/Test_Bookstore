using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using OfficeOpenXml;

namespace Bookstore_Management
{
    public partial class BaoCaoCongNo : UserControl
    {
        DashBoard temp;
        private string connectionString;
        private SqlConnection connection;
        private SqlDataAdapter adapter;
        private DataTable dataTable;
        DatabaseConnection databaseConnection;
        public BaoCaoCongNo()
        {
            InitializeComponent();
            button_BaoCaoCongNo.BackColor = Color.HotPink;
            databaseConnection = new DatabaseConnection();
            connection = databaseConnection.OpenConnection();

            adapter = new SqlDataAdapter();
            dataTable = new DataTable();
            LoadData();

        }

        private void LoadData()
        {
            string query = "select * from BAOCAOTHUNO";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView_BaoCaoCongNo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView_BaoCaoCongNo.DataSource = dataTable;
            }
        }

        public BaoCaoCongNo(DashBoard a)
        {
            InitializeComponent();
            button_BaoCaoCongNo.BackColor = Color.HotPink;
            temp = a;
            databaseConnection = new DatabaseConnection();
            connection = databaseConnection.OpenConnection();

            adapter = new SqlDataAdapter();
            dataTable = new DataTable();
            LoadData();
        }

        private void button_BaoCaoTon_Click(object sender, EventArgs e)
        {
            BaoCaoTon baocao = new BaoCaoTon(temp);
            temp.AddConTrol(baocao);
        }

        private void dataGridView_BaoCaoCongNo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void pictureBox_XuatBaoCao_Click(object sender, EventArgs e)
        {
            // Thực hiện truy vấn cơ sở dữ liệu
            string query = "SELECT * FROM BAOCAOTHUNO";
            DataTable dataTable = new DataTable();

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }
            }

            // Gọi phương thức xuất dữ liệu ra file Excel và truyền dữ liệu DataTable vào
            ExportToExcel(dataTable);
        }

        private void ExportToExcel(DataTable dataTable)
        {
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Báo cáo");

                // Ghi dữ liệu vào worksheet từ DataTable
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1].Value = dataTable.Columns[i].ColumnName;
                }

                for (int row = 0; row < dataTable.Rows.Count; row++)
                {
                    for (int col = 0; col < dataTable.Columns.Count; col++)
                    {
                        worksheet.Cells[row + 2, col + 1].Value = dataTable.Rows[row][col];
                    }
                }

                // Lưu workbook vào file Excel
                string filePath = "D:\\baocaocongno.xlsx"; 
                FileInfo excelFile = new FileInfo(filePath);
                excelPackage.SaveAs(excelFile);
            }
        }

        private void month(object sender, KeyEventArgs e)
        {
            string thang = maskedTextBox_Thang.Text;

            // Thực hiện truy vấn dữ liệu dựa vào tháng
            string query = "SELECT * FROM BAOCAOTHUNO WHERE Thang = @Thang";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Thang", thang);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // Hiển thị dữ liệu trong DataGridView
            dataGridView_BaoCaoCongNo.DataSource = dataTable;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "select COUNT(*) from KHACHHANG where SoTienNo>0";
            SqlCommand cmd = new SqlCommand(query, connection);
            int count = (int)cmd.ExecuteScalar();
            if (count > 0)
            {
                XuatBaoCao();
            }
            LoadData();
        }

        private void XuatBaoCao()
        {
            int d = 0;
            DataTable dt = new DataTable();
            dt.Columns.Add("MaKH");
            dt.Columns.Add("SoTienNo");
            string query1 = "select MaKH,SoTienNo from KHACHHANG where SoTienNo>0 ";
            SqlCommand cmd1 = new SqlCommand(query1, connection);
            SqlDataReader reader = cmd1.ExecuteReader();
            while (reader.Read())
            {
                DataRow row = dt.NewRow();
                row["MaKH"] = reader.GetString(0);
                row["SoTienNo"] = reader.GetInt32(1);
                dt.Rows.Add(row);
            }
            reader.Close();
            string ngay = DateTime.Now.Month.ToString();
            foreach (DataRow row in dt.Rows)
            {
                string qr = "INSERT INTO BAOCAOTHUNO(MaBCTN, MaKH, Thang, NoDau, NoCuoi, SoTienNo) values (@MaBCTN, @MaKH, @Thang, @NoDau, @NoCuoi, @SoTienNo) ";
                string MaBCTN = GetMaBCTN();
                SqlCommand cmd = new SqlCommand(qr, connection);
                cmd.Parameters.AddWithValue("@MaBCTN", MaBCTN);
                cmd.Parameters.AddWithValue("MaKH", row["MaKH"].ToString());
                cmd.Parameters.AddWithValue("@Thang", ngay);
                cmd.Parameters.AddWithValue("@NoDau", row["SoTienNo"].ToString());
                cmd.Parameters.AddWithValue("@NoCuoi", row["SoTienNo"].ToString());
                cmd.Parameters.AddWithValue("@SoTienNo", row["SoTienNo"].ToString());
                var rowef = cmd.ExecuteNonQuery();
                if (rowef > 0)
                    d++;
            }
            MessageBox.Show("Thêm" + d + " báo cáo" );
        }

        private string GetMaBCTN()
        {
            string ma = "           ";
            string qr2 = "select Max(MaBCTN) from BAOCAOTHUNO";
            SqlCommand hd = new SqlCommand(qr2, connection);
            SqlDataReader reader = hd.ExecuteReader();
            while (reader.Read())
            {
                ma = reader.GetString(0);
            }
            reader.Close();
            string max = ma.Substring(3, ma.Length - 4);
            int m = Convert.ToInt32(max);
            m++;
            ma = "BCN" + m;
            return ma;
        }
    }
    
}
