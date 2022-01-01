using System; 
using System.Collections.Generic;

namespace doan.Models
{
    public partial class Taikhoan
    {
        public int MaTk { get; set; }
        public string SoDienThoai { get; set; }
        public string MatKhau { get; set; }
        public int? RoleId { get; set; }

        public virtual Roles Role { get; set; }
    }
}
