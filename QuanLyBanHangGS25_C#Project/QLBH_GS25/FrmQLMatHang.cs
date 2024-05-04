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
    public partial class FrmQLMatHang : Form
    {
        public FrmQLMatHang()
        {
            InitializeComponent();
            load();
            txtMauHH.Text = (bdsoure.Position + 1).ToString();
            lblTong.Text = "of "+bdsoure.Count.ToString();
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
        private DataTable DT = new DataTable();
        private void load()
        {
            //đổ dữ liệu
            bdsoure.DataSource = data.MatHang();
            dgvMatHang.DataSource = bdsoure;
            dgvMatHang.Columns[0].HeaderText = "Mã MH";
            dgvMatHang.Columns[1].HeaderText = "Tên MH";
            dgvMatHang.Columns[2].HeaderText = "Mã Loại MH";
            dgvMatHang.Columns[3].HeaderText = "Đơn giá";
            dgvMatHang.Columns[4].HeaderText = "ĐVT";
            dgvMatHang.Columns[5].HeaderText = "Số lượng tồn";


            //kích thước cột
            DataGridView x = dgvMatHang;
            {
                x.Columns[0].Width = 100;
                x.Columns[1].Width = 150;
                x.Columns[2].Width = 100;
                x.Columns[3].Width = 100;
                x.Columns[4].Width = 100;
                x.Columns[5].Width = 100;

            }
            //font chữ của full bảng
            dgvMatHang.Font = new Font("Century", 10, FontStyle.Regular);
            //màu chữ của dữ liệu đổ vào
            this.dgvMatHang.DefaultCellStyle.ForeColor = Color.Black;

            dgvMatHang.ColumnHeadersDefaultCellStyle.Font = new Font("Century", 10, FontStyle.Regular);
            //màu header và chữ header
            dgvMatHang.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.PaleTurquoise;
            dgvMatHang.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            //màu của dòng được chọn
            dgvMatHang.DefaultCellStyle.SelectionBackColor = Color.PaleTurquoise;
            dgvMatHang.RowHeadersDefaultCellStyle.SelectionBackColor = Color.PaleTurquoise;
            //màu của những dòng k được chọn
            dgvMatHang.DefaultCellStyle.BackColor = Color.White;
            //màu của chữ được chọn
            dgvMatHang.RowsDefaultCellStyle.SelectionForeColor = Color.Black;
            //resize cột, hàng từ người dùng
            //dgvMatHang.AllowUserToResizeColumns = true;
            //dgvMatHang.AllowUserToResizeRows = true;

            dgvMatHang.EnableHeadersVisualStyles = false;
            dgvMatHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //---------------------------
        }
        //Load data từ bảng lên TextBox
        private void loadToTextBox()
        {
            txtMaMH.ReadOnly = true;
            int i;
            i = dgvMatHang.CurrentRow.Index;
            txtMaMH.Text = dgvMatHang.Rows[i].Cells[0].Value.ToString();
            txtTenMH.Text = dgvMatHang.Rows[i].Cells[1].Value.ToString();
            txtMaLoaiMH.Text = dgvMatHang.Rows[i].Cells[2].Value.ToString();
            txtDonGia.Text = dgvMatHang.Rows[i].Cells[3].Value.ToString();
            txtDVT.Text = dgvMatHang.Rows[i].Cells[4].Value.ToString();
            txtSoLuongTon.Text = dgvMatHang.Rows[i].Cells[5].Value.ToString();
        }
        private void dgvMatHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            loadToTextBox();
        }

        private void refresh()
        {
            txtMaMH.ResetText();
            txtTenMH.ResetText();
            txtMaLoaiMH.ResetText();
            txtDonGia.ResetText();
            txtDVT.ResetText();
            txtSoLuongTon.ResetText();
            txtTimKiem.ResetText();
            txtMaMH.Focus();
            txtMaMH.ReadOnly = false;
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

        private void dgvMatHang_SelectionChanged(object sender, EventArgs e)
        {
            UpdateRowAndTotalLabels();
            txtMaMH.Enabled = false;
            txtMaMH.Text = dgvMatHang.CurrentRow.Cells[0].Value.ToString();
            txtTenMH.Text = dgvMatHang.CurrentRow.Cells[1].Value.ToString();
            txtMaLoaiMH.Text = dgvMatHang.CurrentRow.Cells[2].Value.ToString();
            txtDonGia.Text = dgvMatHang.CurrentRow.Cells[3].Value.ToString();
            txtDVT.Text = dgvMatHang.CurrentRow.Cells[4].Value.ToString();
            txtSoLuongTon.Text = dgvMatHang.CurrentRow.Cells[5].Value.ToString();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (rdoMaMH.Checked == true) // Tìm theo Mã MH
            {
                DataView dv = new DataView(data.Executequery("Select * from MatHang where MaMH like '" + txtTimKiem.Text + "%'"));

                if (dv.Count > 0)
                {
                    dgvMatHang.DataSource = dv; // Hiển thị dữ liệu tìm được
                }
                else
                {
                    dgvMatHang.DataSource = null; // Xóa dữ liệu trong DataGridView
                    MessageBox.Show("Không tìm thấy kết quả.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (rdoTenMH.Checked == true) // Tìm theo Tên mặt hàng
            {
                DataView dv = new DataView(data.Executequery("Select * from MatHang where TenMH like N'" + txtTimKiem.Text + "%'"));

                if (dv.Count > 0)
                {
                    dgvMatHang.DataSource = dv; // Hiển thị dữ liệu tìm được
                }
                else
                {
                    dgvMatHang.DataSource = null; // Xóa dữ liệu trong DataGridView
                    MessageBox.Show("Không tìm thấy kết quả.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string MaMH = txtMaMH.Text;
                string TenMH = txtTenMH.Text;
                string MaLoaiMH = txtMaLoaiMH.Text;
                string DonGia = txtDonGia.Text;
                string DVT = txtDVT.Text;
                string SoLuongTon = txtSoLuongTon.Text;

                // Kiểm tra xem có thông tin nào bị thiếu không
                if (string.IsNullOrWhiteSpace(MaMH) ||
                    string.IsNullOrWhiteSpace(TenMH) ||
                    string.IsNullOrWhiteSpace(MaLoaiMH) ||
                    string.IsNullOrWhiteSpace(DonGia) ||
                    string.IsNullOrWhiteSpace(DVT) ||
                    string.IsNullOrWhiteSpace(SoLuongTon))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    // Tìm và focus vào ô textbox bị thiếu thông tin đầu tiên
                    if (string.IsNullOrWhiteSpace(MaMH))
                    {
                        txtMaMH.Focus();
                    }
                    else if (string.IsNullOrWhiteSpace(TenMH))
                    {
                        txtTenMH.Focus();
                    }
                    else if (string.IsNullOrWhiteSpace(MaLoaiMH))
                    {
                        txtMaLoaiMH.Focus();
                    }
                    else if (string.IsNullOrWhiteSpace(DonGia))
                    {
                        txtDonGia.Focus();
                    }
                    else if (string.IsNullOrWhiteSpace(DVT))
                    {
                        txtDVT.Focus();
                    }
                    else if (string.IsNullOrWhiteSpace(SoLuongTon))
                    {
                        txtSoLuongTon.Focus();
                    }

                    return;
                }

                data.ExecuteNonQuery("INSERT into MatHang Values('" + MaMH + "',N'" + TenMH + "',N'" + DVT + "','" + DonGia + "','" + MaLoaiMH + "','" + SoLuongTon + "')");
                MessageBox.Show("Thêm mặt hàng '"+ MaMH + "' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                load();
                refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                int vitri = dgvMatHang.CurrentCell.RowIndex;
                string MaMH = dgvMatHang.Rows[vitri].Cells[0].Value.ToString();
                string TenMH = dgvMatHang.Rows[vitri].Cells[1].Value.ToString();
                string MaLoaiMH = dgvMatHang.Rows[vitri].Cells[2].Value.ToString();
                string DonGia = dgvMatHang.Rows[vitri].Cells[3].Value.ToString();
                string DVT = dgvMatHang.Rows[vitri].Cells[4].Value.ToString();
                string SoLuongTon = dgvMatHang.Rows[vitri].Cells[5].Value.ToString();
                loadToTextBox();
                data.ExecuteNonQuery("update MatHang set TenMH=N'" + TenMH + "',DonGia=" + DonGia + ",DVT=N'" + DVT + "',SoLuongTon='" + SoLuongTon + "' where MaMH='" + MaMH + "' ");
                MessageBox.Show("Sửa mặt hàng '"+MaMH+"' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                load();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Sửa thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int vitri = dgvMatHang.CurrentCell.RowIndex;
                string MaMH = dgvMatHang.Rows[vitri].Cells[0].Value.ToString();
                string TenMH = dgvMatHang.Rows[vitri].Cells[1].Value.ToString();
                string MaLoaiMH = dgvMatHang.Rows[vitri].Cells[2].Value.ToString();
                string DonGia = dgvMatHang.Rows[vitri].Cells[3].Value.ToString();
                string DVT = dgvMatHang.Rows[vitri].Cells[4].Value.ToString();
                string SoLuongTon = dgvMatHang.Rows[vitri].Cells[5].Value.ToString();
                data.ExecuteNonQuery("delete from MatHang where MaMH ='" + MaMH + "'");
                MessageBox.Show("Xóa mặt hàng '"+ MaMH + "' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                load();
                refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xóa mặt hàng thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            load();
            refresh();
            UpdateRowAndTotalLabels();
            txtMaMH.Enabled = true;
        }
    }
}
