using System; 
using System.Collections.Generic;

namespace doan.Models
{
    public partial class Khachhang
    {
        public Khachhang()
        {
            Dondathang = new HashSet<Dondathang>();
        }

        public int MaKh { get; set; }
        public string TenKh { get; set; }
        public string SoDienThoai { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string DiaChi { get; set; }
        public string LoaiKh { get; set; }

        public virtual ICollection<Dondathang> Dondathang { get; set; }
    }
}
