using YksKoclukSistemi.Data;
using YksKoclukSistemi.Models;
using System;

namespace YksKoclukSistemi.Services
{
    public class LogService
    {
        private readonly ApplicationDbContext _context;

        
        public LogService(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public void LogKaydet(string tur, string islem, string mesaj, string kullanici)
        {
            var yeniLog = new Log
            {
                Tur = tur,          
                Islem = islem,      
                Mesaj = mesaj,      
                Kullanici = kullanici, 
                Tarih = DateTime.Now   
            };

            _context.Loglar.Add(yeniLog); 
            _context.SaveChanges();       
        }
    }
}