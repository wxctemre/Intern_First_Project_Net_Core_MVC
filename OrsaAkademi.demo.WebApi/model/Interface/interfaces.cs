using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OrsaAkademi.demo.models.Entity;
using OrsaAkademi.demo.models.Entity.vmmodel;
namespace OrsaAkademi.demo.WebApi.model.Interface
{
    public interface IPersonelEklemeService
    {
        Task<Personeller> PersonelKaydetService(Personeller personeller);
        Task<List<vmPersonelListele>> PersonelListeleService();
        Task<List<MedyaKutuphanesi>> PersonelMedyalarKaydet(List<MedyaKutuphanesi> Medyalar);
        Task<GirisVeKayit> PersonelRegisterService(GirisVeKayit RegisterPersonel);
        Personeller PersonelLoginService(GirisVeKayit LoginPersonel);
        Task<bool> PersonelSilService(int id);
    }
    public interface IGuncellemeService
    {
        Task<vmVeriGetir> VeriGetirService(int id);
        Task<Personeller> VeriyiGuncelleService(Personeller personeller);
        Task<bool> GuncellemePersonelFotografSilService(List<int> idler);

    }
    public interface ITekrarliAlanService
    {
        Task<List<ParamOkullar>> OkulGetirmeDbService();
        Task<bool> okulukaydetdbService(List<vmOkulKaydet> okullar);
        Task<bool> okulGuncelleDbService(List<vmOkulKaydet> okullar);
        Task<bool> okulusildbService(int id);
        Task<bool> okulmedyasildbService(List<int> idler);
    }
}
