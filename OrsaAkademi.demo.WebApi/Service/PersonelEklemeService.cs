using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrsaAkademi.demo.models.Entity;
using OrsaAkademi.demo.models.Entity.vmmodel;
using OrsaAkademi.demo.WebApi.model;
using OrsaAkademi.demo.WebApi.model.Interface;
namespace OrsaAkademi.demo.WebApi.Service
{
    public class PersonelEklemeService : IPersonelEklemeService
    {
        private readonly ApplicationDbContext _db;
        public PersonelEklemeService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Personeller> PersonelKaydetService(Personeller personeller)
        {
    
            if (personeller != null)
            {

                string sifreHash = CalculateMD5Hash(personeller.Sifre);
               personeller.Sifre = sifreHash;
                personeller.aktifMi = 1;
                personeller.silindiMi = 0;
                _db.personellers.Add(personeller);

                await _db.SaveChangesAsync();
                return personeller;
            }
            else { return null; }

        }

        public async Task<List<vmPersonelListele>> PersonelListeleService()
        {
            List<vmPersonelListele> veritabanindanalinanveri = new List<vmPersonelListele>();
            var personellerlist = await _db.personellers.Where(x=>x.aktifMi==1 && x.silindiMi==0).ToListAsync();
            var personellermedyalar = await _db.PersonelMedyalar.Where(a => a.AktifMi == 1 && a.SilindiMi == 0).ToListAsync();
            var MedyaKutuphanesi = await _db.MedyaKutuphanesi.ToListAsync();


           
            if (personellerlist.Count > 0)
            {

                foreach (var x in personellerlist)
                {
                    vmPersonelListele verilerial = new vmPersonelListele();
                    var personelmedyalarsonuncuid = personellermedyalar.Where(s => s.PersonelId == x.Id).LastOrDefault(a => a.AktifMi == 1 && a.SilindiMi == 0);

                    if (personelmedyalarsonuncuid != null)
                    {
                        foreach (var medyalar in MedyaKutuphanesi)
                        {
                            if (personelmedyalarsonuncuid.MedyaId == medyalar.Id)
                            {
                                verilerial.Id = x.Id;
                                verilerial.Ad = x.Ad;
                                verilerial.Soyad = x.Soyad;
                                verilerial.DogumTarihi = x.DogumTarihi;
                                verilerial.Email = x.Email;
                                verilerial.Telefon = x.Telefon;
                                verilerial.MedyaId = medyalar.Id;
                                verilerial.MedyaAdi = medyalar.MedyaAdi;
                                verilerial.MedyaUrl = medyalar.MedyaUrl;
                                var sonOkulId = await _db.okullar
                          .Where(p => p.personelid == x.Id && p.aktifMi == 1 && p.silindiMi == 0)
                          .GroupBy(p => p.Mezunolduguyil)
                          .OrderByDescending(g => g.Key)
                          .Select(g => g.Max(p => p.Id))
                          .FirstOrDefaultAsync();
                                if (sonOkulId != 0)
                                {
                                    var okullar = _db.okullar.FirstOrDefault(d => d.Id == sonOkulId);
                                    var personelokuliliskisi = _db.PersonelEgitimId
       .Where(a => a.PersonelTabloId == sonOkulId && a.aktifMi == 1 && a.silindiMi == 0)
       .OrderByDescending(a => a.TabloId)
       .FirstOrDefault();
                                    if (personelokuliliskisi != null)
                                    {
                                        var okulmedyasinial = _db.MedyaKutuphanesi.FirstOrDefault(e => e.Id == personelokuliliskisi.MedyaID);
                                        verilerial.OkulMedyaAdi = okulmedyasinial.MedyaAdi;
                                        verilerial.OkulMedyaUrl = okulmedyasinial.MedyaUrl;
                                    

                                    }
                                    if (sonOkulId != 0) {
                                        var okulunisminial = _db.paramokullar.FirstOrDefault(g => g.Id == okullar.Okulid).Okuladi;
                                        verilerial.okuladi = okulunisminial;
                                        verilerial.mezunyili = okullar.Mezunolduguyil;
                                    }
                                }
                            }



                        }

                    }
                    else
                    {
                        verilerial.Id = x.Id;
                        verilerial.Ad = x.Ad;
                        verilerial.Soyad = x.Soyad;
                        verilerial.DogumTarihi = x.DogumTarihi;
                        verilerial.Email = x.Email;
                        verilerial.Telefon = x.Telefon;
                        var sonOkulId = await _db.okullar
                       .Where(p => p.personelid == x.Id && p.aktifMi == 1 && p.silindiMi == 0)
                       .GroupBy(p => p.Mezunolduguyil)
                       .OrderByDescending(g => g.Key)
                       .Select(g => g.Max(p => p.Id))
                       .FirstOrDefaultAsync();
                        if (sonOkulId != 0)
                        {
                            var okullar = _db.okullar.FirstOrDefault(d => d.Id == sonOkulId && d.aktifMi == 1 && d.silindiMi == 0);
                            var personelokuliliskisi = _db.PersonelEgitimId
        .Where(a => a.PersonelTabloId == sonOkulId && a.aktifMi == 1 && a.silindiMi == 0)
        .OrderByDescending(a => a.TabloId)
        .FirstOrDefault();
                            if (personelokuliliskisi != null)
                            {
                                var okulmedyasinial = _db.MedyaKutuphanesi.FirstOrDefault(e => e.Id == personelokuliliskisi.MedyaID);
                                verilerial.OkulMedyaAdi = okulmedyasinial.MedyaAdi;
                                verilerial.OkulMedyaUrl = okulmedyasinial.MedyaUrl;
                                var okulunisminial = _db.paramokullar.FirstOrDefault(g => g.Id == okullar.Okulid).Okuladi;
                                verilerial.okuladi = okulunisminial;
                                verilerial.mezunyili = okullar.Mezunolduguyil;


                            }
                        }

                    }
                    veritabanindanalinanveri.Add(verilerial);

                }

            }



            return veritabanindanalinanveri;






        }
        public async Task<GirisVeKayit> PersonelRegisterService(GirisVeKayit RegisterPersonel)
        {
            string sifreHash = CalculateMD5Hash(RegisterPersonel.sifre);

     
            RegisterPersonel.sifre = sifreHash;
            Personeller kayitekle = new Personeller();
            kayitekle.Ad = "Yeni Kullanici";
            kayitekle.Soyad = "Yeni Kullanici";
            kayitekle.DogumTarihi = DateTime.Now;
            kayitekle.Telefon = "00000000000";
            kayitekle.Email = RegisterPersonel.email;
            kayitekle.Sifre = RegisterPersonel.sifre;
            kayitekle.aktifMi = 1;
            kayitekle.silindiMi = 0;
            _db.personellers.Add(kayitekle);
            await _db.SaveChangesAsync();
            return RegisterPersonel;
        }
        private string CalculateMD5Hash(string input)
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
        }

