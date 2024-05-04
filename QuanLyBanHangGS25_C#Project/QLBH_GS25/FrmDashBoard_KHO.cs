using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QLBH_GS25
{
    public partial class FrmDashBoard_KHO : Form
    {
        public FrmDashBoard_KHO()
        {
            InitializeComponent();
            SoPhieuXuat();
            SoPhieuNhap();
        }
        KetNoiCSDL data = new KetNoiCSDL();
        private string connectionString = @"Data Source= East\SQLEXPRESS;Initial Catalog= QLBH_GS25;Integrated Security=True;Persist Security Info= True;User ID=east;Password=1";
        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            listBoxHienThi.Items.Clear(); // Xóa các item cũ trong ListBox

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT MaMH, TenMH, SoLuongTon FROM MatHang WHERE SoLuongTon < 50";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string maMH = reader["MaMH"].ToString();
                            string tenMH = reader["TenMH"].ToString();
                            int soLuongTon = Convert.ToInt32(reader["SoLuongTon"]);

                            string item = $"{maMH} - {tenMH} (Số lượng tồn: {soLuongTon})";
                            listBoxHienThi.Items.Add(item);
                        }
                    }
                }
            }
        }
        private void SoPhieuXuat()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
                string query = $"SELECT COUNT(MaPX) FROM PhieuXuat WHERE NgayLapPX = '{currentDate}'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    int soPhieuXuat = Convert.ToInt32(command.ExecuteScalar());
                    lblSoPX.Text = soPhieuXuat.ToString();
                }
            }
        }

        private void SoPhieuNhap()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
                string query = $"SELECT COUNT(MaPhieuNK) FROM PhieuNhapKho WHERE NgayLapNK = '{currentDate}'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    int soPhieuXuat = Convert.ToInt32(command.ExecuteScalar());
                    lblSoPN.Text = soPhieuXuat.ToString();
                }
            }
        }

        private void btnNCC_Click(object sender, EventArgs e)
        {
            listBoxHienThi.Items.Clear(); // Xóa các item cũ trong ListBox
            listBoxHienThi.ItemHeight = 40; // Chiều cao tuỳ chọn
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT MaNCC, TenNCC, SDTNCC FROM NhaCungCap";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string maNCC = reader["MaNCC"].ToString();
                            string SDTNCC =reader["SDTNCC"].ToString();

                            string item = $"{maNCC} - (SĐT: {SDTNCC})";
                            listBoxHienThi.Items.Add(item);
                        }
                    }
                }
            }
        }

       
    }
}
