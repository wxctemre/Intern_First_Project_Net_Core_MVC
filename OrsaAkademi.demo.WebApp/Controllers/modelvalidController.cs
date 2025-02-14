using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OrsaAkademi.demo.models.Entity;
using System.Linq;

namespace OrsaAkademi.demo.WebApp.Controllers
{
    public class modelvalidController : Controller
    {
        [HttpPost] 
        public ActionResult validatemodel(Personeller personel)
        {
            if(!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, errors = errors });

            }
            else
            {
                return Json(new { success = true });
            }
            
        }


    }
}
