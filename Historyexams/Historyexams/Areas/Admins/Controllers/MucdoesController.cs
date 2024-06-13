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
    public class MucdoesController : Controller
    {
        private readonly HistoryexamsContext _context;

        public MucdoesController(HistoryexamsContext context)
        {
            _context = context;
        }

        // GET: Admins/Mucdoes
        public async Task<IActionResult> Index()
        {
              return _context.Mucdos != null ? 
                          View(await _context.Mucdos.ToListAsync()) :
                          Problem("Entity set 'HistoryexamsContext.Mucdos'  is null.");
        }

        // GET: Admins/Mucdoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Mucdos == null)
            {
                return NotFound();
            }

            var mucdo = await _context.Mucdos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mucdo == null)
            {
                return NotFound();
            }

            return View(mucdo);
        }

        // GET: Admins/Mucdoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admins/Mucdoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Mamd,Tenmd,Mota,Ngaytao,Nguoitao,Isactive,Isdelete")] Mucdo mucdo)
        {
            if (ModelState.IsValid)
            { 
                mucdo.Ngaytao = DateTime.Now;
                _context.Add(mucdo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mucdo);
        }

        // GET: Admins/Mucdoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Mucdos == null)
            {
                return NotFound();
            }

            var mucdo = await _context.Mucdos.FindAsync(id);
            if (mucdo == null)
            {
                return NotFound();
            }
            return View(mucdo);
        }

        // POST: Admins/Mucdoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Mamd,Tenmd,Mota,Ngaytao,Nguoitao,Isactive,Isdelete")] Mucdo mucdo)
        {
            if (id != mucdo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mucdo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MucdoExists(mucdo.Id))
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
            return View(mucdo);
        }

        // GET: Admins/Mucdoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Mucdos == null)
            {
                return NotFound();
            }

            var mucdo = await _context.Mucdos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mucdo == null)
            {
                return NotFound();
            }

            return View(mucdo);
        }

        // POST: Admins/Mucdoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mucdos == null)
            {
                return Problem("Entity set 'HistoryexamsContext.Mucdos'  is null.");
            }
            var mucdo = await _context.Mucdos.FindAsync(id);
            if (mucdo != null)
            {
                _context.Mucdos.Remove(mucdo);
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
                        var obj = _context.Mucdos.Find(Convert.ToInt32(item));
                        _context.Mucdos.Remove(obj);
                        _context.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        public IActionResult IsActive(int id)
        {
            var item = _context.Mucdos.Find(id);
            if (item != null)
            {
                item.Isactive = !item.Isactive;
                var entry = _context.Entry<Mucdo>(item);
                entry.State = EntityState.Modified;
                
                _context.SaveChanges();
                return Json(new { success = true, isActive = item.Isactive });
            }
            return Json(new { success = false });
        }

        private bool MucdoExists(int id)
        {
          return (_context.Mucdos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
