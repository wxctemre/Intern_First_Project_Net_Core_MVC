using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrsaAkademi.demo.models.Entity
{
    public class Personeller
    {
        [Key]
        public int Id { get; set; }

        public string Ad { get; set; }

        public string Soyad { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Telefon numarası geçerli değil")]
        public string Telefon { get; set; }
        public string Email { get; set; }
        public DateTime DogumTarihi { get; set; }
        public string Sifre { get; set; }
        public int aktifMi {  get; set; }   
        public int silindiMi { get; set; }



    }
}
