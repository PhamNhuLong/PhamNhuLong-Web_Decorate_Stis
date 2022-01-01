using AspNetCoreHero.ToastNotification.Abstractions;
using doan.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace doan.Controllers
{
    public class DonDatHangController:Controller
    {
        private readonly StoreContext _context;
        public INotyfService _notyfyService { get; set; }
        public DonDatHangController(StoreContext context, INotyfService notifyService)
        {
            _context = context;
            _notyfyService = notifyService;
        }
        [HttpPost]
        public IActionResult Index_DDH()
        {
            var makh = HttpContext.Session.GetString("KhachHang");
            //makh = "1";
            if (makh != null)
            {
                StoreContext context = HttpContext.RequestServices.GetService(typeof(doan.Models.StoreContext)) as StoreContext;
                if (Request.Cookies["GioHang"] != null)
                {
                    var cart = Request.Cookies["GioHang"];
                    List<GioHang> dataCart = JsonConvert.DeserializeObject<List<GioHang>>(cart);
                    if (dataCart.Count > 0)
                    {
                        ViewBag.carts = dataCart;
                    }
                }
                ViewBag.nhavanchuyen = context.getNVC();
                return View();
            }
            else
            {
                //------------------------------------
                // Nho sua thanh link Login
                return Redirect("/Login/SignIn");
            }
                
        }
        [HttpPost]
        public IActionResult DatHang(string ten, string sdt, string diachi, int nvc)
        {
            if ((ten.Length > 50 || diachi.Length > 50) || (sdt.Length > 10 ))
            {
                _notyfyService.Error("Một số lỗi đã xảy ra. Có vẻ bạn đã nhập sai thông tin giao hàng.");
                return Redirect("/Giohang/Index_GioHang");
            }
            StoreContext context = HttpContext.RequestServices.GetService(typeof(doan.Models.StoreContext)) as StoreContext;
            if (Request.Cookies["GioHang"] != null)
            {
                var cart = Request.Cookies["GioHang"];
                List<GioHang> dataCart = JsonConvert.DeserializeObject<List<GioHang>>(cart);
                if (dataCart.Count > 0)
                {
                    ViewBag.carts = dataCart;
                }

                var tongtien = 0;
                /*string ngaydh = DateTime.Now.ToString("DD/MM/YYYY");*/
                DateTime ngaydh = DateTime.Now;
                foreach (var item in dataCart)
                {
                    tongtien += Convert.ToInt32(item.Soluong * item.sanpham.GiaTien);
                }
                var kh = HttpContext.Session.GetString("KhachHang");
                if (kh != null)
                {
                    int maKH = Convert.ToInt32(kh);
                    int maddh = context.insert_DDH(maKH, tongtien, ngaydh,nvc, ten, sdt, diachi);
                    if (maddh != 0)
                    {
                        foreach (var item in dataCart)
                        {
                            var sump = Convert.ToInt32(item.Soluong * item.sanpham.GiaTien);
                            var tmp = context.insert_CTDH(maddh, item.sanpham.MaSp, item.Soluong, sump);
                            if (tmp == 0)
                            {
                                _notyfyService.Error("Đã có lỗi xảy ra.");
                                return Redirect("/Error404/Page404");
                            }
                            var dataSPnew = context.Product_id(item.sanpham.MaSp);
                            var tmp1 = context.update_SanPham(item.sanpham.MaSp, dataSPnew.SoLuong - item.Soluong);
                        }
                        ViewData["MADDH"] = maddh;
                        Response.Cookies.Delete("GioHang");
                    }
                    else
                    {
                        _notyfyService.Error("Đã có lỗi xảy ra.");
                        return Redirect("/Error404/Page404");
                    }
                }
                return View();
            }
            else
            {
                _notyfyService.Error("Đã có lỗi xảy ra.");
                return Redirect("/Error404/Page404");
            }
        } 
    }
}
