using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrsaAkademi.demo.models.Entity
{
    public class PersonelOkulMedyalariiliski
    {
        [Key]
        public int TabloId { get; set; }
        public int PersonelTabloId { get; set; }
        public int MedyaID { get; set; }
        public int aktifMi { get; set; }
        public int silindiMi { get; set; }

    }
}
