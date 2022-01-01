using System;
using System.Collections.Generic; 

namespace doan.Models
{
    public partial class Sanpham
    {
        public Sanpham()
        {
            Hinhanh = new HashSet<Hinhanh>();
        }

        public int MaSp { get; set; }
        public string TenSp { get; set; }
        public decimal? GiaTien { get; set; }
        public int SoLuong { get; set; }
        public int MaDanhMuc { get; set; }
        public string MoTa { get; set; }
        public int MaNcc { get; set; }

        public virtual Danhmucsp MaDanhMucNavigation { get; set; }
        public virtual Nhacungcap MaNccNavigation { get; set; }
        public virtual ICollection<Hinhanh> Hinhanh { get; set; }
    }
}
