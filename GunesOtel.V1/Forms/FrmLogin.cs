using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GunesOtel.V1.DataAccess;
using GunesOtel.V1.Forms;

namespace GunesOtel.V1
{
    public partial class FrmLogin: Form
    {
        public FrmLogin()
        {
            InitializeComponent();
            // Enter tuşuna basınca giriş yapma
            this.AcceptButton = btnLogin;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = txtUsername.Text.Trim();
            string sifre= txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(sifre))
            {
                MessageBox.Show("Lütfen kullanıcı adı ve şifre girin","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = SqlHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT KullaniciID, AdSoyad, RolID FROM Kullanicilar WHERE KullaniciAdi = @kullaniciAdi AND Sifre = @sifre AND Durum = 1 ";
                SqlCommand cmd= new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
                cmd.Parameters.AddWithValue("@sifre", sifre);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    int kullaniciID = Convert.ToInt32(dr["KullaniciID"]);
                    string adSoyad = dr["AdSoyad"].ToString();
                    int rolID = Convert.ToInt32(dr["RolID"]);

                    MessageBox.Show($"Hoş geldiniz, {adSoyad}", "Giriş Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Rol bazlı yönlendirme:
                    switch (rolID)
                    {
                        case 1:
                            FrmYoneticiAnaPanel frmYonetici = new FrmYoneticiAnaPanel();
                            frmYonetici.Show();
                            break;

                        case 2:
                            FrmMudurAnaPanel frmMudur = new FrmMudurAnaPanel();
                            frmMudur.Show();
                            break;

                        case 3:
                            FrmResepsiyonAnaPanel frmResepsiyon = new FrmResepsiyonAnaPanel();
                            frmResepsiyon.Show();
                            break;

                        default:
                            MessageBox.Show("Yetkisiz kullanıcı rolü!", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                    }

                    this.Hide(); // Giriş formunu gizle
                }
                else
                {
                    MessageBox.Show("Hatalı kullanıcı adı veya şifre!", "Giriş Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
