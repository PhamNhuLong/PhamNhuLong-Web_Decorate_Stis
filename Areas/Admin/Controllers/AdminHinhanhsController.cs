using System; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using doan.Models;
using PagedList.Core;
using System.IO;
using doan.Helpper;
using Microsoft.AspNetCore.Http;

namespace doan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminHinhanhsController : Controller
    {
        private readonly CuaHangDecorateContext _context;

        public AdminHinhanhsController(CuaHangDecorateContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminHinhanhs
        //public async Task<IActionResult> Index()
        //{
        //    var cuaHangDecorateContext = _context.Hinhanh.Include(h => h.MaSpNavigation);
        //    return View(await cuaHangDecorateContext.ToListAsync());
        //}
        public IActionResult Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 20;
            var IsHinhanhs = _context.Hinhanh
                .AsNoTracking()
                .Include(x => x.MaSpNavigation)
                .OrderBy(x => x.MaHinhAnh);
            PagedList<Hinhanh> models = new PagedList<Hinhanh>(IsHinhanhs, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }

        // GET: Admin/AdminHinhanhs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hinhanh = await _context.Hinhanh
                .Include(h => h.MaSpNavigation)
                .FirstOrDefaultAsync(m => m.MaHinhAnh == id);
            if (hinhanh == null)
            {
                return NotFound();
            }

            return View(hinhanh);
        }

        // GET: Admin/AdminHinhanhs/Create
        public IActionResult Create()
        {
            
            ViewData["MaSp"] = new SelectList(_context.Sanpham, "MaSp", "TenSp");
            return View();
        }

        // POST: Admin/AdminHinhanhs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHinhAnh,LinkHinhAnh,MaSp")] Hinhanh hinhanh, IFormFile myfile)// Microsoft.AspNetCore.Http.IFormFile fLinkHinhAnh)
        {
            if (ModelState.IsValid)
            {
                if (myfile != null)
                {
                    string fullPath = Path.Combine(Directory.GetCurrentDirectory(),
                                        "wwwroot", "Image", myfile.FileName);
                    using (var file = new FileStream(fullPath, FileMode.Create))
                    {
                        myfile.CopyTo(file);
                    }
                }
                  hinhanh.LinkHinhAnh = myfile.FileName;

                //hinhanh.MaSpNavigation.TenSp = Utilities.ToTitleCase(hinhanh.MaSpNavigation.TenSp);
                //if (fLinkHinhAnh != null)
                //{
                //    string extension = Path.GetExtension(fLinkHinhAnh.FileName);
                //    string image = Utilities.SEOUrl(hinhanh.MaSpNavigation.TenSp) + extension;
                //    hinhanh.LinkHinhAnh = await Utilities.UploadFile(fLinkHinhAnh, @"Hinh_anh_san_pham", extension.ToLower());
                //}
                //if (string.IsNullOrEmpty(hinhanh.LinkHinhAnh)) hinhanh.LinkHinhAnh = "default.jpg";
                _context.Add(hinhanh);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaSp"] = new SelectList(_context.Sanpham, "MaSp", "TenSp", hinhanh.MaSp);
            return View(hinhanh);
        }

        // GET: Admin/AdminHinhanhs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hinhanh = await _context.Hinhanh.FindAsync(id);
            if (hinhanh == null)
            {
                return NotFound();
            }
            ViewData["MaSp"] = new SelectList(_context.Sanpham, "MaSp", "TenSp", hinhanh.MaSp);
            return View(hinhanh);
        }

        // POST: Admin/AdminHinhanhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaHinhAnh,LinkHinhAnh,MaSp")] Hinhanh hinhanh)
        {
            if (id != hinhanh.MaHinhAnh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hinhanh);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HinhanhExists(hinhanh.MaHinhAnh))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaSp"] = new SelectList(_context.Sanpham, "MaSp", "TenSp", hinhanh.MaSp);
            return View(hinhanh);
        }

        // GET: Admin/AdminHinhanhs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hinhanh = await _context.Hinhanh
                .Include(h => h.MaSpNavigation)
                .FirstOrDefaultAsync(m => m.MaHinhAnh == id);
            if (hinhanh == null)
            {
                return NotFound();
            }

            return View(hinhanh);
        }

        // POST: Admin/AdminHinhanhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hinhanh = await _context.Hinhanh.FindAsync(id);
            _context.Hinhanh.Remove(hinhanh);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HinhanhExists(int id)
        {
            return _context.Hinhanh.Any(e => e.MaHinhAnh == id);
        }
    }
}
