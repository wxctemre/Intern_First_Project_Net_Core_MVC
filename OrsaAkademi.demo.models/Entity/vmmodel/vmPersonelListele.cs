using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrsaAkademi.demo.models.Entity.vmmodel
{
    public class vmPersonelListele
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public DateTime DogumTarihi { get; set; }
        public long MedyaId { get; set; }
        public string MedyaAdi { get; set; } = null;
        public string MedyaUrl { get; set; } = null;
        public long OkulMedyaId { get; set; }   
        public string OkulMedyaAdi { get;set; }
        public string OkulMedyaUrl { get;set; }
        public string okuladi { get; set; }
        public short mezunyili { get; set; }

    }
}
