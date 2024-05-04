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
    public partial class FrmQLHoaDon : Form
    {
        public FrmQLHoaDon()
        {
            InitializeComponent();
            load();
            bdsoure.Position = 0;
            bdsoure.DataSource = data.HoaDon1();
            lblTong.Text = "of " + bdsoure.Count.ToString();
            bdsoure.ListChanged += bdsoure_ListChanged;
            txtMauHH.Text = (bdsoure.Position + 1).ToString();
            
            dgvHoaDon.DataSource = bdsoure;
            btnTimKiem.Enabled = false;
            txtTimKiem.Enabled = false;
            dtNgayLap.Enabled = false;
        }

        private void bdsoure_ListChanged(object sender, ListChangedEventArgs e)
        {
            UpdateRowAndTotalLabels();
        }

        private void UpdateRowAndTotalLabels()
        {
            txtMauHH.Text = (bdsoure.Position + 1).ToString();
            lblTong.Text = "of " + bdsoure.Count.ToString();
        }
        KetNoiCSDL data = new KetNoiCSDL();
        private BindingSource bdsoure = new BindingSource();
        private void load()
        {
            //đổ dữ liệu
            bdsoure.DataSource = data.HoaDon1();
            dgvHoaDon.DataSource = bdsoure;

            dgvHoaDon.Columns[0].HeaderText = "Mã HD";
            dgvHoaDon.Columns[1].HeaderText = "Ngày lập";
            dgvHoaDon.Columns[2].HeaderText = "PTTT";
            dgvHoaDon.Columns[3].HeaderText = "Mã KH";
            dgvHoaDon.Columns[4].HeaderText = "Mã NV";
            dgvHoaDon.Columns[5].HeaderText = "Mã KM";




            //kích thước cột
            DataGridView x = dgvHoaDon;
            {
                x.Columns[0].Width = 85;
                x.Columns[1].Width = 105;
                x.Columns[2].Width = 125;
                x.Columns[3].Width = 95;
                x.Columns[4].Width = 95;
                x.Columns[5].Width = 95;
            }
            //font chữ của full bảng
            dgvHoaDon.Font = new Font("Century", 10, FontStyle.Regular);
            //màu chữ của dữ liệu đổ vào
            this.dgvHoaDon.DefaultCellStyle.ForeColor = Color.Black;

            dgvHoaDon.ColumnHeadersDefaultCellStyle.Font = new Font("Century", 10, FontStyle.Regular);
            //màu header và chữ header
            dgvHoaDon.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.PaleTurquoise;
            dgvHoaDon.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            //màu của dòng được chọn
            dgvHoaDon.DefaultCellStyle.SelectionBackColor = Color.PaleTurquoise;
            dgvHoaDon.RowHeadersDefaultCellStyle.SelectionBackColor = Color.PaleTurquoise;
            //màu của những dòng k được chọn
            dgvHoaDon.DefaultCellStyle.BackColor = Color.White;
            //màu của chữ được chọn
            dgvHoaDon.RowsDefaultCellStyle.SelectionForeColor = Color.Black;


            dgvHoaDon.EnableHeadersVisualStyles = false;
            dgvHoaDon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //-----------------------------
            //font chữ của full bảng
            dgvCTHD.Font = new Font("Century", 10, FontStyle.Regular);
            //màu chữ của dữ liệu đổ vào
            this.dgvCTHD.DefaultCellStyle.ForeColor = Color.Black;

            dgvCTHD.ColumnHeadersDefaultCellStyle.Font = new Font("Century", 10, FontStyle.Regular);
            //màu header và chữ header
            dgvCTHD.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.PaleTurquoise;
            dgvCTHD.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            //màu của dòng được chọn
            dgvCTHD.DefaultCellStyle.SelectionBackColor = Color.White;
            dgvCTHD.RowHeadersDefaultCellStyle.SelectionBackColor = Color.White;
            //màu của những dòng k được chọn
            dgvCTHD.DefaultCellStyle.BackColor = Color.White;
            //màu của chữ được chọn
            dgvCTHD.RowsDefaultCellStyle.SelectionForeColor = Color.Black;


            dgvCTHD.EnableHeadersVisualStyles = false;
            dgvCTHD.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

           
        }

        private void refresh()
        {
            bdsoure.DataSource = data.HoaDon();
            UpdateRowAndTotalLabels();
            dgvHoaDon.DataSource = bdsoure;
            dgvCTHD.DataSource = null;
            radioButton2.Checked = false;
            rdoMaHD.Checked = false;
            txtTongTien.Text = "";
        }


        private decimal LayPhanTramGiamGia(string maKhuyenMai)
        {
            decimal phanTramGiamGia = 0;

            // Thực hiện truy vấn cơ sở dữ liệu để lấy phần trăm giảm giá dựa vào mã khuyến mãi
            // Đây là ví dụ giả định sử dụng ADO.NET để truy vấn cơ sở dữ liệu
            string connectionString = @"Data Source= DESKTOP-4QED5F9\NGOCTRANG;Initial Catalog= QLBH_GS25;Integrated Security=True;Persist Security Info= True;User ID=east;Password=1"; // Thay thế YourConnectionString bằng chuỗi kết nối thực tế
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT GiamGia FROM KhuyenMai WHERE MaKM = @MaKhuyenMai";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaKhuyenMai", maKhuyenMai);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        phanTramGiamGia = Convert.ToDecimal(result);
                    }
                }
            }

            return phanTramGiamGia;
        }

        private void TinhTongTien()
        {
            decimal tongTien = 0;
            decimal giamGia = 0; // Số tiền giảm giá ban đầu

            foreach (DataGridViewRow row in dgvCTHD.Rows)
            {
                decimal donGia = Convert.ToDecimal(row.Cells[2].Value);
                int soLuong = Convert.ToInt32(row.Cells[1].Value);
                tongTien += donGia * soLuong;

            }
            foreach (DataGridViewRow row in dgvHoaDon.Rows) // Sử dụng DataGridView "Hóa đơn"
            {

                string maKhuyenMai = Convert.ToString(row.Cells[5].Value); // Thay 5 bằng tên cột chứa mã khuyến mãi
                decimal phanTramGiamGia = LayPhanTramGiamGia(maKhuyenMai); // Truy vấn để lấy phần trăm giảm giá

                giamGia = (phanTramGiamGia / 100) * (tongTien); // Tính số tiền giảm giá cho sản phẩm này
            }
            tongTien -= giamGia; // Trừ số tiền giảm giá tổng cộng

            txtTongTien.Text = tongTien.ToString("N0") + " VND";
        }


        private void dgvHoaDon_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvHoaDon.SelectedRows.Count > 0)
            {
                // Lấy MaHD của hóa đơn được chọn từ cột đầu tiên (chỉ số cột 0)
                string selectedValue = dgvHoaDon.SelectedRows[0].Cells[0].Value.ToString();

                // Truy vấn cơ sở dữ liệu để lấy thông tin chi tiết hóa đơn
                string query = "SELECT MH.TenMH  [Tên MH], CT.SoLuong [Số lượng] , MH.DonGia [Đơn giá] FROM CTHoaDon CT INNER JOIN MatHang MH ON CT.MaMH = MH.MaMH WHERE CT.MaHD = @MaHD";

                using (SqlConnection connection = new SqlConnection(@"Data Source= DESKTOP-4QED5F9\NGOCTRANG;Initial Catalog= QLBH_GS25;Integrated Security=True;Persist Security Info= True;User ID=east;Password=1"))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaHD", selectedValue);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dtChiTietHoaDon = new DataTable();
                        adapter.Fill(dtChiTietHoaDon);

                        // Đổ dữ liệu lên DataGridView ChiTietHoaDon
                        dgvCTHD.DataSource = dtChiTietHoaDon;
                        TinhTongTien();
                    }
                }
            }

        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            bdsoure.Position = 0;
            txtMauHH.Text = (bdsoure.Position + 1).ToString();
            lblTong.Text = "of " + bdsoure.Count.ToString();
            btnLeft.Enabled = false;
            btnFirst.Enabled = false;
            btnRight.Enabled = true;
            btnLast.Enabled = true;
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            bdsoure.Position -= 1;
            txtMauHH.Text = (bdsoure.Position + 1).ToString();
            lblTong.Text = "of " + bdsoure.Count.ToString();
            if (bdsoure.Position == 0)
            {

                btnLeft.Enabled = false;
                btnFirst.Enabled = false;
            }
            btnRight.Enabled = true;
            btnLast.Enabled = true;
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            bdsoure.Position += 1;
            txtMauHH.Text = (bdsoure.Position + 1).ToString();
            lblTong.Text = "of " + bdsoure.Count.ToString();
            if (bdsoure.Position == bdsoure.Count - 1)
            {
                btnRight.Enabled = false;
                btnLast.Enabled = false;
            }
            btnLeft.Enabled = true;
            btnFirst.Enabled = true;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            bdsoure.Position = bdsoure.Count - 1;
            txtMauHH.Text = (bdsoure.Position + 1).ToString();
            lblTong.Text = "of " + bdsoure.Count.ToString();
            btnLeft.Enabled = true;
            btnFirst.Enabled = true;
            btnRight.Enabled = false;
            btnLast.Enabled = false;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            DataView dv = new DataView(data.Executequery("Select MaHD, NgayLapHD, PTTT,MaKH,MaNV,MaKM from HoaDon where MaHD like '" + txtTimKiem.Text + "'"));

            if (dv.Count > 0)
            {
                dgvHoaDon.DataSource = dv; // Hiển thị dữ liệu tìm được


            }
            else
            {
                dgvHoaDon.DataSource = null; // Xóa dữ liệu trong DataGridView
                MessageBox.Show("Không tìm thấy kết quả.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            UpdateRowAndTotalLabels();
        }

        private void dtNgayLap_ValueChanged(object sender, EventArgs e)
        {
            DateTime selectedDate = dtNgayLap.Value.Date;

            string query = "Select MaHD, NgayLapHD, PTTT,MaKH,MaNV,MaKM from HoaDon WHERE NgayLapHD = @NgayLap";

            using (SqlConnection connection = new SqlConnection(@"Data Source= East\SQLEXPRESS;Initial Catalog= QLBH_GS25;Integrated Security=True;Persist Security Info= True;User ID=east;Password=1"))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NgayLap", selectedDate);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dtHoaDon = new DataTable();
                    adapter.Fill(dtHoaDon);

                    // Đổ dữ liệu lên DataGridView Hóa đơn
                    dgvHoaDon.DataSource = dtHoaDon;
                    UpdateRowAndTotalLabels();

                }
            }
            
        }

        private void rdoMaHD_CheckedChanged(object sender, EventArgs e)
        {
            dgvCTHD.DataSource = null;
            if (rdoMaHD.Checked)
            {

                btnTimKiem.Enabled = true;
                txtTimKiem.Enabled = true;
                dtNgayLap.Enabled = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            dgvCTHD.DataSource = null;
            if (radioButton2.Checked)
            {
                btnTimKiem.Enabled = false;
                txtTimKiem.Enabled = false;
                dtNgayLap.Enabled = true;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            refresh();
        }


        private void dgvHoaDon_DataSourceChanged(object sender, EventArgs e)
        {
            UpdateRowAndTotalLabels();
        }


        private void dgvHoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            UpdateRowAndTotalLabels();
        }
    }
}
