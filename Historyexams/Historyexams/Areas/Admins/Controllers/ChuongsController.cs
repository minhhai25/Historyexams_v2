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
    public class ChuongsController : BaseController
    {
        private readonly HistoryexamsContext _context;

        public ChuongsController(HistoryexamsContext context)
        {
            _context = context;
        }

        // GET: Admins/Chuongs
        public async Task<IActionResult> Index()
        {
            var a = _context.Chuongs;
              return _context.Chuongs != null ? 
                          View(await _context.Chuongs.ToListAsync()) :
                          Problem("Entity set 'HistoryexamsContext.Chuongs'  is null.");
        }

        // GET: Admins/Chuongs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Chuongs == null)
            {
                return NotFound();
            }

            var chuong = await _context.Chuongs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chuong == null)
            {
                return NotFound();
            }

            return View(chuong);
        }

        // GET: Admins/Chuongs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admins/Chuongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Machuong,Tenchuong,Mota,Ngaytao,Nguoitao,Isactive,Isdelete")] Chuong chuong)
        {
            if (ModelState.IsValid)
            {
                chuong.Ngaytao = DateTime.Now;
                _context.Add(chuong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chuong);
        }

        // GET: Admins/Chuongs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Chuongs == null)
            {
                return NotFound();
            }

            var chuong = await _context.Chuongs.FindAsync(id);
            if (chuong == null)
            {
                return NotFound();
            }
            return View(chuong);
        }

        // POST: Admins/Chuongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Machuong,Tenchuong,Mota,Ngaytao,Nguoitao,Isactive,Isdelete")] Chuong chuong)
        {
            if (id != chuong.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chuong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!chuongExists(chuong.Id))
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
            return View(chuong);
        }

        // GET: Admins/Chuongs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Chuongs == null)
            {
                return NotFound();
            }

            var chuong = await _context.Chuongs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chuong == null)
            {
                return NotFound();
            }

            return View(chuong);
        }

        // POST: Admins/Chuongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Chuongs == null)
            {
                return Problem("Entity set 'HistoryexamsContext.Chuongs'  is null.");
            }
            var chuong = await _context.Chuongs.FindAsync(id);
            if (chuong != null)
            {
                _context.Chuongs.Remove(chuong);
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
                        var obj =_context.Chuongs.Find(Convert.ToInt32(item));
                        _context.Chuongs.Remove(obj);
                        _context.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        public IActionResult IsActive(int id)
        {
            var item = _context.Chuongs.Find(id);
            if (item != null)
            {
                item.Isactive = !item.Isactive;
                var entry = _context.Entry<Chuong>(item);
                entry.State = EntityState.Modified;
                //_db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return Json(new { success = true, isActive = item.Isactive });
            }
            return Json(new { success = false });
        }

        private bool chuongExists(int id)
        {
          return (_context.Chuongs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
