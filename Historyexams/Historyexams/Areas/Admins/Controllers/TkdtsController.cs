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
    public class TkdtsController : Controller
    {
        private readonly HistoryexamsContext _context;

        public TkdtsController(HistoryexamsContext context)
        {
            _context = context;
        }

        // GET: Admins/Tkdts
        public async Task<IActionResult> Index()
        {
            var historyexamsContext = _context.Tkdts.Include(t => t.IddethiNavigation).Include(t => t.IdtaikhoanNavigation);
            return View(await historyexamsContext.ToListAsync());
        }

        // GET: Admins/Tkdts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tkdts == null)
            {
                return NotFound();
            }

            var tkdt = await _context.Tkdts
                .Include(t => t.IddethiNavigation)
                .Include(t => t.IdtaikhoanNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tkdt == null)
            {
                return NotFound();
            }

            return View(tkdt);
        }

        // GET: Admins/Tkdts/Create
        public IActionResult Create()
        {
            ViewData["Iddethi"] = new SelectList(_context.Dethis, "Id", "Id");
            ViewData["Idtaikhoan"] = new SelectList(_context.Taikhoans, "Id", "Id");
            return View();
        }

        // POST: Admins/Tkdts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Idtaikhoan,Iddethi,Lan,Tyle,Ngaythi")] Tkdt tkdt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tkdt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Iddethi"] = new SelectList(_context.Dethis, "Id", "Id", tkdt.Iddethi);
            ViewData["Idtaikhoan"] = new SelectList(_context.Taikhoans, "Id", "Id", tkdt.Idtaikhoan);
            return View(tkdt);
        }

        // GET: Admins/Tkdts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tkdts == null)
            {
                return NotFound();
            }

            var tkdt = await _context.Tkdts.FindAsync(id);
            if (tkdt == null)
            {
                return NotFound();
            }
            ViewData["Iddethi"] = new SelectList(_context.Dethis, "Id", "Id", tkdt.Iddethi);
            ViewData["Idtaikhoan"] = new SelectList(_context.Taikhoans, "Id", "Id", tkdt.Idtaikhoan);
            return View(tkdt);
        }

        // POST: Admins/Tkdts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Idtaikhoan,Iddethi,Lan,Tyle,Ngaythi")] Tkdt tkdt)
        {
            if (id != tkdt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tkdt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TkdtExists(tkdt.Id))
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
            ViewData["Iddethi"] = new SelectList(_context.Dethis, "Id", "Id", tkdt.Iddethi);
            ViewData["Idtaikhoan"] = new SelectList(_context.Taikhoans, "Id", "Id", tkdt.Idtaikhoan);
            return View(tkdt);
        }

        // GET: Admins/Tkdts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tkdts == null)
            {
                return NotFound();
            }

            var tkdt = await _context.Tkdts
                .Include(t => t.IddethiNavigation)
                .Include(t => t.IdtaikhoanNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tkdt == null)
            {
                return NotFound();
            }

            return View(tkdt);
        }

        // POST: Admins/Tkdts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tkdts == null)
            {
                return Problem("Entity set 'HistoryexamsContext.Tkdts'  is null.");
            }
            var tkdt = await _context.Tkdts.FindAsync(id);
            if (tkdt != null)
            {
                _context.Tkdts.Remove(tkdt);
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
                        var obj = _context.Tkdts.Find(Convert.ToInt32(item));
                        _context.Tkdts.Remove(obj);
                        _context.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        private bool TkdtExists(int id)
        {
          return (_context.Tkdts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
