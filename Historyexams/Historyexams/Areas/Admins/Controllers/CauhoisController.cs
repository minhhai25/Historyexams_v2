using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Historyexams.Models;
using X.PagedList;

namespace Historyexams.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class CauhoisController : Controller
    {
        private readonly HistoryexamsContext _context;

        public CauhoisController(HistoryexamsContext context)
        {
            _context = context;
        }

        // GET: Admins/Cauhois
        public IActionResult Index( int? page, string title)
        {
            var pageSize = 6;
            if (page == null)
            {
                page=1;
            }
            var pageIndex=page.HasValue ? Convert.ToInt32(page) : 1;
            var item = _context.Cauhois
                .OrderByDescending(c=>c.Id)
                .Include(c => c.IdchuongNavigation)
                .Include(c => c.IdmucdoNavigation)
                .ToPagedList(pageIndex,pageSize);
            if (!string.IsNullOrEmpty(title))
            {
                item = _context.Cauhois.Where(x => x.Macauhoi.Contains(title)).ToPagedList(pageIndex, pageSize); ;
            }
			ViewBag.keyword = title;
			return View( item);
        }

        // GET: Admins/Cauhois/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cauhois == null)
            {
                return NotFound();
            }

            var cauhoi = await _context.Cauhois
                .Include(c => c.IdchuongNavigation)
                .Include(c => c.IdmucdoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cauhoi == null)
            {
                return NotFound();
            }

            return View(cauhoi);
        }

        // GET: Admins/Cauhois/Create
        public IActionResult Create()
        {
            ViewBag.Tenchuong = new SelectList(_context.Chuongs, "Id", "Tenchuong");
            ViewBag.Tenmd = new SelectList(_context.Mucdos, "Id", "Tenmd");
            return View();
        }

        // POST: Admins/Cauhois/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Macauhoi,Idmucdo,Idchuong,Noidung,PaA,PaB,PaC,PaD,PaDung,Ngaytao,Nguoitao,Isactive,Isdelete")] Cauhoi cauhoi)
        {
            if (ModelState.IsValid)
            {
                cauhoi.Ngaytao = DateTime.Now;
                _context.Add(cauhoi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Tenchuong = new SelectList(_context.Chuongs, "Id", "Tenchuong");
            ViewBag.Tenmd = new SelectList(_context.Mucdos, "Id", "Tenmd");
            return View(cauhoi);
        }

        // GET: Admins/Cauhois/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cauhois == null)
            {
                return NotFound();
            }

            var cauhoi = await _context.Cauhois.FindAsync(id);
            if (cauhoi == null)
            {
                return NotFound();
            }
            ViewBag.Tenchuong = new SelectList(_context.Chuongs, "Id", "Tenchuong",cauhoi.Idchuong);
            ViewBag.Tenmd = new SelectList(_context.Mucdos, "Id", "Tenmd",cauhoi.Idmucdo);
            return View(cauhoi);
        }

        // POST: Admins/Cauhois/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Macauhoi,Idmucdo,Idchuong,Noidung,PaA,PaB,PaC,PaD,PaDung,Ngaytao,Nguoitao,Isactive,Isdelete")] Cauhoi cauhoi)
        {
            if (id != cauhoi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cauhoi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CauhoiExists(cauhoi.Id))
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
            ViewBag.Tenchuong = new SelectList(_context.Chuongs, "Id", "Tenchuong",cauhoi.Idchuong);
            ViewBag.Tenmd = new SelectList(_context.Mucdos, "Id", "Tenmd",cauhoi.Idmucdo);
            return View(cauhoi);
        }

        // GET: Admins/Cauhois/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cauhois == null)
            {
                return NotFound();
            }

            var cauhoi = await _context.Cauhois
                .Include(c => c.IdchuongNavigation)
                .Include(c => c.IdmucdoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cauhoi == null)
            {
                return NotFound();
            }

            return View(cauhoi);
        }

        // POST: Admins/Cauhois/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cauhois == null)
            {
                return Problem("Entity set 'HistoryexamsContext.Cauhois'  is null.");
            }
            var cauhoi = await _context.Cauhois.FindAsync(id);
            if (cauhoi != null)
            {
                _context.Cauhois.Remove(cauhoi);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public ActionResult DeleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = _context.Cauhois.Find(Convert.ToInt32(item));
                        _context.Cauhois.Remove(obj);
                        _context.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        public IActionResult IsActive(int id)
        {
           
            var item = _context.Cauhois.Find(id);
			var dTCH = _context.Dtches.Where(x => x.Idcauhoi==id).Count();
			if (item != null && dTCH == 0)
            {
                item.Isactive = !item.Isactive;
                var entry = _context.Entry<Cauhoi>(item);
                entry.State = EntityState.Modified;
                _context.SaveChanges();
                return Json(new { success = true, IsActive = item.Isactive });
            }
            return Json(new { success = false });
        }

        private bool CauhoiExists(int id)
        {
          return (_context.Cauhois?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
