using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GunesOtel.V1.Models
{
    public class Rezervasyon
    {
        public int RezervasyonID { get; set; }
        public int MusteriID { get; set; }
        public int OdaID { get; set; }
        public int KullaniciID { get; set; }             // Rezervasyonu oluşturan kişi
        public DateTime CheckInTarihi { get; set; }
        public DateTime CheckOutTarihi { get; set; }
        public DateTime RezervasyonTarihi { get; set; }
        public string Kaynak { get; set; }               // Web, Telefon, YüzYüze
        public string Durum { get; set; }                // Bekliyor, Onaylandı, İptal
        public byte KisiSayisi { get; set; }
        public string OzelNotlar { get; set; }
    }
}
