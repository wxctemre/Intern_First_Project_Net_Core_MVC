using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrsaAkademi.demo.models.Entity;
using OrsaAkademi.demo.models.Entity.vmmodel;
using OrsaAkademi.demo.WebApi.model.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrsaAkademi.demo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TekrarliAlandbController : Controller
    {
        private readonly ITekrarliAlanService _tekrarlialanservice;
        public TekrarliAlandbController(ITekrarliAlanService TekrarlialanService)
        {
            _tekrarlialanservice = TekrarlialanService;

        }

        [HttpGet]
        [Route("OkulGetirmeDb")]
        public async Task<List<ParamOkullar>> OkulGetirmeDb()
        {
            var result = await _tekrarlialanservice.OkulGetirmeDbService();
            return result;
        }
        [HttpPost]
        [Route("okulkaydetdb")]
        public async Task<bool> okulkaydetdb(List<vmOkulKaydet> okullar)
        {
 
                var result = await _tekrarlialanservice.okulukaydetdbService(okullar);
                return result;
 
        }
        [HttpPut]
        [Route("okulguncelledb")]
        public async Task<bool> okulguncelledb(List<vmOkulKaydet>okullar)
        {
            var result =await _tekrarlialanservice.okulGuncelleDbService(okullar);
            return result;

        }
        [HttpPut]
        [Route("okulusildb/{id}")]
        public async Task<bool> okulusildb(int id)
        {
            var result= await _tekrarlialanservice.okulusildbService(id);
            return result;
        }       
        [HttpPut]
        [Route("okulmedyasildb")]
        public async Task<bool> okulmedyasildb(List<int> idler)
        {
            var result= await _tekrarlialanservice.okulmedyasildbService(idler);
            return result;
        }



    }
}
