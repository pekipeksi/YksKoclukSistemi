using Microsoft.AspNetCore.Mvc;
using YksKoclukSistemi.Data;
using YksKoclukSistemi.Models;
using System.Linq;

namespace YksKoclukSistemi.Controllers
{
    
    [Route("api/ogrenciler")]
    [ApiController]
    public class OgrenciApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OgrenciApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public IActionResult GetOgrenciler()
        {
            var ogrenciler = _context.Kullanicilar
                                     .Where(x => x.Rol == "Ogrenci")
                                     .Select(x => new
                                     {
                                        
                                         x.Id,
                                         x.Ad,
                                         x.Soyad,
                                         x.Email,
                                         Alan = x.Alan ?? "Belirtilmemiş"
                                     })
                                     .ToList();

            return Ok(ogrenciler);
        }

        
        [HttpPost]
        public IActionResult OgrenciEkle([FromBody] KullaniciVeriModeli gelenVeri)
        {
            if (gelenVeri == null) return BadRequest("Veri gönderilmedi.");

            var yeniOgrenci = new Kullanici
            {
                Ad = gelenVeri.Ad,
                Soyad = gelenVeri.Soyad,
                Email = gelenVeri.Email,
                Sifre = "12345", 
                Rol = "Ogrenci",
                Alan = gelenVeri.Alan
            };

            _context.Kullanicilar.Add(yeniOgrenci);
            _context.SaveChanges();

            return Ok(new { mesaj = "Öğrenci başarıyla eklendi!", yeniId = yeniOgrenci.Id });
        }
    }

   
    public class KullaniciVeriModeli
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Email { get; set; }
        public string Alan { get; set; }
    }
}