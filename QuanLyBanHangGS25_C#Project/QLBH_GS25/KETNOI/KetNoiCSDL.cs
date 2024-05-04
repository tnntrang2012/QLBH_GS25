using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace QLBH_GS25
{
    public class KetNoiCSDL
    {
        public string connectString = @"Data Source= DESKTOP-4QED5F9\NGOCTRANG;Initial Catalog= QLBH_GS25;Integrated Security=True;Persist Security Info= True;User ID=east;Password=1";

        public SqlConnection getConnect()
        {
            SqlConnection conn = new SqlConnection(connectString);
            conn.Open();
            return conn;
        }

        public int ExecuteNonQuery(string query)
        {
            int data = 0;
            using (SqlConnection ketnoi = new SqlConnection(connectString))
            {
                ketnoi.Open();
                SqlCommand thucthi = new SqlCommand(query, ketnoi);
                data = thucthi.ExecuteNonQuery();
                ketnoi.Close();
            }
            return data;
        }
        public DataTable Executequery(string query)
        {
            DataTable data = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectString))
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                SqlDataAdapter laydulieu = new SqlDataAdapter(cmd);
                laydulieu.Fill(data);
                sqlCon.Close();
            }
            return data;
        }
        public DataTable NhanVien()
        {
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from NHANVIEN", getConnect());
            adapter.Fill(data);
            return data;
        }
        public DataTable KhachHang()
        {
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from KhachHang", getConnect());
            adapter.Fill(data);
            return data;
        }
        public DataTable TKDangNhap()
        {
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Select MaNV, ChucVu, ID, MatKhau from TKDangNhap", getConnect());
            adapter.Fill(data);
            return data;
        }
        public DataTable HoaDon()
        {
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from HoaDon", getConnect());
            adapter.Fill(data);
            return data;
        }
        public DataTable HoaDon1()
        {
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Select MaHD, NgayLapHD, PTTT,MaKH,MaNV,MaKM from HoaDon", getConnect());
            adapter.Fill(data);
            return data;
        }
        public DataTable KhuyenMai()
        {
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from KhuyenMai ", getConnect());
            adapter.Fill(data);
            return data;
        }
        public DataTable QLTheTV()
        {
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from TheThanhVien ", getConnect());
            adapter.Fill(data);
            return data;
        }
        public DataTable QLNhapKho()
        {
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from PhieuNhapKho ", getConnect());
            adapter.Fill(data);
            return data;
        }
        public DataTable QLXuatKho()
        {
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from PhieuXuat ", getConnect());
            adapter.Fill(data);
            return data;
        }
        public DataTable DSNCC()
        {
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from NhaCungCap ", getConnect());
            adapter.Fill(data);
            return data;
        }
        public DataTable MatHang()
        {
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Select MaMH, TenMH, MaLMH, DonGia, DVT, SoLuongTon from MatHang", getConnect());
            adapter.Fill(data);
            return data;
        }

    }
}
