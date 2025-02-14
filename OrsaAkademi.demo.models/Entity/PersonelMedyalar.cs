using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrsaAkademi.demo.models.Entity
{
    public class PersonelMedyalar
    {
        [Key]
        public int TabloId { get; set; }
        public int PersonelId { get; set; }
        public int MedyaId { get; set; }

        public int AktifMi { get; set; }
        public int SilindiMi { get; set; }

    }
}
