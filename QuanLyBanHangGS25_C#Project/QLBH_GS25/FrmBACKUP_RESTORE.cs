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
    public partial class FrmBACKUP_RESTORE : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source= East\SQLEXPRESS;Initial Catalog=QLBH_GS25;Integrated Security=True;Persist Security Info= True;User ID=east;Password=1");
        public FrmBACKUP_RESTORE()
        {
            InitializeComponent();
        }
        KetNoiCSDL data = new KetNoiCSDL();

        private void btnBBu_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtBack.Text = dlg.SelectedPath;
                btnBackup.Enabled = true;
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            string database = con.Database.ToString();
            if(txtBack.Text == string.Empty)
            {
                MessageBox.Show("Hãy nhập địa chỉ file cần sao lưu!");
            }
            else
            {
                string cmd = "BACKUP DATABASE ["+database+ "] TO DISK= '" + txtBack.Text + "\\" + "QLBH_GS25_Project" + "-" + DateTime.Now.ToString("dd-MM-yyyy--HH-mm-ss") + ".bak'";
                con.Open();
                SqlCommand command = new SqlCommand(cmd, con);
                command.ExecuteNonQuery();
                MessageBox.Show("Sao lưu dữ liệu thành công!");
                con.Close();
                btnBackup.Enabled = false;
            }
        }

        private void btnBRs_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "SQL SERVER database backup files|*.bak";
            dlg.Title = "Database Restore";
            if(dlg.ShowDialog()==DialogResult.OK)
            {
                txtRes.Text = dlg.FileName;
                btnRestore.Enabled = true;
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            string database = con.Database.ToString();
            con.Open();
            try
            {
                string str1 = string.Format("ALTER DATABASE ["+database+"] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
                SqlCommand cmd1 = new SqlCommand(str1, con);
                cmd1.ExecuteNonQuery();

                string str2 = "USE MASTER RESTORE DATABASE [" + database + "] FROM DISK='" + txtRes.Text + "' WITH REPLACE;";
                SqlCommand cmd2 = new SqlCommand(str2, con);
                cmd2.ExecuteNonQuery();

                string str3 = "ALTER DATABASE ["+database+"] SET MULTI_USER";
                SqlCommand cmd3 = new SqlCommand(str3, con);
                cmd3.ExecuteNonQuery();

                MessageBox.Show("Phục hồi dữ liệu thành công!");
                con.Close();
            }
            catch
            {
                
            }
        }

        private void txtBack_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2CircleButton4_Click(object sender, EventArgs e)
        {

        }
    }
}
