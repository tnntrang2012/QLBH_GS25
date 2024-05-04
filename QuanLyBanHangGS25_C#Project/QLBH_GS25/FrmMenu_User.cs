using System;
using System.Data;
using System.Windows.Forms;

namespace QLBH_GS25
{
    public partial class FrmMenu_User : Form
    {
        KetNoiCSDL data = new KetNoiCSDL();
        private BindingSource bdsoure = new BindingSource();
        private DataTable DT = new DataTable();

        public FrmMenu_User()
        {
            InitializeComponent();

        }


        private void FrmMenu_User_Load(object sender, EventArgs e)
        {


        }




        private void btnDangXuat_Click(object sender, EventArgs e)
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

        private void btnDKThanhVien_Click(object sender, EventArgs e)
        {
            FrmDKThanhVien f = new FrmDKThanhVien();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void btnBanHang_Click(object sender, EventArgs e)
        {
            FrmBanHang f = new FrmBanHang();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }
    }
}
