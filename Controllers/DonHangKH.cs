using doan.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace doan.Controllers
{
    public class DonHangKH:Controller
    {
        public IActionResult Index_DonHangKH(int madh)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(doan.Models.StoreContext)) as StoreContext;
            List<GioHang> list = new List<GioHang>();
            var ct = context.GetCtdhs(madh);
            foreach(var item in ct)
            {
                list.Add(new GioHang() { 
                    sanpham =context.Product_id(item.MaSp),
                    Soluong = item.SoLuong,
                    hinhanh = context.HinhAnhSP(item.MaSp)[0].LinkHinhAnh
                });
            }
            ViewBag.donDH = list;
            ViewBag.madh = madh;
            ViewBag.ddh = context.GetDonDHbyid(madh);
            return View();
        }
    }
}
