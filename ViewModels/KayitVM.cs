using System.ComponentModel.DataAnnotations;

namespace YksKoclukSistemi.ViewModels
{
    public class KayitVM
    {
        [Required(ErrorMessage = "Ad zorunludur.")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Soyad zorunludur.")]
        public string Soyad { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Sifre { get; set; }

        [Required]
        [Compare("Sifre", ErrorMessage = "Şifreler uyuşmuyor.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre Tekrar")]
        public string SifreTekrar { get; set; }

        [Required(ErrorMessage = "Lütfen bir rol seçiniz.")]
        public string Rol { get; set; }

       
        [Range(1, 20, ErrorMessage = "Kontenjan 1 ile 20 arasında olmalıdır.")]
        public int? Kontenjan { get; set; }
        public string? Alan { get; set; }
    }
}