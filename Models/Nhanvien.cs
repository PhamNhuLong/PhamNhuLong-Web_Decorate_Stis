using System; 
using System.Collections.Generic;

namespace doan.Models
{
    public partial class Nhanvien
    {
        public Nhanvien()
        {
            Dondathang = new HashSet<Dondathang>();
        }

        public int MaNv { get; set; }
        public string TenNv { get; set; }
        public string SoDienThoai { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string DiaChi { get; set; }
        public DateTime NgayVaoLam { get; set; }
        public string ChucVu { get; set; }

        public virtual ICollection<Dondathang> Dondathang { get; set; }
    }
}
