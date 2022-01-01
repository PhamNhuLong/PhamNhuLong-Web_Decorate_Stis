using doan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace doan.Controllers
{
    public class SanphamController : Controller
    {
        private readonly CuaHangDecorateContext _context;
        public SanphamController(CuaHangDecorateContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? page)
        {
            try
            {
                var pageNumber = page == null || page <= 0 ? 1 : page.Value;
                var pageSize = 21;
                var IsSanphams = _context.Sanpham.AsNoTracking()
                    .OrderBy(x => x.MaSp);
                PagedList<Sanpham> models = new PagedList<Sanpham>(IsSanphams, pageNumber, pageSize);
                ViewBag.CurrentPage = pageNumber;
                return View(models);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }


        }
        [Route("/{id}/{tendanhmuc}.html")]

        public IActionResult List(int id, int page = 1)
        {
            try
            {
                var pageSize = 9;
                var danhmuc = _context.Danhmucsp.Find(id);
                var IsSanphams = _context.Sanpham.AsNoTracking()
                    .Where(x => x.MaDanhMuc == id)
                    .OrderBy(x => x.MaSp);
                PagedList<Sanpham> models = new PagedList<Sanpham>(IsSanphams, page, pageSize);
                ViewBag.CurrentPage = page;
                ViewBag.CurrentCat = danhmuc;
                return View(models);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }


        }


        //[Route ("/{id}.html"), Name = "ProductDetails")]
        [Route("/{id}.html")]

        public IActionResult Details(int id)
        {
            try
            {
                var sanpham = _context.Sanpham.Include(x => x.MaDanhMucNavigation).FirstOrDefault(x => x.MaSp == id);
                if (sanpham == null)
                {
                    return RedirectToAction("Index");

                }


                var lsProduct = _context.Sanpham.AsNoTracking()
                    .Where(x => x.MaDanhMuc == sanpham.MaDanhMuc && x.MaSp != id)
                    .OrderBy(x => x.MaSp)
                    .Take(4)
                    .ToList();

                ViewBag.Sanpham = lsProduct;
                return View(sanpham);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

        }



    }
}
