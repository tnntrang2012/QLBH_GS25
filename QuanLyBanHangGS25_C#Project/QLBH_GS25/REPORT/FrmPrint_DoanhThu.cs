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
using Microsoft.Reporting.WinForms;

namespace QLBH_GS25.REPORT
{
    public partial class FrmPrint_DoanhThu : Form
    {
        public FrmPrint_DoanhThu()
        {
            InitializeComponent();
        }

        private void FrmBaoCao_DoanhThu_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            DateTime from = DateTime.Parse(dtTuNgay.Text);
            DateTime to = DateTime.Parse(dtDenNgay.Text);

            SqlConnection connection = new SqlConnection(@"Data Source= DESKTOP-4QED5F9\NGOCTRANG;Initial Catalog= QLBH_GS25;Integrated Security=True;Persist Security Info= True;User ID=east;Password=1");
            SqlDataAdapter da = new SqlDataAdapter("select * from DoanhThuTheoNgay where NgayLapHD >= '"+from+"' and NgayLapHD <='"+to+"' ", connection);
            DataSet ds = new DataSet();
            da.Fill(ds, "DA_DoanhThu");

            ReportDataSource dataSource = new ReportDataSource("DataSetDoanhThu", ds.Tables[0]);

            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(dataSource);
            this.reportViewer1.RefreshReport();
        }

        private void dtTuNgay_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtDenNgay_ValueChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res = MessageBox.Show("Bạn muốn về màn hình chính?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                this.Close();
            }
            else
            {
                this.Show();
            }
        }
    }
}
