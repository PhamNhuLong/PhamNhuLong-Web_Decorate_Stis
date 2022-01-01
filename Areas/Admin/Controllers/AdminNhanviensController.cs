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
    public class AdminNhanviensController : Controller
    {
        private readonly CuaHangDecorateContext _context;

        public AdminNhanviensController(CuaHangDecorateContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminNhanviens
        public async Task<IActionResult> Index()
        {
            

            return View(await _context.Nhanvien.ToListAsync());
        }

        // GET: Admin/AdminNhanviens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {

                return NotFound();
            }

            var nhanvien = await _context.Nhanvien
                .FirstOrDefaultAsync(m => m.MaNv == id);
            if (nhanvien == null)
            {
                return NotFound();
            }

            return View(nhanvien);
        }

        // GET: Admin/AdminNhanviens/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminNhanviens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNv,TenNv,SoDienThoai,NgaySinh,GioiTinh,DiaChi,NgayVaoLam,ChucVu")] Nhanvien nhanvien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nhanvien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nhanvien);
        }

        // GET: Admin/AdminNhanviens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanvien = await _context.Nhanvien.FindAsync(id);
            if (nhanvien == null)
            {
                return NotFound();
            }
            return View(nhanvien);
        }

        // POST: Admin/AdminNhanviens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaNv,TenNv,SoDienThoai,NgaySinh,GioiTinh,DiaChi,NgayVaoLam,ChucVu")] Nhanvien nhanvien)
        {
            if (id != nhanvien.MaNv)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhanvien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhanvienExists(nhanvien.MaNv))
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
            return View(nhanvien);
        }

        // GET: Admin/AdminNhanviens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanvien = await _context.Nhanvien
                .FirstOrDefaultAsync(m => m.MaNv == id);
            if (nhanvien == null)
            {
                return NotFound();
            }

            return View(nhanvien);
        }

        // POST: Admin/AdminNhanviens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nhanvien = await _context.Nhanvien.FindAsync(id);
            _context.Nhanvien.Remove(nhanvien);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhanvienExists(int id)
        {
            return _context.Nhanvien.Any(e => e.MaNv == id);
        }
    }
}
