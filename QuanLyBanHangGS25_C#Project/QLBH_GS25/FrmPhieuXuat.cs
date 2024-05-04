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
    public partial class FrmPhieuXuat : Form
    {
        public FrmPhieuXuat()
        {
            InitializeComponent();
            dgvPhieuXuat.Font = new Font("Arial", 10, FontStyle.Bold);
            lblNgayXuat.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        KetNoiCSDL data = new KetNoiCSDL();
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaMH.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập mã mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtSoLuong.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập số lượng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (!int.TryParse(txtSoLuong.Text, out int soLuong))
                    {
                        MessageBox.Show("Số lượng phải là số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                   
                    else
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dgvPhieuXuat);

                        row.Cells[0].Value = txtMaMH.Text;
                        row.Cells[1].Value = txtTenMH.Text;
                        row.Cells[2].Value = txtSoLuong.Text;

                        dgvPhieuXuat.Rows.Add(row);

                        txtMaMH.Text = "";
                        txtTenMH.Text = "";
                        txtSoLuong.Text = "";
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string TaoMaPhieuXuat_TuDong()
        {
            // Thực hiện logic để tạo mã hóa đơn tự động tăng
            // Đây chỉ là ví dụ đơn giản

            // Lấy mã hóa đơn cuối cùng từ cơ sở dữ liệu (giả sử mã hóa đơn có dạng "HD001", "HD002",...)
            string maPXCuoi = "";

            // Kết nối và mở kết nối tới cơ sở dữ liệu
            using (SqlConnection connection = new SqlConnection(@"Data Source= East\SQLEXPRESS;Initial Catalog= QLBH_GS25;Integrated Security=True;Persist Security Info= True;User ID=east;Password=1"))
            {
                connection.Open();

                // Xây dựng câu truy vấn SQL để lấy mã hóa đơn cuối cùng
                string sqlQuery = "SELECT TOP 1 MaPX FROM PhieuXuat ORDER BY MaPX DESC";

                // Tạo đối tượng SqlCommand và thực thi câu truy vấn SQL
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        maPXCuoi = reader["MaPX"].ToString();
                    }
                    reader.Close();
                }
            }

            // Tạo mã hóa đơn tự động tăng bằng cách tăng mã hóa đơn cuối cùng lên 1 đơn vị
            string maPN = "PX001"; // Mã hóa đơn mặc định nếu không có mã hóa đơn trong cơ sở dữ liệu

            if (!string.IsNullOrEmpty(maPXCuoi))
            {
                int soPXCuoi = int.Parse(maPXCuoi.Substring(2));
                maPN = "PX" + (soPXCuoi + 1).ToString("D3");
            }
            return maPN;
        }
        private void ThemCTPhieuXuat(string maPX, string maMH, int soLuongXuat)
        {
            try
            {
                // Kết nối và mở kết nối tới cơ sở dữ liệu
                using (SqlConnection connection = new SqlConnection(@"Data Source= East\SQLEXPRESS;Initial Catalog= QLBH_GS25;Integrated Security=True;Persist Security Info= True;User ID=east;Password=1"))
                {
                    connection.Open();

                    // Xây dựng câu truy vấn SQL để thêm chi tiết hóa đơn
                    string sqlQuery = "INSERT INTO ChiTietPhieuXuat (MaPX,MaMH,SoLuongXuat)" +
                        "VALUES (@MaPX, @MaMH, @SoLuong)";

                    // Tạo đối tượng SqlCommand và thiết lập các tham số
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@MaPX", maPX);
                        command.Parameters.AddWithValue("@MaMH", maMH);
                        command.Parameters.AddWithValue("@SoLuong", soLuongXuat);

                        // Thực thi câu truy vấn SQL để thêm CTphieuxuat
                        command.ExecuteNonQuery();
                    }
                    //CapNhatSoLuongTonMatHang();
                }
            }
            catch
            {

            }
        }

        //private void CapNhatSoLuongTonMatHang()
        //{
        //    try
        //    {
        //        if (dgvPhieuXuat.RowCount == 0)
        //        {
        //            MessageBox.Show("Không có sản phẩm nào trong hóa đơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            return; // Dừng chương trình và không thực hiện các bước tiếp theo
        //        }
        //        // Kết nối và mở kết nối tới cơ sở dữ liệu
        //        using (SqlConnection connection = new SqlConnection(data.connectString))
        //        {
        //            connection.Open();

        //            // Lặp qua các dòng trong DataGridView
        //            foreach (DataGridViewRow row in dgvPhieuXuat.Rows)
        //            {
        //                if (row.Cells["MaMH"].Value != null)
        //                {
        //                    // Truy xuất thông tin về mặt hàng và số lượng bán
        //                    string maMatHang = row.Cells["MaMH"].Value.ToString();
        //                    if (row.Cells["SoLuong"].Value != null && int.TryParse(row.Cells["SoLuong"].Value.ToString(), out int slxuat))
        //                    {

        //                        // Xây dựng câu truy vấn SQL để cập nhật số lượng tồn
        //                        string sqlQuery = "UPDATE MatHang SET SoLuongTon = SoLuongTon - @SoLuongXuat WHERE MaMH = @MaMatHang";

        //                        // Tạo đối tượng SqlCommand và thiết lập các tham số
        //                        using (SqlCommand command = new SqlCommand(sqlQuery, connection))
        //                        {
        //                            command.Parameters.AddWithValue("@SoLuongXuat", slxuat);
        //                            command.Parameters.AddWithValue("@MaMatHang", maMatHang);

        //                            // Thực thi câu truy vấn SQL để cập nhật số lượng tồn
        //                            command.ExecuteNonQuery();
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Xử lý ngoại lệ nếu có lỗi xảy ra
        //        MessageBox.Show("Lỗi khi cập nhật số lượng tồn: " + ex.Message);
        //    }
        //}

        private void ThemPX(out string maPX)
        {
            maPX = ""; // Khởi tạo giá trị mã phiếu xuất

            try
            {
                maPX = TaoMaPhieuXuat_TuDong(); // Tạo mã phiếu xuất tự động tăng
                string ngayLapPX = DateTime.Now.ToString("yyyy-MM-dd");
                // Lấy mã nhân viên từ textbox
                string maNhanVien = txtMaNV.Text;
                if (string.IsNullOrEmpty(maNhanVien))
                {
                    MessageBox.Show("Nhập Mã Nhân viên", "Thông báo");
                    txtMaNV.Focus();
                    return; // Dừng việc thêm hóa đơn và thoát khỏi phương thức
                }

                // Kết nối và mở kết nối tới cơ sở dữ liệu
                using (SqlConnection connection = new SqlConnection(data.connectString))
                {
                    connection.Open();

                    string query = "INSERT INTO PhieuXuat (MaPX, NgayLapPX, MaNV) " +
                                   $"VALUES ('{maPX}', '{ngayLapPX}', '{maNhanVien}')";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có lỗi xảy ra
                MessageBox.Show("Lỗi khi thêm phiếu xuất: " + ex.Message);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string maPX;


            try
            {
                // Gọi phương thức thêm hóa đơn và lấy mã hóa đơn vừa thêm
                ThemPX(out maPX);

                if (maPX != " ")
                {
                    // Gọi phương thức thêm chi tiết hóa đơn cho từng mặt hàng trong DataGridView dgvHoaDon
                    foreach (DataGridViewRow row in dgvPhieuXuat.Rows)
                    {
                        // Lấy giá trị từ các ô tương ứng trong hàng
                        string maMatHang = row.Cells["MaMH"].Value?.ToString();
                        int soLuong = int.Parse(row.Cells["SoLuong"].Value?.ToString() ?? "0");
                       


                        // Gọi phương thức thêm chi tiết hóa đơn cho từng mặt hàng (nếu mã mặt hàng và số lượng hợp lệ)

                        ThemCTPhieuXuat(maPX, maMatHang, soLuong);


                    }
                    // Hiển thị thông báo "Lưu hóa đơn thành công" (nếu muốn)

                    txtMaNV.Text = "";
                    
                    dgvPhieuXuat.Rows.Clear(); // Xóa tất cả các dòng trong DataGridView
                    MessageBox.Show("Lưu phiếu xuất " + maPX + " thành công", "Thông báo");

                }

            }
            catch (Exception ex)
            {

                // Xử lý ngoại lệ nếu có lỗi xảy ra khi thêm hóa đơn
                MessageBox.Show("Lưu phiếu xuất thất bại: " + ex.Message, "Thông báo");
            }
        }

        private void txtMaMH_TextChanged(object sender, EventArgs e)
        {
            string MaMH = txtMaMH.Text;

            if (!string.IsNullOrWhiteSpace(MaMH))
            {
                SqlCommand cmd = new SqlCommand("select MaMH, TenMH from MatHang where MaMH = '" + txtMaMH.Text + "'", data.getConnect());
                data.getConnect();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtMaMH.Text = dr["MaMH"].ToString();
                    txtTenMH.Text = dr["TenMH"].ToString();
                    
                }
                else if (!dr.Read())
                {
                    txtTenMH.Text = "";
                }
                dr.Close();
            }
            else if (string.IsNullOrWhiteSpace(MaMH))
            {
                txtTenMH.Text = "";
                txtSoLuong.Text = "";
            }
        }

        private void dgvPhieuXuat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvPhieuXuat.Columns[e.ColumnIndex].Name;
            if (colName == "Xoa")
            {
                if (dgvPhieuXuat.CurrentRow != null && !dgvPhieuXuat.CurrentRow.IsNewRow)
                {
                    // Lấy dòng đang được chọn
                    DataGridViewRow selectedRow = dgvPhieuXuat.Rows[e.RowIndex];

                    // Xóa dòng đang được chọn khỏi DataGridView
                    dgvPhieuXuat.Rows.Remove(selectedRow);
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
