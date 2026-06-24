using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
using YksKoclukSistemi.Data;
using YksKoclukSistemi.Models;

namespace YksKoclukSistemi.Controllers
{
    public class KocController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public KocController(ApplicationDbContext context)
        {
            _context = context;
        }

        private bool OturumKapaliMi()
        {
            var rol = HttpContext.Session.GetString("Rol");
            return rol != "Koc";
        }

        public IActionResult Index()
        {
            if (OturumKapaliMi()) return RedirectToAction("Login", "Account");

            var kocId = HttpContext.Session.GetInt32("KullaniciId");
            var ogrencilerim = _context.Kullanicilar.Where(x => x.KocId == kocId && x.Rol == "Ogrenci").ToList();

            var kocBilgisi = _context.Kullanicilar.Find(kocId);
            int toplamKontenjan = kocBilgisi.Kontenjan ?? 0;
            int mevcutOgrenci = ogrencilerim.Count;
            int kalanKontenjan = toplamKontenjan - mevcutOgrenci;

            ViewBag.TopKontenjan = toplamKontenjan;
            ViewBag.Mevcut = mevcutOgrenci;
            ViewBag.Kalan = kalanKontenjan;

            return View(ogrencilerim);
        }

        public IActionResult Programlar(int ogrenciId)
        {
            if (OturumKapaliMi()) return RedirectToAction("Login", "Account");

            var ogrenci = _context.Kullanicilar.Find(ogrenciId);
            if (ogrenci == null) return RedirectToAction("Index");

            ViewBag.Ogrenci = ogrenci;

            var programlar = _context.HaftalikProgramlar
                                     .Where(x => x.OgrenciId == ogrenciId)
                                     .Include(x => x.Gorevler)
                                     .OrderByDescending(x => x.BaslangicTarihi)
                                     .ToList();

            return View(programlar);
        }

        [HttpPost]
        public IActionResult HaftaEkle(int ogrenciId, DateTime baslangicTarihi)
        {
            if (OturumKapaliMi()) return RedirectToAction("Login", "Account");

            if (baslangicTarihi == DateTime.MinValue) baslangicTarihi = DateTime.Today;
            DateTime bitis = baslangicTarihi.AddDays(6);

            var yeniProgram = new HaftalikProgram
            {
                OgrenciId = ogrenciId,
                BaslangicTarihi = baslangicTarihi,
                BitisTarihi = bitis
            };

            _context.HaftalikProgramlar.Add(yeniProgram);
            _context.SaveChanges();

            
            Logla("Bilgi", "Program Oluşturma", $"Öğrenci ID: {ogrenciId} için {baslangicTarihi.ToShortDateString()} haftası oluşturuldu.");

            BildirimEkle("Yeni hafta programı oluşturuldu.");
            return RedirectToAction("Programlar", new { ogrenciId = ogrenciId });
        }

        public IActionResult ProgramDetay(int programId)
        {
            if (OturumKapaliMi()) return RedirectToAction("Login", "Account");

            if (!_context.Dersler.Any())
            {
                var yuklenecekDersler = new List<Ders>
                {
                    new Ders { Ad = "TYT Matematik" }, new Ders { Ad = "TYT Türkçe" },
                    new Ders { Ad = "TYT Fizik" }, new Ders { Ad = "TYT Kimya" },
                    new Ders { Ad = "TYT Biyoloji" }, new Ders { Ad = "TYT Tarih" },
                    new Ders { Ad = "AYT Matematik" }, new Ders { Ad = "AYT Edebiyat" },
                    new Ders { Ad = "Geometri" }, new Ders { Ad = "Paragraf" }
                };
                _context.Dersler.AddRange(yuklenecekDersler);
                _context.SaveChanges();
            }

            var program = _context.HaftalikProgramlar
                                  .Include(x => x.Gorevler)
                                  .ThenInclude(g => g.Ders)
                                  .Include(x => x.Ogrenci)
                                  .FirstOrDefault(x => x.Id == programId);

            if (program == null) return RedirectToAction("Index");

            ViewBag.Dersler = _context.Dersler.OrderBy(d => d.Ad).ToList();

            return View(program);
        }

        [HttpPost]
        public IActionResult GorevEkle(int programId, int dersId, List<string> secilenGunler, string icerik)
        {
            if (OturumKapaliMi()) return RedirectToAction("Login", "Account");

            if (dersId > 0 && !string.IsNullOrEmpty(icerik) && secilenGunler != null && secilenGunler.Count > 0)
            {
                foreach (var gun in secilenGunler)
                {
                    var yeniGorev = new Gorev
                    {
                        HaftalikProgramId = programId,
                        Gun = gun,
                        DersId = dersId,
                        Aciklama = icerik,
                        YapildiMi = false
                    };
                    _context.Gorevler.Add(yeniGorev);
                }
                _context.SaveChanges();

                
                Logla("Bilgi", "Görev Ekleme", $"Program ID: {programId} için {secilenGunler.Count} adet görev eklendi. İçerik: {icerik}");

                BildirimEkle("Görevler başarıyla eklendi.");
            }
            else
            {
                BildirimEkle("Lütfen ders seçin, açıklama girin ve gün işaretleyin!", false);
            }

            return RedirectToAction("ProgramDetay", new { programId = programId });
        }

        public IActionResult GorevSil(int id)
        {
            if (OturumKapaliMi()) return RedirectToAction("Login", "Account");

            var gorev = _context.Gorevler.Find(id);

            if (gorev != null)
            {
                int progId = gorev.HaftalikProgramId;
                _context.Gorevler.Remove(gorev);
                _context.SaveChanges();

                
                Logla("Bilgi", "Görev Silme", $"Görev ID: {id} silindi.");

                return RedirectToAction("ProgramDetay", new { programId = progId });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult HaftaSil(int id)
        {
            if (OturumKapaliMi()) return RedirectToAction("Login", "Account");

            var program = _context.HaftalikProgramlar.Find(id);
            if (program != null)
            {
                int ogrenciId = program.OgrenciId;

                var gorevler = _context.Gorevler.Where(x => x.HaftalikProgramId == id).ToList();
                if (gorevler.Any())
                {
                    _context.Gorevler.RemoveRange(gorevler);
                }

                _context.HaftalikProgramlar.Remove(program);
                _context.SaveChanges();

                
                Logla("Uyarı", "Program Silme", $"Program ID: {id} ve içindeki tüm görevler silindi.");

                BildirimEkle("Program ve içeriği silindi.");
                return RedirectToAction("Programlar", new { ogrenciId = ogrenciId });
            }

            return RedirectToAction("Index");
        }
    }
}