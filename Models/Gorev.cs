using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YksKoclukSistemi.Models
{
    public class Gorev
    {
        [Key]
        public int Id { get; set; }

        public string Aciklama { get; set; } 
        public string Gun { get; set; } 
        public bool YapildiMi { get; set; } = false;

       
        public int HaftalikProgramId { get; set; }
        [ForeignKey("HaftalikProgramId")]
        public virtual HaftalikProgram HaftalikProgram { get; set; }

        public int? DersId { get; set; }
        [ForeignKey("DersId")]
        public virtual Ders? Ders { get; set; }
    }
}