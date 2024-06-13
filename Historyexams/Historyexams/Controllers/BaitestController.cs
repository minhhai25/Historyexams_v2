using Historyexams.Models;
using Historyexams.ModelViews;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Newtonsoft.Json;
using SQLitePCL;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Razor.Parser;
using X.PagedList;


namespace Historyexams.Controllers
{
    public class BaitestController : Controller
    {

        private readonly HistoryexamsContext _context;

        public BaitestController(HistoryexamsContext context)
        {
            _context = context;

        }
        // GET: BaitestController
        public ActionResult Index(int? page)
        {
            var pageSize = 6;
            if (page == null)
            {
                page = 1;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var model = _context.Baitests.Where(x=>x.Isactive).ToList().ToPagedList(pageIndex, pageSize);
            return View(model);
        }
        public IActionResult ChonDeThi(int id)
        {

            var dataMember = JsonConvert.DeserializeObject<Taikhoan>(HttpContext.Session.GetString("TaiKhoan"));

            //var idTaiKhoan = _context.Taikhoans.Where(x => x.Email.Equals(dataMember.Email)).FirstOrDefault().Id;
            var lstDeThi = _context.Dethis.Where(x => x.Idbaitest == id && x.Isactive).ToList();
            var model = new List<DethiLichSu>();
            foreach (var item in lstDeThi)
            {
                var TaiKhoaDeThi = _context.Tkdts.Where(x => x.Iddethi == item.Id && x.Idtaikhoan == dataMember.Id ).ToList();
                model.Add(new DethiLichSu()
                {
                    dethi = item,
                    tkde = TaiKhoaDeThi,
                });
            }
            
            return View(model);
        }
        public IActionResult LamBaiThi(int? id)
        {
            var dataMember = JsonConvert.DeserializeObject<Taikhoan>(HttpContext.Session.GetString("TaiKhoan"));

           
            var danhSachCauHoi = _context.Cauhois
              .Where(c => _context.Dtches.Any(dtch => dtch.Iddethi == id && dtch.Idcauhoi == c.Id)&& c.Isactive).ToList();
            var ttDethi = _context.Dethis.Find(id);
            var taiKhoanView = dataMember.Hoten;

            var deThi = _context.Dethis.ToList();
            var tkDt = _context.Tkdts
                .Where(x => deThi.Select(y => y.Id).Contains(x.Id))
                .Include(x => x.IdtaikhoanNavigation)
                .ToList();
            //var taiKhoanView= tkDt.Select(y=> new TaiKhoanViewModel
            //{
            //    Iddethi = y.Iddethi,
            //    Idtaikhoan = y.Idtaikhoan,
            //    Hoten = y.IdtaikhoanNavigation.Hoten,

            //}
            //).ToList();
            //List < Traloi> cautraloi = new List<Traloi>();
            //foreach (var traloi in danhSachCauHoi)
            //{

            //	var Iddtch = _context.Dtches.Where(x => x.Iddethi == id && x.Idcauhoi == traloi.Id).FirstOrDefault().Id;
            //	var tl = new Traloi()
            //	{
            //		Iddtch = Iddtch,
            //		PaChon = null,
            //		PaDung = traloi.PaDung,
            //		Lan = 1,
            //		Ngaythi = DateTime.Now,
            //	};

            //	_context.Tralois.Add(tl);
            //	_context.SaveChanges();
            //}

            ViewBag.Thoigian = ttDethi.Thoigian;
            ViewBag.Socauhoi = ttDethi.Socauhoi;
            ViewBag.DanhSachCauHoi = danhSachCauHoi;
            ViewBag.taiKhoanView = taiKhoanView;
            ViewBag.deThiId = id;
            return View(danhSachCauHoi);
        }

        [HttpPost]
        public IActionResult TraLoi(string tralois )
        {
            var list = JsonConvert.DeserializeObject<List<TraloiViewModel>>(tralois);
			
			if (list == null)
            {
                return BadRequest("Không có phương án nào được gửi.");
            }

            try
            {
                int soCauDung = 0;
                //int tongSoCau = list.Count;
              
                var dataMember = JsonConvert.DeserializeObject<Taikhoan>(HttpContext.Session.GetString("TaiKhoan"));

                int lanThi = _context.Tkdts.Count(x => x.Idtaikhoan == dataMember.Id && x.Iddethi == list.FirstOrDefault().DeThiId) + 1;
				var kQua = new Tkdt
				{
					Idtaikhoan = dataMember.Id,
					Iddethi = list.FirstOrDefault()?.DeThiId ?? 0,
					Tyle = "",
					Ngaythi = DateTime.Now,
					Lan = lanThi,
				};
				_context.Tkdts.Add(kQua);
				_context.SaveChanges();

                var tkdt = _context.Tkdts.FirstOrDefault(x => x.Idtaikhoan == dataMember.Id
                                && x.Iddethi == list.FirstOrDefault().DeThiId
                                && x.Lan == lanThi);
                var idtkdt = tkdt.Id;
				foreach (var traloi in list)
                {

                    var cauhoi = _context.Cauhois.Where(x => x.Id == traloi.CauHoiId).FirstOrDefault();
                    var Iddtch = _context.Dtches.Where(x => x.Iddethi == traloi.DeThiId && x.Idcauhoi == traloi.CauHoiId).FirstOrDefault().Id;
                    var tl = new Traloi()
                    {
                        Iddtch = Iddtch,
                        PaChon = traloi.PhuongAn,
                        PaDung = cauhoi.PaDung,
                        Lan = lanThi,
                        Ngaythi = DateTime.Now,
                        Tkdtid = idtkdt,
                    };
                    _context.Tralois.Add(tl);
                    _context.SaveChanges();
                    if (tl.PaChon == cauhoi.PaDung)
                    {
                        soCauDung++;
                    }


                }
                
                ViewBag.Taikhoans = dataMember;
                var tongSoCau = _context.Dethis.Where(x => x.Id == kQua.Iddethi).Select(x=>x.Socauhoi).FirstOrDefault();

                //  int tongSoCau = _context.Dethis.Select(x => x.Socauhoi).Where(x => x.Id == )

                tkdt.Tyle = soCauDung + "/" + tongSoCau;
                TempData["TyLe"] = soCauDung + "/" + tongSoCau;
                _context.Tkdts.Update(tkdt);
                _context.SaveChanges();
                //return RedirectToAction("KetQua", new { tyLeDung = tyLeDung });

                return Json(new { success = true, message = "Các phương án đã được lưu thành công." });

            }
            catch (Exception ex)
            {
               
                return StatusCode(500, $"Đã xảy ra lỗi: {ex.Message}");
            }
        }

        public IActionResult KetQua()
        {
            ViewBag.TyLeDung = TempData["TyLe"];
            return View();
        }
        public IActionResult XemLai(int? idtkdt, int? iddethi, int lan)
        {
            var dataMember = JsonConvert.DeserializeObject<Taikhoan>(HttpContext.Session.GetString("TaiKhoan"));
            var xemLaiBai = new List<LichsuThiViewModel>();
            var danhSachCauHoi = _context.Cauhois
                .Where(c => _context.Dtches.Any(dtch => dtch.Iddethi == iddethi && dtch.Idcauhoi == c.Id))
                .ToList();
            foreach (var cauhoi in danhSachCauHoi)
            {

                var dTCH = _context.Dtches
                    .Where(x => x.Idcauhoi == cauhoi.Id && x.Iddethi == iddethi)
                    .FirstOrDefault();

                if (dTCH != null)
                {
                    var paChon = _context.Tralois
                        .Where(x => x.Iddtch == dTCH.Id && x.Lan == lan && x.Tkdtid == idtkdt)
                        .Select(x => x.PaChon)
                        .FirstOrDefault();
                    var idTK = dataMember.Id;

					xemLaiBai.Add(new LichsuThiViewModel()
                    {
                        Noidung = cauhoi.Noidung,
                        PaA = cauhoi.PaA,
                        PaB = cauhoi.PaB,
                        PaC = cauhoi.PaC,
                        PaD = cauhoi.PaD,
                        PaDung = cauhoi.PaDung,
                        idde = dTCH.Iddethi,
                        PaChon = paChon,
                        idTaiKhoan = idTK,
                    });
                }
            }

            return View(xemLaiBai);
        }

    }

}

