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
    public partial class FrmBanHang : Form
    {
        public FrmBanHang()
        {
            InitializeComponent();
            lblNgayHH.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtMaKH.Enabled = false;
            txtHoTenKH.Enabled = false;
            txtDiemTL.Enabled = false;
            txtSDTKH.Enabled = false;

            txtTenMH.ReadOnly = true;
            txtDonGia.ReadOnly = true;
            txtDVT.ReadOnly = true;
            txtDonGia.ReadOnly = true;
            txtSLTon.ReadOnly = true;
            txtGiamGia.Text = "0";
            txtGiamGia.ReadOnly = true;
            dgvHoaDon.Columns[0].Width = 70;

            //Clock
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            //Update TongTien
            dgvHoaDon.CellValueChanged += dgvHoaDon_CellValueChanged;

            //Chỉ cho phép chỉnh sửa cột số lượng
            DataGridViewTextBoxColumn colSoLuong = new DataGridViewTextBoxColumn();

            foreach (DataGridViewColumn col in dgvHoaDon.Columns)
            {
                if (col.Name != "ColSoLuong")
                {
                    col.ReadOnly = true;
                }
            }
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            proClock.Invoke((MethodInvoker)delegate
            {
                proClock.Text = DateTime.Now.ToString("HH:mm:ss");
                proClock.Value = Convert.ToInt32(DateTime.Now.Second);
            });
        }

        KetNoiCSDL data = new KetNoiCSDL();
        private BindingSource bdsoure = new BindingSource();

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
        public void refresh()
        {
            //refresh MH
            txtMaMH.Text = "";
            txtTenMH.Text = "";
            txtDonGia.Text = "";
            txtDVT.Text = "";
            txtSLTon.Text = "";
            txtSoLuong.Text = "";

            dgvHoaDon.Rows.Clear(); // Xóa tất cả các dòng trong DataGridView

            //refresh KH
            rdoKhTichDiem.Checked = false;
            rdoTichDiem.Checked = false;
            txtMaKH.Enabled = false;
            txtHoTenKH.Enabled = false;
            txtDiemTL.Enabled = false;
            txtSDTKH.Enabled = false;

            //refresh NV
            txtMaNV.Text = "(Nhập Mã NV)";
            txtMaKM.Text = "";


            //refresh thanh toan
            rdoTienMat.Checked = false;
            rdoChuyenKhoan.Checked = false;
            txtTienNhan.Text = "";
            lblGiamGia.Text = "...";
            lblTienThua.Text = "...";
            lblTongTien.Text = "...";


        }
        private void FrmBanHang_Load(object sender, EventArgs e)
        {

        }
        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            if (rdoTichDiem.Checked)
            {
                SqlCommand cmd = new SqlCommand("select KH.MaKH, HoTenKH, DiemTL from KhachHang KH join TheThanhVien TTV ON KH.MaKH = TTV.MaKH where KH.DiDongKH = '"+txtSDTKH.Text+"'", data.getConnect());
                data.getConnect();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtMaKH.Text = dr["MaKH"].ToString();
                    txtHoTenKH.Text = dr["HoTenKH"].ToString();
                    txtDiemTL.Text = dr["DiemTL"].ToString();
                }
                dr.Close();
            }
            else if (rdoKhTichDiem.Checked)
            {
                MessageBox.Show("Không thể tra cứu khi chọn vào ô này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 'Tích điểm' để Tra cứu!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtMaMH_TextChanged(object sender, EventArgs e)
        {
            string MaMH = txtMaMH.Text;

            if (!string.IsNullOrWhiteSpace(MaMH))
            {
                SqlCommand cmd = new SqlCommand("select * from MatHang where MaMH = '" + txtMaMH.Text + "'", data.getConnect());
                data.getConnect();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtMaMH.Text = dr["MaMH"].ToString();
                    txtTenMH.Text = dr["TenMH"].ToString();
                    txtDonGia.Text = dr["DonGia"].ToString();
                    txtDVT.Text = dr["DVT"].ToString();

                    txtSLTon.Text = dr["SoLuongTon"].ToString();
                }
                else if (!dr.Read())
                {
                    txtTenMH.Text = "";
                    txtDonGia.Text = "";
                    txtDVT.Text = "";

                    txtSLTon.Text = "";
                    txtSoLuong.Text = "";
                }
                dr.Close();
            }
            else if (string.IsNullOrWhiteSpace(MaMH))
            {
                txtTenMH.Text = "";
                txtDonGia.Text = "";
                txtDVT.Text = "";

                txtSLTon.Text = "";
                txtSoLuong.Text = "";
            }
        }

        private void rdoTichDiem_CheckedChanged(object sender, EventArgs e)
        {
            txtMaKH.ResetText();
            txtMaKH.ReadOnly = true;         
            txtMaKH.Enabled = true;
            txtHoTenKH.Enabled = true;
            txtHoTenKH.ReadOnly = true;
            txtSDTKH.Enabled = true;
            txtDiemTL.Enabled = true;
            txtDiemTL.ReadOnly = true;
        }

        private void rdoKhTichDiem_CheckedChanged(object sender, EventArgs e)
        {
            txtMaKH.Text = "KH000";
            txtMaKH.Enabled = true;
            txtHoTenKH.ResetText();
            txtSDTKH.ResetText();
            txtDiemTL.ResetText();
            txtMaKH.ReadOnly = true;
            txtHoTenKH.Enabled = false;
            txtSDTKH.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaMH.Text == "" || txtTenMH.Text == "" || txtDVT.Text == "" || txtDonGia.Text == "")
                {
                    MessageBox.Show("Vui lòng kiểm tra lại thông tin Mặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtSoLuong.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập số lượng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dgvHoaDon);
                    row.Cells[0].Value = txtMaMH.Text;
                    row.Cells[1].Value = txtTenMH.Text;
                    row.Cells[2].Value = txtDVT.Text;
                    row.Cells[3].Value = txtDonGia.Text;
                    row.Cells[4].Value = txtSoLuong.Text;
                    dgvHoaDon.Rows.Add(row);

                    txtMaMH.Text = "";
                    txtTenMH.Text = "";
                    txtDVT.Text = "";
                    txtDonGia.Text = "";
                    txtSoLuong.Text = "";
                    txtSLTon.Text = "";

                    // Tính tổng tiền và điểm tích lũy
                    TinhTongTien();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Button Xóa
        private void guna2Button2_Click(object sender, EventArgs e)
        {


        }

        //button Thoát
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res = MessageBox.Show("Bạn muốn đăng xuất?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                FrmLogin f = new FrmLogin();
                this.Hide();
                f.ShowDialog();
                this.Close();
            }
            else
            {
                this.Show();
            }
        }

        //---------------------  Tính toán  ---------------------
        private void TinhTongTien()
        {
            decimal tongTien = 0;

            foreach (DataGridViewRow row in dgvHoaDon.Rows)
            {
                decimal donGia = Convert.ToDecimal(row.Cells["ColDonGia"].Value);
                int soLuong = Convert.ToInt32(row.Cells["ColSoLuong"].Value);
                tongTien += donGia * soLuong;
            }
            decimal GiamGia;
            decimal.TryParse(txtGiamGia.Text, out GiamGia);

            GiamGia = (GiamGia / 100) * tongTien;
            tongTien = tongTien - GiamGia;

            if (GiamGia % 1 == 0)
            {
                lblGiamGia.Text = ((int)GiamGia).ToString("N0") + " VND";
            }
            else
            {
                lblGiamGia.Text = GiamGia + " VND";
            }

            if (GiamGia % 1 == 0)
            {
                lblTongTien.Text = ((int)tongTien).ToString("N0") + " VND";
            }
            else
            {
                lblTongTien.Text = tongTien + " VND";
            }

            
        }

        private void dgvHoaDon_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Khi số lượng trong DataGridView thay đổi, tính lại tổng tiền
            TinhTongTien();
        }
        private void txtMaNV_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SqlCommand cmd = new SqlCommand("select * from Nhanvien where MaNV = '" + txtMaNV.Text + "'", data.getConnect());
                data.getConnect();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtMaNV.Text = dr["MaNV"].ToString();
                }
                else if (!dr.Read())
                {
                    txtMaNV.Text = "";
                }
                dr.Close();
            }
        }

        private void txtMaNV_MouseClick(object sender, MouseEventArgs e)
        {
            txtMaNV.Text = "";
        }

        private void txtMaKM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SqlCommand cmd = new SqlCommand("select * from KhuyenMai where MaKM = '" + txtMaKM.Text + "'", data.getConnect());
                data.getConnect();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtMaKM.Text = dr["MaKM"].ToString();
                    txtGiamGia.Text = dr["Giamgia"].ToString();
                }
                else if (!dr.Read())
                {
                    txtGiamGia.Text = "0";
                }
                dr.Close();
            }
        }

        private void txtTienNhan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                // Lấy giá trị từ TextBox txtTienNhan
                string tienNhanText = txtTienNhan.Text.Replace(",", "");
                if (!long.TryParse(tienNhanText, out long tienNhan))
                {
                    MessageBox.Show("Giá trị tiền nhận không hợp lệ!");
                    return;
                }

                string tongTienText = lblTongTien.Text.Replace(" VND", "").Replace(",", "");
                if (!long.TryParse(tongTienText, out long tongTien))
                {
                    MessageBox.Show("Giá trị tiền thừa không hợp lệ!");
                    return;
                }

                // Tính tiền thừa
                long tienThua = tienNhan - tongTien;

                // Hiển thị kết quả trong Label lblTienThua
                lblTienThua.Text = (tienThua >= 0 ? tienThua.ToString("N0") : "0") + " VND";
            }
        }

        private void txtTienNhan_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTienNhan.Text))
            {
                string formattedText = string.Empty;

                // Xóa dấu phẩy và chuyển đổi thành số thập phân
                string rawText = txtTienNhan.Text.Replace(",", "");
                if (long.TryParse(rawText, out long tienNhan))
                {
                    // Định dạng thành chuỗi với dấu phẩy cách hàng trăm
                    formattedText = string.Format("{0:#,#}", tienNhan);
                }
                else
                {
                    // Xử lý khi giá trị không hợp lệ
                }

                // Cập nhật giá trị trong TextBox và đặt con trỏ chọn ở cuối chuỗi
                txtTienNhan.TextChanged -= txtTienNhan_TextChanged; // Tạm thời tắt sự kiện để tránh vòng lặp vô hạn
                txtTienNhan.Text = formattedText;
                txtTienNhan.SelectionStart = formattedText.Length;
                txtTienNhan.TextChanged += txtTienNhan_TextChanged; // Bật lại sự kiện
            }
        }

        private void txtGiamGia_TextChanged(object sender, EventArgs e)
        {
            TinhTongTien();
        }
        private void CapNhatSoLuongTonMatHangDaBan()
        {
            try
            {
                if (dgvHoaDon.RowCount == 0)
                {
                    MessageBox.Show("Không có sản phẩm nào trong hóa đơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return; // Dừng chương trình và không thực hiện các bước tiếp theo
                }
                // Kết nối và mở kết nối tới cơ sở dữ liệu
                using (SqlConnection connection = new SqlConnection(data.connectString))
                {
                    connection.Open();

                    // Lặp qua các dòng trong DataGridView
                    foreach (DataGridViewRow row in dgvHoaDon.Rows)
                    {
                        if (row.Cells["ColMaMH"].Value != null)
                        {
                            // Truy xuất thông tin về mặt hàng và số lượng bán
                            string maMatHang = row.Cells["ColMaMH"].Value.ToString();
                            if (row.Cells["ColSoLuong"].Value != null && int.TryParse(row.Cells["ColSoLuong"].Value.ToString(), out int soLuongBan))
                            {

                                // Xây dựng câu truy vấn SQL để cập nhật số lượng tồn
                                string sqlQuery = "UPDATE MatHang SET SoLuongTon = SoLuongTon - @SoLuongBan WHERE MaMH = @MaMatHang";

                                // Tạo đối tượng SqlCommand và thiết lập các tham số
                                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                                {
                                    command.Parameters.AddWithValue("@SoLuongBan", soLuongBan);
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

        private void ThemHoaDon(out string maHoaDon)
        {
            maHoaDon = ""; // Khởi tạo giá trị mã hóa đơn

            try
            {
                maHoaDon = TaoMaHoaDonTuDong(); // Tạo mã hóa đơn tự động tăng
                string ngayLapHD = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // Lấy ngày hiện tại
                                                                        // Kiểm tra người dùng đã chọn phương thức thanh toán hay chưa

                string phuongThucTT = "";
                if (rdoTienMat.Checked)
                {
                    phuongThucTT = "Tiền mặt";
                }
                else if (rdoChuyenKhoan.Checked)
                {
                    phuongThucTT = "Chuyển khoản";
                }
                if (!rdoTienMat.Checked && !rdoChuyenKhoan.Checked)
                {
                    MessageBox.Show("Chọn Phương thức thanh toán", "Thông báo");
                    return; // Dừng việc thêm hóa đơn và thoát khỏi phương thức
                }
                // Tiếp tục thêm các điều kiện kiểm tra hợp lệ cho các textbox khác (VD: txtMaKH, txtMaNV, txtMaKM) nếu cần

                // Lấy mã nhân viên từ textbox
                string maKhachHang = txtMaKH.Text;
                if (string.IsNullOrEmpty(txtMaKH.Text))
                {
                    MessageBox.Show("Nhập Mã khách hàng", "Thông báo");
                    return; // Dừng việc thêm hóa đơn và thoát khỏi phương thức
                }
                // Lấy mã nhân viên từ textbox
                string maNhanVien = txtMaNV.Text;
                if (string.IsNullOrEmpty(txtMaNV.Text) || maNhanVien == "(Nhập Mã NV)")
                {
                    MessageBox.Show("Nhập Mã Nhân viên", "Thông báo");
                    txtMaNV.Focus();
                    return; // Dừng việc thêm hóa đơn và thoát khỏi phương thức

                }
                // Lấy mã khuyến mãi từ combobox
                string maKhuyenMai = txtMaKM.Text;
                if (string.IsNullOrEmpty(txtMaKM.Text))
                {
                    MessageBox.Show("Nhập Mã Khuyến mãi", "Thông báo");
                    txtMaKM.Focus();
                    return; // Dừng việc thêm hóa đơn và thoát khỏi phương thức
                }



                
                string TienNhanText = txtTienNhan.Text.Replace(",", "");
                if (!long.TryParse(TienNhanText, out long TienNhan))
                {
                    MessageBox.Show("Giá trị tiền nhận không hợp lệ!");
                    return;
                }


                string tienThuaText = lblTienThua.Text.Replace(" VND", "").Replace(",", "");
                if (!long.TryParse(tienThuaText, out long TienThua))
                {
                    MessageBox.Show("Giá trị tiền thừa không hợp lệ!");
                    return;
                }



                // Kết nối và mở kết nối tới cơ sở dữ liệu
                using (SqlConnection connection = new SqlConnection(data.connectString))
                {
                    connection.Open();

                    string query = "INSERT INTO HoaDon (MaHD, NgayLapHD, PTTT, MaKH, MaNV, MaKM, TienNhan, TienThua) " +
                                   $"VALUES ('{maHoaDon}', '{ngayLapHD}', N'{phuongThucTT}', '{maKhachHang}', '{maNhanVien}', '{maKhuyenMai}', {TienNhan},{TienThua})";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có lỗi xảy ra
                MessageBox.Show("Lỗi khi thêm hóa đơn mới: " + ex.Message);
            }

        }

        // Hàm tạo mã hóa đơn tự động tăng
        private string TaoMaHoaDonTuDong()
        {
            // Thực hiện logic để tạo mã hóa đơn tự động tăng
            // Đây chỉ là ví dụ đơn giản

            // Lấy mã hóa đơn cuối cùng từ cơ sở dữ liệu (giả sử mã hóa đơn có dạng "HD001", "HD002",...)
            string maHoaDonCuoi = "";

            // Kết nối và mở kết nối tới cơ sở dữ liệu
            using (SqlConnection connection = new SqlConnection(data.connectString))
            {
                connection.Open();

                // Xây dựng câu truy vấn SQL để lấy mã hóa đơn cuối cùng
                string sqlQuery = "SELECT TOP 1 MaHD FROM HoaDon ORDER BY MaHD DESC";

                // Tạo đối tượng SqlCommand và thực thi câu truy vấn SQL
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        maHoaDonCuoi = reader["MaHD"].ToString();
                    }

                    reader.Close();
                }
            }

            // Tạo mã hóa đơn tự động tăng bằng cách tăng mã hóa đơn cuối cùng lên 1 đơn vị
            string maHoaDon = "HD001"; // Mã hóa đơn mặc định nếu không có mã hóa đơn trong cơ sở dữ liệu

            if (!string.IsNullOrEmpty(maHoaDonCuoi))
            {
                int soHoaDonCuoi = int.Parse(maHoaDonCuoi.Substring(2));
                maHoaDon = "HD" + (soHoaDonCuoi + 1).ToString("D3");
            }
            return maHoaDon;
        }

        private void ThemChiTietHoaDon(string maHoaDon, string maMatHang, int soLuong)
        {
            try
            {
                    
                // Kết nối và mở kết nối tới cơ sở dữ liệu
                using (SqlConnection connection = new SqlConnection(@"Data Source= East\SQLEXPRESS;Initial Catalog= QLBH_GS25;Integrated Security=True;Persist Security Info= True;User ID=east;Password=1"))
                {
                    connection.Open();

                    // Xây dựng câu truy vấn SQL để thêm chi tiết hóa đơn
                    string sqlQuery = "INSERT INTO CTHoaDon (MaHD, MaMH, SoLuong) " +
                        "VALUES (@MaHD, @MaMH, @SoLuong)";

                    // Tạo đối tượng SqlCommand và thiết lập các tham số
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@MaHD", maHoaDon);
                        command.Parameters.AddWithValue("@MaMH", maMatHang);
                        command.Parameters.AddWithValue("@SoLuong", soLuong);

                        // Thực thi câu truy vấn SQL để thêm chi tiết hóa đơn
                        command.ExecuteNonQuery();
                        
                    }
                    CapNhatSoLuongTonMatHangDaBan();
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có lỗi xảy ra
                MessageBox.Show("Lỗi khi thêm chi tiết hóa đơn: " + ex.Message);
            }
        }

        string maHoaDon;
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            try
            {
                // Gọi phương thức thêm hóa đơn và lấy mã hóa đơn vừa thêm
                ThemHoaDon(out maHoaDon);

                if (maHoaDon != " ")
                {
                    // Gọi phương thức thêm chi tiết hóa đơn cho từng mặt hàng trong DataGridView dgvHoaDon
                    foreach (DataGridViewRow row in dgvHoaDon.Rows)
                    {
                        // Lấy giá trị từ các ô tương ứng trong hàng
                        string maMatHang = row.Cells["ColMaMH"].Value?.ToString();
                        int soLuong = int.Parse(row.Cells["ColSoLuong"].Value?.ToString() ?? "0");

                        // Gọi phương thức thêm chi tiết hóa đơn cho từng mặt hàng (nếu mã mặt hàng và số lượng hợp lệ)
                        if (!string.IsNullOrEmpty(maMatHang) && soLuong > 0)
                        {
                            ThemChiTietHoaDon(maHoaDon, maMatHang, soLuong);
                        }
                        
                    }

                    // Hiển thị thông báo "Lưu hóa đơn thành công" 
                    MessageBox.Show("Lưu hóa đơn " + maHoaDon + " thành công", "Thông báo");
                    refresh();
                    txtMaKH.Text = "";
                    txtSDTKH.Text = "";
                    txtHoTenKH.Text = "";
                    txtDiemTL.Text = "";
                    txtGiamGia.Text = "0";
                    

                }

            }
            catch (Exception ex)
            {

                // Xử lý ngoại lệ nếu có lỗi xảy ra khi thêm hóa đơn
                MessageBox.Show("Lưu hóa đơn thất bại: " + ex.Message, "Thông báo");
            }
        }

        private void dgvHoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvHoaDon.Columns[e.ColumnIndex].Name;
            if (colName == "ColDelete")
            {
                if (dgvHoaDon.CurrentRow != null && !dgvHoaDon.CurrentRow.IsNewRow)
                {
                    // Lấy dòng đang được chọn
                    DataGridViewRow selectedRow = dgvHoaDon.Rows[e.RowIndex];

                    // Xóa dòng đang được chọn khỏi DataGridView
                    dgvHoaDon.Rows.Remove(selectedRow);
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {

            // Lấy giá trị từ ô TextBox "Soluongton"
            int soluongTon;
            if (int.TryParse(txtSLTon.Text, out soluongTon))
            {
                // Lấy giá trị từ ô TextBox "Soluongra"
                int soluongRa;
                if (int.TryParse(txtSoLuong.Text, out soluongRa))
                {
                    // Kiểm tra điều kiện số lượng tồn
                    if (soluongTon < soluongRa)
                    {
                        MessageBox.Show("Số lượng tồn không đủ vui lòng nhập lại!", "Lỗi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        // Xóa giá trị trong ô TextBox "Soluongra"
                        txtSoLuong.Text = string.Empty;
                    }
                }
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            FrmMenu_User f = new FrmMenu_User();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void btnDangKi_Click(object sender, EventArgs e)
        {
            FrmDKThanhVien dKThanhVien = new FrmDKThanhVien();
            dKThanhVien.ShowDialog();
        }

        private void btnInHD_Click(object sender, EventArgs e)
        {
            new REPORT.FrmPrint_Billingg(maHoaDon).Show();
        }

        private void dgvHoaDon_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            
            TinhTongTien();
        }
    }
}