        public Personeller PersonelLoginService(GirisVeKayit LoginPersonel)
        {

            
            string sifreHash = CalculateMD5Hash(LoginPersonel.sifre);


            LoginPersonel.sifre = sifreHash;
            var result = _db.personellers.FirstOrDefault(x => x.Email == LoginPersonel.email && x.Sifre == LoginPersonel.sifre);


            if (result != null)
            {

                var personeller = new Personeller();
                personeller.Email = result.Email;
                personeller.Ad = result.Ad;
                personeller.Soyad = result.Soyad;
                return personeller;

            }
            else
            {
                return null;
            }


        }

        public async Task<List<MedyaKutuphanesi>> PersonelMedyalarKaydet(List<MedyaKutuphanesi> Medyalar)
        {
            var idtutucu = new PersonelMedyalar();
            var medyakutuphanesiid = new List<long>();
            if (Medyalar.Count > 0)
            {
                foreach (var photo in Medyalar)
                {
                    idtutucu.PersonelId = (int)photo.Id;
                    photo.Id = 0;
                    _db.MedyaKutuphanesi.Add(photo);
                    await _db.SaveChangesAsync();
                    medyakutuphanesiid.Add(photo.Id);
                }
                foreach (var personelmedyalar in medyakutuphanesiid)
                {
                    var personelmedyalarikaydet = new PersonelMedyalar()
                    {
                        PersonelId = idtutucu.PersonelId,
                        MedyaId = (int)personelmedyalar,
                        AktifMi = 1,
                        SilindiMi = 0
                    };
                    _db.Add(personelmedyalarikaydet);
                    await _db.SaveChangesAsync();

                }
                return Medyalar;

            }
            return null;

        }

        public async Task<bool> PersonelSilService(int id)
        {
            var personel = _db.personellers.FirstOrDefault(p => p.Id == id);
            if (personel != null)
            {
                personel.aktifMi = 0;
                personel.silindiMi = 1;
                _db.personellers.Update(personel);
                await _db.SaveChangesAsync();

                return true;
            }
            else { return false; }


        }
    }
}
