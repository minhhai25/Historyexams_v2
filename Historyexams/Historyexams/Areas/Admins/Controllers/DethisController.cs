using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Historyexams.Models;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Drawing;
using Historyexams.ModelViews;
using System.Net.WebSockets;
using System.Collections;

namespace Historyexams.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class DethisController : Controller
    {
        private readonly HistoryexamsContext _context;

        public DethisController(HistoryexamsContext context)
        {
            _context = context;
        }

        // GET: Admins/Dethis
        public IActionResult Index(int? Idchuong,int? Idbaitest,int? Idmd)
        {
			
			
			var lstdethi = _context.Dethis.Include(d => d.IdbaitestNavigation).Include(d => d.IdchuongNavigation).Include(d => d.IdmucdoNavigation).ToList();
           
            ViewBag.Tenbaitest = new SelectList(_context.Baitests, "Id", "Tenbaitest",Idbaitest);
            ViewBag.Tenmd = new SelectList(_context.Mucdos, "Id", "Tenmd",Idmd);
            ViewBag.Tenchuong = new SelectList(_context.Chuongs, "Id", "Tenchuong", Idchuong);


			

			if (Idchuong>0 || Idbaitest>0 || Idmd>0 ) {
                lstdethi = lstdethi.Where(x => x.Idchuong == Idchuong && (Idbaitest == null || x.Idbaitest == Idbaitest) && (Idmd == null || x.Idmucdo == Idmd)).ToList();
            }
            return View(lstdethi);
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Dethis == null)
            {
                return NotFound();
            }

            var dethi = await _context.Dethis
                .Include(d => d.IdbaitestNavigation)
                .Include(d => d.IdchuongNavigation)
                .Include(d => d.IdmucdoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dethi == null)
            {
                return NotFound();
            }

            return View(dethi);
        }

        // GET: Admins/Dethis/Create
        public IActionResult Create(int? Idchuong, int? Idmd,int Idbaitest)
        {
            ViewBag.Tenbaitest = new SelectList(_context.Baitests, "Id", "Tenbaitest",Idbaitest);
            ViewBag.Tenmd = new SelectList(_context.Mucdos, "Id", "Tenmd",Idmd);
            ViewBag.Tenchuong = new SelectList(_context.Chuongs, "Id", "Tenchuong",Idchuong);
            return View();
        }

        // POST: Admins/Dethis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Madethi,Tendethi,Mota,Idmucdo,Idbaitest,Idchuong,Thoigian,Socauhoi,Ngaytao,Nguoitao,Isactive,Isdelete")] Dethi dethi)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //var baiTest=_context.Baitests.ToList();
                    dethi.Ngaytao = DateTime.Now;
                    dethi.Nguoitao = "MinhHai";
                    
                    _context.Add(dethi);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Không thể lưu thay đổi. Vui lòng thử lại, và nếu vấn đề vẫn tiếp diễn, hãy liên hệ với quản trị viên hệ thống.");
                    return View(dethi);
                }
            }
            ViewBag.Tenbaitest = new SelectList(_context.Baitests.ToList(), "Id", "Tenbaitest",dethi.Idbaitest);
            ViewBag.Tenmd = new SelectList(_context.Mucdos.ToList(), "Id", "Tenmd",dethi.Idmucdo);
            ViewBag.Tenchuong = new SelectList(_context.Chuongs.ToList(), "Id", "Tenchuong",dethi.Idchuong);

            return View(dethi);
        }

        // GET: Admins/Dethis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Dethis == null)
            {
                return NotFound();
            }

            var dethi = await _context.Dethis.FindAsync(id);
            if (dethi == null)
            {
                return NotFound();
            }
            ViewBag.Tenbaitest = new SelectList(_context.Baitests, "Id", "Tenbaitest");
            ViewBag.Tenmd = new SelectList(_context.Mucdos, "Id", "Tenmd");
            ViewBag.Tenchuong = new SelectList(_context.Chuongs, "Id", "Tenchuong");
            return View(dethi);
        }

        // POST: Admins/Dethis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Madethi,Tendethi,Mota,Idmucdo,Idbaitest,Idchuong,Thoigian,Socauhoi,Ngaytao,Nguoitao,Isactive,Isdelete")] Dethi dethi)
        {
            if (id != dethi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dethi.Ngaytao=DateTime.Now;
					//dethi.Ngaytao = _context.Dethis.Where(x => x.Id == dethi.Id).FirstOrDefault().Ngaytao;
					_context.Update(dethi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DethiExists(dethi.Id))
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
            ViewBag.Tenbaitest = new SelectList(_context.Baitests, "Id", "Tenbaitest");
            ViewBag.Tenmd = new SelectList(_context.Mucdos, "Id", "Tenmd");
            ViewBag.Tenchuong = new SelectList(_context.Chuongs, "Id", "Tenchuong");
            return View(dethi);
        }

        // GET: Admins/Dethis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Dethis == null)
            {
                return NotFound();
            }

            var dethi = await _context.Dethis
                .Include(d => d.IdbaitestNavigation)
                .Include(d => d.IdchuongNavigation)
                .Include(d => d.IdmucdoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dethi == null)
            {
                return NotFound();
            }

            return View(dethi);
        }

        // POST: Admins/Dethis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Dethis == null)
            {
                return Problem("Entity set 'HistoryexamsContext.Dethis'  is null.");
            }
            var dethi = await _context.Dethis.FindAsync(id);
            if (dethi != null)
            {
                _context.Dethis.Remove(dethi);
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
                        var obj = _context.Dethis.Find(Convert.ToInt32(item));
                        if(obj != null)
                        {
                            var traLoi= _context.Tralois.Any(x=>x.Iddtch==obj.Id);
                            if (traLoi)
                            {
								return Json(new { success = false });
							}
                        }
                        _context.Dethis.Remove(obj);
                        _context.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        public IActionResult IsActive(int id)
        {
            var item = _context.Dethis.Find(id);
            if (item != null)
            {
                item.Isactive = !item.Isactive;
                var entry = _context.Entry<Dethi>(item);
                entry.State = EntityState.Modified;
                //_db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return Json(new { success = true, IsActive = item.Isactive });
            }
            return Json(new { success = false });
        }
        public async Task<IActionResult> Qlcauhoi(int? id)
        {
			
			if (id == null || _context.Dethis == null)
            {
                return NotFound();
            }

            var dethi = await _context.Dethis
                .Include(d => d.IdbaitestNavigation)
                .Include(d => d.IdchuongNavigation)
                .Include(d => d.IdmucdoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dethi == null)
            {
                return NotFound();
            }
            // Lấy danh sách các câu hỏi tương ứng với đề thi

            var danhSachCauHoi = await _context.Cauhois
                .Where(c => _context.Dtches.Any(dtch => dtch.Iddethi == id && dtch.Idcauhoi == c.Id))
                .ToListAsync();

            ViewBag.DanhSachCauHoi = danhSachCauHoi;

            return View(dethi); 
        }
		public IActionResult XoaCHDT(int? id)
		{
            if (id == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy câu hỏi nào cho đề thi này.";
                return RedirectToAction("Qlcauhoi", new { id = id });
            }
				var dtches= _context.Dtches.Where(x=>x.Iddethi==id).ToList();
            
			if (dtches == null || dtches.Count == 0)
			{
                return NotFound();
				
			}
            else
            {
                foreach(var dtch in dtches)
                {
                    var traLoi = _context.Tralois.Any(x=>x.Iddtch==dtch.Id);
                    if (traLoi)
                    {
                        TempData["ErrorMessage"] = "Không thể xóa vì đã có người thực hiện thi đề này!";
						return RedirectToAction("Qlcauhoi", new { id = id });
					}
                }
				_context.Dtches.RemoveRange(dtches);
				_context.SaveChanges();
			}
			return RedirectToAction("Qlcauhoi", new { id = id });

		}

		public async Task<IActionResult> Xoacauhoi(int id,int deThiId)  
        {   
            try
            {
               
			
				var cauHoi = await _context.Dtches.Where(x => x.Idcauhoi == id && x.Iddethi == deThiId).FirstOrDefaultAsync() ;
				
				if (cauHoi == null)
                {

                    return Json(new { success = false, message = "Câu hỏi không tồn tại." });
				}

				var checkCauTraLoi = await _context.Tralois.Where(x => x.Iddtch == cauHoi.Id).FirstOrDefaultAsync();
				
				if(checkCauTraLoi ==null)
                {
					_context.Dtches.Remove(cauHoi);
                }
                else
                {
					return Json(new { success = false, message = "Câu hỏi đã có câu trả lời." });
				}

                _context.SaveChanges();

              
                return Json(new { success = true, message = "Đã xóa được câu hỏi." }); ;
            }
            catch (Exception ex)
            {
                // Trả về thông báo lỗi nếu có lỗi xảy ra
                return Json(new { success = false, message = "Đã xảy ra lỗi khi xóa câu hỏi." });
            }

        }
        private bool DethiExists(int id)
        {
            return (_context.Dethis?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        
        public IActionResult ThemCHVaoDeThi(int? id, int? Idchuong, int? Idmd)
        {
			ViewBag.IdDethi = id;

			if (!Idchuong.HasValue || !Idmd.HasValue || Idchuong == 0 || Idmd == 0)
            {
               
                return RedirectToAction("ThemCHTH", new { id = id });
            }
            var model = new List<CauHoiViewModel>();
            var dtch = _context.Dtches.Where(d=>d.Iddethi == id).Select(x=>x.Idcauhoi).ToList();
			var soCauHoi = _context.Dethis.Where(x => x.Id == id).Select(x => x.Socauhoi).FirstOrDefault();
			var cauhois = _context.Cauhois.Where(x => !_context.Dtches.Where(x => x.Iddethi == id).Select(x => x.Idcauhoi).Contains(x.Id)).ToList();
			if (Idmd > 0 && Idchuong > 0)
			{
				cauhois = cauhois.Where(x => x.Idchuong == Idchuong && x.Idmucdo == Idmd).ToList();
			}
			if (dtch.Count() >= soCauHoi)
			{
				TempData["ErrorMessage"] = "Đã có đủ câu hỏi ";
				return RedirectToAction("Qlcauhoi", new { id = id });
			}
			foreach (var item in cauhois)
            {
                var cauhoi = new CauHoiViewModel
                {
                    Id = item.Id,
                    Macauhoi = item.Macauhoi,
                    Noidung = item.Noidung,
                    IsChecked = false,
                    Idchuong=item.Idchuong,
                    Idmucdo=item.Idmucdo
                };
                model.Add(cauhoi);
            }
           
            var tenMD = _context.Mucdos.Where(x => x.Id == Idmd).FirstOrDefault();
            var Tenchuong = _context.Chuongs.Where(x => x.Id == Idchuong).FirstOrDefault();
           
                ViewBag.Tenmd = tenMD.Tenmd;
                ViewBag.Tenchuong = Tenchuong.Tenchuong;

                ViewBag.Idmucdo = Idmd;
                ViewBag.Idchuong = Idchuong;
              
                return View(model);
            
            
        }
        public IActionResult ThemCHTH( int Idmd ,int Idchuong  , int id) {
			ViewBag.IdDethi = id;
			var model = new List<CauHoiViewModel>();
			var dtch = _context.Dtches.Where(d => d.Iddethi == id).Select(x => x.Idcauhoi).ToList();

			var lstCauHoi = _context.Cauhois.Include(x=>x.IdchuongNavigation).Include(y=>y.IdmucdoNavigation).ToList();
            var soCauHoi = _context.Dethis.Where(x => x.Id == id).Select(x => x.Socauhoi).FirstOrDefault();
            ViewBag.Tenmd = new SelectList(_context.Mucdos, "Id", "Tenmd", Idmd);
            ViewBag.Tenchuong = new SelectList(_context.Chuongs, "Id", "Tenchuong", Idchuong);
            var soCauThieu = soCauHoi - dtch.Count();
            ViewBag.soCauThieu = soCauThieu;

			if (Idchuong>0 || Idmd > 0)
            {
				lstCauHoi = lstCauHoi.Where(x => x.Idchuong == Idchuong || Idchuong==null  && (Idmd == null || x.Idmucdo == Idmd)).ToList();
			}
			if (dtch.Count() >= soCauHoi)
			{
				TempData["ErrorMessage"] = "Đã có đủ câu hỏi ";
				return RedirectToAction("Qlcauhoi", new { id = id });
			}
				foreach (var item in lstCauHoi)
			{
				var cauhoi = new CauHoiViewModel
				{
					Id = item.Id,
					Macauhoi = item.Macauhoi,
					Noidung = item.Noidung,
					IsChecked = false,
					Idchuong = item.Idchuong,
					Idmucdo = item.Idmucdo
				};
				model.Add(cauhoi);
			}
			return View(model);
        }
        [HttpPost]
        public IActionResult ThemCHVaoDeThi(List<CauHoiViewModel> model, int idDethi)
        {

            // lấy iddt và idcauhoi => thêm vào bảng dtch
            try
            {
                foreach (var item in model)
                {
                    if(item.IsChecked == true)
                    {
                        var dtch = new Dtch()
                        {
                            Iddethi = idDethi,
                            Idcauhoi = item.Id,
                        };
                        _context.Dtches.Add(dtch);
                    }
                }
                _context.SaveChanges();
                return RedirectToAction("Qlcauhoi", new { id = idDethi });
            }
            catch (Exception)
            {

                throw;
            }
            //return View(model);
        }
        public IActionResult SinhCH(int? id , int Idchuong,int idMd)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dsCauHoi= _context.Cauhois.Where(x=>x.Idchuong== Idchuong && x.Idmucdo==idMd).ToList();
            var soCauHoi = _context.Dethis.Where(x => x.Id == id).Select(x=>x.Socauhoi).FirstOrDefault();
            var thoiGian = _context.Dethis.Where(x => x.Id == id).Select(x=>x.Thoigian).FirstOrDefault();
            var cauHoiDT = _context.Dtches.Count(x => x.Iddethi == id);
            if (idMd!=null && Idchuong==null){
                dsCauHoi = _context.Cauhois.Where(x =>  x.Idmucdo == idMd).ToList();
            }
            else
            {
               if (idMd == null && Idchuong != null) {
					dsCauHoi = _context.Cauhois.Where(x => x.Idchuong == Idchuong).ToList();
                }
                else
                {
					dsCauHoi = _context.Cauhois.ToList();
				}

			}
            if (soCauHoi > dsCauHoi.Count)
            {
				TempData["ErrorMessage"] = "Số lượng câu hỏi vượt quá số lượng yêu cầu";
				return RedirectToAction("Qlcauhoi", new { id = id });
			}
            if (cauHoiDT >= soCauHoi)
            {
                //return BadRequest("Đã có đủ câu hỏi ");
                TempData["ErrorMessage"] = "Đã có đủ câu hỏi ";
                return RedirectToAction("Qlcauhoi", new { id = id });
			}

            var random = new Random();
            var selectedCauHoiIds= new List<int>();


            var tempDsCauHoi = new List<Cauhoi>(dsCauHoi);

            while (selectedCauHoiIds.Count() < soCauHoi)
            {
                int randomIndex = random.Next(tempDsCauHoi.Count);
                var selectedCauHoi = tempDsCauHoi[randomIndex];

                selectedCauHoiIds.Add(selectedCauHoi.Id);
                tempDsCauHoi.RemoveAt(randomIndex); 
            }
           
            foreach (var cauHoiId in selectedCauHoiIds)
			{
				var dtch = new Dtch
				{
					Iddethi = id.Value,
					Idcauhoi = cauHoiId
				};
                
				_context.Dtches.Add(dtch);
			}
            

            _context.SaveChanges();

			return RedirectToAction("Qlcauhoi", new { id = id });
		
        }
     
	}
}
