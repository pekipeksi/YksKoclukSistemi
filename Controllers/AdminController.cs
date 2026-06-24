using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using YksKoclukSistemi.Data;
using YksKoclukSistemi.Models;
using YksKoclukSistemi.ViewModels;

namespace YksKoclukSistemi.Controllers
{
    public class AdminController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string? ad, string? rol, string? alan, string? durum)
        {
            if (HttpContext.Session.GetString("Rol")?.Trim() != "Yonetici")
            {
                return RedirectToAction("Login", "Account");
            }

            var sorgu = _context.Kullanicilar.Include(x => x.Koc).AsQueryable();

            if (!string.IsNullOrEmpty(ad))
            {
                sorgu = sorgu.Where(x => x.Ad.Contains(ad) || x.Soyad.Contains(ad));
            }

            if (!string.IsNullOrEmpty(rol))
            {
                sorgu = sorgu.Where(x => x.Rol == rol);
            }

            if (!string.IsNullOrEmpty(alan))
            {
                sorgu = sorgu.Where(x => x.Alan == alan);
            }

            if (!string.IsNullOrEmpty(durum))
            {
                if (durum == "Atanmis")
                {
                    sorgu = sorgu.Where(x => x.KocId != null);
                }
                else if (durum == "Atanmamis")
                {
                    sorgu = sorgu.Where(x => x.KocId == null && x.Rol == "Ogrenci");
                }
            }

            ViewBag.Koclar = _context.Kullanicilar
                .Where(x => x.Rol == "Koc")
                .Select(k => new
                {
                    k.Id,
                    k.Ad,
                    k.Soyad,
                    Kontenjan = k.Kontenjan ?? 20,
                    Dolu = _context.Kullanicilar.Count(u => u.KocId == k.Id)
                })
                .ToList();

            var model = new KullaniciAramaVM
            {
                AdAra = ad,
                RolAra = rol,
                AlanAra = alan,
                DurumAra = durum,
                Kullanicilar = sorgu.OrderByDescending(x => x.Id).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult KocAta(int ogrenciId, int kocId)
        {
            var ogrenci = _context.Kullanicilar.Find(ogrenciId);
            if (ogrenci != null)
            {
                ogrenci.KocId = kocId;
                _context.SaveChanges();

               
                Logla("Bilgi", "Koç Atama", $"{ogrenci.Ad} {ogrenci.Soyad} isimli öğrenciye koç atandı (Koç ID: {kocId}).");

                BildirimEkle($"{ogrenci.Ad} adlı öğrenciye koç atandı.");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult KullaniciSil(int id)
        {
            var user = _context.Kullanicilar.Find(id);
            if (user != null)
            {
               
                string silinenIsim = $"{user.Ad} {user.Soyad} ({user.Email})";

                _context.Kullanicilar.Remove(user);
                _context.SaveChanges();

               
                Logla("Uyarı", "Kullanıcı Silme", $"{silinenIsim} isimli kullanıcı silindi.");

                BildirimEkle("Kullanıcı silindi.", false);
            }
            return RedirectToAction("Index");
        }
    }
}