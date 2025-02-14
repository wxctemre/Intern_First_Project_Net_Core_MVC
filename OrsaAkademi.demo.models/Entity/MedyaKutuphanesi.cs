using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrsaAkademi.demo.models.Entity
{
   public class MedyaKutuphanesi
    {
        [Key]
        public long Id { get; set; }
        public string MedyaAdi { get; set; } = null;
        public string MedyaUrl { get; set;}= null;
     
    }
}
