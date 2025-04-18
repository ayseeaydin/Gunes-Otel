using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GunesOtel.V1.Models
{
    public class Musteri
    {
        public int MusteriID { get; set; }
        public string AdSoyad { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string TCKimlikNo { get; set; }
        public string PasaportNo { get; set; }
        public DateTime? DogumTarihi { get; set; }
        public string Adres { get; set; }
        public string Cinsiyet { get; set; }
        public DateTime KayitTarihi { get; set; }
        public string Notlar { get; set; }
    }
}
