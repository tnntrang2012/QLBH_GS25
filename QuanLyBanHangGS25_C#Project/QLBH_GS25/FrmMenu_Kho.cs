using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Forms;

namespace QLBH_GS25
{
    public partial class FrmMenu_Kho : Form
    {
        public FrmMenu_Kho()
        {
            InitializeComponent();
            Timer timer = new Timer();
            timer.Interval = 1000; // Cập nhật mỗi 1 giây (1000 ms)
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Cập nhật thời gian
            lblGio.Text = DateTime.Now.ToString("HH:mm:ss");

            // Cập nhật ngày
            lblNgay.Text = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("vi-VN"));
        }


        private void btnPhieuNhap_Click(object sender, EventArgs e)
        {
            openChildForm(new FrmPhieuNhap());
        }

        private void btnPhieuXuat_Click(object sender, EventArgs e)
        {
            openChildForm(new FrmPhieuXuat());
        }

        #region Method
        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelMDI.Controls.Add(childForm);
            panelMDI.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }
        #endregion

        private void lblWelcome_Click(object sender, EventArgs e)
        {

        }


        private void guna2Button1_Click(object sender, EventArgs e)
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

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            openChildForm(new FrmDashBoard_KHO());
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            openChildForm(new FrmHome_Kho());
        }
    }
}
