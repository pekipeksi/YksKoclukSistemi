using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection; 
using YksKoclukSistemi.Services; 

namespace YksKoclukSistemi.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }

        
        public void BildirimEkle(string mesaj, bool basariliMi = true)
        {
            TempData["Mesaj"] = mesaj;
            TempData["BasariliMi"] = basariliMi;
        }

        
        public void Logla(string tur, string islem, string mesaj)
        {
            try
            {
               
                var logService = HttpContext.RequestServices.GetService<LogService>();

                var kullanici = HttpContext.Session.GetString("Email") ?? "Anonim";

                if (logService != null)
                {
                    logService.LogKaydet(tur, islem, mesaj, kullanici);
                }
            }
            catch
            {
                
            }
        }
    }
}