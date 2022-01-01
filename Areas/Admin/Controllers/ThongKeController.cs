using doan.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace doan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ThongKeController : Controller
    {
        private readonly StoreContext _context;

        public ThongKeController(StoreContext context)
        {
            _context = context;
        }

        public IActionResult SanPham_DanhMuc()

        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(doan.Models.StoreContext)) as StoreContext;
            var listdmsp = new SortedList<string, int>();
            listdmsp = context.slSanPham_DanhMuc();
            ViewBag.ListDMSP = listdmsp;
            return View();
        }
        public string test()

        {
            return "test";
        }


        public IActionResult DanhThu_Thang(int year)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(doan.Models.StoreContext)) as StoreContext;
            SortedList<string, int> listdtt = _context.DanhSo_Thang();
            ViewBag.ListDTT = listdtt;

            return View();
        }

        public IActionResult Top10DonHang()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(doan.Models.StoreContext)) as StoreContext;
            List<Dondathang> listdh = context.get10_Dondathang();
            ViewBag.ListDH = listdh;
            return View();

        }

        public IActionResult TK_NhaVanChuyen()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(doan.Models.StoreContext)) as StoreContext;
            var listnvc = new SortedList<string, int>();
            listnvc = context.TK_NhaVanChuyen();
            ViewBag.ListNVC = listnvc;
            return View();
        }
        public IActionResult TK_KhachHang()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(doan.Models.StoreContext)) as StoreContext;
            var listkh = new SortedList<string, int>();
            listkh = context.GetKH_Loai();
            ViewBag.ListKH = listkh;
            return View();
        }
        public IActionResult DoanhThu_Ngay()
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(doan.Models.StoreContext)) as StoreContext;
            var listDT = new SortedList<string, int>();
            listDT = context.DanhSo_Ngay();
            ViewBag.ListDT = listDT;
            return View();
        }
    }
}
