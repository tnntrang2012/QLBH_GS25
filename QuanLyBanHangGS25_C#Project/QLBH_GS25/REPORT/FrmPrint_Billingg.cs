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
    public partial class FrmPrint_Billingg : Form
    {
        string maHoaDon;
        public FrmPrint_Billingg(string MaHD)
        {
            InitializeComponent();
            maHoaDon = MaHD;
        }

 

        private void FormPrint_Billingg_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(@"Data Source= East\SQLEXPRESS;Initial Catalog= QLBH_GS25;Integrated Security=True;Persist Security Info= True;User ID=east;Password=1");
            SqlDataAdapter da = new SqlDataAdapter("select * from TongTienBill where MaHD = '" + maHoaDon + "'", connection);
            DataSet ds = new DataSet();
            da.Fill(ds, "Bill");

            ReportDataSource dataSource = new ReportDataSource("DataSet_RP", ds.Tables[0]);

            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(dataSource);
            this.reportViewer1.RefreshReport();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(@"Data Source= East\SQLEXPRESS;Initial Catalog= QLBH_GS25;Integrated Security=True;Persist Security Info= True;User ID=east;Password=1");
            SqlDataAdapter da = new SqlDataAdapter("select * from TongTienBill where MaHD = '" + txtMaHD.Text + "'", connection);
            DataSet ds = new DataSet();
            da.Fill(ds, "Bill");

            ReportDataSource dataSource = new ReportDataSource("DataSet_RP", ds.Tables[0]);

            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(dataSource);
            this.reportViewer1.RefreshReport();
        }
    }
}
