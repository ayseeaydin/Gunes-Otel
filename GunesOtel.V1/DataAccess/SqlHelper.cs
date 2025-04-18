using System.Data.SqlClient;  //Sql Server ile bağlantı kurmak için gerekli
using System.Configuration;   //App.config dosyasındaki ayarlara ulaşmak için gerekli

namespace GunesOtel.V1.DataAccess
{
    // SqlHelper sınıfı: veritabanı bağlantılarını merkezi olarak yöneten yardımcı sınıf
    public static class SqlHelper
    {
        // App.config içinde tanımlı olan "DbConn" bağlantı cümlesini alıyoruz:
        private static string connStr= ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString;

        // Bu metot, Sql Server bağlantısı oluşturur
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connStr); // yeni bağlantı nesnesi döndürür
        }

        // Bu metot, veritabanına bağlanılıp bağlanılmadığını test eder:
        public static bool TestConnection()
        {
            try
            {
                using(SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
