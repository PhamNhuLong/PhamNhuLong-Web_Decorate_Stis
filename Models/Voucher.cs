 using System;
using System.Collections.Generic;

namespace doan.Models
{
    public partial class Voucher
    {
        public Voucher()
        {
            Dondathang = new HashSet<Dondathang>();
        }

        public int MaVoucher { get; set; }
        public string TenVoucher { get; set; }
        public Double TiLeGiamGia { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }

        public virtual ICollection<Dondathang> Dondathang { get; set; }
    }
}
