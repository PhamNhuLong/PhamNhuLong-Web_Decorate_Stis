//using doan.Models;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using doan.Models;
using PagedList.Core;

namespace doan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SearchController : Controller
    {
        private readonly CuaHangDecorateContext _context;

        public SearchController(CuaHangDecorateContext context)
        {
            _context = context;
        }
        

        //search sarn phaarm


        [HttpPost]
        public IActionResult SearchSanpham(string keyword)
        {
            List<Sanpham> ls = new List<Sanpham>();
            if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
            {
                return PartialView("ListProductsSearchPartial", null);
            }
            ls = _context.Sanpham
                .AsNoTracking()
                .Include(a => a.MaDanhMucNavigation)
                .Include(a => a.MaNccNavigation)

                .Where(x => x.TenSp.Contains(keyword))
                .OrderBy(x => x.TenSp)
                .Take(10)
                .ToList();
            if (ls == null)
            {
                return PartialView("ListProductsSearchPartial", null);

            }
            else
            {
                return PartialView("ListProductsSearchPartial", ls);

            }
        }

    }
}
