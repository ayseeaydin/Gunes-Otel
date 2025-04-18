using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GunesOtel.V1.Models
{
    public class Personel
    {
        public int PersonelID { get; set; }
        public string AdSoyad { get; set; }
        public string TCKimlikNo { get; set; }
        public DateTime? DogumTarihi { get; set; }
        public string Cinsiyet { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string Adres { get; set; }
        public string Pozisyon { get; set; }
        public string IBAN { get; set; }
        public DateTime? IseBaslamaTarihi { get; set; }
        public DateTime? IstenAyrilmaTarihi { get; set; }
        public bool Durum { get; set; }
        public DateTime OlusturmaTarihi { get; set; }
    }
}
