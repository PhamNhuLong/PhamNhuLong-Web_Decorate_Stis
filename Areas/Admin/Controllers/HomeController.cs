using doan.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace doan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            StoreContext context = HttpContext.RequestServices.GetService(typeof(doan.Models.StoreContext)) as StoreContext;

            int slKhachHang = context.sl_KhachHang();
            ViewBag.SLKH = slKhachHang;

            int slSanPham = context.sl_SanPham();
            ViewBag.SLSP = slSanPham;

            int slDonHang = context.sl_DonHang();
            ViewBag.SLDH = slDonHang;

            int slNhanVien = context.sl_NhanVien();
            ViewBag.SLNV = slNhanVien;

            var slistSP = new SortedList<string, string>();
            slistSP = context.Top6SPbanchay();
            ViewBag.sListSP = slistSP;

            List<Dondathang> list_DDH = new List<Dondathang>();
            list_DDH = context.get8_Dondathang();
            ViewBag.ListDDH = list_DDH;
            var sl_khachhang = new SortedList<string, int>();
            sl_khachhang = context.GetKH_Loai();
            ViewBag.sListKH = sl_khachhang;

            return View();
        }
    }
}
