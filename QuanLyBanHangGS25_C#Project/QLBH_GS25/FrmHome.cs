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
    public partial class FrmHome : Form
    {
        public FrmHome()
        {
            InitializeComponent();
            panel2button.Visible = false;
            SoPhieuNhap();
            SoPhieuXuat();
            SoHD();
            DoanhThu();

     
        }

         

        private string connectionString = @"Data Source= DESKTOP-4QED5F9\NGOCTRANG;Initial Catalog= QLBH_GS25;Integrated Security=True;Persist Security Info= True;User ID=east;Password=1";

        private Form activeForm2 = null;
        private void openChildForm2Button(Form childForm1)
        {
            if (activeForm2 != null)
                activeForm2.Close();
            activeForm2 = childForm1;
            childForm1.TopLevel = false;
            childForm1.FormBorderStyle = FormBorderStyle.None;
            childForm1.Dock = DockStyle.Fill;
            panel2button.Controls.Add(childForm1);
            panel2button.Tag = childForm1;
            childForm1.BringToFront();
            childForm1.Show();

        }

        private void btnTheThanhVien_Click(object sender, EventArgs e)
        {
            panel2button.BringToFront();
            panel2button.Visible = true;
            openChildForm2Button(new FrmQLTheThanhVien());
        }

        private void btnKhuyenMai_Click(object sender, EventArgs e)
        {
            panel2button.BringToFront();
            panel2button.Visible = true; // Đặt tên của Panel Thống kê trong giao diện của bạn
            openChildForm2Button(new FrmQLKhuyenMai());
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
                    lblPX.Text = soPhieuXuat.ToString();
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
                    lblPN.Text = soPhieuXuat.ToString();
                }
            }
        }
        private void SoHD()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
                string query = $"SELECT COUNT(MaHD) FROM HoaDon WHERE Convert(date,NgayLapHD) = '{currentDate}'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    int sohd = Convert.ToInt32(command.ExecuteScalar());
                    lblSoHD.Text = sohd.ToString();
                }
            }
        }
        private void DoanhThu()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
                string query = $"SELECT FORMAT(SUM(CT.SoLuong * MH.DonGia), '#,###.##') FROM HOADON HD JOIN CTHOADON CT ON HD.MaHD = CT.MaHD JOIN MatHang MH ON CT.MaMH = MH.MaMH WHERE Convert(date, HD.NgayLapHD) = '{currentDate}'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        decimal doanhThu = Convert.ToDecimal(result);
                        lblDT.Text = doanhThu.ToString("N0");
                    }
                    else
                    {
                        lblDT.Text = "0"; // Không có doanh thu
                    }
                }
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }
    }
}
