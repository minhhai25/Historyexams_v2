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
    public class DtchesController : Controller
    {
        private readonly HistoryexamsContext _context;

        public DtchesController(HistoryexamsContext context)
        {
            _context = context;
        }

        // GET: Admins/Dtches
        public async Task<IActionResult> Index()
        {
            var historyexamsContext = _context.Dtches.Include(d => d.IdcauhoiNavigation).Include(d => d.IddethiNavigation);
            return View(await historyexamsContext.ToListAsync());
        }

        // GET: Admins/Dtches/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Dtches == null)
            {
                return NotFound();
            }

            var dtch = await _context.Dtches
                .Include(d => d.IdcauhoiNavigation)
                .Include(d => d.IddethiNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dtch == null)
            {
                return NotFound();
            }

            return View(dtch);
        }

        // GET: Admins/Dtches/Create
        public IActionResult Create()
        {
            ViewData["Idcauhoi"] = new SelectList(_context.Cauhois, "Id", "Id");
            ViewData["Iddethi"] = new SelectList(_context.Dethis, "Id", "Id");
            return View();
        }

        // POST: Admins/Dtches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Iddethi,Idcauhoi")] Dtch dtch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dtch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idcauhoi"] = new SelectList(_context.Cauhois, "Id", "Id", dtch.Idcauhoi);
            ViewData["Iddethi"] = new SelectList(_context.Dethis, "Id", "Id", dtch.Iddethi);
            return View(dtch);
        }

        // GET: Admins/Dtches/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Dtches == null)
            {
                return NotFound();
            }

            var dtch = await _context.Dtches.FindAsync(id);
            if (dtch == null)
            {
                return NotFound();
            }
            ViewData["Idcauhoi"] = new SelectList(_context.Cauhois, "Id", "Id", dtch.Idcauhoi);
            ViewData["Iddethi"] = new SelectList(_context.Dethis, "Id", "Id", dtch.Iddethi);
            return View(dtch);
        }

        // POST: Admins/Dtches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Iddethi,Idcauhoi")] Dtch dtch)
        {
            if (id != dtch.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dtch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DtchExists(dtch.Id))
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
            ViewData["Idcauhoi"] = new SelectList(_context.Cauhois, "Id", "Id", dtch.Idcauhoi);
            ViewData["Iddethi"] = new SelectList(_context.Dethis, "Id", "Id", dtch.Iddethi);
            return View(dtch);
        }

        // GET: Admins/Dtches/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Dtches == null)
            {
                return NotFound();
            }

            var dtch = await _context.Dtches
                .Include(d => d.IdcauhoiNavigation)
                .Include(d => d.IddethiNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dtch == null)
            {
                return NotFound();
            }

            return View(dtch);
        }

        // POST: Admins/Dtches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Dtches == null)
            {
                return Problem("Entity set 'HistoryexamsContext.Dtches'  is null.");
            }
            var dtch = await _context.Dtches.FindAsync(id);
            if (dtch != null)
            {
                _context.Dtches.Remove(dtch);
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
                        var obj = _context.Dtches.Find(Convert.ToInt32(item));
                        _context.Dtches.Remove(obj);
                        _context.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        private bool DtchExists(long id)
        {
          return (_context.Dtches?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
