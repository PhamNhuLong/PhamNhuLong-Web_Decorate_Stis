using AspNetCoreHero.ToastNotification.Abstractions;
using doan.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace doan.Controllers
{
    public class TaiKhoanKHController: Controller
    {
        private readonly StoreContext _context;
        public INotyfService _notyfyService { get; set; }
        public TaiKhoanKHController(StoreContext context, INotyfService notifyService)
        {
            _context = context;
            _notyfyService = notifyService;
        }
        [HttpGet]
        public IActionResult Index_TaiKhoan()
        {
            var makh = HttpContext.Session.GetString("KhachHang");
            if (makh == null)
            {
                _notyfyService.Warning("Bạn chưa đăng nhập.");
                return Redirect("/Home/Index");
            }
            else
            {
                StoreContext context = HttpContext.RequestServices.GetService(typeof(doan.Models.StoreContext)) as StoreContext;
                ViewBag.Khachhang = context.GetKhachHangbyid(Convert.ToInt32(makh));
                ViewBag.Dondathang = context.GetDonHangbyidKH(Convert.ToInt32(makh));
                return View();
            }            
        }
        [HttpPost]        
        public IActionResult DoiPass(string pass, string passwordtk, string confirmpasswordtk)
        {
            StoreContext context = HttpContext.RequestServices.GetService(typeof(doan.Models.StoreContext)) as StoreContext;
            var tk = HttpContext.Session.GetString("TaiKhoan");
            int matk = Convert.ToInt32(tk);
            if (pass.Equals(context.GetTaikhoanbyid(matk).MatKhau))
            {
                if (passwordtk.Equals(confirmpasswordtk))
                {
                    if (passwordtk.Length>20)
                    {
                        _notyfyService.Error("Đã có lỗi xảy ra.");
                        return Redirect("/TaiKhoanKH/Index_TaiKhoan");
                    }
                    var tmp = context.DoiPass(matk, passwordtk);
                    if (tmp != 0)
                    {
                        _notyfyService.Success("Đổi mật khẩu thành công.");
                    }
                    else
                    {
                        _notyfyService.Error("Đổi mật khẩu thất bại.");
                    }
                    /*return Redirect("/TaiKhoanKH/Index_TaiKhoan");*/
                }
                else
                {
                    _notyfyService.Error("Xác nhận lại mật khẩu sai.");
                    /*return Redirect("/TaiKhoanKH/Index_TaiKhoan");*/
                }
            }
            else
            {
                _notyfyService.Error("Mật khẩu hiện tại sai.");
                /*return Redirect("/TaiKhoanKH/Index_TaiKhoan");*/
            }
            return Redirect("/TaiKhoanKH/Index_TaiKhoan");
        }
    }
}
