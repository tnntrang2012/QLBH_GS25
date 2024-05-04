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
    public partial class FrmQLTheThanhVien : Form
    {
        public FrmQLTheThanhVien()
        {
            InitializeComponent();
            load();
        }
        KetNoiCSDL data = new KetNoiCSDL();
        private BindingSource bdsoure = new BindingSource();
        private void load()
        {
            //đổ dữ liệu
            bdsoure.DataSource = data.QLTheTV();
            dgvTTV.DataSource = bdsoure;
            dgvTTV.Columns[0].HeaderText = "Mã Thẻ";
            dgvTTV.Columns[1].HeaderText = "Điểm TL";
            dgvTTV.Columns[2].HeaderText = "Ngày ĐK";
            dgvTTV.Columns[3].HeaderText = "Mã KH";
     

            //kích thước cột
            DataGridView x = dgvTTV;
            {
                x.Columns[0].Width = 100;
                x.Columns[1].Width = 100;
                x.Columns[2].Width = 160;
                x.Columns[3].Width = 97;
     

            }
            //font chữ của full bảng
            dgvTTV.Font = new Font("Century", 10, FontStyle.Regular);
            //màu chữ của dữ liệu đổ vào
            this.dgvTTV.DefaultCellStyle.ForeColor = Color.Black;

            dgvTTV.ColumnHeadersDefaultCellStyle.Font = new Font("Century", 10, FontStyle.Regular);
            //màu header và chữ header
            dgvTTV.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.PaleTurquoise;
            dgvTTV.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            //màu của dòng được chọn
            dgvTTV.DefaultCellStyle.SelectionBackColor = Color.PaleTurquoise;
            dgvTTV.RowHeadersDefaultCellStyle.SelectionBackColor = Color.PaleTurquoise;
            //màu của những dòng k được chọn
            dgvTTV.DefaultCellStyle.BackColor = Color.White;
            //màu của chữ được chọn
            dgvTTV.RowsDefaultCellStyle.SelectionForeColor = Color.Black;


            dgvTTV.EnableHeadersVisualStyles = false;
            dgvTTV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private void refresh()
        {
            txtMaThe.ResetText();
            txtMaKH.ResetText();
            txtDiemTL.ResetText();
            dtDK.Value = DateTime.Now;
           
        }
        private void loadToTextBox()
        {
            txtMaKH.ReadOnly = true;
            txtMaThe.ReadOnly = true;
            int i;
            i = dgvTTV.CurrentRow.Index;
            txtMaThe.Text = dgvTTV.Rows[i].Cells[0].Value.ToString();
            txtDiemTL.Text = dgvTTV.Rows[i].Cells[1].Value.ToString();
            dtDK.Value = Convert.ToDateTime(dgvTTV.Rows[i].Cells[2].Value);
            txtMaKH.Text = dgvTTV.Rows[i].Cells[3].Value.ToString();

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                int vitri = dgvTTV.CurrentCell.RowIndex;
                string MaThe = dgvTTV.Rows[vitri].Cells[0].Value.ToString();
                string DiemTL = dgvTTV.Rows[vitri].Cells[1].Value.ToString();
                string NgayDK = dgvTTV.Rows[vitri].Cells[2].Value.ToString();
                string MaKH = dgvTTV.Rows[vitri].Cells[3].Value.ToString();
             


                loadToTextBox();
                data.ExecuteNonQuery("update TheThanhVien set DiemTL= " + DiemTL + " where MaThe = '"+MaThe+"'");
                MessageBox.Show("Sửa thẻ '" + MaThe + "' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                int vitri = dgvTTV.CurrentCell.RowIndex;
                string MaThe = dgvTTV.Rows[vitri].Cells[0].Value.ToString();
                string DiemTL = dgvTTV.Rows[vitri].Cells[1].Value.ToString();
                string NgayDK = dgvTTV.Rows[vitri].Cells[2].Value.ToString();
                string MaKH = dgvTTV.Rows[vitri].Cells[3].Value.ToString();



                loadToTextBox();
                data.ExecuteNonQuery("delete from TheThanhVien where MaThe ='" + MaThe + "'");
                MessageBox.Show("Xóa thẻ '" + MaThe + "' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (cboTimKiem.SelectedIndex == 0)// Tìm theo Mã thẻ
            {
                DataView dv = new DataView(data.Executequery("Select * from TheThanhVien where MaThe like '" + txtTimKiem.Text + "%'"));
                if (dv.Count > 0)
                {
                    dgvTTV.DataSource = dv;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy kết quả", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (cboTimKiem.SelectedIndex == 1) // Tìm theo mã KH
            {
                DataView dv = new DataView(data.Executequery("Select * from TheThanhVien where MaKH like '" + txtTimKiem.Text + "%'"));
                if (dv.Count > 0)
                {
                    dgvTTV.DataSource = dv;
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

        private void dgvTTV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            loadToTextBox();
        }
    }
}
