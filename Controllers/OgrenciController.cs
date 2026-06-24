using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YksKoclukSistemi.Data;
using YksKoclukSistemi.Models;

namespace YksKoclukSistemi.Controllers
{
    public class OgrenciController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public OgrenciController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Rol") != "Ogrenci") return RedirectToAction("Login", "Account");

            var ogrenciId = HttpContext.Session.GetInt32("KullaniciId");

           
            var ogrenciBilgisi = _context.Kullanicilar
                                         .Include(u => u.Koc)
                                         .FirstOrDefault(u => u.Id == ogrenciId);

            
            ViewBag.KocAdSoyad = ogrenciBilgisi?.Koc != null
                                 ? $"{ogrenciBilgisi.Koc.Ad} {ogrenciBilgisi.Koc.Soyad}"
                                 : "Henüz Atanmadı";

          
            var programlar = _context.HaftalikProgramlar
                                     .Include(p => p.Gorevler) 
                                     .Where(p => p.OgrenciId == ogrenciId)
                                     .OrderByDescending(p => p.BaslangicTarihi)
                                     .ToList();

            return View(programlar);
        }

       
        public IActionResult ProgramDetay(int id)
        {
            var program = _context.HaftalikProgramlar
                                  .Include(p => p.Gorevler)
                                  .ThenInclude(g => g.Ders)
                                  .FirstOrDefault(p => p.Id == id);

            if (program == null) return RedirectToAction("Index");

            return View(program);
        }

        
        [HttpPost]
        public IActionResult GorevDurumDegistir(int id)
        {
            var gorev = _context.Gorevler.Find(id);
            if (gorev != null)
            {
                gorev.YapildiMi = !gorev.YapildiMi;
                _context.SaveChanges();
                return Json(new { success = true, yeniDurum = gorev.YapildiMi });
            }
            return Json(new { success = false });
        }
    }
}