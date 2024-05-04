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
    public partial class XuatKho : Form
    {
        public XuatKho()
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
            bdsoure.DataSource = data.QLXuatKho();
            dgvPhieuXuat.DataSource = bdsoure;

            dgvPhieuXuat.Columns[0].HeaderText = "Mã PX";
            dgvPhieuXuat.Columns[1].HeaderText = "Ngày lập";
            dgvPhieuXuat.Columns[2].HeaderText = "Mã NV";
     

            //kích thước cột
            DataGridView x = dgvPhieuXuat;
            {
                x.Columns[0].Width = 120;
                x.Columns[1].Width = 150;
                x.Columns[2].Width = 120;
   


            }
            //font chữ của full bảng
            dgvPhieuXuat.Font = new Font("Century", 10, FontStyle.Regular);
            //màu chữ của dữ liệu đổ vào
            this.dgvPhieuXuat.DefaultCellStyle.ForeColor = Color.Black;

            dgvPhieuXuat.ColumnHeadersDefaultCellStyle.Font = new Font("Century", 10, FontStyle.Regular);
            //màu header và chữ header
            dgvPhieuXuat.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgvPhieuXuat.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            //màu của dòng được chọn
            dgvPhieuXuat.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgvPhieuXuat.RowHeadersDefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            //màu của những dòng k được chọn
            dgvPhieuXuat.DefaultCellStyle.BackColor = Color.White;
            //màu của chữ được chọn
            dgvPhieuXuat.RowsDefaultCellStyle.SelectionForeColor = Color.Black;


            dgvPhieuXuat.EnableHeadersVisualStyles = false;
            dgvPhieuXuat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //-----------------------------
            //font chữ của full bảng
            dgvCTPX.Font = new Font("Century", 10, FontStyle.Regular);
            //màu chữ của dữ liệu đổ vào
            this.dgvCTPX.DefaultCellStyle.ForeColor = Color.Black;

            dgvCTPX.ColumnHeadersDefaultCellStyle.Font = new Font("Century", 10, FontStyle.Regular);
            //màu header và chữ header
            dgvCTPX.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgvCTPX.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            //màu của dòng được chọn
            dgvCTPX.DefaultCellStyle.SelectionBackColor = Color.White;
            dgvCTPX.RowHeadersDefaultCellStyle.SelectionBackColor = Color.White;
            //màu của những dòng k được chọn
            dgvCTPX.DefaultCellStyle.BackColor = Color.White;
            //màu của chữ được chọn
            dgvCTPX.RowsDefaultCellStyle.SelectionForeColor = Color.Black;


            dgvCTPX.EnableHeadersVisualStyles = false;
            dgvCTPX.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


        }

        private void dgvPhieuXuat_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPhieuXuat.SelectedRows.Count > 0)
            {
                // Lấy MaHD của hóa đơn được chọn từ cột đầu tiên (chỉ số cột 0)
                string selectedValue = dgvPhieuXuat.SelectedRows[0].Cells[0].Value.ToString();

                // Truy vấn cơ sở dữ liệu để lấy thông tin chi tiết hóa đơn
                string query = "SELECT CT.MaPX [Mã PX],CT.MaMH [Mã MH], MH.TenMH [Tên MH], SoLuongXuat [Số lượng] FROM ChiTietPhieuXuat CT JOIN PhieuXuat PX ON CT.MaPX=px.MaPX JOIN MatHang MH ON CT.MaMH=MH.MaMH where PX.MaPX =@MaPX";

                using (SqlConnection connection = new SqlConnection(@"Data Source= DESKTOP-4QED5F9\NGOCTRANG;Initial Catalog= QLBH_GS25;Integrated Security=True;Persist Security Info= True;User ID=east;Password=1"))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaPX", selectedValue);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dtCTPX = new DataTable();
                        adapter.Fill(dtCTPX);

                        // Đổ dữ liệu lên DataGridView ChiTietHoaDon
                        dgvCTPX.DataSource = dtCTPX;
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
