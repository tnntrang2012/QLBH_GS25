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
    public partial class FrmPhieuNhap : Form
    {
        public FrmPhieuNhap()
        {
            InitializeComponent();
            load();
            lblNgayNhap.Text = DateTime.Now.ToString("dd/MM/yyyy");

        }
        KetNoiCSDL data = new KetNoiCSDL();
        private BindingSource bdsoure = new BindingSource();

        private void load()
        {
            //font chữ của full bảng
            dgvPhieuNhap.Font = new Font("Arial", 10, FontStyle.Bold);

        }
        private void CapNhatSoLuongTonMatHang()
        {
            try
            {
                if (dgvPhieuNhap.RowCount == 0)
                {
                    MessageBox.Show("Không có sản phẩm nào trong hóa đơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return; // Dừng chương trình và không thực hiện các bước tiếp theo
                }
                // Kết nối và mở kết nối tới cơ sở dữ liệu
                using (SqlConnection connection = new SqlConnection(data.connectString))
                {
                    connection.Open();

                    // Lặp qua các dòng trong DataGridView
                    foreach (DataGridViewRow row in dgvPhieuNhap.Rows)
                    {
                        if (row.Cells["MaMH"].Value != null)
                        {
                            // Truy xuất thông tin về mặt hàng và số lượng bán
                            string maMatHang = row.Cells["MaMH"].Value.ToString();
                            if (row.Cells["SoLuong"].Value != null && int.TryParse(row.Cells["SoLuong"].Value.ToString(), out int slnhap))
                            {

                                // Xây dựng câu truy vấn SQL để cập nhật số lượng tồn
                                string sqlQuery = "UPDATE MatHang SET SoLuongTon = SoLuongTon + @SoLuongNhap WHERE MaMH = @MaMatHang";

                                // Tạo đối tượng SqlCommand và thiết lập các tham số
                                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                                {
                                    command.Parameters.AddWithValue("@SoLuongNhap", slnhap);
                                    command.Parameters.AddWithValue("@MaMatHang", maMatHang);

                                    // Thực thi câu truy vấn SQL để cập nhật số lượng tồn
                                    command.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có lỗi xảy ra
                MessageBox.Show("Lỗi khi cập nhật số lượng tồn: " + ex.Message);
            }
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaMH.Text == "")
                {
                    MessageBox.Show("Vui lòng kiểm tra lại thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtSL.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập số lượng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtDonGia.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập đơn giá!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (!int.TryParse(txtSL.Text, out int soLuong))
                    {
                        MessageBox.Show("Số lượng phải là số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (!decimal.TryParse(txtDonGia.Text, out decimal donGia))
                    {
                        MessageBox.Show("Đơn giá phải là số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dgvPhieuNhap);

                        row.Cells[0].Value = txtMaMH.Text;
                        row.Cells[1].Value = txtSL.Text;
                        row.Cells[2].Value = txtDonGia.Text;

                        dgvPhieuNhap.Rows.Add(row);

                        txtMaMH.Text = "";
                        txtSL.Text = "";
                        txtDonGia.Text = "";
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string TaoMaPN_TuDong()
        {
            // Thực hiện logic để tạo mã hóa đơn tự động tăng
            // Đây chỉ là ví dụ đơn giản

            // Lấy mã hóa đơn cuối cùng từ cơ sở dữ liệu (giả sử mã hóa đơn có dạng "HD001", "HD002",...)
            string maPNCuoi = "";

            // Kết nối và mở kết nối tới cơ sở dữ liệu
            using (SqlConnection connection = new SqlConnection(@"Data Source= East\SQLEXPRESS;Initial Catalog= QLBH_GS25;Integrated Security=True;Persist Security Info= True;User ID=east;Password=1"))
            {
                connection.Open();

                // Xây dựng câu truy vấn SQL để lấy mã hóa đơn cuối cùng
                string sqlQuery = "SELECT TOP 1 MaPhieuNK FROM PhieuNhapKho ORDER BY MaPhieuNK DESC";

                // Tạo đối tượng SqlCommand và thực thi câu truy vấn SQL
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        maPNCuoi = reader["MaPhieuNK"].ToString();
                    }
                    reader.Close();
                }
            }

            // Tạo mã hóa đơn tự động tăng bằng cách tăng mã hóa đơn cuối cùng lên 1 đơn vị
            string maPN = "PN001"; // Mã hóa đơn mặc định nếu không có mã hóa đơn trong cơ sở dữ liệu

            if (!string.IsNullOrEmpty(maPNCuoi))
            {
                int soPNCuoi = int.Parse(maPNCuoi.Substring(2));
                maPN = "PN" + (soPNCuoi + 1).ToString("D3");
            }
            return maPN;
        }
        private void ThemChiTietPhieuNhap(string maPN, string maMH, int soLuongNhap, int GiaNhap)
        {
            try
            {
                // Kết nối và mở kết nối tới cơ sở dữ liệu
                using (SqlConnection connection = new SqlConnection(@"Data Source= East\SQLEXPRESS;Initial Catalog= QLBH_GS25;Integrated Security=True;Persist Security Info= True;User ID=east;Password=1"))
                {
                    connection.Open();

                    // Xây dựng câu truy vấn SQL để thêm chi tiết hóa đơn
                    string sqlQuery = "INSERT INTO CTPhieuNhapKho (MaPhieuNK,MaMH,SoLuongNhap,GiaNhap)"+
                        "VALUES (@MaPN, @MaMH, @SoLuong, @GiaNhap)";

                    // Tạo đối tượng SqlCommand và thiết lập các tham số
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@MaPN", maPN);
                        command.Parameters.AddWithValue("@MaMH", maMH);
                        command.Parameters.AddWithValue("@SoLuong", soLuongNhap);
                        command.Parameters.AddWithValue("@GiaNhap", GiaNhap);


                        // Thực thi câu truy vấn SQL để thêm chi tiết hóa đơn
                        command.ExecuteNonQuery();
                    }
                    CapNhatSoLuongTonMatHang();
                }
            }
            catch
            {

            }
        }
    
        private void ThemPN(out string maPN)
        {
            maPN = ""; // Khởi tạo giá trị mã hóa đơn

            try
            {
                maPN = TaoMaPN_TuDong(); // Tạo mã hóa đơn tự động tăng

                string maNCC = txtMaNCC.Text;
                if (string.IsNullOrEmpty(maNCC))
                {
                    MessageBox.Show("Nhập Mã Nhà cung cấp!", "Thông báo");
                    txtMaNCC.Focus();
                    return; // Dừng việc thêm hóa đơn và thoát khỏi phương thức
                }

                string ngayLapNK = DateTime.Now.ToString("yyyy-MM-dd");

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

                    string query = "INSERT INTO PhieuNhapKho (MaPhieuNK, NgayLapNK, MaNV, MaNCC) " +
                                   $"VALUES ('{maPN}', '{ngayLapNK}', '{maNhanVien}', '{maNCC}')";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có lỗi xảy ra
                MessageBox.Show("Lỗi khi thêm phiếu nhập: " + ex.Message);
            }
        }

        private void FrmPhieuNhap_Load(object sender, EventArgs e)
        {


        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string maPN;


            try
            {
                // Gọi phương thức thêm hóa đơn và lấy mã hóa đơn vừa thêm
                ThemPN(out maPN);

                if (maPN != " ")
                {
                    // Gọi phương thức thêm chi tiết hóa đơn cho từng mặt hàng trong DataGridView dgvHoaDon
                    foreach (DataGridViewRow row in dgvPhieuNhap.Rows)
                    {
                        // Lấy giá trị từ các ô tương ứng trong hàng
                        string maMatHang = row.Cells["MaMH"].Value?.ToString();
                        int soLuong = int.Parse(row.Cells["SoLuong"].Value?.ToString() ?? "0");
                        int giaNhap = int.Parse(row.Cells["GiaNhap"].Value?.ToString() ?? "0");


                        // Gọi phương thức thêm chi tiết hóa đơn cho từng mặt hàng (nếu mã mặt hàng và số lượng hợp lệ)

                        ThemChiTietPhieuNhap(maPN, maMatHang, soLuong, giaNhap);


                    }
                    // Hiển thị thông báo "Lưu hóa đơn thành công" (nếu muốn)

                    txtMaNV.Text = "";
                    txtMaNCC.Text = "";
                    dgvPhieuNhap.Rows.Clear(); // Xóa tất cả các dòng trong DataGridView
                    MessageBox.Show("Lưu phiếu nhâp "+maPN+" thành công", "Thông báo");

                }

            }
            catch (Exception ex)
            {

                // Xử lý ngoại lệ nếu có lỗi xảy ra khi thêm hóa đơn
                MessageBox.Show("Lưu phiếu nhập thất bại: " + ex.Message, "Thông báo");
            }
        }

        private void txtMaNCC_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dgvPhieuNhap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvPhieuNhap.Columns[e.ColumnIndex].Name;
            if (colName == "Xoa")
            {
                if (dgvPhieuNhap.CurrentRow != null && !dgvPhieuNhap.CurrentRow.IsNewRow)
                {
                    // Lấy dòng đang được chọn
                    DataGridViewRow selectedRow = dgvPhieuNhap.Rows[e.RowIndex];

                    // Xóa dòng đang được chọn khỏi DataGridView
                    dgvPhieuNhap.Rows.Remove(selectedRow);
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
