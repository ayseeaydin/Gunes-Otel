using GunesOtel.V1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GunesOtel.V1.Helpers
{
    // Uygulama boyunca aktif kullanıcı bilgilerini saklayan statik helper sınıfı
    public static class SessionHelper
    {
        /// <summary>
        /// Aktif kullanıcı
        /// </summary>
        public static Kullanici AktifKullanici { get; set; }

        /// <summary>
        /// Kullanıcının rolü
        /// </summary>
        public static string RolAdi { get; set; }

        /// <summary>
        /// Kullanıcı giriş yaptı mı?
        /// </summary>
        public static bool IsLoggedIn => AktifKullanici != null;
    }
}
