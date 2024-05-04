
namespace QLBH_GS25
{
    partial class FrmQLKho
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmQLKho));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.xUẤTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NhapKhoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.XuatKho_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelMDIKho = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.menuStrip1.SuspendLayout();
            this.panelMDIKho.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xUẤTToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1578, 40);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // xUẤTToolStripMenuItem
            // 
            this.xUẤTToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.xUẤTToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NhapKhoToolStripMenuItem,
            this.XuatKho_ToolStripMenuItem});
            this.xUẤTToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xUẤTToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.xUẤTToolStripMenuItem.Name = "xUẤTToolStripMenuItem";
            this.xUẤTToolStripMenuItem.Padding = new System.Windows.Forms.Padding(30, 0, 26, 0);
            this.xUẤTToolStripMenuItem.Size = new System.Drawing.Size(238, 36);
            this.xUẤTToolStripMenuItem.Text = "QUẢN LÝ KHO";
            // 
            // NhapKhoToolStripMenuItem
            // 
            this.NhapKhoToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.NhapKhoToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NhapKhoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("NhapKhoToolStripMenuItem.Image")));
            this.NhapKhoToolStripMenuItem.Name = "NhapKhoToolStripMenuItem";
            this.NhapKhoToolStripMenuItem.Size = new System.Drawing.Size(270, 36);
            this.NhapKhoToolStripMenuItem.Text = "NHẬP KHO";
            this.NhapKhoToolStripMenuItem.Click += new System.EventHandler(this.NhapKhoToolStripMenuItem_Click);
            // 
            // XuatKho_ToolStripMenuItem
            // 
            this.XuatKho_ToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.XuatKho_ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("XuatKho_ToolStripMenuItem.Image")));
            this.XuatKho_ToolStripMenuItem.Name = "XuatKho_ToolStripMenuItem";
            this.XuatKho_ToolStripMenuItem.Size = new System.Drawing.Size(270, 36);
            this.XuatKho_ToolStripMenuItem.Text = "XUẤT KHO";
            this.XuatKho_ToolStripMenuItem.Click += new System.EventHandler(this.XuatKho_ToolStripMenuItem_Click);
            // 
            // panelMDIKho
            // 
            this.panelMDIKho.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panelMDIKho.Controls.Add(this.guna2PictureBox1);
            this.panelMDIKho.Location = new System.Drawing.Point(0, 130);
            this.panelMDIKho.Name = "panelMDIKho";
            this.panelMDIKho.Size = new System.Drawing.Size(1566, 804);
            this.panelMDIKho.TabIndex = 1;
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("guna2PictureBox1.Image")));
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(253, 52);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(956, 689);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 0;
            this.guna2PictureBox1.TabStop = false;
            // 
            // FrmQLKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1578, 929);
            this.Controls.Add(this.panelMDIKho);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmQLKho";
            this.Text = "FrmQLKho";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelMDIKho.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem xUẤTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NhapKhoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem XuatKho_ToolStripMenuItem;
        private Guna.UI2.WinForms.Guna2Panel panelMDIKho;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
    }
}