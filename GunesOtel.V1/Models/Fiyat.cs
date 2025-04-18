using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GunesOtel.V1.Models
{
    public class Fiyat
    {
        public int FiyatID { get; set; }
        public int OdaID { get; set; }              // Hangi odaya ait
        public int SezonID { get; set; }            // Hangi sezona ait
        public decimal Tutar { get; set; }          // Fiyat
        public DateTime GuncellemeTarihi { get; set; }
    }
}
