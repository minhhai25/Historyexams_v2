using Historyexams.Models;
using Historyexams.ModelViews;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;

namespace Historyexams.Controllers
{
	public class HomeController : Controller
	{
		//HistoryexamsContext db = new HistoryexamsContext();
		private readonly ILogger<HomeController> _logger;
		private readonly HistoryexamsContext _context;

		public HomeController(ILogger<HomeController> logger, HistoryexamsContext context)
		{
			_context = context;
			_logger = logger;
		}

		public IActionResult Index()
		{
			//nếu chưa đăng nhập
			if (HttpContext.Session.GetString("TaiKhoan") == null)
			{
				return Redirect("/Login/Login/?url=/Home/Index");
			}
			else
			{
				var top3Exam = (from x in _context.Tkdts
								join dt in _context.Dethis on x.Iddethi equals dt.Id
								join bt in _context.Baitests on dt.Idbaitest equals bt.Id
								group x by new { bt.Id, bt.Tenbaitest} into g
								select new ThongKeViewModel
								{
									Tenbaitest = g.Key.Tenbaitest,
									Soluotthi = g.Count()
								})
								.OrderByDescending(x => x.Soluotthi)
								.Take(3)
								.ToList();


				//var top3ExamCounts = _context.Tkdts
				//.Join(_context.Dethis, tk => tk.Iddethi, dt => dt.Id, (tk, dt) => new { tk, dt })
				//.Join(_context.Baitests, x => x.dt.Idbaitest, bt => bt.Id, (x, bt) => new { x, bt })
				//.GroupBy(g => new { g.bt.Id, g.bt.Tenbaitest, g.bt.Mota })
				//.Select(group => new ThongKeViewModel
				//{
				//	Tenbaitest = group.Key.Tenbaitest,
				//	Mota = group.Key.Mota,
				//	Soluotthi = group.Count()
				//})
				//.OrderByDescending(x => x.Soluotthi)
				//.Take(3)
				//.ToList();
				return View(top3Exam); 
			}
		}
	
	
	
	
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
