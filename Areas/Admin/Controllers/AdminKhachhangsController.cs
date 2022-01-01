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
    public class AdminKhachhangsController : Controller
    {
        private readonly CuaHangDecorateContext _context;

        public AdminKhachhangsController(CuaHangDecorateContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminKhachhangs
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Khachhang.ToListAsync());
        //}
        public IActionResult Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 20;
            var IsKhachhangs = _context.Khachhang.AsNoTracking()
                .OrderBy(x => x.MaKh);
            PagedList<Khachhang> models = new PagedList<Khachhang>(IsKhachhangs, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }

        // GET: Admin/AdminKhachhangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachhang = await _context.Khachhang
                .FirstOrDefaultAsync(m => m.MaKh == id);
            if (khachhang == null)
            {
                return NotFound();
            }

            return View(khachhang);
        }

        // GET: Admin/AdminKhachhangs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminKhachhangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaKh,TenKh,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKh")] Khachhang khachhang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(khachhang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(khachhang);
        }

        // GET: Admin/AdminKhachhangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachhang = await _context.Khachhang.FindAsync(id);
            if (khachhang == null)
            {
                return NotFound();
            }
            return View(khachhang);
        }

        // POST: Admin/AdminKhachhangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaKh,TenKh,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKh")] Khachhang khachhang)
        {
            if (id != khachhang.MaKh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(khachhang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhachhangExists(khachhang.MaKh))
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
            return View(khachhang);
        }

        // GET: Admin/AdminKhachhangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachhang = await _context.Khachhang
                .FirstOrDefaultAsync(m => m.MaKh == id);
            if (khachhang == null)
            {
                return NotFound();
            }

            return View(khachhang);
        }

        // POST: Admin/AdminKhachhangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var khachhang = await _context.Khachhang.FindAsync(id);
            _context.Khachhang.Remove(khachhang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KhachhangExists(int id)
        {
            return _context.Khachhang.Any(e => e.MaKh == id);
        }
    }
}
