using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrsaAkademi.demo.models.Entity
{
    public class GirisVeKayit
    {
        [Key]
        public int Id { get; set; }
        public string email { get; set; }
        public string sifre { get; set; }



    }
}
