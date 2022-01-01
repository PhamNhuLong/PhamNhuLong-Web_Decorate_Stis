using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace doan.Models
{
    public class StoreContext
    {
        private readonly IConfiguration configuration;
        public string ConnectionString { get; set; } // Biến thành viên

        public StoreContext(string connectionstring)
        {
            this.ConnectionString = connectionstring;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
        /* -----------------------------
        *  SQL Search 
        *--------------------------------*/
        public List<Sanpham> sqlSearchSP(string tukhoa)
        {
            List<Sanpham> list = new List<Sanpham>();
            string khoa;
            char[] ch = new char[tukhoa.Length];

            // Copy character by character into array 
            for (int i = 0; i < tukhoa.Length; i++)
            {
                ch[i] = tukhoa[i];
            }
            khoa = String.Join("%", ch);
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                string str = @"SELECT * FROM SANPHAM WHERE TENSP LIKE '%" + khoa + "%' or MaDanhMuc in ( select MaDanhMuc from DANHMUCSP where TenDanhMuc like'%" + khoa + "%')";
                SqlCommand cmd = new SqlCommand(str, conn);
                //cmd.Parameters.AddWithValue("tukhoa", tukhoa);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Sanpham()
                        {
                            MaSp = Convert.ToInt32(reader["MaSp"]),
                            TenSp = reader["TenSp"].ToString(),
                            GiaTien = Convert.ToInt32(reader["GiaTien"])
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }
        /* -----------------------------
       *  SQL Them vao gio hang 
       *--------------------------------*/
        public Sanpham Product_id(int id)
        {
            Sanpham cart_item = new Sanpham();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = "select * from sanpham where masp = " + id + "";
                SqlCommand cmd = new SqlCommand(query, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cart_item.MaSp = id;
                        cart_item.TenSp = reader["Tensp"].ToString();
                        cart_item.SoLuong = Convert.ToInt32(reader["Soluong"]);
                        cart_item.GiaTien = Convert.ToInt32(reader["giatien"]);
                    }
                    reader.Close();
                }
                conn.Close();

            }
            return cart_item;
        }

        public List<Hinhanh> HinhAnhSP(int id)
        {
            List<Hinhanh> list = new List<Hinhanh>();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = "select * from HINHANH where masp = " + id + "";
                SqlCommand cmd = new SqlCommand(query, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Hinhanh()
                        {
                            MaHinhAnh = Convert.ToInt32(reader["mahinhanh"]),
                            MaSp = Convert.ToInt32(reader["masp"]),
                            LinkHinhAnh = reader["LinkHinhAnh"].ToString()
                        });
                    }

                    //hinhanh = id.ToString();
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }
        /* -----------------------------
       *  SQL 
       *--------------------------------*/


        /* -----------------------------
       *  SQL Don Dat Hang 
       *--------------------------------*/
        public List<Nhavanchuyen> getNVC()
        {
            List<Nhavanchuyen> list_Nvc = new List<Nhavanchuyen>();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = "select * from nhavanchuyen";
                SqlCommand cmd = new SqlCommand(query, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list_Nvc.Add(new Nhavanchuyen()
                        {
                            MaNvc = Convert.ToInt32(reader["MaNVC"]),
                            TenNvc = reader["TenNVC"].ToString(),
                            DiaChi = reader["diachi"].ToString(),
                            Email = reader["email"].ToString()
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list_Nvc;
        }
        /* -----------------------------
       *  SQL Dat Hang 
       *--------------------------------*/
        public int insert_DDH(int maKH, int tongtien, DateTime ngay, int nvc, string ten, string sdt, string diachi)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "insert into DonDatHang(MAKH,MAVOUCHER,TONGDONHANG,SOTIENGIAM,THANHTIEN,MANV,NGAYDATHANG,MANVC, TENNGUOINHAN,SDTNGUOINHAN,DIACHINHAN, TINHTRANGDONHANG) " +
                    "values(@makh,1,@thanhtien,0, @thanhtien,1, @ngaydat, @nvc, @ten, @sdt, @diachi,0)";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("makh", maKH);
                cmd.Parameters.AddWithValue("thanhtien", tongtien);
                cmd.Parameters.AddWithValue("ngaydat", ngay.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("nvc", nvc);
                cmd.Parameters.AddWithValue("ten", ten);
                cmd.Parameters.AddWithValue("sdt", sdt);
                cmd.Parameters.AddWithValue("diachi", diachi);

                var tmp = 0;
                if (cmd.ExecuteNonQuery() != 0)
                {
                    /* var st = "SELECT * from DONDATHANG " +
                         "where makh=@makh and thanhtien=@thanhtien and ngaydathang=@ngaydat and tennguoinhan=@ten" +
                         " and SDTNGUOINHAN=@sdt and DIACHINHAN=@diachi";
                     SqlCommand cm = new SqlCommand(st, conn);
                     cm.Parameters.AddWithValue("makh", maKH);
                     cm.Parameters.AddWithValue("thanhtien", tongtien);
                     cm.Parameters.AddWithValue("ngaydat", ngay);
                     cm.Parameters.AddWithValue("ten", ten);
                     cm.Parameters.AddWithValue("sdt", sdt);
                     cm.Parameters.AddWithValue("diachi", diachi);*/
                    var st = "select @@IDENTITY as 'Indentity'";
                    SqlCommand cm = new SqlCommand(st, conn);
                    using (var reader = cm.ExecuteReader())
                    {
                        while (reader.Read()) tmp = Convert.ToInt32(reader["Indentity"]);
                    }
                }

                conn.Close();

                return (Convert.ToInt32(tmp));
            }
        }
        public int insert_CTDH(int maDDH, int maSP, int sl, int tien)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "insert into CTDH(MADDH,MASP,SOLUONG,GIATIEN) values(@maddh, @masp, @sl, @tien)";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("maddh", maDDH);
                cmd.Parameters.AddWithValue("masp", maSP);
                cmd.Parameters.AddWithValue("sl", sl);
                cmd.Parameters.AddWithValue("tien", tien);
                var tmp = cmd.ExecuteNonQuery();
                conn.Close();
                return (tmp);
            }
        }
        public int update_SanPham(int maSP, int sl)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "update SANPHAM set SoLuong=@sl where MaSP=@masp";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("masp", maSP);
                cmd.Parameters.AddWithValue("sl", sl);
                var tmp = cmd.ExecuteNonQuery();
                conn.Close();
                return (tmp);
            }
        }

        /* -----------------------------
       *  SQL Dang nhap
       *--------------------------------*/
        public Taikhoan GetTaikhoan(string sdt, string pass)
        {
            Taikhoan tk = new Taikhoan();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = "select * from TaiKhoan where Sodienthoai = @sdt and matkhau=@pass";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("sdt", sdt);
                cmd.Parameters.AddWithValue("pass", pass);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tk.MaTk = Convert.ToInt32(reader["MaTK"]);
                        tk.SoDienThoai = reader["Sodienthoai"].ToString();
                        tk.MatKhau = reader["Matkhau"].ToString();
                        tk.RoleId = Convert.ToInt32(reader["Roleid"]);
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return tk;
        }
        public Roles GetRoles(int? roleid)
        {
            Roles r = new Roles();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = "select * from Roles where RoleID = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("id", roleid);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        r.RoleName = reader["RoleName"].ToString();
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return r;
        }
        public Khachhang GetKhachHang(string sdt)
        {
            Khachhang kh = new Khachhang();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = "select * from KhachHang where Sodienthoai = @sdt";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("sdt", sdt);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        kh.MaKh = Convert.ToInt32(reader["MaKh"]);
                        kh.SoDienThoai = reader["Sodienthoai"].ToString();
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return kh;
        }
        /* -----------------------------
       *  SQL Dang ky
       *--------------------------------*/
        public int insert_KhachHang(string tenKH, string sdt, DateTime ngay, string gt, string diachi)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "insert into KhachHang(TENKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) " +
                    "values(@tenkh, @sdt, @ngaysinh, @gt, @diachi,'Dong')";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("tenkh", tenKH);
                cmd.Parameters.AddWithValue("sdt", sdt);
                cmd.Parameters.AddWithValue("ngaysinh", ngay.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("gt", gt);
                cmd.Parameters.AddWithValue("diachi", diachi);
                var tmp = cmd.ExecuteNonQuery();

                conn.Close();

                return (Convert.ToInt32(tmp));
            }
        }
        public int insert_TaiKhoan(string sdt, string pass)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "insert into TaiKhoan(SoDienThoai,MatKhau,roleid) " +
                    "values(@sdt, @pass,2)";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("sdt", sdt);
                cmd.Parameters.AddWithValue("pass", pass);
                /*var tmp = cmd.ExecuteNonQuery();*/
                var tmp = 0;
                if (cmd.ExecuteNonQuery() != 0)
                {
                    var st = "select @@IDENTITY as 'Indentity'";
                    SqlCommand cm = new SqlCommand(st, conn);
                    using (var reader = cm.ExecuteReader())
                    {
                        while (reader.Read()) tmp = Convert.ToInt32(reader["Indentity"]);
                    }
                }

                conn.Close();

                return (Convert.ToInt32(tmp));
            }
        }
        /* -----------------------------
      *  SQL Tai Khoan KH
      *--------------------------------*/
        public Khachhang GetKhachHangbyid(int id)
        {
            Khachhang kh = new Khachhang();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = "select * from KhachHang where MaKh = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        kh.MaKh = Convert.ToInt32(reader["MaKh"]);
                        kh.TenKh = reader["TenKh"].ToString();
                        kh.DiaChi = reader["diachi"].ToString();
                        kh.GioiTinh = reader["gioitinh"].ToString();
                        kh.SoDienThoai = reader["Sodienthoai"].ToString();
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return kh;
        }
        public List<Dondathang> GetDonHangbyidKH(int id)
        {
            List<Dondathang> ddh = new List<Dondathang>();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = "select * from Dondathang where MaKh = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tt = (reader["tinhtrangdonhang"] != DBNull.Value) ? Convert.ToInt32(reader["tinhtrangdonhang"]) : 1;
                        ddh.Add(new Dondathang()
                        {
                            MaDdh = Convert.ToInt32(reader["maddh"]),
                            NgayDatHang = Convert.ToDateTime(reader["ngaydathang"]),
                            ThanhTien = Convert.ToInt32(reader["thanhtien"]),
                            TinhTrangDonHang = tt
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return ddh;
        }
        public Taikhoan GetTaikhoanbyid(int id)
        {
            Taikhoan tk = new Taikhoan();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = "select * from TaiKhoan where matk = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tk.MaTk = Convert.ToInt32(reader["MaTK"]);
                        tk.SoDienThoai = reader["Sodienthoai"].ToString();
                        tk.MatKhau = reader["Matkhau"].ToString();
                        tk.RoleId = Convert.ToInt32(reader["Roleid"]);
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return tk;
        }

        public int DoiPass(int matk, string pass)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = "update TaiKhoan set matkhau=@pass" +
                    " where matk=@matk";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("matk", matk);
                cmd.Parameters.AddWithValue("pass", pass);
                var tmp = cmd.ExecuteNonQuery();
                conn.Close();
                return (Convert.ToInt32(tmp));
            }
        }

        public List<Ctdh> GetCtdhs(int id)
        {
            List<Ctdh> ctdh = new List<Ctdh>();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = "select * from CTDH where MaDDH = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ctdh.Add(new Ctdh()
                        {
                            MaDdh = Convert.ToInt32(reader["maddh"]),
                            MaSp = Convert.ToInt32(reader["masp"]),
                            SoLuong = Convert.ToInt32(reader["soluong"]),
                            GiaTien = Convert.ToInt32(reader["giatien"])
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return ctdh;
        }
        public Dondathang GetDonDHbyid(int id)
        {
            Dondathang ddh = new Dondathang();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = "select * from Dondathang where maddh = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ddh.MaDdh = Convert.ToInt32(reader["MaDDH"]);
                        ddh.TenNguoiNhan = reader["TenNguoiNhan"].ToString();
                        ddh.SDTNguoiNhan = reader["SDTNguoiNhan"].ToString();
                        ddh.DiaChiNhan = reader["DiaChiNhan"].ToString();
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return ddh;
        }
        //lấy danh số  tháng hiện tại
        public int CurMonth_DanhSo()
        {
            int ds = 0;
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                var str = "select sum(ThanhTien) as DanhSo from DonDatHang where year(NgayDatHang)=year(getdate()) and month(ngaydathang)=month(getdate())";
                SqlCommand cmd = new SqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    ds = Convert.ToInt32(reader["DanhSo"]);
                    reader.Close();
                }
                conn.Close();
                return ds;

            }
        }

        //Lấy doanh thu theo ngay


        public SortedList<string, int> DanhSo_Ngay()
        {
            SortedList<string, int> list = new SortedList<string, int>();
            string key = "";
            int va = 0;
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = @"select  NgayDatHang, sum(ThanhTien) as doanhso
                            from DONDATHANG

                            Group by NgayDatHang";
                SqlCommand cmd = new SqlCommand(str, conn);


                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        key = reader["NgayDatHang"].ToString();
                        va = Convert.ToInt32(reader["doanhso"]);
                        list.Add(key, va);
                    }
                    reader.Close();
                }
                conn.Close();

            }

            return list;
        }
        //Thống kê doanh số theo tháng
        public SortedList<string, int> DanhSo_Thang()
        {
            SortedList<string, int> list = new SortedList<string, int>();
            string key = "";
            int va = 0;
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = @"select concat(month(NgayDatHang),'/',year(ngaydathang)) as Thang, sum(ThanhTien) as doanhso
                            from DONDATHANG
                            Group by month(NgayDatHang) ,year(NgayDatHang)";
                SqlCommand cmd = new SqlCommand(str, conn);


                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        key = reader["Thang"].ToString();
                        va = Convert.ToInt32(reader["doanhso"]);
                        list.Add(key, va);

                    }
                    reader.Close();
                }
                conn.Close();

            }

            return list;
        }
        //lấy tổng số lượng sản phẩm trong từng danh mục
        public SortedList<string, int> slSanPham_DanhMuc()
        {
            using (SqlConnection conn = GetConnection())
            {
                var listdmsp = new SortedList<string, int>();
                conn.Open();
                string key;
                int value;
                var str = @"select dm.MaDanhMuc, TenDanhMuc, COUNT(*) as slsp
                            from DANHMUCSP dm inner
                            join SANPHAM sp on dm.MaDanhMuc = sp.MaDanhMuc
                            group by dm.MaDanhMuc, TenDanhMuc order by slsp desc";
                SqlCommand cmd = new SqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        key = reader["TenDanhMuc"].ToString();
                        value = Convert.ToInt32(reader["slsp"]);
                        listdmsp.Add(key, value);
                    }
                    reader.Close();
                }
                conn.Close();
                return listdmsp;

            }
        }

        //lấy 8 đơn đặt hàng gần nhất

        public List<Dondathang> get8_Dondathang()
        {
            List<Dondathang> list_Ddh = new List<Dondathang>();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = "select top 8 * from DONDATHANG order by ngaydathang desc";
                SqlCommand cmd = new SqlCommand(query, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list_Ddh.Add(new Dondathang()
                        {
                            MaDdh = Convert.ToInt32(reader["MaDDH"]),
                            MaKh = Convert.ToInt32(reader["MaKh"]),
                            MaVoucher = Convert.ToInt32(reader["MaVoucher"]),
                            TongDonHang = Convert.ToDecimal(reader["TongDonHang"]),
                            SoTienGiam = Convert.ToDecimal(reader["SoTienGiam"]),
                            ThanhTien = Convert.ToDecimal(reader["ThanhTien"]),
                            MaNv = Convert.ToInt32(reader["MaNV"]),
                            NgayDatHang = Convert.ToDateTime(reader["NgayDatHang"]),
                            MaNvc = Convert.ToInt32(reader["MaNvc"])
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list_Ddh;
        }

        //Số lượng các loai KH
        public SortedList<string, int> GetKH_Loai()
        {
            var sList = new SortedList<string, int>();
            string key;
            int value;
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = @"select LoaiKH, count(*) as sl
	                            from KHACHHANG
	                            group by LoaiKH order by sl desc";
                SqlCommand cmd = new SqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        key = reader["LoaiKH"].ToString();
                        value = Convert.ToInt32(reader["sl"]);
                        sList.Add(key, value);
                    }
                    reader.Close();
                }
                conn.Close();

            }

            return sList;
        }
        //lấy 10 đơn đặt hàng có giá trị cao nhất
        public List<Dondathang> get10_Dondathang()
        {
            List<Dondathang> list_Ddh = new List<Dondathang>();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = "select top 10 * from DONDATHANG order by thanhtien desc";
                SqlCommand cmd = new SqlCommand(query, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list_Ddh.Add(new Dondathang()
                        {
                            MaDdh = Convert.ToInt32(reader["MaDDH"]),
                            MaKh = Convert.ToInt32(reader["MaKh"]),
                            MaVoucher = Convert.ToInt32(reader["MaVoucher"]),
                            TongDonHang = Convert.ToDecimal(reader["TongDonHang"]),
                            SoTienGiam = Convert.ToDecimal(reader["SoTienGiam"]),
                            ThanhTien = Convert.ToDecimal(reader["ThanhTien"]),
                            MaNv = Convert.ToInt32(reader["MaNV"]),
                            NgayDatHang = Convert.ToDateTime(reader["NgayDatHang"]),
                            MaNvc = Convert.ToInt32(reader["MaNvc"])
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list_Ddh;
        }

        //thống kê nhà vận chuyển
        public SortedList<string, int> TK_NhaVanChuyen()
        {
            var sList = new SortedList<string, int>();
            string key;
            int value;
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = @"select n.manvc,TenNVC, count(*) as sl
                            from NHAVANCHUYEN n inner join DONDATHANG d on n.manvc = d.manvc
                            group by n.MaNVC, tenNVC
                            order by sl desc";
                SqlCommand cmd = new SqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        key = reader["TenNVC"].ToString();
                        value = Convert.ToInt32(reader["sl"]);
                        sList.Add(key, value);
                    }
                    reader.Close();
                }
                conn.Close();

            }

            return sList;
        }

        //đếm tổng số lượng khách hàng
        public int sl_KhachHang()
        {
            int sl = 0;
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = @"select count(*) as sl from KhachHang";
                SqlCommand cmd = new SqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sl = Convert.ToInt32(reader["sl"]);
                    }
                    reader.Close();
                }
                conn.Close();

            }
            return sl;
        }

        //đếm số lượng danh mục
        public int sl_DonHang()
        {
            int sl = 0;
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = @"select count(*) as sl from DonDatHang";
                SqlCommand cmd = new SqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sl = Convert.ToInt32(reader["sl"]);
                    }
                    reader.Close();
                }
                conn.Close();

            }
            return sl;
        }

        //Đêm số lượng sản phẩm
        public int sl_SanPham()
        {
            int sl = 0;
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = @"select count(*) as sl from SanPham";
                SqlCommand cmd = new SqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sl = Convert.ToInt32(reader["sl"]);
                    }
                    reader.Close();
                }
                conn.Close();

            }
            return sl;
        }

        //Đếm số lượng Nhan Viên
        public int sl_NhanVien()
        {
            int sl = 0;
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var str = @"select count(*) as sl from NhanVien";
                SqlCommand cmd = new SqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sl = Convert.ToInt32(reader["sl"]);
                    }
                    reader.Close();
                }
                conn.Close();

            }
            return sl;
        }

        //top sản phảm bán chạy
        public SortedList<string, string> Top6SPbanchay()
        {
            using (SqlConnection conn = GetConnection())
            {
                var listdmsp = new SortedList<string, string>();
                conn.Open();
                string key;
                string value;
                var str = @"select top 3  s.masp, TenSP, Tendanhmuc, sum(c.soluong) as sl
                            from SANPHAM s, danhmucsp d, DONDATHANG h, ctdh c
                            where s.madanhmuc=d.madanhmuc and h.maddh = c.maddh and s.MaSP = c.MaSP
                            group by s.MaSP, TenSP, TenDanhMuc
                            order by sl desc";
                SqlCommand cmd = new SqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        key = reader["TenSP"].ToString();
                        value = reader["TenDanhMuc"].ToString();
                        listdmsp.Add(key, value);
                    }
                    reader.Close();
                }
                conn.Close();
                return listdmsp;

            }
        }
    }
}
