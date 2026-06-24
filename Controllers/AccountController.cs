using Microsoft.AspNetCore.Mvc;
using YksKoclukSistemi.Models;
using YksKoclukSistemi.Data;
using YksKoclukSistemi.ViewModels;
using System.Linq;

namespace YksKoclukSistemi.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var kullanici = _context.Kullanicilar
                .FirstOrDefault(x => x.Email == model.Email && x.Sifre == model.Sifre);

            if (kullanici == null)
            {
              
                Logla("Hata", "Giriş Başarısız", $"{model.Email} hatalı şifre veya mail ile denedi.");

                BildirimEkle("E-posta veya şifre hatalı!", false);
                return View(model);
            }

            
            Logla("Bilgi", "Giriş Başarılı", $"{kullanici.Ad} {kullanici.Soyad} ({kullanici.Rol}) giriş yaptı.");

            HttpContext.Session.SetInt32("KullaniciId", kullanici.Id);
            HttpContext.Session.SetString("Ad", kullanici.Ad);
            HttpContext.Session.SetString("Rol", kullanici.Rol);

            
            HttpContext.Session.SetString("Email", kullanici.Email);

            string rol = kullanici.Rol.Trim();

            if (rol == "Yonetici") return RedirectToAction("Index", "Admin");
            else if (rol == "Koc") return RedirectToAction("Index", "Koc");
            else return RedirectToAction("Index", "Ogrenci");
        }

        [HttpGet]
        public IActionResult Kayit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Kayit(KayitVM model)
        {
            if (!ModelState.IsValid) return View(model);

            if (_context.Kullanicilar.Any(x => x.Email == model.Email))
            {
                Logla("Uyarı", "Kayıt Hatası", $"{model.Email} zaten sistemde kayıtlı.");
                BildirimEkle("Bu e-posta adresi zaten kullanımda!", false);
                return View(model);
            }

            var yeniKullanici = new Kullanici
            {
                Ad = model.Ad,
                Soyad = model.Soyad,
                Email = model.Email,
                Sifre = model.Sifre,
                Rol = model.Rol,
                Alan = model.Rol == "Ogrenci" ? model.Alan : null
            };

            _context.Kullanicilar.Add(yeniKullanici);
            _context.SaveChanges();

            
            Logla("Bilgi", "Yeni Kayıt", $"{model.Email} adresiyle yeni bir {model.Rol} kaydoldu.");

            BildirimEkle("Kayıt başarılı! Lütfen giriş yapınız.", true);
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            
            Logla("Bilgi", "Çıkış", "Kullanıcı çıkış yaptı.");

            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}