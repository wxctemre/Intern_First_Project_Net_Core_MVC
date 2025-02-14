using Microsoft.AspNetCore.Mvc;
using OrsaAkademi.demo.models.Entity;
using OrsaAkademi.demo.WebApi.model.Interface;
using System.Threading.Tasks;
using OrsaAkademi.demo.models.Entity.vmmodel;
using System.Collections.Generic;
namespace OrsaAkademi.demo.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class GuncellemedbController : Controller
    {
        private readonly IGuncellemeService _GuncellemeService;
        public GuncellemedbController(IGuncellemeService GuncellemeService)
        {
            _GuncellemeService = GuncellemeService;

        }

        [HttpGet]
        public async Task<vmVeriGetir> VeriGetir(int id)
        {
            var result=await _GuncellemeService.VeriGetirService(id);
            return result;
        }
        [Route("GuncellemeIslemi")]
        [HttpPut]
        public async Task<Personeller> GuncellemeIslemi(Personeller personeller)
        {
            var result = await _GuncellemeService.VeriyiGuncelleService(personeller);
            return result;
        }
        [HttpPut]
        [Route("MedyalariSil")]
        public async Task<bool> MedyalariSil([FromBody] List<int> idler)
        {
            var result = await _GuncellemeService.GuncellemePersonelFotografSilService(idler);
            return result;
        }


    }
}
