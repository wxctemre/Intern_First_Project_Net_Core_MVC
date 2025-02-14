using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OrsaAkademi.demo.models;
using OrsaAkademi.demo.models.Entity;
using OrsaAkademi.demo.models.Entity.vmmodel;
namespace OrsaAkademi.demo.WebApp.Models.Interface
{
    public interface IPersonellerService
    {
        Task<Personeller> PersonellerKaydetService(Personeller personeller);
        Task<List<vmPersonelListele>> PersonelleriListeleService();
        Task<List<MedyaKutuphanesi>> PersonelFotografiKaydetService(IFormCollection fotografyuklemealani);
        Task<GirisVeKayit> personelregisterlaService(GirisVeKayit registerpersonel);
        Task<Personeller> PersonelLoginService(GirisVeKayit loginpersonel);
        Task<bool> PersonelSil(int id);
    }
    public interface IGuncellemeService
    {
        Task<vmVeriGetir> GuncellemeVeriGetirService(int id);

        Task<Personeller> KisiyiGuncelleService(Personeller personel);
        Task<bool> PersonelFotografSilService(List<int> idler);

    }
    public interface ITekrarliAlanService
    {
        Task<List<ParamOkullar>> OkulGetirmeService();
        Task<bool> okulukaydetService(IFormCollection tekrarlialan);
       Task<bool> okulGuncelleService(IFormCollection tekrarlialan);
        Task<bool> OkulbilgisiSilService(int id);
        Task<bool> OkulmedyasilService(List<int> idler);
    }



}
