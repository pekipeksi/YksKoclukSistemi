    using YksKoclukSistemi.Models;

namespace YksKoclukSistemi.ViewModels
{
    public class KullaniciAramaVM
    {
        public List<Kullanici> Kullanicilar { get; set; }
        public string? AdAra { get; set; }
        public string? RolAra { get; set; }
        public string? AlanAra { get; set; }
        public string? DurumAra { get; set; } 
    }
}