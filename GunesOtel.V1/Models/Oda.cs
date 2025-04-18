using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GunesOtel.V1.Models
{
    public class Oda
    {
        public int OdaID { get; set; }
        public string OdaNumarasi { get; set; }
        public int OdaTipID { get; set; }
        public int Kat { get; set; }
        public string Durum { get; set; }      // Boş, Dolu, Bakımda
        public string Aciklama { get; set; }
    }
}
