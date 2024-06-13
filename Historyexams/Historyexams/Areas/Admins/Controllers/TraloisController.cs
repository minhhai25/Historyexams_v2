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
    public class TraloisController : Controller
    {
        private readonly HistoryexamsContext _context;

        public TraloisController(HistoryexamsContext context)
        {
            _context = context;
        }

        // GET: Admins/Tralois
        public async Task<IActionResult> Index(int? page)
        {
            var pageSize = 12;
            if (page == null)
            {
                page = 1;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var historyexamsContext = _context.Tralois.Include(t => t.IddtchNavigation).ToPagedListAsync(pageIndex, pageSize);
            return View(await historyexamsContext);
        }

        // GET: Admins/Tralois/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Tralois == null)
            {
                return NotFound();
            }

            var traloi = await _context.Tralois
                .Include(t => t.IddtchNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (traloi == null)
            {
                return NotFound();
            }

            return View(traloi);
        }

        // GET: Admins/Tralois/Create
        public IActionResult Create()
        {
            ViewData["Iddtch"] = new SelectList(_context.Dtches, "Id", "Id");
            return View();
        }

        // POST: Admins/Tralois/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Iddtch,PaChon,PaDung,Lan,Ngaythi")] Traloi traloi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(traloi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Iddtch"] = new SelectList(_context.Dtches, "Id", "Id", traloi.Iddtch);
            return View(traloi);
        }

        // GET: Admins/Tralois/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Tralois == null)
            {
                return NotFound();
            }

            var traloi = await _context.Tralois.FindAsync(id);
            if (traloi == null)
            {
                return NotFound();
            }
            ViewData["Iddtch"] = new SelectList(_context.Dtches, "Id", "Id", traloi.Iddtch);
            return View(traloi);
        }

        // POST: Admins/Tralois/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Iddtch,PaChon,PaDung,Lan,Ngaythi")] Traloi traloi)
        {
            if (id != traloi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(traloi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TraloiExists(traloi.Id))
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
            ViewData["Iddtch"] = new SelectList(_context.Dtches, "Id", "Id", traloi.Iddtch);
            return View(traloi);
        }

        // GET: Admins/Tralois/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Tralois == null)
            {
                return NotFound();
            }

            var traloi = await _context.Tralois
                .Include(t => t.IddtchNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (traloi == null)
            {
                return NotFound();
            }

            return View(traloi);
        }

        // POST: Admins/Tralois/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Tralois == null)
            {
                return Problem("Entity set 'HistoryexamsContext.Tralois'  is null.");
            }
            var traloi = await _context.Tralois.FindAsync(id);
            if (traloi != null)
            {
                _context.Tralois.Remove(traloi);
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
                        var obj = _context.Tralois.Find(Convert.ToInt64(item));
                        _context.Tralois.Remove(obj);
                        _context.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        private bool TraloiExists(long id)
        {
          return (_context.Tralois?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
