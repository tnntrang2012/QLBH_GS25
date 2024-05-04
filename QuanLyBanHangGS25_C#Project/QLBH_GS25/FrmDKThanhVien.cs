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
    public partial class FrmDKThanhVien : Form
    {
        public FrmDKThanhVien()
        {
            InitializeComponent();
            txtHoTen.Focus();
            txtMaKH.Text = TaoMaKH().ToString();
            txtMaKH.ReadOnly = true;
        }
        KetNoiCSDL data = new KetNoiCSDL();
        private BindingSource bdsoure = new BindingSource();
        private DataTable DT = new DataTable();
       
        private void refresh()
        {
            txtHoTen.ResetText();
            dtNgaySinh.ResetText();
            cboGioiTinh.ResetText();
            txtDiaChi.ResetText();
            txtEmail.ResetText();
            txtSDT.ResetText();
        }
        private string TaoMaKH()
        {
            // Lấy mã KH cuối cùng từ cơ sở dữ liệu (giả sử mã KH có dạng "KH001", "KH002",...)
            string maKHcuoi = "";

            // Kết nối và mở kết nối tới cơ sở dữ liệu
            using (SqlConnection connection = new SqlConnection(data.connectString))
            {
                connection.Open();

                // Xây dựng câu truy vấn SQL để lấy mã KH cuối cùng
                string sqlQuery = "SELECT TOP 1 MaKH FROM KhachHang ORDER BY MaKH DESC";

                // Tạo đối tượng SqlCommand và thực thi câu truy vấn SQL
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        maKHcuoi = reader["MaKH"].ToString();
                    }

                    reader.Close();
                }
            }

            // Tạo mã KH tự động tăng bằng cách tăng mã KH cuối cùng lên 1 đơn vị
            string maKH = "KH001"; // Mã KH mặc định nếu không có mã KH trong cơ sở dữ liệu

            if (!string.IsNullOrEmpty(maKHcuoi))
            {
                int soKHcuoi = int.Parse(maKHcuoi.Substring(2));
                maKH = "KH" + (soKHcuoi + 1).ToString("D3");
            }
            return maKH;
        }

        private void ThemKH(out string MaKH)
        {


            MaKH = "";
            

            try
            {
                
                if (string.IsNullOrEmpty(txtHoTen.Text))
                {
                    MessageBox.Show("Nhập thiếu Họ tên khách hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtHoTen.Focus();
                    return;
                }
                DateTime ngaySinh;
                if (!DateTime.TryParse(dtNgaySinh.Text, out ngaySinh))
                {
                    MessageBox.Show("Vui lòng nhập ngày sinh hợp lệ (dd/MM/yyyy).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrEmpty(cboGioiTinh.Text))
                {
                    MessageBox.Show("Vui lòng chọn giới tính khách hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cboGioiTinh.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtSDT.Text))
                {
                    MessageBox.Show("Nhập Số điện thoại khách hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSDT.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtEmail.Text))
                {
                    MessageBox.Show("Nhập Email; tin khách hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmail.Focus();
                    return;
                }
                if (MessageBox.Show("Lưu thông tin khách hàng này?", "Đăng kí thành viên", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    MaKH = TaoMaKH();
                    string Hoten = txtHoTen.Text;
                    DateTime NgaySinh = dtNgaySinh.Value;
                    string GioiTinh = cboGioiTinh.Text;
                    string DiaChi = txtDiaChi.Text;
                    string SDT = txtSDT.Text;
                    string Email = txtEmail.Text;

                    data.ExecuteNonQuery("INSERT into KhachHang Values('" + MaKH + "',N'" + Hoten + "','" + NgaySinh + "',N'" + GioiTinh + "',N'" + DiaChi + "','" + SDT + "','" + Email + "')");
                    MessageBox.Show("Thêm Khách hàng: '" + MaKH + "' - "+Hoten+" thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DangKiTheThanhVien(string MaKH)
        {
            string MaThe = "";
            
            try
            {
                if (MessageBox.Show("Đăng kí Thành viên!", "Đăng kí thành viên", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    MaThe = TaoTheThanhVienTuDong();
                    int DiemTL = 0;
                    string NgayDK = DateTime.Now.ToString("yyyy-MM-dd");
                    //MaKH = TaoTheThanhVienTuDong();
                    data.ExecuteNonQuery("INSERT into TheThanhVien Values('" + MaThe + "'," + DiemTL + ",'" + NgayDK + "','"+MaKH+"')");
                    MessageBox.Show("Thêm Thành viên '" + MaKH + "' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private string TaoTheThanhVienTuDong()
        {
            // Lấy mã KH cuối cùng từ cơ sở dữ liệu (giả sử mã KH có dạng "TV001", "TV002",...)
            string maTTVcuoi = "";

            // Kết nối và mở kết nối tới cơ sở dữ liệu
            using (SqlConnection connection = new SqlConnection(data.connectString))
            {
                connection.Open();

                // Xây dựng câu truy vấn SQL để lấy mã KH cuối cùng
                string sqlQuery = "SELECT TOP 1 MaThe FROM TheThanhVien ORDER BY MaThe DESC";

                // Tạo đối tượng SqlCommand và thực thi câu truy vấn SQL
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        maTTVcuoi = reader["MaThe"].ToString();
                    }

                    reader.Close();
                }
            }

            // Tạo mã TV tự động tăng bằng cách tăng mã TV cuối cùng lên 1 đơn vị
            string maThe = "TV001"; // Mã TV mặc định nếu không có mã TV trong cơ sở dữ liệu

            if (!string.IsNullOrEmpty(maTTVcuoi))
            {
                int soThecuoi = int.Parse(maTTVcuoi.Substring(2));
                maThe = "TV" + (soThecuoi + 1).ToString("D3");
            }
            return maThe;
        }
        private void gbtnThem_Click(object sender, EventArgs e)
        {
            string MaKH;
            ThemKH(out MaKH);
            DangKiTheThanhVien(MaKH);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            refresh();
        }

        
    }
}
