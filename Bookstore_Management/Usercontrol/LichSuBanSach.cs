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
    public partial class LichSuBanSach : UserControl
    {
        DatabaseConnection databaseConnection;
        SqlConnection connection;
        public LichSuBanSach()
        {
            InitializeComponent();
            databaseConnection = new DatabaseConnection();
            connection = databaseConnection.OpenConnection();

            UpdateGridView();

        }

        private void UpdateGridView()
        {
            string query = "SELECT * FROM HOADON";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView_LichSuBan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView_LichSuBan.DataSource = dataTable;
            }
        }

        private void button_TimKiem_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = dateTimePicker_NgayBan.Value.Date;
            TimKiemTheoNgayHD(selectedDate);
        }

        private void TimKiemTheoNgayHD(DateTime selectedDate)
        {
            string query = "SELECT * FROM HOADON WHERE NgayHD = @NgayHD" ;
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NgayHD", selectedDate.Date);
            SqlDataReader reader = command.ExecuteReader();

            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            //dataGridView_LichSuBan.Dock = DockStyle.Fill;
            dataGridView_LichSuBan.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dataGridView_LichSuBan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView_LichSuBan.DataSource = dataTable;
            reader.Close();
        }

    }
}
