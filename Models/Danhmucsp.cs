using System; 
using System.Collections.Generic;

namespace doan.Models
{
    public partial class Danhmucsp
    {
        public Danhmucsp()
        {
            Sanpham = new HashSet<Sanpham>();
        }

        public int MaDanhMuc { get; set; }
        public string TenDanhMuc { get; set; }
        public string MoTa { get; set; }

        public virtual ICollection<Sanpham> Sanpham { get; set; }
    }
}
