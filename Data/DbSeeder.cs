using YksKoclukSistemi.Models;

namespace YksKoclukSistemi.Data
{
    public static class DbSeeder
    {
        public static void YoneticiEkle(ApplicationDbContext context)
        {
            
            if (!context.Kullanicilar.Any(x => x.Rol == "Yonetici"))
            {
                var admin = new Kullanici
                {
                    Ad = "Süper",
                    Soyad = "Yönetici",
                    Email = "admin@yks.com",
                    Sifre = "123", 
                    Rol = "Yonetici",
                    Kontenjan = 0 
                };

                context.Kullanicilar.Add(admin);
                context.SaveChanges();
            }
        }
    }
}