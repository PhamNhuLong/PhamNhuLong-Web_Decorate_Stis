using System; 
using System.Collections.Generic;

namespace doan.Models
{
    public partial class Hinhanh
    {
        public int MaHinhAnh { get; set; }
        public string LinkHinhAnh { get; set; }
        public int? MaSp { get; set; }

        public virtual Sanpham MaSpNavigation { get; set; }
    }
}
