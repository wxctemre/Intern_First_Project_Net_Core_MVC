using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OrsaAkademi.demo.models.Entity;
using OrsaAkademi.demo.models.Entity.vmmodel;
using OrsaAkademi.demo.WebApi.model;
using OrsaAkademi.demo.WebApi.model.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrsaAkademi.demo.WebApi.Service
{
     
    public class TekrarliAlandbService: ITekrarliAlanService
    {
        private readonly ApplicationDbContext _db;
        public TekrarliAlandbService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<ParamOkullar>> OkulGetirmeDbService()
        {
            var okullar = await _db.paramokullar.ToListAsync();
            return okullar; 
        }

        public async Task<bool> okulukaydetdbService(List<vmOkulKaydet> okullar)
        {
            if(okullar != null)
            {
                foreach(var item in okullar)
                { var personel = item.OkulBilgileri;
                    personel.aktifMi = 1;
                    personel.silindiMi = 0;
                    await _db.okullar.AddAsync(personel);
                    await _db.SaveChangesAsync();
                    var personelid=personel.Id;
                    foreach(var medyakaydet in item.OkulMedyasi)
                    {
                        PersonelOkulMedyalariiliski personelmedyalar = new PersonelOkulMedyalariiliski();
                        await _db.MedyaKutuphanesi.AddAsync(medyakaydet);
                        await _db.SaveChangesAsync();
                        var medyaid= medyakaydet.Id;
                        personelmedyalar.PersonelTabloId=personelid;
                        personelmedyalar.MedyaID=(int)medyaid;
                        personelmedyalar.aktifMi = 1;
                        personelmedyalar.silindiMi = 0;
                        await _db.PersonelEgitimId.AddAsync(personelmedyalar);
                        await _db.SaveChangesAsync();

                    }
                }

              

                return true;

            }
            else
            {
                return false;
            }
        }
        public async Task<bool> okulGuncelleDbService(List<vmOkulKaydet> okullar)
        {
            if (okullar != null)
            {
                foreach (var item in okullar)
                {
                    var okulBilgileri = item.OkulBilgileri;
                    var existingOkul = await _db.okullar.FindAsync(okulBilgileri.Id);

                    if (existingOkul != null)
                    {
                  
                        existingOkul.Okulid = okulBilgileri.Okulid;
                        existingOkul.Mezunolduguyil = okulBilgileri.Mezunolduguyil;
                        existingOkul.personelid = okulBilgileri.personelid;
                      
                        existingOkul.aktifMi = okulBilgileri.aktifMi;
                        existingOkul.silindiMi = okulBilgileri.silindiMi;
                        existingOkul.sirano = okulBilgileri.sirano;

                        _db.okullar.Update(existingOkul);
                        await _db.SaveChangesAsync();
                    }
                    else
                    {
                     
                        okulBilgileri.aktifMi = 1;
                        okulBilgileri.silindiMi = 0;
                        await _db.okullar.AddAsync(okulBilgileri);
                        await _db.SaveChangesAsync();
                    }

                    var personelId = okulBilgileri.Id;

                   
                  

                 
                    foreach (var medyakaydet in item.OkulMedyasi)
                    {
                        PersonelOkulMedyalariiliski personelMedyalar = new PersonelOkulMedyalariiliski();
                        var existingMedia = await _db.MedyaKutuphanesi.FindAsync(medyakaydet.Id);

                        if (existingMedia == null)
                        {
                          
                            await _db.MedyaKutuphanesi.AddAsync(medyakaydet);
                            await _db.SaveChangesAsync();
                        }
                        else
                        {
                       
                            existingMedia.MedyaAdi = medyakaydet.MedyaAdi;
                            existingMedia.MedyaUrl = medyakaydet.MedyaUrl;
                            _db.MedyaKutuphanesi.Update(existingMedia);
                            await _db.SaveChangesAsync();
                        }

                        var medyaId = medyakaydet.Id;
                        personelMedyalar.PersonelTabloId = personelId;
                        personelMedyalar.MedyaID = (int)medyaId;
                        personelMedyalar.aktifMi = 1;
                        personelMedyalar.silindiMi = 0;
                        await _db.PersonelEgitimId.AddAsync(personelMedyalar);
                        await _db.SaveChangesAsync();
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> okulusildbService(int id)
        {
            var okul = _db.okullar.FirstOrDefault(x=>x.Id== id);
            okul.aktifMi = 0;
            okul.silindiMi = 1;
           _db.okullar.Update(okul);
          await _db.SaveChangesAsync();
            var personelokulmedyalar = _db.PersonelEgitimId.Where(x => x.PersonelTabloId == id);
           foreach(var s in personelokulmedyalar)
            {
                s.aktifMi = 0;
                s.silindiMi = 1;
                _db.PersonelEgitimId.Update(s);
                await _db.SaveChangesAsync();
            }
         
          
            return true;
        }

        public async Task<bool> okulmedyasildbService(List<int> idler)
        {
            foreach(var id in idler)
            {
                var okulmedyalar =   _db.PersonelEgitimId.FirstOrDefault(x => x.MedyaID == id);
                okulmedyalar.aktifMi = 0;
                okulmedyalar.silindiMi= 1;
                _db.PersonelEgitimId.Update(okulmedyalar); await _db.SaveChangesAsync();


            }
            return true;
       
        }
    }
}
