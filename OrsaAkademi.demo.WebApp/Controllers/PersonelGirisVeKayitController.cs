using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrsaAkademi.demo.models.Entity;
using OrsaAkademi.demo.WebApp.Models.Interface;
using System.Threading.Tasks;

namespace OrsaAkademi.demo.WebApp.Controllers
{
    public class PersonelGirisVeKayitController : Controller
    {
        private readonly IPersonellerService _PersonellerService;

        public PersonelGirisVeKayitController(IPersonellerService personellerService)
        {
            _PersonellerService = personellerService;
        }


        [HttpPost]
        [Route("/kullaniciregister")]
        public Task<GirisVeKayit> kullaniciregister(GirisVeKayit kullanicikayit)
        {
            var result = _PersonellerService.personelregisterlaService(kullanicikayit);
            return result;
        }
        [HttpPut]
        [Route("/kullaniciLogin")]
        public async Task<Personeller> kullaniciLogin(GirisVeKayit kulllanicigiris)
        {

            var result = await _PersonellerService.PersonelLoginService(kulllanicigiris);
            if (result != null)
            {
                var userData = new
                {
                    Email = result.Email,
                    Ad = result.Ad,
                    soyad = result.Soyad
                };

                string userDataJson = JsonConvert.SerializeObject(userData);

                HttpContext.Session.SetString("UserEmail", userDataJson);

            }
            return result;
        }
        [HttpPost]
        [Route("/Logout")]
        public IActionResult Logout()
        {

            HttpContext.Session.Clear();
     
            return Ok();  
        }
        public IActionResult GirisEkrani()
        {
            string userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
            {

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        public IActionResult KayitEkrani()
        {

            return View();

        }
    }
}
