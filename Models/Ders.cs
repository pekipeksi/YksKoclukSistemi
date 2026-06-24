using System.ComponentModel.DataAnnotations;

namespace YksKoclukSistemi.Models
{
    public class Ders
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Ders Adı")]
        public string Ad { get; set; } 

        public virtual ICollection<Gorev> Gorevler { get; set; }
    }
}