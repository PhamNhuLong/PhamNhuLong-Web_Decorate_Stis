using doan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace doan.Controllers
{
    public class SearchtheogiaController : Controller
    {
        private readonly CuaHangDecorateContext _context;

        public SearchtheogiaController(CuaHangDecorateContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Searchgia(string giasanpham)
        {
            List<Sanpham> ls = new List<Sanpham>();
            if (string.IsNullOrEmpty(giasanpham) || giasanpham.Length < 1)
            {
                return PartialView("ListTheoGia", null);
            }
            ls = _context.Sanpham
                .AsNoTracking()
                .Include(a => a.MaDanhMucNavigation)
                .Include(a => a.MaNccNavigation)

                //.Where(x => x.TenSp.Contains(keyword))
                .Where(x => x.TenSp.Contains(giasanpham))

                .OrderBy(x => x.TenSp)
                .Take(10)
                .ToList();
            if (ls == null)
            {
                return PartialView("ListTheoGia", null);

            }
            else
            {
                return PartialView("ListTheoGia", ls);

            }
        }
        
    }
}
