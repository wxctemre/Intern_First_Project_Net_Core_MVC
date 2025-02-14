using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
using Microsoft.EntityFrameworkCore;
using OrsaAkademi.demo.models.Entity;
using OrsaAkademi.demo.models.Entity.vmmodel;
using OrsaAkademi.demo.WebApi.model;
using OrsaAkademi.demo.WebApi.model.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OrsaAkademi.demo.WebApi.Service
{
    public class GuncellemeService : IGuncellemeService
    {
        private readonly ApplicationDbContext _db;
        public GuncellemeService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> GuncellemePersonelFotografSilService(List<int> idler)
        {
            var medya = await _db.PersonelMedyalar.ToListAsync();
            if (idler != null)
            {

                foreach (var item in medya)
                {
                    foreach (var id in idler)
                    {
                        if(id == item.MedyaId)
                        {
                            item.AktifMi = 0;
                            item.SilindiMi = 1;
                            _db.PersonelMedyalar.Update(item);
                            await _db.SaveChangesAsync();
                        }

                    }

                }
                 
                return true;
            }
            else { return false; }  
        }

        public async Task<vmVeriGetir> VeriGetirService(int id)
        {
            List<MedyaKutuphanesi> medyalar = new List<MedyaKutuphanesi>();

            var personelial = await _db.personellers.FirstOrDefaultAsync(x => x.Id == id);
            var personelmedyalar = await _db.PersonelMedyalar.Where(x => x.PersonelId == personelial.Id && x.AktifMi == 1 && x.SilindiMi == 0).ToListAsync();
            var medyakutuphanesi = await _db.MedyaKutuphanesi.ToListAsync();
            var personelokullar = await _db.okullar.Where(x => x.aktifMi == 1 && x.silindiMi == 0 && x.personelid == id)
                                                    .OrderBy(x => x.sirano)
                                                    .ToListAsync();
            var personelOkulMedyalarial = await _db.PersonelEgitimId.Where(x => x.aktifMi == 1 && x.silindiMi == 0).ToListAsync();

            if (personelmedyalar.Count > 0)
            {
                foreach (var medyaidleri in personelmedyalar)
                {
                    MedyaKutuphanesi medyayiekle = new MedyaKutuphanesi();
                    foreach (var fotograf in medyakutuphanesi)
                    {
                        if (medyaidleri.MedyaId == fotograf.Id)
                        {
                            medyayiekle.Id = fotograf.Id;
                            medyayiekle.MedyaAdi = fotograf.MedyaAdi;
                            medyayiekle.MedyaUrl = fotograf.MedyaUrl;
                            medyalar.Add(medyayiekle);
                        }
                    }
                }

                var personebilgileri = new vmVeriGetir
                {
                    Okulbilgileri = new List<vmOkulKaydet>()
                };

                foreach (var item in personelokullar)
                {
                    List<MedyaKutuphanesi> okulMedyalari = new List<MedyaKutuphanesi>();

                    foreach (var okulmedyalar in personelOkulMedyalarial)
                    {
                        if (item.Id == okulmedyalar.PersonelTabloId)
                        {
                            var okulmedyasi = medyakutuphanesi.FirstOrDefault(x => x.Id == okulmedyalar.MedyaID);
                            if (okulmedyasi != null)
                            {
                                okulMedyalari.Add(okulmedyasi);
                            }
                        }
                    }

                    var okulBilgisi = new vmOkulKaydet
                    {
                        OkulBilgileri = item,
                        OkulMedyasi = okulMedyalari
                    };

                    personebilgileri.Okulbilgileri.Add(okulBilgisi);
                }


                personebilgileri.personellers = personelial;
                personebilgileri.medyalar = medyalar;

                return personebilgileri;
            }
            else
            {
                var personebilgileri = new vmVeriGetir
                {
                    Okulbilgileri = new List<vmOkulKaydet>()
                };

                foreach (var item in personelokullar)
                {
                    List<MedyaKutuphanesi> okulMedyalari = new List<MedyaKutuphanesi>();

                    foreach (var okulmedyalar in personelOkulMedyalarial)
                    {
                        if (item.Id == okulmedyalar.PersonelTabloId)
                        {
                            var okulmedyasi = medyakutuphanesi.FirstOrDefault(x => x.Id == okulmedyalar.MedyaID);
                            if (okulmedyasi != null)
                            {
                                okulMedyalari.Add(okulmedyasi);
                            }
                        }
                    }

                    var okulBilgisi = new vmOkulKaydet
                    {
                        OkulBilgileri = item,
                        OkulMedyasi = okulMedyalari
                    };

                    personebilgileri.Okulbilgileri.Add(okulBilgisi);
                }
                
                personebilgileri.personellers = personelial;
                return personebilgileri;
            }
        }

        public async Task<Personeller> VeriyiGuncelleService(Personeller personeller)
        {
            if (personeller != null)
            {
                var personel = _db.personellers.FirstOrDefault(x => x.Id == personeller.Id);
                if (personel == null)
                {
                    return null;
                }

                if (personel.Sifre != personeller.Sifre)
                {
                    string sifreHash = await CalculateMD5Hash(personeller.Sifre);
                    personel.Sifre = sifreHash;
                }
                personel.Ad=personeller.Ad;
                personel.Soyad = personeller.Soyad; 
                personel.DogumTarihi = personeller.DogumTarihi;
                personel.Email = personeller.Email; 
                personel.Telefon = personeller.Telefon;
               
                _db.personellers.Update(personel);
                await _db.SaveChangesAsync();
                return personel;
            }
            else
            {
                return null;
            }
        }
        private async Task<string> CalculateMD5Hash(string input)
        {
            return await Task.Run(() =>
            {
                using (MD5 md5 = MD5.Create())
                {
                    byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                    byte[] hashBytes = md5.ComputeHash(inputBytes);


                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < hashBytes.Length; i++)
                    {
                        sb.Append(hashBytes[i].ToString("x2"));
                    }
                    return sb.ToString();
                }
            });
        }
    }
}
