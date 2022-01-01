using System; 
using System.Collections.Generic;

namespace doan.Models
{
    public partial class Nhavanchuyen
    {
        public Nhavanchuyen()
        {
            Dondathang = new HashSet<Dondathang>();
        }

        public int MaNvc { get; set; }
        public string TenNvc { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Dondathang> Dondathang { get; set; }
    }
}
