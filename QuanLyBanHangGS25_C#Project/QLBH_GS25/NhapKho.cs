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
    public partial class NhapKho : Form
    {
        public NhapKho()
        {
            InitializeComponent();
            load();
            bdsoure.Position = 0;
            txtMauHH.Text = (bdsoure.Position + 1).ToString();
            lblTong.Text = "of " + bdsoure.Count.ToString();
        }
        KetNoiCSDL data = new KetNoiCSDL();
        private BindingSource bdsoure = new BindingSource();
        private void load()
        {
            //đổ dữ liệu
            bdsoure.DataSource = data.QLNhapKho();
            dgvPhieuNhap.DataSource = bdsoure;

            dgvPhieuNhap.Columns[0].HeaderText = "Mã PN";
            dgvPhieuNhap.Columns[1].HeaderText = "Ngày lập";
            dgvPhieuNhap.Columns[2].HeaderText = "Mã NV";
            dgvPhieuNhap.Columns[3].HeaderText = "Mã NCC";

            //kích thước cột
            DataGridView x = dgvPhieuNhap;
            {
                x.Columns[0].Width = 85;
                x.Columns[1].Width = 105;
                x.Columns[2].Width = 125;
                x.Columns[3].Width = 95;
                
        
            }
            //font chữ của full bảng
            dgvPhieuNhap.Font = new Font("Century", 10, FontStyle.Regular);
            //màu chữ của dữ liệu đổ vào
            this.dgvPhieuNhap.DefaultCellStyle.ForeColor = Color.Black;

            dgvPhieuNhap.ColumnHeadersDefaultCellStyle.Font = new Font("Century", 10, FontStyle.Regular);
            //màu header và chữ header
            dgvPhieuNhap.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgvPhieuNhap.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            //màu của dòng được chọn
            dgvPhieuNhap.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgvPhieuNhap.RowHeadersDefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            //màu của những dòng k được chọn
            dgvPhieuNhap.DefaultCellStyle.BackColor = Color.White;
            //màu của chữ được chọn
            dgvPhieuNhap.RowsDefaultCellStyle.SelectionForeColor = Color.Black;


            dgvPhieuNhap.EnableHeadersVisualStyles = false;
            dgvPhieuNhap.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //-----------------------------
            //font chữ của full bảng
            dgvCTPN.Font = new Font("Century", 10, FontStyle.Regular);
            //màu chữ của dữ liệu đổ vào
            this.dgvCTPN.DefaultCellStyle.ForeColor = Color.Black;

            dgvCTPN.ColumnHeadersDefaultCellStyle.Font = new Font("Century", 10, FontStyle.Regular);
            //màu header và chữ header
            dgvCTPN.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgvCTPN.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            //màu của dòng được chọn
            dgvCTPN.DefaultCellStyle.SelectionBackColor = Color.White;
            dgvCTPN.RowHeadersDefaultCellStyle.SelectionBackColor = Color.White;
            //màu của những dòng k được chọn
            dgvCTPN.DefaultCellStyle.BackColor = Color.White;
            //màu của chữ được chọn
            dgvCTPN.RowsDefaultCellStyle.SelectionForeColor = Color.Black;


            dgvCTPN.EnableHeadersVisualStyles = false;
            dgvCTPN.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


        }
        private void TinhTongTien()
        {
            decimal tongTien = 0;
     

            foreach (DataGridViewRow row in dgvCTPN.Rows)
            {
                
                int soLuong = Convert.ToInt32(row.Cells[2].Value);
                decimal donGia = Convert.ToDecimal(row.Cells[3].Value);
                tongTien += donGia * soLuong;
            }
            txtTongTien.Text = tongTien.ToString("N0") + " VND";
        }

        private void dgvPhieuNhap_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPhieuNhap.SelectedRows.Count > 0)
            {
                // Lấy MaHD của hóa đơn được chọn từ cột đầu tiên (chỉ số cột 0)
                string selectedValue = dgvPhieuNhap.SelectedRows[0].Cells[0].Value.ToString();

                // Truy vấn cơ sở dữ liệu để lấy thông tin chi tiết hóa đơn
                string query = "SELECT CT.MaPhieuNK [Mã PN], CT.MaMH [Mã MH], CT.SoLuongNhap [Số lượng], CT.GiaNhap [Giá nhập] FROM CTPhieuNhapKho CT JOIN PhieuNhapKho PN on CT.MaPhieuNK = PN.MaPhieuNK WHERE CT.MaPhieuNK = @MaPN";

                using (SqlConnection connection = new SqlConnection(@"Data Source= DESKTOP-4QED5F9\NGOCTRANG;Initial Catalog= QLBH_GS25;Integrated Security=True;Persist Security Info= True;User ID=east;Password=1"))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaPN", selectedValue);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dtCTPN = new DataTable();
                        adapter.Fill(dtCTPN);

                        // Đổ dữ liệu lên DataGridView ChiTietHoaDon
                        dgvCTPN.DataSource = dtCTPN;
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
    }
}
