using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GunesOtel.V1.DataAccess;

namespace GunesOtel.V1
{
    public partial class FrmLogin: Form
    {
        public FrmLogin()
        {
            InitializeComponent();
            // Enter tuşuna basınca giriş yapma
            this.AcceptButton = btnLogin;
            this.Load += FrmLogin_Load;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            if (SqlHelper.TestConnection())
            {
                MessageBox.Show("✅ Veritabanı bağlantısı başarılı.", "Bağlantı Durumu", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("❌ Veritabanına bağlanılamadı!", "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
