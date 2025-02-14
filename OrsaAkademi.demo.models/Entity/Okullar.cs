using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrsaAkademi.demo.models.Entity
{
    public class Okullar
    {
        public int Id { get; set; }
        public int Okulid { get; set; }
        public short Mezunolduguyil { get; set; }
        public short personelid { get; set; }
        public int aktifMi { get; set; }
        public int silindiMi { get; set; }
        public int sirano { get; set; }
    }
}
