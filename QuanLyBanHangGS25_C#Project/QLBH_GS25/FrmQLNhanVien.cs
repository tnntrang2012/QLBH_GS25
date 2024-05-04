using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBH_GS25
{
    public partial class FrmQLNhanVien : Form
    {
        public FrmQLNhanVien()
        {
            InitializeComponent();
            load();

            txtMauHH.Text = (bdsoure.Position + 1).ToString();
            lblTong.Text = "of " + bdsoure.Count.ToString();
            bdsoure.ListChanged += bdsoure_ListChanged;
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
            bdsoure.DataSource = data.NhanVien();
            dgvNhanVien.DataSource = bdsoure;
            dgvNhanVien.Columns[0].HeaderText = "Mã NV";
            dgvNhanVien.Columns[1].HeaderText = "Họ tên";
            dgvNhanVien.Columns[2].HeaderText = "Giới tính";
            dgvNhanVien.Columns[3].HeaderText = "Ngày sinh";
            dgvNhanVien.Columns[4].HeaderText = "Địa chỉ";
            dgvNhanVien.Columns[5].HeaderText = "SĐT";
            dgvNhanVien.Columns[6].HeaderText = "Email";
            dgvNhanVien.Columns[7].HeaderText = "CCCD";


            ////kích thước cột
            DataGridView x = dgvNhanVien;
            {
                x.Columns[0].Width = 80;
                x.Columns[1].Width = 110;
                x.Columns[2].Width = 95;
                x.Columns[3].Width = 100;
                x.Columns[4].Width = 100;
                x.Columns[5].Width = 100;
                x.Columns[6].Width = 120;
                x.Columns[7].Width = 100;
            }
            //font chữ của full bảng
            dgvNhanVien.Font = new Font("Century", 10, FontStyle.Regular);
            //màu chữ của dữ liệu đổ vào
            this.dgvNhanVien.DefaultCellStyle.ForeColor = Color.Black;

            dgvNhanVien.ColumnHeadersDefaultCellStyle.Font = new Font("Century", 10, FontStyle.Regular);
            //màu header và chữ header
            dgvNhanVien.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.PaleTurquoise;
            dgvNhanVien.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            //màu của dòng được chọn
            dgvNhanVien.DefaultCellStyle.SelectionBackColor = Color.PaleTurquoise;
            dgvNhanVien.RowHeadersDefaultCellStyle.SelectionBackColor = Color.PaleTurquoise;
            //màu của những dòng k được chọn
            dgvNhanVien.DefaultCellStyle.BackColor = Color.White;
            //màu của chữ được chọn
            dgvNhanVien.RowsDefaultCellStyle.SelectionForeColor = Color.Black;
            //resize cột, hàng từ người dùng
            dgvNhanVien.AllowUserToResizeColumns = true;
            dgvNhanVien.AllowUserToResizeRows = true;

            dgvNhanVien.EnableHeadersVisualStyles = false;
            dgvNhanVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private void loadToTextBox()
        {
            txtMaNV.ReadOnly = true;
            int i;
            i = dgvNhanVien.CurrentRow.Index;

            txtMaNV.Text = dgvNhanVien.Rows[i].Cells[0].Value.ToString();
            txtHoten.Text = dgvNhanVien.Rows[i].Cells[1].Value.ToString();
            cboGioiTinh.Text = dgvNhanVien.Rows[i].Cells[2].Value.ToString();
            dtNgaySinh.Value = Convert.ToDateTime(dgvNhanVien.Rows[i].Cells[3].Value);
            txtDiaChi.Text = dgvNhanVien.Rows[i].Cells[4].Value.ToString();
            txtSDT.Text = dgvNhanVien.Rows[i].Cells[5].Value.ToString();
            txtEmail.Text = dgvNhanVien.Rows[i].Cells[6].Value.ToString();
            txtCCCD.Text = dgvNhanVien.Rows[i].Cells[7].Value.ToString();

        }
        private void refresh()
        {
            txtMaNV.ResetText();
            txtHoten.ResetText();
            cboGioiTinh.SelectedIndex = -1;
            dtNgaySinh.Value = DateTime.Now;
            txtDiaChi.ResetText();
            txtSDT.ResetText();
            txtEmail.ResetText();
            txtCCCD.ResetText();
            txtMaNV.Focus();
            txtMaNV.ReadOnly = false;
            txtMaNV.Enabled = true;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string MaNV = txtMaNV.Text;
                string HoTen = txtHoten.Text;
                string GioiTinh = cboGioiTinh.Text;
                DateTime NgaySinh = dtNgaySinh.Value;
                string DiaChi = txtDiaChi.Text;
                string SDT = txtSDT.Text;
                string Email = txtEmail.Text;
                string CCCD = txtCCCD.Text;
                if (string.IsNullOrWhiteSpace(MaNV) ||
                string.IsNullOrWhiteSpace(HoTen) ||
                string.IsNullOrWhiteSpace(GioiTinh) ||
                string.IsNullOrWhiteSpace(DiaChi) ||
                string.IsNullOrWhiteSpace(SDT) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(CCCD))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin của nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Không thực hiện thêm nếu thiếu thông tin
                }
                data.ExecuteNonQuery("INSERT into NhanVien Values('" + MaNV + "',N'" + HoTen + "',N'" + GioiTinh + "','" + NgaySinh + "',N'" + DiaChi + "','" + SDT + "','" + Email + "','" + CCCD + "')");
                MessageBox.Show("Thêm Nhân viên '" + HoTen + "' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                load();
                refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khóa chính! Vui lòng kiểm tra lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                int vitri = dgvNhanVien.CurrentCell.RowIndex;
                string MaNV = dgvNhanVien.Rows[vitri].Cells[0].Value.ToString();
                string HoTen = dgvNhanVien.Rows[vitri].Cells[1].Value.ToString();
                string GioiTinh = dgvNhanVien.Rows[vitri].Cells[2].Value.ToString();
                string NgaySinh = dgvNhanVien.Rows[vitri].Cells[3].Value.ToString();
                string DiaChi = dgvNhanVien.Rows[vitri].Cells[4].Value.ToString();
                string SDT = dgvNhanVien.Rows[vitri].Cells[5].Value.ToString();
                string Email = dgvNhanVien.Rows[vitri].Cells[6].Value.ToString();
                string CCCD = dgvNhanVien.Rows[vitri].Cells[7].Value.ToString();

                loadToTextBox();
                data.ExecuteNonQuery("update NhanVien set MaNV = '" + MaNV + "', HoTenNV=N'" + HoTen + "',GioiTinhNV=N'" + GioiTinh + "',NgaySinhNV='" + NgaySinh + "',DiaChiNV=N'" + DiaChi + "',DiDongNV='" + SDT + "', EmailNV = '" + Email + "',CMND = '" + CCCD + "' where MaNV='" + MaNV + "' ");
                MessageBox.Show("Sửa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                load();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int vitri = dgvNhanVien.CurrentCell.RowIndex;
                string MaNV = dgvNhanVien.Rows[vitri].Cells[0].Value.ToString();
                string HoTen = dgvNhanVien.Rows[vitri].Cells[1].Value.ToString();
                string GioiTinh = dgvNhanVien.Rows[vitri].Cells[2].Value.ToString();
                string NgaySinh = dgvNhanVien.Rows[vitri].Cells[3].Value.ToString();
                string DiaChi = dgvNhanVien.Rows[vitri].Cells[4].Value.ToString();
                string SDT = dgvNhanVien.Rows[vitri].Cells[5].Value.ToString();
                string Email = dgvNhanVien.Rows[vitri].Cells[6].Value.ToString();
                string CCCD = dgvNhanVien.Rows[vitri].Cells[7].Value.ToString();
                load();
                refresh();
                loadToTextBox();
                data.ExecuteNonQuery("delete from NhanVien where MaNV ='" + MaNV + "'");
                MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            refresh();
        }


        private void dgvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //loadToTextBox();
        }

        private void FrmQLNhanVien_Load(object sender, EventArgs e)
        {

        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            FrmMenu_Admin f = new FrmMenu_Admin();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (cboTimKiem.SelectedIndex == 0)// Tìm theo Mã NV
            {
                DataView dv = new DataView(data.Executequery("Select * from NhanVien where MaNV like '" + txtTimKiem.Text + "%'"));
                if (dv.Count > 0)
                {
                    dgvNhanVien.DataSource = dv;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy kết quả", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (cboTimKiem.SelectedIndex == 1) // Tìm theo Họ tên nhân viên
            {
                DataView dv = new DataView(data.Executequery("Select * from NhanVien where HoTenNV like N'%" + txtTimKiem.Text + "%'"));
                if (dv.Count > 0)
                {
                    dgvNhanVien.DataSource = dv;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy kết quả", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn điều kiện tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void dgvNhanVien_SelectionChanged(object sender, EventArgs e)
        {
            UpdateRowAndTotalLabels();
            txtMaNV.Text = dgvNhanVien.CurrentRow.Cells[0].Value.ToString();
            txtHoten.Text = dgvNhanVien.CurrentRow.Cells[1].Value.ToString();
            cboGioiTinh.Text = dgvNhanVien.CurrentRow.Cells[2].Value.ToString();
            dtNgaySinh.Text = dgvNhanVien.CurrentRow.Cells[3].Value.ToString();
            txtDiaChi.Text = dgvNhanVien.CurrentRow.Cells[4].Value.ToString();
            txtSDT.Text = dgvNhanVien.CurrentRow.Cells[5].Value.ToString();
            txtEmail.Text = dgvNhanVien.CurrentRow.Cells[6].Value.ToString();
            txtCCCD.Text = dgvNhanVien.CurrentRow.Cells[7].Value.ToString();
            txtMaNV.Enabled = false;
        }

    }
}
