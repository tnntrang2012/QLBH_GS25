using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLBH_GS25
{
   
    public partial class FrmLogin : Form
    {

        DataTable DT = new DataTable();
        KetNoiCSDL data = new KetNoiCSDL();  
        public FrmLogin()
        {
            InitializeComponent();
            cboChucVu.Items.AddRange(new object[] {"Quản lý", "Nhân viên bán hàng","Nhân viên kho"});
            cboChucVu.SelectedIndex = 0;
        }
        private bool KiemTraDangNhap(string username, string password, string chucvu)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source= DESKTOP-4QED5F9\NGOCTRANG;Initial Catalog= QLBH_GS25;Integrated Security=True"))//Persist Security Info= True;User ID=east;Password=1"))
            {
                connection.Open();
                string sql = "SELECT COUNT(*) FROM TKDangNhap WHERE ID = @UserName AND MatKhau = @PassWord AND ChucVu = @ChucVu";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@UserName", username);
                command.Parameters.AddWithValue("@PassWord", password);
                command.Parameters.AddWithValue("@ChucVu", chucvu);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }
        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            string username = txtTK.Text;
            string password = txtMK.Text;
            string chucvu = cboChucVu.SelectedItem.ToString();
            // Kiểm tra người dùng đã nhập tên đăng nhập và mật khẩu hay chưa
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Kiểm tra tài khoản và mật khẩu trong cơ sở dữ liệu
            bool isValid = KiemTraDangNhap(username, password, chucvu);

            if (isValid)
            {
                if (chucvu == "Quản lý")
                {
                    MessageBox.Show("Thông tin hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FrmMenu_Admin f = new FrmMenu_Admin();
                    this.Hide();
                    f.ShowDialog();
                    this.Close();
                }
                else if (chucvu == "Nhân viên bán hàng")
                {
                    MessageBox.Show("Thông tin hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FrmMenu_User f = new FrmMenu_User();
                    this.Hide();
                    f.ShowDialog();
                    this.Close();
                }
                else if (chucvu == "Nhân viên kho")
                {
                    MessageBox.Show("Thông tin hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FrmMenu_Kho f = new FrmMenu_Kho();
                    this.Hide();
                    f.ShowDialog();
                    this.Close();
                }

            }
            else
            {
                MessageBox.Show("Vui lòng kiểm tra lại thông tin !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            DialogResult res;
            res = MessageBox.Show("Bạn muốn thoát khỏi chương trình?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                
                this.Close();
            }
            else
            {
                this.Show();
            }
        }

        private void txtTK_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMK_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
