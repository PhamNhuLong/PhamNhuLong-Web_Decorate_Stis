using System; 
using System.Collections.Generic;

namespace doan.Models
{
    public partial class Nhacungcap
    {
        public Nhacungcap()
        {
            Sanpham = new HashSet<Sanpham>();
        }

        public int MaNcc { get; set; }
        public string TenNcc { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Sanpham> Sanpham { get; set; }
    }
}
