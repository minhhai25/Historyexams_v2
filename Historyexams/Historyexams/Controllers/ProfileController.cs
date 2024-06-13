using Historyexams.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Historyexams.Controllers
{
	public class ProfileController : Controller
	{
		private readonly HistoryexamsContext _context;

		public ProfileController(HistoryexamsContext context)
		{
			_context = context;

		}

		// GET: ProfileController/Edit/5
		public ActionResult Edit()
		{
			var dataMember = JsonConvert.DeserializeObject<Taikhoan>(HttpContext.Session.GetString("TaiKhoan"));
			return View(dataMember);
		}
		// POST: ProfileController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Taikhoan model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var dataMember = JsonConvert.DeserializeObject<Taikhoan>(HttpContext.Session.GetString("TaiKhoan"));
					var profile = _context.Taikhoans.FirstOrDefault(t => t.Id == dataMember.Id);

					if (profile != null)
					{
						// Update profile fields
						profile.Hoten = model.Hoten;
						profile.Dienthoai = model.Dienthoai;
						profile.Email = model.Email;
						profile.Gioitinh = model.Gioitinh;
						profile.Diachi = model.Diachi;

						_context.Update(profile);
						_context.SaveChanges();

						// Update session data
						HttpContext.Session.SetString("TaiKhoan", JsonConvert.SerializeObject(profile));
					}
					return RedirectToAction("Edit");
				}
				catch
				{
					return View(model);
				}
			}
			return View(model);
		}


	}
}
