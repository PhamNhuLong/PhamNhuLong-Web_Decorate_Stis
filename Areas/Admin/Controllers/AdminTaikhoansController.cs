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
    public class AdminTaikhoansController : Controller
    {
        private readonly CuaHangDecorateContext _context;

        public AdminTaikhoansController(CuaHangDecorateContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminTaikhoans
        public async Task<IActionResult> Index()
        {

            ViewData["QuyenTruyCap"] = new SelectList(_context.Roles, "RoleId", "RoleName");
            var cuaHangDecorateContext = _context.Taikhoan.Include(t => t.Role);
            return View(await cuaHangDecorateContext.ToListAsync());
        }

        // GET: Admin/AdminTaikhoans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taikhoan = await _context.Taikhoan
                .Include(t => t.Role)
                .FirstOrDefaultAsync(m => m.MaTk == id);
            if (taikhoan == null)
            {
                return NotFound();
            }

            return View(taikhoan);
        }

        // GET: Admin/AdminTaikhoans/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId");
            return View();
        }

        // POST: Admin/AdminTaikhoans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaTk,SoDienThoai,MatKhau,RoleId")] Taikhoan taikhoan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taikhoan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", taikhoan.RoleId);
            return View(taikhoan);
        }

        // GET: Admin/AdminTaikhoans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taikhoan = await _context.Taikhoan.FindAsync(id);
            if (taikhoan == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName", taikhoan.RoleId);
            return View(taikhoan);
        }

        // POST: Admin/AdminTaikhoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaTk,SoDienThoai,MatKhau,RoleId")] Taikhoan taikhoan)
        {
            if (id != taikhoan.MaTk)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taikhoan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaikhoanExists(taikhoan.MaTk))
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
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", taikhoan.RoleId);
            return View(taikhoan);
        }

        // GET: Admin/AdminTaikhoans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taikhoan = await _context.Taikhoan
                .Include(t => t.Role)
                .FirstOrDefaultAsync(m => m.MaTk == id);
            if (taikhoan == null)
            {
                return NotFound();
            }

            return View(taikhoan);
        }

        // POST: Admin/AdminTaikhoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taikhoan = await _context.Taikhoan.FindAsync(id);
            _context.Taikhoan.Remove(taikhoan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaikhoanExists(int id)
        {
            return _context.Taikhoan.Any(e => e.MaTk == id);
        }
    }
}
