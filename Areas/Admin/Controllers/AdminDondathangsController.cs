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
    public class AdminDondathangsController : Controller
    {
        private readonly CuaHangDecorateContext _context;

        public INotyfService _notyfyService { get; set; }
        public AdminDondathangsController(CuaHangDecorateContext context, INotyfService notifyService)
        {
            _context = context;
            _notyfyService = notifyService;
        }

        // GET: Admin/AdminDondathangs
        public IActionResult Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 20;
            var IsDondathangs = _context.Dondathang
                .AsNoTracking()
                .Include(x => x.MaKhNavigation)
                .Include(x => x.MaNvNavigation)
                .Include(x => x.MaNvcNavigation)
                .OrderBy(x => x.MaDdh);
            PagedList<Dondathang> models = new PagedList<Dondathang>(IsDondathangs, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }
        //public async Task<IActionResult> Index()
        //{
        //    var cuaHangDecorateContext = _context.Dondathang.Include(d => d.MaKhNavigation).Include(d => d.MaNvNavigation).Include(d => d.MaNvcNavigation).Include(d => d.MaVoucherNavigation);
        //    return View(await cuaHangDecorateContext.ToListAsync());
        //}

        // GET: Admin/AdminDondathangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dondathang = await _context.Dondathang
                .Include(d => d.MaKhNavigation)
                .Include(d => d.MaNvNavigation)
                .Include(d => d.MaNvcNavigation)
                .Include(d => d.MaVoucherNavigation)
                .FirstOrDefaultAsync(m => m.MaDdh == id);
            if (dondathang == null)
            {
                return NotFound();
            }

            return View(dondathang);
        }

        // GET: Admin/AdminDondathangs/Create
        public IActionResult Create()
        {
            ViewData["MaKh"] = new SelectList(_context.Khachhang, "TenKh", "DiaChi");
            ViewData["MaNv"] = new SelectList(_context.Nhanvien, "MaNv", "ChucVu");
            ViewData["MaNvc"] = new SelectList(_context.Nhavanchuyen, "MaNvc", "MaNvc");
            ViewData["MaVoucher"] = new SelectList(_context.Voucher, "MaVoucher", "TenVoucher");
            return View();
        }

        // POST: Admin/AdminDondathangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaDdh,MaKh,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNv,NgayDatHang,MaNvc")] Dondathang dondathang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dondathang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaKh"] = new SelectList(_context.Khachhang, "MaKh", "DiaChi", dondathang.MaKh);
            ViewData["MaNv"] = new SelectList(_context.Nhanvien, "MaNv", "ChucVu", dondathang.MaNv);
            ViewData["MaNvc"] = new SelectList(_context.Nhavanchuyen, "MaNvc", "MaNvc", dondathang.MaNvc);
            ViewData["MaVoucher"] = new SelectList(_context.Voucher, "MaVoucher", "TenVoucher", dondathang.MaVoucher);
            return View(dondathang);
        }

        // GET: Admin/AdminDondathangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dondathang = await _context.Dondathang.FindAsync(id);
            if (dondathang == null)
            {
                return NotFound();
            }
            ViewData["MaKh"] = new SelectList(_context.Khachhang, "MaKh", "TenKh", dondathang.MaKh);
            ViewData["MaNv"] = new SelectList(_context.Nhanvien, "MaNv", "TenNv", dondathang.MaNv);
            ViewData["MaNvc"] = new SelectList(_context.Nhavanchuyen, "MaNvc", "TenNvc", dondathang.MaNvc);
            ViewData["MaVoucher"] = new SelectList(_context.Voucher, "MaVoucher", "TenVoucher", dondathang.MaVoucher);
            return View(dondathang);
        }

        // POST: Admin/AdminDondathangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaDdh,MaKh,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNv,NgayDatHang,MaNvc,TenNguoiNhan,SDTNguoiNhan,DiaChiNhan,TinhTrangDonHang")] Dondathang dondathang)
        {
            if (id != dondathang.MaDdh)
            {
                return NotFound();
            }
            
            
            dondathang.ThanhTien = dondathang.TongDonHang - dondathang.SoTienGiam;
            if (dondathang.TongDonHang <= 0)
            {
                _notyfyService.Warning("Sửa không thành công: Tổng đơn hàng phải lớn hơn 0");
            }

            else if(dondathang.SoTienGiam > dondathang.TongDonHang || dondathang.SoTienGiam <0)
            {
                _notyfyService.Warning("Sửa không thành công: Số tiền giảm không hợp lệ");

            }
            else
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dondathang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DondathangExists(dondathang.MaDdh))
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
            ViewData["MaKh"] = new SelectList(_context.Khachhang, "MaKh", "DiaChi", dondathang.MaKh);
            ViewData["MaNv"] = new SelectList(_context.Nhanvien, "MaNv", "ChucVu", dondathang.MaNv);
            ViewData["MaNvc"] = new SelectList(_context.Nhavanchuyen, "MaNvc", "MaNvc", dondathang.MaNvc);
            ViewData["MaVoucher"] = new SelectList(_context.Voucher, "MaVoucher", "TenVoucher", dondathang.MaVoucher);
            return View(dondathang);
        }

        // GET: Admin/AdminDondathangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dondathang = await _context.Dondathang
                .Include(d => d.MaKhNavigation)
                .Include(d => d.MaNvNavigation)
                .Include(d => d.MaNvcNavigation)
                .Include(d => d.MaVoucherNavigation)
                .FirstOrDefaultAsync(m => m.MaDdh == id);
            if (dondathang == null)
            {
                return NotFound();
            }

            return View(dondathang);
        }

        // POST: Admin/AdminDondathangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dondathang = await _context.Dondathang.FindAsync(id);
            _context.Dondathang.Remove(dondathang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DondathangExists(int id)
        {
            return _context.Dondathang.Any(e => e.MaDdh == id);
        }
    }
}
