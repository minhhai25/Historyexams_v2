using Historyexams.Models;
using Historyexams.ModelViews;
using Microsoft.AspNetCore.Mvc;

namespace Historyexams.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class HomeController : Controller
    {
        private readonly HistoryexamsContext _context;

        public HomeController(HistoryexamsContext context)
        {
            _context = context;
        }
        public IActionResult ThongKe()
        {
            var SocauHoi = _context.Cauhois.ToList().Count();
            ViewBag.socau = SocauHoi;
            var Sodethi = _context.Dethis.ToList().Count();
            ViewBag.sode = Sodethi;
            var Soluotthi = _context.Tkdts.ToList().Count();
            ViewBag.soluot = Soluotthi;

            var Luottheobai = (from x in _context.Tkdts
                               join dt in _context.Dethis on x.Iddethi equals dt.Id
                               join bt in _context.Baitests on dt.Idbaitest equals bt.Id
                               group x by new { bt.Id, bt.Tenbaitest } into g
                               select new ThongKeViewModel
                               {
                                   Tenbaitest = g.Key.Tenbaitest,
                                   Soluotthi = g.Count()
                               })
                                .OrderByDescending(x => x.Soluotthi)

                                .ToList();
            ViewBag.Luottheobai = Luottheobai;
            return View();
        }
    }
}
