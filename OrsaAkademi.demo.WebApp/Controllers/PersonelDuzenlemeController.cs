using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrsaAkademi.demo.models.Entity;
using OrsaAkademi.demo.models.Entity.vmmodel;
using OrsaAkademi.demo.WebApp.Models.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrsaAkademi.demo.WebApp.Controllers
{
    public class PersonelDuzenlemeController : Controller
    {
        private readonly IGuncellemeService _GuncellemeService;

        public PersonelDuzenlemeController(IGuncellemeService GuncellemeService)
        {
            _GuncellemeService = GuncellemeService;
        }

        public IActionResult PersonelDuzenlemeSayfasi()
        {
            string userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
            {

                return RedirectToAction("KayitEkrani", "PersonelGirisVeKayit");
            }

            return View();
        }

        [HttpGet]
        public async Task<vmVeriGetir> GuncellemeGetir(int id)
        {
            var result = await _GuncellemeService.GuncellemeVeriGetirService(id);
            return result;
        }

        [HttpPut]
        public async Task<Personeller> PersoneliGuncelle(Personeller personel)
        {
            var result = await _GuncellemeService.KisiyiGuncelleService(personel);
            return result;

        }

        [HttpDelete]
        [Route("/FotografSil")]
        public async Task<bool> FotografSil(List<int> idler)
        {
            if (idler != null )
            {
                var result = await _GuncellemeService.PersonelFotografSilService(idler);
                return result;
            }
            else
            {
                return false;
            }

        }
   

    }

}
