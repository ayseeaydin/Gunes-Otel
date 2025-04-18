using System;
using System.Data;
using System.Data.SqlClient;
using GunesOtel.V1.DataAccess;
using GunesOtel.V1.Models;
using GunesOtel.V1.Helpers;

namespace GunesOtel.V1.Business
{
    // Kullanıcı girişi ve yetkilendirme işlemlerini yöneten sınıf
    public class AuthManager
    {
        /// <summary>
        /// Kullanıcı girişi yapar
        /// </summary>
        /// <returns>Başarılı ise kullanıcı nesnesi, başarısız ise null</returns>
        public Kullanici Login(string username, string password)
        {
            try
            {
                // Kullanıcıyı sorgula
                string query = "SELECT * FROM Kullanicilar WHERE KullaniciAdi = @KullaniciAdi AND Durum = 1";
                SqlParameter param = SqlHelper.CreateParameter("@KullaniciAdi", username);

                DataTable dt = SqlHelper.ExecuteDataTable(query, param);

                if (dt.Rows.Count == 0)
                {
                    // Kullanıcı bulunamadı
                    return null;
                }

                // İlk satırı al
                DataRow row = dt.Rows[0];

                // Şifreyi doğrudan karşılaştır
                string storedPassword = row["Sifre"].ToString();
                if (!SecurityHelper.VerifyPassword(password, storedPassword))
                {
                    // Şifre yanlış
                    return null;
                }

                // Kullanıcı nesnesini oluştur
                Kullanici kullanici = new Kullanici
                {
                    KullaniciID = Convert.ToInt32(row["KullaniciID"]),
                    KullaniciAdi = row["KullaniciAdi"].ToString(),
                    Sifre = storedPassword,
                    AdSoyad = row["AdSoyad"].ToString(),
                    Email = row["Email"] != DBNull.Value ? row["Email"].ToString() : null,
                    Telefon = row["Telefon"] != DBNull.Value ? row["Telefon"].ToString() : null,
                    RolID = Convert.ToInt32(row["RolID"]),
                    PersonelId = Convert.ToInt32(row["PersonelID"]),
                    Durum = Convert.ToBoolean(row["Durum"]),
                    OlusturulmaTarihi = Convert.ToDateTime(row["OlusturmaTarihi"])
                };

                // Son giriş tarihini güncelle
                UpdateLastLogin(kullanici.KullaniciID);

                return kullanici;
            }
            catch (Exception ex)
            {
                // Burada hata loglanabilir
                Console.WriteLine("Login hatası: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Kullanıcının son giriş tarihini günceller
        /// </summary>
        private void UpdateLastLogin(int kullaniciId)
        {
            try
            {
                string query = "UPDATE Kullanicilar SET SonGirisTarihi = @SonGirisTarihi WHERE KullaniciID = @KullaniciID";
                SqlParameter[] parameters =
                {
                    SqlHelper.CreateParameter("@KullaniciID", kullaniciId),
                    SqlHelper.CreateParameter("@SonGirisTarihi", DateTime.Now)
                };

                SqlHelper.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                // Burada hata loglanabilir, ancak giriş işlemini etkilememesi için hatayı yutuyoruz
                Console.WriteLine("Son giriş tarihi güncellenirken hata: " + ex.Message);
            }
        }

        /// <summary>
        /// Kullanıcının rolüne göre yetki kontrolü yapar
        /// </summary>
        public string GetRoleName(int rolId)
        {
            try
            {
                string query = "SELECT RolAdi FROM Roller WHERE RolID = @RolID";
                SqlParameter param = SqlHelper.CreateParameter("@RolID", rolId);

                object result = SqlHelper.ExecuteScalar(query, param);
                return result != null ? result.ToString() : "Bilinmeyen Rol";
            }
            catch (Exception ex)
            {
                // Hata logu
                Console.WriteLine("Rol getirme hatası: " + ex.Message);
                return "Bilinmeyen Rol";
            }
        }
    }
}
