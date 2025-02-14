using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using OrsaAkademi.demo.models;
using OrsaAkademi.demo.models.Entity;
using OrsaAkademi.demo.WebApi.model.Interface;
namespace OrsaAkademi.demo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GirisVeRegisterController : Controller
    {
       
      
            private readonly IPersonelEklemeService _personelservice;
            public GirisVeRegisterController(IPersonelEklemeService personelEklemeService)
            {
                _personelservice = personelEklemeService;

            }
            [HttpPost]
            [Route("personeliregisterla")]
            public async Task<GirisVeKayit> personeliregisterla(GirisVeKayit registerpersonel)
            {
                var result = await _personelservice.PersonelRegisterService(registerpersonel);
                return result;
            }

            [HttpPut]
            [Route("PersonelLogin")]
            public  Personeller PersonelLogin(GirisVeKayit registerpersonel)
            {
                var result =   _personelservice.PersonelLoginService(registerpersonel);
                return result;
            }

        
    }
}