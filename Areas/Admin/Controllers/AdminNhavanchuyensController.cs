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
    public class AdminNhavanchuyensController : Controller
    {
        private readonly CuaHangDecorateContext _context;

        public AdminNhavanchuyensController(CuaHangDecorateContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminNhavanchuyens
        public async Task<IActionResult> Index()
        {
            return View(await _context.Nhavanchuyen.ToListAsync());
        }

        // GET: Admin/AdminNhavanchuyens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhavanchuyen = await _context.Nhavanchuyen
                .FirstOrDefaultAsync(m => m.MaNvc == id);
            if (nhavanchuyen == null)
            {
                return NotFound();
            }

            return View(nhavanchuyen);
        }

        // GET: Admin/AdminNhavanchuyens/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminNhavanchuyens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNvc,TenNvc,DiaChi,Email")] Nhavanchuyen nhavanchuyen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nhavanchuyen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nhavanchuyen);
        }

        // GET: Admin/AdminNhavanchuyens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhavanchuyen = await _context.Nhavanchuyen.FindAsync(id);
            if (nhavanchuyen == null)
            {
                return NotFound();
            }
            return View(nhavanchuyen);
        }

        // POST: Admin/AdminNhavanchuyens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaNvc,TenNvc,DiaChi,Email")] Nhavanchuyen nhavanchuyen)
        {
            if (id != nhavanchuyen.MaNvc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhavanchuyen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhavanchuyenExists(nhavanchuyen.MaNvc))
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
            return View(nhavanchuyen);
        }

        // GET: Admin/AdminNhavanchuyens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhavanchuyen = await _context.Nhavanchuyen
                .FirstOrDefaultAsync(m => m.MaNvc == id);
            if (nhavanchuyen == null)
            {
                return NotFound();
            }

            return View(nhavanchuyen);
        }

        // POST: Admin/AdminNhavanchuyens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nhavanchuyen = await _context.Nhavanchuyen.FindAsync(id);
            _context.Nhavanchuyen.Remove(nhavanchuyen);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhavanchuyenExists(int id)
        {
            return _context.Nhavanchuyen.Any(e => e.MaNvc == id);
        }
    }
}
