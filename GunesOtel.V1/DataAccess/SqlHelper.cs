using System;
using System.Data.SqlClient;  //Sql Server ile bağlantı kurmak için gerekli
using System.Configuration;
using System.Data;   //App.config dosyasındaki ayarlara ulaşmak için gerekli

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

        // SELECT sorgusu çalıştırır ve sonuçları DataTable olarak döndürür.
        public static DataTable ExecuteDataTable(string commandText,params SqlParameter[] parameters)
        {
            DataTable dt=new DataTable();

            try
            {
                using (SqlConnection conn = GetConnection())
                    using(SqlCommand cmd=new SqlCommand(commandText, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    conn.Open();

                    using(SqlDataAdapter da=new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                }
            }
            catch(Exception ex) 
            {
                // Şimdilik sadece hatayı yeniden fırlatıyoruz, ileride loglama eklenebilir
                throw new Exception("Veritabanı işlemi sırasında hata oluştu: " + ex.Message, ex);
            }

            return dt;
        }

        /// <summary>
        /// SELECT sorgusunun ilk satır ve ilk sütunundaki değeri döndürür
        /// </summary>
        public static object ExecuteScalar(string commandText, params SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Veritabanı işlemi sırasında hata oluştu: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// INSERT, UPDATE, DELETE gibi veri değiştiren sorguları çalıştırır
        /// </summary>
        public static int ExecuteNonQuery(string commandText, params SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Veritabanı işlemi sırasında hata oluştu: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// INSERT sorgusu çalıştırır ve eklenen kaydın ID'sini döndürür
        /// </summary>
        public static int ExecuteInsertReturnId(string commandText, params SqlParameter[] parameters)
        {
            // SCOPE_IDENTITY() ekleyerek yeni eklenen kaydın ID'sini alıyoruz
            commandText += "; SELECT SCOPE_IDENTITY();";

            try
            {
                object result = ExecuteScalar(commandText, parameters);
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Veritabanı ekleme işlemi sırasında hata oluştu: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// SELECT sorgusunu çalıştırır ve SqlDataReader döndürür
        /// </summary>
        public static SqlDataReader ExecuteReader(string commandText, params SqlParameter[] parameters)
        {
            SqlConnection conn = null;

            try
            {
                conn = GetConnection();
                SqlCommand cmd = new SqlCommand(commandText, conn);

                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                conn.Open();
                // CommandBehavior.CloseConnection ile reader kapandığında bağlantı da kapanacak
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                // Bağlantıyı kapatıyoruz
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                throw new Exception("Veritabanı okuma işlemi sırasında hata oluştu: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// SQL parametresi oluşturur
        /// </summary>
        public static SqlParameter CreateParameter(string parameterName, object value)
        {
            return new SqlParameter(parameterName, value ?? DBNull.Value);
        }
    }
}
