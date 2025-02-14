using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using OrsaAkademi.demo.models;
using OrsaAkademi.demo.models.Entity;
using OrsaAkademi.demo.models.Entity.vmmodel;
using OrsaAkademi.demo.WebApp.Helpers;
using OrsaAkademi.demo.WebApp.Models.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
namespace OrsaAkademi.demo.WebApp.Service
{
    public class PersonellerService : IPersonellerService
    {
        private readonly HttpClient _httpClient;
        public PersonellerService(HttpClient httpClient)
        {
             var httpal= new HttpclientOlusturucu();
            _httpClient = httpal.httpolusturucu(httpClient);
        }

        public async Task<List<MedyaKutuphanesi>> PersonelFotografiKaydetService(IFormCollection fotografyuklemealani)
        {
          List<string>filenames=new List<string>();
           var medyalar=new List<MedyaKutuphanesi>();
            foreach(var file in fotografyuklemealani.Files)
            {
                var filename = Path.GetFileName(file.FileName);
                var newImageName = Guid.NewGuid()+ filename;
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/MedyaKutuphanesi/", newImageName);
                var stream = new FileStream(filepath,FileMode.Create);
                file.CopyTo(stream);
                filenames.Add(newImageName);
                string personelIdvalue = null;
                foreach (string key in fotografyuklemealani.Keys)
                {
                    if(key.Contains("PersonelId"))
                    {
                        string valuestring = fotografyuklemealani[key];
                        string[] values = valuestring.ToString().Split(',');
                        if(values.Length > 0)
                        {
                            personelIdvalue = values[0];
                            long personelid = Convert.ToInt64(personelIdvalue);
                            var medya = new MedyaKutuphanesi
                            {
                                Id = personelid,
                                MedyaAdi = filename,
                                MedyaUrl = newImageName
                            };
                            medyalar.Add(medya);
                            break;

                        }
                    }
                }

         
         



            }
            var jsondata = JsonConvert.SerializeObject(medyalar);
            var content = new StringContent(jsondata, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"api/Personeller/PersonelMedyalarKaydet", content);

            if (response.IsSuccessStatusCode)
            {
                var responsedata = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<MedyaKutuphanesi>>(responsedata);
                return result;

            }
            else
            {
                return null;
            }

        }

        public async Task<List<vmPersonelListele>> PersonelleriListeleService()
        {
          
            var response = await _httpClient.GetAsync($"api/Personeller/PersonelListem");

            if (response.IsSuccessStatusCode)
            {
                var responsedata = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<vmPersonelListele>>(responsedata);
                return result;

            }
            else
            {
                return null;
            }

        }

        public async Task<Personeller> PersonellerKaydetService(Personeller personeller)
        {
            var jsondata=JsonConvert.SerializeObject(personeller);
            var content=new  StringContent(jsondata,Encoding.UTF8,"application/json");
            var response = await _httpClient.PostAsync($"api/Personeller/PersonelKaydet", content);

            if(response.IsSuccessStatusCode)
            {
                var responsedata= await response.Content.ReadAsStringAsync();
                var result=JsonConvert.DeserializeObject<Personeller>(responsedata);
                return result;

            }
            else
            {
                return null;
            }
        }
        public async Task<GirisVeKayit> personelregisterlaService(GirisVeKayit registerpersonel)
        {
            var jsonData = JsonConvert.SerializeObject(registerpersonel);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"api/GirisVeRegister/personeliregisterla", content);

            if (response.IsSuccessStatusCode)
            {

                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<GirisVeKayit>(responseData);
                return result;
            }
            else
            {
                throw new Exception("Employee gönderme işlemi başarısız oldu. HTTP status code: " + response.StatusCode);
            }


        }

        public async Task<Personeller> PersonelLoginService(GirisVeKayit loginpersonel)
        {
            var jsonData = JsonConvert.SerializeObject(loginpersonel);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("/api/GirisVeRegister/PersonelLogin", content);
            if (response.IsSuccessStatusCode)
            {

                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Personeller>(responseData);
                return result;
            }
            else
            {
                throw new Exception("Employee gönderme işlemi başarısız oldu. HTTP status code: " + response.StatusCode);
            }

        }

        public async Task<bool> PersonelSil(int id)
        {

         
            var response = await _httpClient.DeleteAsync($"api/Personeller/PersonelSil?id={id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
