using System; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using doan.Models;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace doan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminVouchersController : Controller
    {
        private readonly CuaHangDecorateContext _context;

        public INotyfService _notyfyService { get; set; }
        public AdminVouchersController(CuaHangDecorateContext context, INotyfService notifyService)
        {
            _context = context;
            _notyfyService = notifyService;
        }

        // GET: Admin/AdminVouchers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Voucher.ToListAsync());
        }

        // GET: Admin/AdminVouchers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voucher = await _context.Voucher
                .FirstOrDefaultAsync(m => m.MaVoucher == id);
            if (voucher == null)
            {
                return NotFound();
            }

            return View(voucher);
        }

        // GET: Admin/AdminVouchers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminVouchers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaVoucher,TenVoucher,TiLeGiamGia,NgayBatDau,NgayKetThuc")] Voucher voucher)
        {
            if (ModelState.IsValid)
            {
                if(voucher.NgayBatDau > voucher.NgayKetThuc)
                {
                    _notyfyService.Success("Thêm không thành công");

                    return NotFound();
                }    
                _context.Add(voucher);
                await _context.SaveChangesAsync();
                _notyfyService.Success("Tạo mới thành công");

                return RedirectToAction(nameof(Index));
            }
            return View(voucher);
        }

        // GET: Admin/AdminVouchers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voucher = await _context.Voucher.FindAsync(id);
            if (voucher == null)
            {
                return NotFound();
            }
            return View(voucher);
        }

        // POST: Admin/AdminVouchers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaVoucher,TenVoucher,TiLeGiamGia,NgayBatDau,NgayKetThuc")] Voucher voucher)
        {
            
            if (id != voucher.MaVoucher)
            {
                return NotFound();
            }
            if (voucher.NgayBatDau > voucher.NgayKetThuc)
            {
                _notyfyService.Success("Sửa không thành công: Ngày bắt đầu phải nhỏ hơn ngày kết thúc");

                //return RedirectToAction(nameof(Index));
            }
            else
            if (ModelState.IsValid)
            {
                try
                {
                        if(voucher.TiLeGiamGia > 1)
                        {
                        voucher.TiLeGiamGia /= 100;
                        }
                    
                        _context.Update(voucher);
                        await _context.SaveChangesAsync();
                    
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                    
                    if (!VoucherExists(voucher.MaVoucher))
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
            return View(voucher);
        }

        // GET: Admin/AdminVouchers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voucher = await _context.Voucher
                .FirstOrDefaultAsync(m => m.MaVoucher == id);
            if (voucher == null)
            {
                return NotFound();
            }

            return View(voucher);
        }

        // POST: Admin/AdminVouchers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var voucher = await _context.Voucher.FindAsync(id);
            _context.Voucher.Remove(voucher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoucherExists(int id)
        {
            return _context.Voucher.Any(e => e.MaVoucher == id);
        }
    }
}
