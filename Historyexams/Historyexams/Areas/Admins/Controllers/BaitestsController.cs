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
    public class BaitestsController : Controller
    {
        private readonly HistoryexamsContext _context;

        public BaitestsController(HistoryexamsContext context)
        {
            _context = context;
        }

        // GET: Admins/Baitests
        public async Task<IActionResult> Index(string title)
        {
            var items = _context.Baitests.OrderByDescending(x => x.Mabaitest).ToList();
            if (!string.IsNullOrEmpty(title))
            {
                items = _context.Baitests.Where(x => x.Mabaitest.Contains(title)).ToList();
            }
            ViewBag.keyword = title;
            return View(items);
            //return _context.Baitests != null ? 
            //              View(await _context.Baitests.ToListAsync()) :
            //              Problem("Entity set 'HistoryexamsContext.Baitests'  is null.");
        }

        // GET: Admins/Baitests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Baitests == null)
            {
                return NotFound();
            }

            var baitest = await _context.Baitests
                .FirstOrDefaultAsync(m => m.Id == id);
            if (baitest == null)
            {
                return NotFound();
            }

            return View(baitest);
        }

        // GET: Admins/Baitests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admins/Baitests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Mabaitest,Tenbaitest,Mota,Iduser,Ngaytao,Nguoitao,Isactive,Isdelete")] Baitest baitest)
        {
            if (ModelState.IsValid)
            {
                baitest.Ngaytao = DateTime.Now;
                _context.Add(baitest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(baitest);
        }

        // GET: Admins/Baitests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Baitests == null)
            {
                return NotFound();
            }

            var baitest = await _context.Baitests.FindAsync(id);
            if (baitest == null)
            {
                return NotFound();
            }
            return View(baitest);
        }

        // POST: Admins/Baitests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Mabaitest,Tenbaitest,Mota,Iduser,Ngaytao,Nguoitao,Isactive,Isdelete")] Baitest baitest)
        {
            if (id != baitest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
					//baitest.Ngaytao = _context.Baitests.Where(x => x.Id == baitest.Id).FirstOrDefault().Ngaytao;
					_context.Update(baitest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaitestExists(baitest.Id))
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
            return View(baitest);
        }

        // GET: Admins/Baitests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Baitests == null)
            {
                return NotFound();
            }

            var baitest = await _context.Baitests
                .FirstOrDefaultAsync(m => m.Id == id);
            if (baitest == null)
            {
                return NotFound();
            }

            return View(baitest);
        }

        // POST: Admins/Baitests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Baitests == null)
            {
                return Problem("Entity set 'HistoryexamsContext.Baitests'  is null.");
            }
            var baitest = await _context.Baitests.FindAsync(id);
            if (baitest != null)
            {
                _context.Baitests.Remove(baitest);
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
                        var obj = _context.Baitests.Find(Convert.ToInt32(item));
                        _context.Baitests.Remove(obj);
                        _context.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        public IActionResult IsActive(int id)
        {
            var item = _context.Baitests.Find(id);
            if (item != null)
            {
                item.Isactive = !item.Isactive;
                var entry = _context.Entry<Baitest>(item);
                entry.State = EntityState.Modified;
                //_db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return Json(new { success = true, IsActive = item.Isactive });
            }
            return Json(new { success = false });
        }
        private bool BaitestExists(int id)
        {
          return (_context.Baitests?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
