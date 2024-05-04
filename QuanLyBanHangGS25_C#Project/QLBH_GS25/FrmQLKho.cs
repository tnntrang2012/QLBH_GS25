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
    public partial class FrmQLKho : Form
    {
        public FrmQLKho()
        {
            InitializeComponent();
        }

        private void NhapKhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new NhapKho());
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
            panelMDIKho.Controls.Add(childForm);
            panelMDIKho.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        #endregion

        private void XuatKho_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new XuatKho());
        }
    }
}
