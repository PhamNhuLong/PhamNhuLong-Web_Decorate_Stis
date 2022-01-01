using System; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using doan.Models;

namespace doan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminNhacungcapsController : Controller
    {
        private readonly CuaHangDecorateContext _context;

        public AdminNhacungcapsController(CuaHangDecorateContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminNhacungcaps
        public async Task<IActionResult> Index()
        {
            return View(await _context.Nhacungcap.ToListAsync());
        }

        // GET: Admin/AdminNhacungcaps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhacungcap = await _context.Nhacungcap
                .FirstOrDefaultAsync(m => m.MaNcc == id);
            if (nhacungcap == null)
            {
                return NotFound();
            }

            return View(nhacungcap);
        }

        // GET: Admin/AdminNhacungcaps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminNhacungcaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNcc,TenNcc,DiaChi,Email")] Nhacungcap nhacungcap)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nhacungcap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nhacungcap);
        }

        // GET: Admin/AdminNhacungcaps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhacungcap = await _context.Nhacungcap.FindAsync(id);
            if (nhacungcap == null)
            {
                return NotFound();
            }
            return View(nhacungcap);
        }

        // POST: Admin/AdminNhacungcaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaNcc,TenNcc,DiaChi,Email")] Nhacungcap nhacungcap)
        {
            if (id != nhacungcap.MaNcc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhacungcap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhacungcapExists(nhacungcap.MaNcc))
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
            return View(nhacungcap);
        }

        // GET: Admin/AdminNhacungcaps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhacungcap = await _context.Nhacungcap
                .FirstOrDefaultAsync(m => m.MaNcc == id);
            if (nhacungcap == null)
            {
                return NotFound();
            }

            return View(nhacungcap);
        }

        // POST: Admin/AdminNhacungcaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nhacungcap = await _context.Nhacungcap.FindAsync(id);
            _context.Nhacungcap.Remove(nhacungcap);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhacungcapExists(int id)
        {
            return _context.Nhacungcap.Any(e => e.MaNcc == id);
        }
    }
}
