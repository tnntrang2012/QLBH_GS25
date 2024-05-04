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
    public partial class FrmLoad : Form
    {
        public FrmLoad()
        {
            InitializeComponent();
        }

        int starPoint = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            starPoint += 3;
            guna2ProgressBar1.Value = starPoint;
            if(guna2ProgressBar1.Value == 100)
            {
                guna2ProgressBar1.Value = 0;
                timer1.Stop();
                this.Hide();
                FrmLogin f = new FrmLogin();
                f.ShowDialog();
                this.Close();
            }
            
        }

        private void FrmLoading_Load(object sender, EventArgs e)
        {
            timer1.Start();
            

        }

    }
}
