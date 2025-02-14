using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OrsaAkademi.demo.models.Entity;
using OrsaAkademi.demo.WebApp.Helpers;
using OrsaAkademi.demo.WebApp.Models.Interface;
using OrsaAkademi.demo.models.Entity.vmmodel;
using System.Collections.Generic;
namespace OrsaAkademi.demo.WebApp.Service
{
    public class GuncellemeService : IGuncellemeService
    {
        private readonly HttpClient _httpClient;

        public GuncellemeService(HttpClient httpClient)
        {
            var httpcliental = new HttpclientOlusturucu();
            _httpClient = httpcliental.httpolusturucu(httpClient);
        }

        public async Task<vmVeriGetir> GuncellemeVeriGetirService(int id)
        {
            var response = await _httpClient.GetAsync($"api/Guncellemedb?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var responsedata = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<vmVeriGetir>(responsedata);
                return result;

            }
            else { return null; }

        }

        public async Task<Personeller> KisiyiGuncelleService(Personeller personel)
        {
            var jsondata = JsonConvert.SerializeObject(personel);
            var content = new StringContent(jsondata, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/Guncellemedb/GuncellemeIslemi", content);

            if (response.IsSuccessStatusCode)
            {
                var responsedata = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Personeller>(responsedata);
                return result;

            }
            else
            {
                return null;
            }

        }

        public async Task<bool> PersonelFotografSilService(List<int> idler)
        {
            var jsonData = JsonConvert.SerializeObject(idler);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("/api/Guncellemedb/MedyalariSil", content);
            return response.IsSuccessStatusCode;
        }
    }
}
