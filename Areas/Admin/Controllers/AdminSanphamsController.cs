using System; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using doan.Models;
using PagedList.Core;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace doan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminSanphamsController : Controller
    {
        private readonly CuaHangDecorateContext _context;

        public INotyfService _notyfyService { get; set; }
        public AdminSanphamsController(CuaHangDecorateContext context, INotyfService notifyService)
        {
            _context = context;
            _notyfyService = notifyService;
        }

        // GET: Admin/AdminSanphams


        public IActionResult Index(int page = 1, int madanhmuc = 0)
        {
            var pageNumber = page;
            var pageSize = 20;
            List<Sanpham> IsSanphams = new List<Sanpham>();
            if (madanhmuc != 0)
            {
                IsSanphams = _context.Sanpham
                .AsNoTracking()
                .Where(x=>x.MaDanhMuc==madanhmuc)
                .Include(x => x.MaDanhMucNavigation)
                .Include(x => x.MaNccNavigation)
                .OrderBy(x => x.MaSp).ToList();
            }
            else
            {
                IsSanphams = _context.Sanpham
                .AsNoTracking()
                .Include(x => x.MaDanhMucNavigation)
                .Include(x => x.MaNccNavigation)
                .OrderBy(x => x.MaSp).ToList();
            }

            PagedList<Sanpham> models = new PagedList<Sanpham>(IsSanphams.AsQueryable(), pageNumber, pageSize);
            ViewBag.Currentmadanhmuc = madanhmuc;
            ViewBag.CurrentPage = pageNumber;
            
            ViewData["Danhmuc"] = new SelectList(_context.Danhmucsp, "MaDanhMuc", "TenDanhMuc", madanhmuc);

            ViewData["Nhacungcap"] = new SelectList(_context.Nhacungcap, "MaNcc", "TenNcc");

            return View(models);
        }
        public IActionResult Filtter(int madanhmuc = 0)
        {
            var url = $"/Admin/AdminSanphams?madanhmuc={madanhmuc}";
            if (madanhmuc == 0)
            {
                url = $"/Admin/AdminSanphams";
            }
            return Json(new { status = "success", redirectUrl = url });
        }
        // GET: Admin/AdminSanphams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanpham = await _context.Sanpham
                .Include(s => s.MaDanhMucNavigation)
                .Include(s => s.MaNccNavigation)
                .FirstOrDefaultAsync(m => m.MaSp == id);
            if (sanpham == null)
            {
                return NotFound();
            }

            return View(sanpham);
        }

        // GET: Admin/AdminSanphams/Create
        public IActionResult Create()
        {
            ViewData["Danhmuc"] = new SelectList(_context.Danhmucsp, "MaDanhMuc", "TenDanhMuc");
            ViewData["Nhacungcap"] = new SelectList(_context.Nhacungcap, "MaNcc", "TenNcc");
            return View();
        }

        // POST: Admin/AdminSanphams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSp,TenSp,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNcc")] Sanpham sanpham)
        {
            if (sanpham.TenSp == null || sanpham.GiaTien == null || sanpham.MoTa == null)
            {
                _notyfyService.Warning("Thêm không thành công, có giá trị chưa được nhập");


            } else
            if (ModelState.IsValid)
            {
                
                _context.Add(sanpham);
                await _context.SaveChangesAsync();
                _notyfyService.Success("Tạo mới thành công");

                return RedirectToAction(nameof(Index));
            }
            
            ViewData["Danhmuc"] = new SelectList(_context.Danhmucsp, "MaDanhMuc", "TenDanhMuc", sanpham.MaDanhMuc);
            ViewData["Nhacungcap"] = new SelectList(_context.Nhacungcap, "MaNcc", "TenNcc", sanpham.MaNcc);
            return View(sanpham);
        }

        // GET: Admin/AdminSanphams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanpham = await _context.Sanpham.FindAsync(id);
            if (sanpham == null)
            {
                return NotFound();
            }
            ViewData["Danhmuc"] = new SelectList(_context.Danhmucsp, "MaDanhMuc", "TenDanhMuc", sanpham.MaDanhMuc);
            ViewData["Nhacungcap"] = new SelectList(_context.Nhacungcap, "MaNcc", "TenNcc", sanpham.MaNcc);
            return View(sanpham);
        }

        // POST: Admin/AdminSanphams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaSp,TenSp,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNcc")] Sanpham sanpham)
        {
            if (id != sanpham.MaSp)
            {
                return NotFound();
            }
            else if(sanpham.TenSp == null || sanpham.GiaTien == null || sanpham.MoTa == null)
            {
                _notyfyService.Warning("Cập nhật không thành công, có giá trị chưa được nhập");


            } else
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sanpham);
                    await _context.SaveChangesAsync();
                    _notyfyService.Success("Cập nhật thành công");

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanphamExists(sanpham.MaSp))
                    {
                        _notyfyService.Success("Đã có lỗi xảy ra");

                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Danhmuc"] = new SelectList(_context.Danhmucsp, "MaDanhMuc", "TenDanhMuc", sanpham.MaDanhMuc);
            ViewData["Nhacungcap"] = new SelectList(_context.Nhacungcap, "MaNcc", "TenNcc", sanpham.MaNcc);
            return View(sanpham);
        }

        // GET: Admin/AdminSanphams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanpham = await _context.Sanpham
                .Include(s => s.MaDanhMucNavigation)
                .Include(s => s.MaNccNavigation)
                .FirstOrDefaultAsync(m => m.MaSp == id);
            if (sanpham == null)
            {
                return NotFound();
            }

            return View(sanpham);
        }

        // POST: Admin/AdminSanphams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sanpham = await _context.Sanpham.FindAsync(id);
            _context.Sanpham.Remove(sanpham);
            await _context.SaveChangesAsync();
            _notyfyService.Success("Xóa thành công");

            return RedirectToAction(nameof(Index));
        }

        private bool SanphamExists(int id)
        {
            return _context.Sanpham.Any(e => e.MaSp == id);
        }
    }
}
