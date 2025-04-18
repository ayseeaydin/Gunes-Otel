using System;
using System.Text;

namespace GunesOtel.V1.Helpers
{
    // Güvenlikle ilgili yardımcı metodlar içeren sınıf
    public class SecurityHelper
    {
        /// <summary>
        /// Şifre kontrolü yapar (açık metin)
        /// </summary>
        public static bool VerifyPassword(string inputPassword, string storedPassword)
        {
            // Doğrudan metin karşılaştırması
            return inputPassword == storedPassword;
        }

        /// <summary>
        /// Rastgele bir şifre oluşturur
        /// </summary>
        public static string GenerateRandomPassword(int length = 8)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();

            for (int i = 0; i < length; i++)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }

            return res.ToString();
        }
    }
}
