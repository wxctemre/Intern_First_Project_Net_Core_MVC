using Microsoft.AspNetCore.Mvc;
using OrsaAkademi.demo.models.Entity;
using System.Threading.Tasks;
using OrsaAkademi.demo.WebApi.Service;
using System.Collections.Generic;
using OrsaAkademi.demo.WebApi.model.Interface;
using Microsoft.CodeAnalysis.CSharp;
using OrsaAkademi.demo.WebApi.Migrations;
using OrsaAkademi.demo.models.Entity.vmmodel;
namespace OrsaAkademi.demo.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PersonellerController : Controller
    {
        private readonly IPersonelEklemeService _personelservice;
        public PersonellerController(IPersonelEklemeService personelEklemeService)
        {
            _personelservice = personelEklemeService;

        }
        [Route("PersonelKaydet")]
        [HttpPost]
        public async Task<models.Entity.Personeller> PersonelKaydet(models.Entity.Personeller personeller)
        {
            var result = await _personelservice.PersonelKaydetService(personeller);
            return result;
        }
        [Route("PersonelMedyalarKaydet")]
        [HttpPost]
        public async Task<List<MedyaKutuphanesi>> PersonelMedyalarKaydet(List<MedyaKutuphanesi> Medyalar)
        {
            var result  = await _personelservice.PersonelMedyalarKaydet(Medyalar);
            return result;
        }


        [Route("PersonelListem")]
        [HttpGet]
        public async Task<List<vmPersonelListele>> PersonelListem()
        {
            var result = await _personelservice.PersonelListeleService();
                return result;
        }

        [HttpDelete]
        [Route("PersonelSil")]
        public async Task<bool> PersonelSil(int id)
        {

            var result = await _personelservice.PersonelSilService(id);
            return result;
        }

    }
}
