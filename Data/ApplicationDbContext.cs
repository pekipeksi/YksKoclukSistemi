using Microsoft.EntityFrameworkCore;
using YksKoclukSistemi.Models;

namespace YksKoclukSistemi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<HaftalikProgram> HaftalikProgramlar { get; set; }

        public DbSet<Gorev> Gorevler { get; set; }
        public DbSet<Ders> Dersler { get; set; }
        public DbSet<Log> Loglar { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ders>().HasData(
                new Ders { Id = 1, Ad = "TYT Matematik" },
                new Ders { Id = 2, Ad = "AYT Matematik" },
                new Ders { Id = 3, Ad = "Geometri" },
                new Ders { Id = 4, Ad = "Fizik" },
                new Ders { Id = 5, Ad = "Kimya" },
                new Ders { Id = 6, Ad = "Biyoloji" },
                new Ders { Id = 7, Ad = "Türkçe" },
                new Ders { Id = 8, Ad = "Tarih" },
                new Ders { Id = 9, Ad = "Coğrafya" }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}