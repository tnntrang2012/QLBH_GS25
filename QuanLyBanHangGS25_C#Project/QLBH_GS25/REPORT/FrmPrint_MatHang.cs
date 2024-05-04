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
    public partial class FrmPrint_MatHang : Form
    {
        public FrmPrint_MatHang()
        {
            InitializeComponent();
        }

        private void FrmPrint_MatHang_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (rdoAllMH.Checked==true)
            {
                SqlConnection connection = new SqlConnection(@"Data Source= East\SQLEXPRESS;Initial Catalog= QLBH_GS25;Integrated Security=True;Persist Security Info= True;User ID=east;Password=1");
                SqlDataAdapter da = new SqlDataAdapter("select * from MatHang", connection);
                DataSet ds = new DataSet();
                da.Fill(ds, "DA_MatHang");

                ReportDataSource dataSource = new ReportDataSource("DataSetMH", ds.Tables[0]);

                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(dataSource);
                this.reportViewer1.RefreshReport();
            }
            else if(rdoSapHet.Checked==true)
            {
                SqlConnection connection = new SqlConnection(@"Data Source= East\SQLEXPRESS;Initial Catalog= QLBH_GS25;Integrated Security=True;Persist Security Info= True;User ID=east;Password=1");
                SqlDataAdapter da = new SqlDataAdapter("select * from MatHang where SoLuongTon <= 50", connection);
                DataSet ds = new DataSet();
                da.Fill(ds, "DA_MatHang");

                ReportDataSource dataSource = new ReportDataSource("DataSetMH", ds.Tables[0]);

                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(dataSource);
                this.reportViewer1.RefreshReport();
            }
            
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
