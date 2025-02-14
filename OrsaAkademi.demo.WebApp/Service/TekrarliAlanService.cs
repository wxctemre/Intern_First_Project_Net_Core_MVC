using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OrsaAkademi.demo.models.Entity;
using OrsaAkademi.demo.models.Entity.vmmodel;
using OrsaAkademi.demo.WebApp.Helpers;
using OrsaAkademi.demo.WebApp.Models.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OrsaAkademi.demo.WebApp.Service
{
    public class TekrarliAlanService : ITekrarliAlanService
    {
        private readonly HttpClient _httpClient;
        public TekrarliAlanService(HttpClient httpClient)
        {
            var httpal = new HttpclientOlusturucu();
            _httpClient = httpal.httpolusturucu(httpClient);
        }

        public async Task<bool> OkulbilgisiSilService(int id)
        {

            var response = await _httpClient.PutAsync($"api/TekrarliAlandb/okulusildb/{id}", null);
            return response.IsSuccessStatusCode;

        }

        public async Task<List<ParamOkullar>> OkulGetirmeService()
        {
            var response = await _httpClient.GetAsync($"api/TekrarliAlandb/OkulGetirmeDb");
            if(response.IsSuccessStatusCode)
            {
                var responseData=await response.Content.ReadAsStringAsync();
                var result =JsonConvert.DeserializeObject<List<ParamOkullar>>(responseData);
                return result;

            }
            else
            {
                throw new Exception("okul getirme işlemi  başarısız oldu. HTTP status code: " + response.StatusCode);
            }



        }
        public async Task<bool> okulGuncelleService(IFormCollection tekrarlialan)
        {
            List<vmOkulKaydet> okullar = new List<vmOkulKaydet>();
            var personelId = tekrarlialan["personelid"];

            foreach (var key in tekrarlialan.Keys)
            {
                if (key.StartsWith("schools[") && (key.Contains("schoolId") || key.Contains("yenialanokul")))
                {
                    var index = key.Split(new[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries)[1];
                    var okulId = tekrarlialan[$"schools[{index}][schoolId]"];
                    var yenialanOkulId = tekrarlialan[$"schools[{index}][yenialanokul]"];
                    var mezuniyetYili = tekrarlialan[$"schools[{index}][graduationYear]"];
                    var yenialanMezuniyetYili = tekrarlialan[$"schools[{index}][yenialangraduationYear]"];
                    var customDivId = tekrarlialan[$"schools[{index}][customDivId]"];
                    var okulBilgiId = tekrarlialan[$"schools[{index}][okulBilgiId]"];

 
                    var okulViewModel = new vmOkulKaydet
                    {
                        OkulBilgileri = new Okullar
                        {
                            Okulid = !string.IsNullOrEmpty(okulId) ? Convert.ToInt32(okulId) : (!string.IsNullOrEmpty(yenialanOkulId) ? Convert.ToInt32(yenialanOkulId) : 0),
                            Mezunolduguyil = (short)(!string.IsNullOrEmpty(mezuniyetYili) ? Convert.ToInt16(mezuniyetYili) : (!string.IsNullOrEmpty(yenialanMezuniyetYili) ? Convert.ToInt16(yenialanMezuniyetYili) : 0)),
                            personelid = Convert.ToInt16(personelId),
                            aktifMi = 1,
                            silindiMi = 0,
                            sirano = Convert.ToInt16(customDivId)
                        },
                        OkulMedyasi = new List<MedyaKutuphanesi>()
                    };

                    if (!string.IsNullOrEmpty(okulBilgiId))
                    {
                        okulViewModel.OkulBilgileri.Id = Convert.ToInt32(okulBilgiId);
                    }

                    var imagesKeyPrefix = $"schools[{index}][images]";
                    var files = tekrarlialan.Files.Where(f => f.Name.StartsWith(imagesKeyPrefix)).ToList();
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            var resimad = new MedyaKutuphanesi();
                            var fileName = Path.GetFileName(file.FileName);
                            var newImageName = Guid.NewGuid() + fileName;
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/MedyaKutuphanesi/", newImageName);
                            resimad.MedyaAdi = fileName;
                            resimad.MedyaUrl = newImageName;
                            okulViewModel.OkulMedyasi.Add(resimad);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }
                        }
                    }
                    okullar.Add(okulViewModel);
                }
            }

            if (okullar.Count > 0)
            {
                var jsondata = JsonConvert.SerializeObject(okullar);
                var content = new StringContent(jsondata, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/TekrarliAlandb/okulguncelledb", content);
                return response.IsSuccessStatusCode;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> OkulmedyasilService(List<int> idler)
        {
            var jsondata = JsonConvert.SerializeObject(idler);
            var content = new StringContent(jsondata, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/TekrarliAlandb/okulmedyasildb", content);
            return response.IsSuccessStatusCode;

        }

        public async Task<bool> okulukaydetService(IFormCollection tekrarlialan)
        {
            List<vmOkulKaydet> okullar = new List<vmOkulKaydet>();
            foreach (var key in tekrarlialan.Keys)
            {
                if (key.StartsWith("okul_"))
                {
                    var okulNo = int.Parse(key.Split('_')[1]);
                    var okulidal = tekrarlialan["okul_" + okulNo];
                    var mezuniyetYili = tekrarlialan["mezuniyet_" + okulNo];
                    var personelId = tekrarlialan["personelId"];

                   
                   
                    var customDivNumber = tekrarlialan["customDivNumber_" + okulNo];

                    var okulViewModel = new vmOkulKaydet
                    {
                        OkulBilgileri = new Okullar
                        {
                            Okulid = Convert.ToInt32(okulidal),
                            Mezunolduguyil = Convert.ToInt16(mezuniyetYili),
                            personelid = Convert.ToInt16(personelId),
                            aktifMi = 1, 
                            silindiMi = 0, 
                            sirano = Convert.ToInt16(customDivNumber)
                        },
                        OkulMedyasi = new List<MedyaKutuphanesi>()
                    };

                    var resimKey = key.Replace("okul_", "okulResim_");
                    var files = tekrarlialan.Files.GetFiles(resimKey);
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            var resimad = new MedyaKutuphanesi();
                            var fileName = Path.GetFileName(file.FileName);
                            var newImageName = Guid.NewGuid() + fileName;
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/MedyaKutuphanesi/", newImageName);
                            resimad.MedyaAdi = fileName;
                            resimad.MedyaUrl = newImageName;
                            okulViewModel.OkulMedyasi.Add(resimad);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }
                        }
                    }
                    okullar.Add(okulViewModel);
                }
            }

            if (okullar.Count > 0)
            {
                var jsondata = JsonConvert.SerializeObject(okullar);
                var content = new StringContent(jsondata, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"api/TekrarliAlandb/okulkaydetdb", content);
                return response.IsSuccessStatusCode;
            }
            else
            {
                return false;
            }
        }

    }
}


