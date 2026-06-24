using System;
using System.Collections.Generic;

namespace YksKoclukSistemi.Models
{
    public class HaftalikProgram
    {
        public int Id { get; set; }

        public int OgrenciId { get; set; }
        public virtual Kullanici Ogrenci { get; set; }

        public int HaftaNumarasi { get; set; } 
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public List<Gorev> Gorevler { get; set; }
    }
}