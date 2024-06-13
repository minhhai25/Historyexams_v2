using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Historyexams.Models;


namespace Historyexams.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class TaikhoansController : Controller
    {
        private readonly HistoryexamsContext _context;

        public TaikhoansController(HistoryexamsContext context)
        {
            _context = context;
        }

        // GET: Admins/Taikhoans
        public async Task<IActionResult> Index(string title)
        {
			var items = _context.Taikhoans.OrderByDescending(x => x.Mataikhoan).ToList() ;
			if (!string.IsNullOrEmpty(title))
			{
				items = _context.Taikhoans.Where(x => x.Mataikhoan.Contains(title)).ToList();
			}
			ViewBag.keyword = title;
            return View(items);
			//return _context.Taikhoans != null ? 
   //                       View(await _context.Taikhoans.ToListAsync()) :
   //                       Problem("Entity set 'HistoryexamsContext.Taikhoans'  is null.");
        }
        // GET: Admins/Taikhoans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Taikhoans == null)
            {
                return NotFound();
            }

            var taikhoan = await _context.Taikhoans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taikhoan == null)
            {
                return NotFound();
            }

            return View(taikhoan);
        }

        // GET: Admins/Taikhoans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admins/Taikhoans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Mataikhoan,Hoten,Matkhau,Dienthoai,Email,Gioitinh,Diachi,Ngaytao,Nguoitao,Ngaysua,Isactive,Isdelete")] Taikhoan taikhoan)
        {
            if (ModelState.IsValid)
            {
                taikhoan.Ngaytao = DateTime.Now;
                _context.Add(taikhoan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taikhoan);
        }

        // GET: Admins/Taikhoans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Taikhoans == null)
            {
                return NotFound();
            }

            var taikhoan = await _context.Taikhoans.FindAsync(id);
            if (taikhoan == null)
            {
                return NotFound();
            }
            return View(taikhoan);
        }

        // POST: Admins/Taikhoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Mataikhoan,Hoten,Matkhau,Dienthoai,Email,Gioitinh,Diachi,Ngaytao,Nguoitao,Ngaysua,Isactive,Isdelete")] Taikhoan taikhoan)
        {
            if (id != taikhoan.Id)
            {

                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //var date = _context.Taikhoans;
					taikhoan.Ngaysua = DateTime.Now;
                    //taikhoan.Ngaytao = _context.Taikhoans.Where(x => x.Id == taikhoan.Id).FirstOrDefault().Ngaytao;
                    //taikhoan.Matkhau = _context.Taikhoans.Where(x => x.Id == taikhoan.Id).FirstOrDefault().Matkhau;
                    _context.Update(taikhoan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaikhoanExists(taikhoan.Id))
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
            return View(taikhoan);
        }

        // GET: Admins/Taikhoans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Taikhoans == null)
            {
                return NotFound();
            }

            var taikhoan = await _context.Taikhoans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taikhoan == null)
            {
                return NotFound();
            }

            return View(taikhoan);
        }

        // POST: Admins/Taikhoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Taikhoans == null)
            {
                return Problem("Entity set 'HistoryexamsContext.Taikhoans'  is null.");
            }
            var taikhoan = await _context.Taikhoans.FindAsync(id);
            if (taikhoan != null)
            {
                _context.Taikhoans.Remove(taikhoan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = _context.Taikhoans.Find(Convert.ToInt32(item));
                        _context.Taikhoans.Remove(obj);
                        _context.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        public IActionResult IsActive(int id)
        {
            var item = _context.Taikhoans.Find(id);
            if (item != null)
            {
                item.Isactive = !item.Isactive;
                var entry = _context.Entry<Taikhoan>(item);
                entry.State = EntityState.Modified;
                //_db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return Json(new { success = true, isActive = item.Isactive });
            }
            return Json(new { success = false });
        }

        private bool TaikhoanExists(int id)
        {
          return (_context.Taikhoans?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
