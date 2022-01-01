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
    public class LoginController:Controller
    {
        private readonly StoreContext _context;
        public INotyfService _notyfyService { get; set; }
        public LoginController(StoreContext context, INotyfService notifyService)
        {
            _context = context;
            _notyfyService = notifyService;
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            var makh = HttpContext.Session.GetString("KhachHang");
            if (makh != null)
            {
                _notyfyService.Warning("Bạn đã đăng nhập. Hãy đăng xuất trước khi thực hiện nếu muốn.");
                return Redirect("/Home/Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            var makh = HttpContext.Session.GetString("KhachHang");
            if (makh != null)
            {
                _notyfyService.Warning("Bạn đã đăng nhập. Hãy đăng xuất trước khi thực hiện nếu muốn.");
                return Redirect("/Home/Index");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Dangnhap(string sdt, string pass)
        {
            var makh = HttpContext.Session.GetString("KhachHang");
            StoreContext context = HttpContext.RequestServices.GetService(typeof(doan.Models.StoreContext)) as StoreContext;
            Taikhoan tk = context.GetTaikhoan(sdt, pass);
            if (tk.MaTk != 0)
            {
                Roles role = context.GetRoles(tk.RoleId);
                if (role.RoleName.Equals("Khách hàng"))
                {
                    Khachhang kh = context.GetKhachHang(tk.SoDienThoai);
                    HttpContext.Session.SetString("KhachHang", kh.MaKh.ToString());
                    HttpContext.Session.SetString("TaiKhoan", tk.MaTk.ToString());
                }
                else
                {
                    return Redirect("/Admin");
                }
                _notyfyService.Success("Đăng nhập thành công.");
                return Redirect("/Home/Index");
            }
            else
            {
                _notyfyService.Error("Đăng nhập không thành công. Kiểm tra thông tin đăng nhập.");
                
                return RedirectToAction(nameof(SignIn));
            }            
        }
        [HttpPost]
        public IActionResult Dangky(string tenKH, string sdt, DateTime ngay, string gt, string diachi, string pass, string confirmpass)
        {
            if ((tenKH.Length > 50 || diachi.Length > 50) || (sdt.Length > 10 || pass.Length > 20))
            {
                _notyfyService.Error("Một số lỗi đã xảy ra. Vui lòng đăng ký lại.");
                return Redirect("/Login/SignUp");
            }
            if (pass.Equals(confirmpass))
            {


                StoreContext context = HttpContext.RequestServices.GetService(typeof(doan.Models.StoreContext)) as StoreContext;
                Khachhang kh = context.GetKhachHang(sdt);
                if (kh.MaKh != 0)
                {
                    _notyfyService.Error("Số điện thoại đã đăng ký. Vui lòng dùng số khác.");
                    return Redirect("/Login/SignUp");
                }
                else
                {
                    int check = context.insert_KhachHang(tenKH, sdt, ngay, gt, diachi);
                    if (check != 0)
                    {
                        int tmp = context.insert_TaiKhoan(sdt, pass);
                        if (tmp != 0)
                        {
                            Khachhang k = context.GetKhachHang(sdt);
                            HttpContext.Session.SetString("TaiKhoan", tmp.ToString());
                            HttpContext.Session.SetString("KhachHang", k.MaKh.ToString());
                            _notyfyService.Success("Đăng ký thành công.");
                            return Redirect("/Home/Index");
                        }
                    }
                }
                _notyfyService.Error("Đăng nhập không thành công. Kiểm tra thông tin đăng nhập.");
                return Redirect("/Login/SignUp");
            }
            else
            {
                _notyfyService.Error("Xác nhận lại mật khẩu sai.");
                return Redirect("/Login/SignUp");
            }
        }
        public IActionResult Signout()
        {
            var makh = HttpContext.Session.GetString("KhachHang");
            if (makh == null)
            {
                _notyfyService.Warning("Bạn chưa đăng nhập.");
                return Redirect("/Home/Index");
            }
            HttpContext.Session.Remove("KhachHang");
            _notyfyService.Success("Đăng xuất thành công.");
            return Redirect("/Home/Index");
        }
    }
}
