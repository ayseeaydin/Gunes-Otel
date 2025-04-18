using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GunesOtel.V1.Models
{
    public class RezervasyonEkHizmet
    {
        public int ID { get; set; }
        public int RezervasyonID { get; set; }
        public int HizmetID { get; set; }
        public int Adet { get; set; }
        public decimal BirimFiyat { get; set; }       // O anki sezonluk fiyat
        public decimal ToplamFiyat { get; set; }      // BirimFiyat * Adet
    }
}
