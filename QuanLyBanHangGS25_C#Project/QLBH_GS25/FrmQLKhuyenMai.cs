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
    public partial class FrmQLKhuyenMai : Form
    {
       
        public FrmQLKhuyenMai()
        {
            InitializeComponent();
            load();
   

        }
        KetNoiCSDL data = new KetNoiCSDL();
        private BindingSource bdsoure = new BindingSource();
        private void load()
        {
            //đổ dữ liệu
            bdsoure.DataSource = data.KhuyenMai();
            dgvKM.DataSource = bdsoure;
            dgvKM.Columns[0].HeaderText = "Mã KM";
            dgvKM.Columns[1].HeaderText = "Tên KM";
            dgvKM.Columns[2].HeaderText = "Bắt đầu";
            dgvKM.Columns[3].HeaderText = "Kết thúc";
            dgvKM.Columns[4].HeaderText = "Giảm giá";

            //kích thước cột
            DataGridView x = dgvKM;
            {
                x.Columns[0].Width = 60;
                x.Columns[1].Width = 160;
                x.Columns[2].Width = 90;
                x.Columns[3].Width = 90;
                x.Columns[4].Width = 55;

            }
            //font chữ của full bảng
            dgvKM.Font = new Font("Century", 10, FontStyle.Regular);
            //màu chữ của dữ liệu đổ vào
            this.dgvKM.DefaultCellStyle.ForeColor = Color.Black;

            dgvKM.ColumnHeadersDefaultCellStyle.Font = new Font("Century", 10, FontStyle.Regular);
            //màu header và chữ header
            dgvKM.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.PaleTurquoise;
            dgvKM.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            //màu của dòng được chọn
            dgvKM.DefaultCellStyle.SelectionBackColor = Color.PaleTurquoise;
            dgvKM.RowHeadersDefaultCellStyle.SelectionBackColor = Color.PaleTurquoise;
            //màu của những dòng k được chọn
            dgvKM.DefaultCellStyle.BackColor = Color.White;
            //màu của chữ được chọn
            dgvKM.RowsDefaultCellStyle.SelectionForeColor = Color.Black;


            dgvKM.EnableHeadersVisualStyles = false;
            dgvKM.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private void refresh()
        {
            txtMaKM.Enabled = true;
            txtMaKM.ReadOnly = false;
            txtMaKM.Text="";
            txtTenKM.ResetText();
            txtGiamGia.ResetText();
            dtBD.Value = DateTime.Now;
            dtKT.Value = DateTime.Now;
        }
        private void loadToTextBox()
        {
            txtMaKM.ReadOnly = true;
            txtMaKM.Enabled = false;
            int i;
            i = dgvKM.CurrentRow.Index;
            txtMaKM.Text = dgvKM.Rows[i].Cells[0].Value.ToString();
            txtTenKM.Text = dgvKM.Rows[i].Cells[1].Value.ToString();
            dtBD.Value = Convert.ToDateTime(dgvKM.Rows[i].Cells[2].Value);
            dtKT.Value = Convert.ToDateTime(dgvKM.Rows[i].Cells[3].Value);
            txtGiamGia.Text = dgvKM.Rows[i].Cells[4].Value.ToString();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string MaKM = txtMaKM.Text;
                string TenKM = txtTenKM.Text;
                string GiamGia = "";
                DateTime BD = dtBD.Value;
                DateTime KT = dtKT.Value;
                if (string.IsNullOrWhiteSpace(MaKM) || string.IsNullOrWhiteSpace(TenKM))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (BD.CompareTo(KT) < 0)
                {
                    GiamGia = txtGiamGia.Text;

                    if (!string.IsNullOrEmpty(GiamGia) && double.TryParse(GiamGia, out double discountValue))
                    {
                        // Kiểm tra nếu giá trị giảm giá nằm trong khoảng từ 0 đến 100
                        if (discountValue > 0 && discountValue < 100)
                        {
                            data.ExecuteNonQuery("INSERT into KhuyenMai Values('" + MaKM + "',N'" + TenKM + "','" + BD + "','" + KT + "','" + GiamGia + "')");
                            MessageBox.Show("Thêm Khuyến mãi thành công, Mã KM: '" + MaKM + "'", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            load();
                            refresh();
                            MessageBox.Show("Giảm giá đã được áp dụng!");
                        }
                        else
                        {
                            MessageBox.Show("Giá trị giảm giá phải nằm trong khoảng từ 0 đến 100!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng nhập một giá trị số hợp lệ cho giảm giá!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Ngày bắt đầu phải trước ngày kết thúc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                
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
                int vitri = dgvKM.CurrentCell.RowIndex;
                string MaKM = dgvKM.Rows[vitri].Cells[0].Value.ToString();
                string TenKM = dgvKM.Rows[vitri].Cells[1].Value.ToString();
                string BD = dgvKM.Rows[vitri].Cells[2].Value.ToString();
                string KT = dgvKM.Rows[vitri].Cells[3].Value.ToString();
                string GiamGia = dgvKM.Rows[vitri].Cells[4].Value.ToString();
                

                loadToTextBox();
                data.ExecuteNonQuery("update KhuyenMai set TenKM=N'" + TenKM + "',NgayBDKM='" + BD + "',NgayKTKM='" + KT + "',GiamGia='" + GiamGia + "'");
                MessageBox.Show("Sửa '"+MaKM+"' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                int vitri = dgvKM.CurrentCell.RowIndex;
                string MaKM = dgvKM.Rows[vitri].Cells[0].Value.ToString();
                string TenKM = dgvKM.Rows[vitri].Cells[1].Value.ToString();
                string BD = dgvKM.Rows[vitri].Cells[2].Value.ToString();
                string KT = dgvKM.Rows[vitri].Cells[3].Value.ToString();
                string GiamGia = dgvKM.Rows[vitri].Cells[4].Value.ToString();


               
                data.ExecuteNonQuery("delete from KhuyenMai where MaKM ='" + MaKM + "'");
                loadToTextBox();
                load();
                refresh();
                MessageBox.Show("Xóa '"+MaKM+"' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                

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

        
        private void btnHome_Click(object sender, EventArgs e)
        {
    
            this.Close();
        }
        

        private void dgvKM_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            loadToTextBox();

        }
    }
}
