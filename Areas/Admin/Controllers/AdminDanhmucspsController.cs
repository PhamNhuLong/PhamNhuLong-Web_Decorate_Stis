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
    public class AdminDanhmucspsController : Controller
    {
        private readonly CuaHangDecorateContext _context;

        public AdminDanhmucspsController(CuaHangDecorateContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminDanhmucsps
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Danhmucsp.ToListAsync());
        //}
        public IActionResult Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 20;
            var IsDanhmucsanphams = _context.Danhmucsp.AsNoTracking()
                .OrderBy(x => x.MaDanhMuc);
            PagedList<Danhmucsp> models = new PagedList<Danhmucsp>(IsDanhmucsanphams, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }

        // GET: Admin/AdminDanhmucsps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhmucsp = await _context.Danhmucsp
                .FirstOrDefaultAsync(m => m.MaDanhMuc == id);
            if (danhmucsp == null)
            {
                return NotFound();
            }

            return View(danhmucsp);
        }

        // GET: Admin/AdminDanhmucsps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminDanhmucsps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaDanhMuc,TenDanhMuc,MoTa")] Danhmucsp danhmucsp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(danhmucsp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(danhmucsp);
        }

        // GET: Admin/AdminDanhmucsps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhmucsp = await _context.Danhmucsp.FindAsync(id);
            if (danhmucsp == null)
            {
                return NotFound();
            }
            return View(danhmucsp);
        }

        // POST: Admin/AdminDanhmucsps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaDanhMuc,TenDanhMuc,MoTa")] Danhmucsp danhmucsp)
        {
            if (id != danhmucsp.MaDanhMuc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(danhmucsp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DanhmucspExists(danhmucsp.MaDanhMuc))
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
            return View(danhmucsp);
        }

        // GET: Admin/AdminDanhmucsps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhmucsp = await _context.Danhmucsp
                .FirstOrDefaultAsync(m => m.MaDanhMuc == id);
            if (danhmucsp == null)
            {
                return NotFound();
            }

            return View(danhmucsp);
        }

        // POST: Admin/AdminDanhmucsps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var danhmucsp = await _context.Danhmucsp.FindAsync(id);
            _context.Danhmucsp.Remove(danhmucsp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DanhmucspExists(int id)
        {
            return _context.Danhmucsp.Any(e => e.MaDanhMuc == id);
        }
    }
}
