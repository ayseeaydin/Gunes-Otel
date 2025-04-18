using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GunesOtel.V1.Models
{
    public class Sezon
    {
        public int SezonID { get; set; }
        public string SezonAdi { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }

    }
}
