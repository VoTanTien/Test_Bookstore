using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bookstore_Management
{
    public partial class BaoCaoTon : UserControl
    {
        DashBoard temp;
        private string connectionString;
        private SqlConnection connection;
        private SqlDataAdapter adapter;
        private DataTable dataTable;
        DatabaseConnection databaseConnection;
        public BaoCaoTon()
        {
            InitializeComponent();
            button_BaoCaoTon.BackColor = Color.HotPink;
            databaseConnection = new DatabaseConnection();
            connection = databaseConnection.OpenConnection();

            adapter = new SqlDataAdapter();
            dataTable = new DataTable();
            LoadData();


        }
        public BaoCaoTon(DashBoard a)
        {
            InitializeComponent();
            button_BaoCaoTon.BackColor = Color.HotPink;
            temp = a;
            databaseConnection = new DatabaseConnection();
            connection = databaseConnection.OpenConnection();

            adapter = new SqlDataAdapter();
            dataTable = new DataTable();
            LoadData();
        }

        private void LoadData()
        {
            string query = "select * from BAOCAOTONKHO";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView_BaoCaoTon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView_BaoCaoTon.DataSource = dataTable;
            }
        }

        private void button_BaoCaoCongNo_Click(object sender, EventArgs e)
        {
            BaoCaoCongNo baocao = new BaoCaoCongNo(temp);
            temp.AddConTrol(baocao);

        }

        private void pictureBox_XuatBaoCao_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM BAOCAOTONKHO";
            DataTable dataTable = new DataTable();

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }
            }

            ExportToExcel(dataTable);
        }

        private void button_BaoCaoTon_Click(object sender, EventArgs e)
        {
            String thang = maskedTextBox_Thang.Text;

            // Tạo đối tượng SqlCommand để thực thi truy vấn
            SqlCommand command = new SqlCommand("SELECT * FROM BAOCAOTONKHO WHERE Thang = @Thang", connection);
            command.Parameters.AddWithValue("@Thang", thang); // Sử dụng giá trị tháng từ MaskedTextBox

            // Tạo đối tượng SqlDataAdapter để lấy dữ liệu từ truy vấn
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            // Tạo đối tượng DataTable để lưu trữ dữ liệu từ truy vấn
            DataTable dataTable = new DataTable();

            // Đổ dữ liệu từ SqlDataAdapter vào DataTable
            adapter.Fill(dataTable);

            // Hiển thị dữ liệu lên GridView
            dataGridView_BaoCaoTon.DataSource = dataTable;


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
                string filePath = "D:\\baocaotonkho.xlsx";
                FileInfo excelFile = new FileInfo(filePath);
                excelPackage.SaveAs(excelFile);
            }
        }

        private void thang(object sender, KeyEventArgs e)
        {
            string thang = maskedTextBox_Thang.Text;

            // Thực hiện truy vấn dữ liệu dựa vào tháng
            string query = "SELECT * FROM BAOCAOTONKHO WHERE Thang = @Thang";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Thang", thang);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // Hiển thị dữ liệu trong DataGridView
            dataGridView_BaoCaoTon.DataSource = dataTable;
        }
    }
}
