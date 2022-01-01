using System; 
using System.Collections.Generic;

namespace doan.Models
{
    public partial class Roles
    {
        public Roles()
        {
            Taikhoan = new HashSet<Taikhoan>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string MoTa { get; set; }

        public virtual ICollection<Taikhoan> Taikhoan { get; set; }
    }
}
