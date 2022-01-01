using System; 
using System.Collections.Generic;

namespace doan.Models
{
    public partial class Dondathang
    {
        public int MaDdh { get; set; }
        public int MaKh { get; set; }
        public int? MaVoucher { get; set; }
        public decimal? TongDonHang { get; set; }
        public decimal? SoTienGiam { get; set; }
        public decimal? ThanhTien { get; set; }
        public int? MaNv { get; set; }
        public DateTime? NgayDatHang { get; set; }
        public int? MaNvc { get; set; }
        public string? TenNguoiNhan { get; set; }
        public string? SDTNguoiNhan { get; set; }
        public string? DiaChiNhan { get; set; }

        public int TinhTrangDonHang { get; set; }

        public virtual Khachhang MaKhNavigation { get; set; }
        public virtual Nhanvien MaNvNavigation { get; set; }
        public virtual Nhavanchuyen MaNvcNavigation { get; set; }
        public virtual Voucher MaVoucherNavigation { get; set; }
    }
}
