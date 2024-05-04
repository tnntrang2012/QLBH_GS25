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
    public partial class FrmQLTaiKhoan : Form
    {
        public FrmQLTaiKhoan()
        {
            InitializeComponent();
            load();
        }

        KetNoiCSDL data = new KetNoiCSDL();
        private BindingSource bdsoure = new BindingSource();
        private DataTable DT = new DataTable();


        private void load()
        {
            //đổ dữ liệu
            bdsoure.DataSource = data.TKDangNhap();
            dgvTaiKhoan.DataSource = bdsoure;
            dgvTaiKhoan.Columns[0].HeaderText = "Mã Nhân viên";
            dgvTaiKhoan.Columns[1].HeaderText = "Chức vụ";
            dgvTaiKhoan.Columns[2].HeaderText = "Tài khoản";
            dgvTaiKhoan.Columns[3].HeaderText = "Mật khẩu";

            ////kích thước cột
            //DataGridView x = dgvTaiKhoan;
            //{
            //    x.Columns[0].Width = 130;
            //    x.Columns[1].Width = 87;
            //    x.Columns[2].Width = 100;
            //    x.Columns[3].Width = 100;
            //}
            //font chữ của full bảng
            dgvTaiKhoan.Font = new Font("Century", 10, FontStyle.Regular);
            //màu chữ của dữ liệu đổ vào
            this.dgvTaiKhoan.DefaultCellStyle.ForeColor = Color.Black;

            dgvTaiKhoan.ColumnHeadersDefaultCellStyle.Font = new Font("Century", 10, FontStyle.Regular);
            //màu header và chữ header
            dgvTaiKhoan.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.PaleTurquoise;
            dgvTaiKhoan.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            //màu của dòng được chọn
            dgvTaiKhoan.DefaultCellStyle.SelectionBackColor = Color.PaleTurquoise;
            dgvTaiKhoan.RowHeadersDefaultCellStyle.SelectionBackColor = Color.PaleTurquoise;
            //màu của những dòng k được chọn
            dgvTaiKhoan.DefaultCellStyle.BackColor = Color.White;
            //màu của chữ được chọn
            dgvTaiKhoan.RowsDefaultCellStyle.SelectionForeColor = Color.Black;
            //resize cột, hàng từ người dùng
            dgvTaiKhoan.AllowUserToResizeColumns = false;
            dgvTaiKhoan.AllowUserToResizeRows = false;

            dgvTaiKhoan.EnableHeadersVisualStyles = false;
            dgvTaiKhoan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void refresh()
        {
            txtTK.ResetText();
            txtMK.ResetText();
            cboCV.ResetText();
            txtMaNV.ResetText();
            txtMaNV.Focus();
            dgvTaiKhoan.ClearSelection();
            txtMaNV.ReadOnly = false;
            txtTimKiem.Text = "";
        }
        //Load data từ bảng lên TextBox
        private void loadToTextBox()
        {
            txtMaNV.ReadOnly = true;

            int i;
            i = dgvTaiKhoan.CurrentRow.Index;
            txtMaNV.Text = dgvTaiKhoan.Rows[i].Cells[0].Value.ToString();
            cboCV.Text = dgvTaiKhoan.Rows[i].Cells[1].Value.ToString();
            txtTK.Text = dgvTaiKhoan.Rows[i].Cells[2].Value.ToString();
            txtMK.Text = dgvTaiKhoan.Rows[i].Cells[3].Value.ToString();

        }


        private void dgvTaiKhoan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            loadToTextBox();
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {

                string manv = txtMaNV.Text;
                string chucvu = cboCV.Text;
                string id = txtTK.Text;
                string mk = txtMK.Text;
                if (string.IsNullOrWhiteSpace(manv) ||
                     string.IsNullOrWhiteSpace(chucvu) ||
                    string.IsNullOrWhiteSpace(id) ||
                    string.IsNullOrWhiteSpace(mk))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    // Tìm và focus vào ô textbox bị thiếu thông tin đầu tiên
                    if (string.IsNullOrWhiteSpace(manv))
                    {
                        txtMaNV.Focus();
                    }
                    else if (string.IsNullOrWhiteSpace(chucvu))
                    {
                        cboCV.Focus();
                    }
                    else if (string.IsNullOrWhiteSpace(id))
                    {
                        txtTK.Focus();
                    }
                    else if (string.IsNullOrWhiteSpace(mk))
                    {
                        txtMK.Focus();
                    }

                    return;
                }
                data.ExecuteNonQuery("insert into TKDangNhap values('" + id + "','" + mk + "',N'" + chucvu + "','" + manv + "')");
                MessageBox.Show("Thêm tài khoản " + manv.Trim() + " thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                load();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thêm tài khoản thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            try
            {
                int vitri = dgvTaiKhoan.CurrentCell.RowIndex;
                string MaNV = dgvTaiKhoan.Rows[vitri].Cells[0].Value.ToString();
                string CV = dgvTaiKhoan.Rows[vitri].Cells[1].Value.ToString();
                string ID = dgvTaiKhoan.Rows[vitri].Cells[2].Value.ToString();
                string MK = dgvTaiKhoan.Rows[vitri].Cells[3].Value.ToString();

                loadToTextBox();
                data.ExecuteNonQuery("update TKDangNhap set ID='" + ID + "',MatKhau='" + MK + "' where MaNV='" + MaNV + "' ");
                MessageBox.Show("Sửa tài khoản '" + MaNV.Trim() + "' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                load();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            try
            {
                int vitri = dgvTaiKhoan.CurrentCell.RowIndex;
                string MaNV = dgvTaiKhoan.Rows[vitri].Cells[0].Value.ToString();
                string CV = dgvTaiKhoan.Rows[vitri].Cells[1].Value.ToString();
                string ID = dgvTaiKhoan.Rows[vitri].Cells[2].Value.ToString();
                string MK = dgvTaiKhoan.Rows[vitri].Cells[3].Value.ToString();
                data.ExecuteNonQuery("delete from TKDangNhap where MaNV ='" + MaNV + "'");
                load();
                MessageBox.Show("Xóa tài khoản '" + MaNV.Trim() + "' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xóa tài khoản thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void btnRefresh_Click_1(object sender, EventArgs e)
        {
            refresh();
            load();
        }


        private void btnTimKiem_Click_1(object sender, EventArgs e)
        {
            try
            {
                DataView dv = new DataView(data.Executequery("Select MaNV, ChucVu, ID, MatKhau from TKDangNhap where MaNV like '" + txtTimKiem.Text + "%' OR ID like '" + txtTimKiem.Text + "%'"));
                dgvTaiKhoan.DataSource = dv;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Tìm tài khoản thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

    }
}
