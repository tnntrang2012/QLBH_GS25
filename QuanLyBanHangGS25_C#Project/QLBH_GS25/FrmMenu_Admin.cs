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
    public partial class FrmMenu_Admin : Form
    {

        public FrmMenu_Admin()
        {
            InitializeComponent();
            panelTK.Visible = false;

            panelMDI.SendToBack();

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            
            timer.Start();
            lblNgay.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            proClock.Invoke((MethodInvoker)delegate
            {
                proClock.Text = DateTime.Now.ToString("HH:mm:ss");
                proClock.Value = Convert.ToInt32(DateTime.Now.Second);
            });
            
        }

        private void btnLogOut_Click(object sender, EventArgs e)
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

        private bool isPanelOpened = false;
        private void btnThongKe_Click(object sender, EventArgs e)
        {
            if (!isPanelOpened)
            {
                panelMDI.BringToFront();
                openChildForm(new FrmHome());
                panelTK.BringToFront();
                // Mở Panel
                panelTK.Visible = true; // Đặt tên của Panel Thống kê trong giao diện của bạn
                isPanelOpened = true;

                /*panelTK.BringToFront();*/ // Đảm bảo Panel hiển thị trên các lớp khác
            }
            else
            {
                // Đóng Panel
                panelTK.Visible = false; // Đặt tên của Panel Thống kê trong giao diện của bạn
                isPanelOpened = false;
            }
        }

     
        private void btnKhuyenMai_Click(object sender, EventArgs e)
        {
            // Mở Panel
    
            openChildForm2Button(new FrmQLKhuyenMai());



        }
        private void btnTheThanhVien_Click(object sender, EventArgs e)
        {


            openChildForm2Button(new FrmQLTheThanhVien());

        }

        private void btnKho_Click(object sender, EventArgs e)
        {
            panelMDI.BringToFront();
            openChildForm(new FrmQLKho());
        }

        private void btnMatHang_Click(object sender, EventArgs e)
        {
            panelMDI.BringToFront();
            openChildForm(new FrmQLMatHang());

        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            panelMDI.BringToFront();
            openChildForm(new FrmQLHoaDon());
            //FrmQLHoaDon f = new FrmQLHoaDon();
            //this.Hide();
            //f.ShowDialog();
            //this.Close();
        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            panelMDI.BringToFront();
            openChildForm(new FrmQLTaiKhoan());
            //FrmQLTaiKhoan f = new FrmQLTaiKhoan();
            //this.Hide();
            //f.ShowDialog();
            //this.Close();
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            panelMDI.BringToFront();
            openChildForm(new FrmQLNhanVien());
            //FrmQLNhanVien f = new FrmQLNhanVien();
            //this.Hide();
            //f.ShowDialog();
            //this.Close();
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
        private Form activeForm2 = null;
        private void openChildForm2Button(Form childForm1)
        {
            if (activeForm2 != null)
                activeForm2.Close();
            activeForm2 = childForm1;
            childForm1.TopLevel = false;
            childForm1.FormBorderStyle = FormBorderStyle.None;
            childForm1.Dock = DockStyle.Fill;

            childForm1.BringToFront();
            childForm1.Show();

        }

        private void closeChildForm()
        {
            if (activeForm2 != null)
            {
                activeForm2.SendToBack();
            }
        }



        #endregion

        private void btnHome_Click(object sender, EventArgs e)
        {


            panelMDI.BringToFront();
            openChildForm(new FrmHome());
            //panel2button.Visible = false;
            //panel2button.SendToBack();
        }

        private void btnDoanhThu_Click(object sender, EventArgs e)
        {
            new REPORT.FrmPrint_DoanhThu().Show();
        }

        private void btnKH_Click(object sender, EventArgs e)
        {
            new REPORT.FrmPrint_DSKhachHang().Show();
        }

        private void btnMH_Click(object sender, EventArgs e)
        {
            new REPORT.FrmPrint_MatHang().Show();
        }

        private void btnNCC_Click(object sender, EventArgs e)
        {
            panelMDI.BringToFront();
            openChildForm(new FrmQLNhaCungCap());
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            panelMDI.BringToFront();
            openChildForm(new FrmBACKUP_RESTORE());
        }
    }
}
