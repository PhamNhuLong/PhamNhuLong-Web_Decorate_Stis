using System; 
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace doan.Models
{
    public partial class Ctdh
    {
        
        public int MaDdh { get; set; }
        
        public int MaSp { get; set; }
        public int SoLuong { get; set; }
        public decimal? GiaTien { get; set; }

        public virtual Dondathang MaDdhNavigation { get; set; }
        public virtual Sanpham MaSpNavigation { get; set; }
    }
}
