using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrsaAkademi.demo.models.Entity.vmmodel
{
    public class vmVeriGetir
    { 
        public Personeller personellers { get; set; }
        public List<MedyaKutuphanesi> medyalar { get; set; }
        public List<vmOkulKaydet> Okulbilgileri { get; set; }   
    }
}
