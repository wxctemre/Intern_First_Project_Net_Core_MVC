using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrsaAkademi.demo.models.Entity;
using OrsaAkademi.demo.WebApp.Models.Interface;
using OrsaAkademi.demo.WebApp.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrsaAkademi.demo.WebApp.Controllers
{
    public class TekrarliAlaninController : Controller
    {
        private readonly ITekrarliAlanService _TekrarliAlanService;

        public TekrarliAlaninController(ITekrarliAlanService tekrarlialanservice)
        {
            _TekrarliAlanService = tekrarlialanservice;
        }


        [HttpGet]
        [Route("OkullariGetir")]
        public async Task<List<ParamOkullar>> OkullariGetir()
        {
            var result = await _TekrarliAlanService.OkulGetirmeService();
            return result;
        }

        [HttpPost]
        public async Task<bool> tekrarlialanveriyolla(IFormCollection tekrarlialan)
        {

 
                var result = await _TekrarliAlanService.okulukaydetService(tekrarlialan);
                return result;
 
        }
        [HttpPut]
        public async Task<bool> tekrarlialanGuncelleAsync(IFormCollection tekrarlialan)
        {

            var result = await _TekrarliAlanService.okulGuncelleService(tekrarlialan);
            return result;



        }
        [HttpPut]
        [Route("/OkulbilgisiSil")]
        public async Task<bool> OkulbilgisiSil(int id)
        {
            var result = await _TekrarliAlanService.OkulbilgisiSilService(id);
            return result;
        }  
        [HttpDelete]
        [Route("/OkulMedyaSil")]
        public async Task<bool> OkulmedyaSil(List<int> idler)
        {
            var result = await _TekrarliAlanService.OkulmedyasilService(idler);
            return result;
        }



    }
}
