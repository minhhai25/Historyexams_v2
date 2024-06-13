using Historyexams.Models;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Historyexams.Controllers
{
    public class LoginController : Controller
    {
        private readonly HistoryexamsContext _context;

        public LoginController(HistoryexamsContext context)
        {
            _context = context;
        }
		//[HttpGet]
		public IActionResult Register( )
		{
            string dataJson = HttpContext.Session.GetString("TaiKhoan");
            if (dataJson != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
		}
		[HttpPost]
        public IActionResult Register(Taikhoan model)
        {
            try
            {
                var pass = getHashSha256(model.Matkhau);
                model.Matkhau = pass;
                model.Ngaytao = DateTime.Now;
                model.Ngaysua = DateTime.Now;
                model.Isactive = true;
                _context.Taikhoans.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Login", "Login");
            }
            catch (Exception ex)
            {
                TempData["errorRegisty"] = "Lỗi đăng ký ;" + ex.Message;
                return RedirectToAction("Register");
            }

        }
        [HttpGet]
        public IActionResult Login()
        {
           string dataJson = HttpContext.Session.GetString("TaiKhoan");
            if(dataJson != null) {
                //var dataLogin = Newtonsoft.Json.JsonConvert.DeserializeObject<Taikhoan>(dataJson);
                return RedirectToAction("Index", "Home");
            }
            
            var modelLogin = new Taikhoan();
            return View(modelLogin);
           
        }
        [HttpPost]
        public IActionResult Login(Taikhoan model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            // xử lý phần logic đăng nhaapjh tại đây
            var pass = getHashSha256(model.Matkhau);
            var dataLogin = _context.Taikhoans.Where(x => x.Email.Equals(model.Email) && x.Matkhau.Equals(pass)).FirstOrDefault();
            if (dataLogin != null)
            {
                ViewBag.Login = "Đăng nhập thành công";
                var jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(dataLogin);
                HttpContext.Session.SetString("TaiKhoan", jsonStr);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("Matkhau", "Email hoặc mật khẩu khôg đúng! Vui lòng nhập lại");
                return View(model);
            }
        }
        public static string getHashSha256(string text)
        {
			if (string.IsNullOrEmpty(text))
			{
				return null; // Trả về null nếu input là null hoặc rỗng
			}
			string hash = "";
            using (var sha256 = new SHA256Managed())
            {
                var hashdBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                hash = BitConverter.ToString(hashdBytes).Replace("-", "").ToLower();
            }
            return hash;
        }
        [HttpGet]
        public IActionResult Logout()
        {
				HttpContext.Session.Remove("TaiKhoan");
				return RedirectToAction("Login", "Login");
		}
      
    }
}
