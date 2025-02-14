using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrsaAkademi.demo.models.Entity.vmmodel
{
   
    public class vmOkulKaydet
    {
        public Okullar OkulBilgileri { get; set; }
        public List<MedyaKutuphanesi> OkulMedyasi { get; set; }
 
    }
}
