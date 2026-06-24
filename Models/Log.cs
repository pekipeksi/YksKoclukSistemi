using System;

namespace YksKoclukSistemi.Models
{
    public class Log
    {
        public int Id { get; set; }

        public string? Tur { get; set; }

        public string? Islem { get; set; }
        public string? Mesaj { get; set; }
        public DateTime Tarih { get; set; } = DateTime.Now;
        public string? Kullanici { get; set; }
    }
}