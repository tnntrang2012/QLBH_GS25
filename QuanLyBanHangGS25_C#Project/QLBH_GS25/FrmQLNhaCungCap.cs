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
    public partial class FrmQLNhaCungCap : Form
    {
        public FrmQLNhaCungCap()
        {
            InitializeComponent();
            load();
        }
        KetNoiCSDL data = new KetNoiCSDL();
        private BindingSource bdsoure = new BindingSource();
        private void load()
        {
            //đổ dữ liệu
            bdsoure.DataSource = data.DSNCC();
            dgvNCC.DataSource = bdsoure;
            dgvNCC.Columns[0].HeaderText = "Mã NCC";
            dgvNCC.Columns[1].HeaderText = "Tên CTY";
            dgvNCC.Columns[2].HeaderText = "Email";
            dgvNCC.Columns[3].HeaderText = "SĐT";


            ////kích thước cột
            DataGridView x = dgvNCC;
            {
                x.Columns[0].Width = 80;
                x.Columns[1].Width = 180;
                x.Columns[2].Width = 150;
                x.Columns[3].Width = 100;

            }
            //font chữ của full bảng
            dgvNCC.Font = new Font("Century", 10, FontStyle.Regular);
            //màu chữ của dữ liệu đổ vào
            this.dgvNCC.DefaultCellStyle.ForeColor = Color.Black;

            dgvNCC.ColumnHeadersDefaultCellStyle.Font = new Font("Century", 10, FontStyle.Regular);
            //màu header và chữ header
            dgvNCC.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.PaleTurquoise;
            dgvNCC.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            //màu của dòng được chọn
            dgvNCC.DefaultCellStyle.SelectionBackColor = Color.PaleTurquoise;
            dgvNCC.RowHeadersDefaultCellStyle.SelectionBackColor = Color.PaleTurquoise;
            //màu của những dòng k được chọn
            dgvNCC.DefaultCellStyle.BackColor = Color.White;
            //màu của chữ được chọn
            dgvNCC.RowsDefaultCellStyle.SelectionForeColor = Color.Black;


            dgvNCC.EnableHeadersVisualStyles = false;
            dgvNCC.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void loadToTextBox()
        {
            txtMaNCC.ReadOnly = true;
            int i;
            i = dgvNCC.CurrentRow.Index;
            txtMaNCC.Text = dgvNCC.Rows[i].Cells[0].Value.ToString();
            txtTenNCC.Text = dgvNCC.Rows[i].Cells[1].Value.ToString();
            txtEmail.Text = dgvNCC.Rows[i].Cells[2].Value.ToString();
            txtSDT.Text = dgvNCC.Rows[i].Cells[3].Value.ToString();
        }
        private void refresh()
        {
            txtMaNCC.ResetText();
            txtTenNCC.ResetText();
            txtSDT.ResetText();
            txtEmail.ResetText();

            txtMaNCC.Focus();
            txtMaNCC.ReadOnly = false;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string MaNCC = txtMaNCC.Text;
                string TenNCC = txtTenNCC.Text;
                string SDT = txtSDT.Text;
                string Email = txtEmail.Text;
                // Kiểm tra xem có thông tin nào bị thiếu không
                if (string.IsNullOrWhiteSpace(MaNCC) ||
                    string.IsNullOrWhiteSpace(TenNCC) ||
                    string.IsNullOrWhiteSpace(SDT) ||
                    string.IsNullOrWhiteSpace(Email))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    // Tìm và focus vào ô textbox bị thiếu thông tin đầu tiên
                    if (string.IsNullOrWhiteSpace(MaNCC))
                    {
                        txtMaNCC.Focus();
                    }
                    else if (string.IsNullOrWhiteSpace(TenNCC))
                    {
                        txtTenNCC.Focus();
                    }
                    else if (string.IsNullOrWhiteSpace(SDT))
                    {
                        txtSDT.Focus();
                    }
                    else if (string.IsNullOrWhiteSpace(Email))
                    {
                        txtEmail.Focus();
                    }

                    return;
                }


                data.ExecuteNonQuery("INSERT into NhaCungCap Values('" + MaNCC + "',N'" + TenNCC + "','" + Email + "','" + SDT + "')");
                MessageBox.Show("Thêm '" + MaNCC + "' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                load();
                refresh();
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int vitri = dgvNCC.CurrentCell.RowIndex;
                string MaNCC = dgvNCC.Rows[vitri].Cells[0].Value.ToString();
                string Ten = dgvNCC.Rows[vitri].Cells[1].Value.ToString();
                string Email = dgvNCC.Rows[vitri].Cells[2].Value.ToString();
                string SDT = dgvNCC.Rows[vitri].Cells[3].Value.ToString();

                load();
                refresh();
                data.ExecuteNonQuery("delete from NhaCungCap where MaNCC = '" + MaNCC +"'");
                MessageBox.Show("Xóa thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                
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
                int vitri = dgvNCC.CurrentCell.RowIndex;
                string MaNCC = dgvNCC.Rows[vitri].Cells[0].Value.ToString();
                string Ten = dgvNCC.Rows[vitri].Cells[1].Value.ToString();
                
                string Email = dgvNCC.Rows[vitri].Cells[2].Value.ToString();
                string SDT = dgvNCC.Rows[vitri].Cells[3].Value.ToString();
                string MaNCCTruoc = MaNCC;
          

                loadToTextBox();
                data.ExecuteNonQuery("update NhaCungCap set TenNCC= N'" + Ten + "',EmailNCC ='" + Email + "',SDTNCC='" + SDT + "' where MaNCC='" + MaNCC + "' ");
                if (MaNCC != MaNCCTruoc)
                {
                    MessageBox.Show("Không được sửa mã nhà cung cấp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    // Lấy lại mã nhà cung cấp ban đầu và cập nhật lại DataGridView
                    dgvNCC.Rows[vitri].Cells[0].Value = MaNCCTruoc;
                    
                    return;
                }
                MessageBox.Show("Sửa thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                load();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FrmQLNhaCungCap_Load(object sender, EventArgs e)
        {

        }

        private void dgvNCC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            loadToTextBox();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (cboTimKiem.SelectedIndex == 0)// Tìm theo Mã NV
            {
                DataView dv = new DataView(data.Executequery("Select * from NhaCungcap where MaNCC like '" + txtTimKiem.Text + "%'"));
                if (dv.Count > 0)
                {
                    dgvNCC.DataSource = dv;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy kết quả", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (cboTimKiem.SelectedIndex == 1) // Tìm theo Họ tên nhân viên
            {
                DataView dv = new DataView(data.Executequery("Select * from NhaCungcap where SDTNCC like '" + txtTimKiem.Text + "%'"));
                if (dv.Count > 0)
                {
                    dgvNCC.DataSource = dv;
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
    }
}
