using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrsaAkademi.demo.models;
using OrsaAkademi.demo.models.Entity;
using OrsaAkademi.demo.models.Entity.vmmodel;
using OrsaAkademi.demo.WebApp.Models.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace OrsaAkademi.demo.WebApp.Controllers
{
    public class PersonelEklemeController : Controller
    {
        private readonly IPersonellerService _PersonellerService;

        public PersonelEklemeController(IPersonellerService personellerService)
        {
            _PersonellerService = personellerService;
        }

        
        public IActionResult PersonelListesi()
        {
            //string userEmail = HttpContext.Session.GetString("UserEmail");
            //if (string.IsNullOrEmpty(userEmail))
            //{

            //    return RedirectToAction("KayitEkrani", "PersonelGirisVeKayit");
            //}

            return View();
        }
        [HttpGet]
        public async Task<List<vmPersonelListele>> PersonelListeleme()
        {
            var result = await _PersonellerService.PersonelleriListeleService();
                return result;
        }
        public IActionResult PersonelEkleme()
        {
            string userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
            {

                return RedirectToAction("KayitEkrani", "PersonelGirisVeKayit");
            }
            return View();

        }
        


        [HttpPost]
        public async Task<Personeller> PersonelEkleme2(Personeller personeller)
        {
            var result =await _PersonellerService.PersonellerKaydetService(personeller);
            return result;
        }



        [HttpPost]
        public async Task<List<MedyaKutuphanesi>> PersonelFotografiKaydet(IFormCollection fotografyuklemealani)
        {
            if (fotografyuklemealani.Files.Count > 0)
            {
                var result = await _PersonellerService.PersonelFotografiKaydetService(fotografyuklemealani);
                return result;
            }
            else
            {
                return null;
            }
        }


        [HttpDelete]
        public async Task<bool> PersonelSil(int id)
        {
            var result = await _PersonellerService.PersonelSil(id);

            return result;
        }





    }
}
