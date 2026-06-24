using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YksKoclukSistemi.Models
{
    public class Kullanici
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Ad")]
        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        public string Ad { get; set; }

        [Display(Name = "Soyad")]
        [Required(ErrorMessage = "Soyad alanı zorunludur.")]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "E-posta zorunludur.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur.")]
        public string Sifre { get; set; }

        [Required]
        public string Rol { get; set; } 

        public int? Kontenjan { get; set; }
        public string? Alan { get; set; } 

        public int? KocId { get; set; }

        [ForeignKey("KocId")]
        public virtual Kullanici? Koc { get; set; }

        [InverseProperty("Koc")]
        public virtual ICollection<Kullanici> Ogrencilerim { get; set; }

       

    }
}