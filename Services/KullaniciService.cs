using YksKoclukSistemi.Data;
using YksKoclukSistemi.Models;
using YksKoclukSistemi.ViewModels;

namespace YksKoclukSistemi.Services
{
    public class KullaniciService
    {
        private readonly ApplicationDbContext _context;

        public KullaniciService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Kullanici GirisYap(LoginVM model)
        {
            var kullanici = _context.Kullanicilar.FirstOrDefault(x => x.Email == model.Email && x.Sifre == model.Sifre);
            return kullanici;
        }

        public void KayitOl(KayitVM model, string rol)
        {
            var yeniKullanici = new Kullanici
            {
                Ad = model.Ad,
                Soyad = model.Soyad,
                Email = model.Email,
                Sifre = model.Sifre,
                Rol = rol,
                
                Kontenjan = (rol == "Koc") ? model.Kontenjan : null,
                Alan = (rol == "Ogrenci") ? model.Alan : null
            };

            _context.Kullanicilar.Add(yeniKullanici);
            _context.SaveChanges();
        }
    }
}