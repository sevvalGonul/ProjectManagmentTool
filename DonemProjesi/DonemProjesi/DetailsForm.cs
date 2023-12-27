using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DonemProjesi
{
    public partial class DetailsForm : Form
    {
        private string detaylar;
        public DetailsForm(string detaylar)
        {
            InitializeComponent();
            // Her iki yönde de kaydırabilmek icin
            richTextBoxDetaylar.ScrollBars = RichTextBoxScrollBars.Both;
            this.detaylar = detaylar;
        }

        private void kaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Metin Dosyası (*.txt)|*.txt|Tüm Dosyalar (*.*)|*.*";
                saveFileDialog.Title = "Bilgileri Kaydet";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // RichTextBox'taki proje detaylarını dosyaya kaydet.
                    File.WriteAllText(saveFileDialog.FileName, richTextBoxDetaylar.Text);

                    MessageBox.Show("Bilgiler başarıyla kaydedildi.");
                }
            }
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();

            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                // Kullanıcı bir font seçtiğinde, seçilen fontu TextBox'a uygula
                richTextBoxDetaylar.Font = fontDialog.Font;
            }
        }

        private void btnRenk_Click(object sender, EventArgs e)
        {
            // Renk seçim dialogunu oluştur.
            ColorDialog clrDialog = new ColorDialog();

            // Eğer kullanıcı renk seçimi yaparsa
            if (clrDialog.ShowDialog() == DialogResult.OK)
            {
                // Seçilen rengi RichTextBox'a uygular
                richTextBoxDetaylar.SelectionColor = clrDialog.Color; // Metin rengi
                btnRenk.BackColor = clrDialog.Color; // Buton rengi

            }
        }

        private void kapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();  // ???
        }

        private void hakkındaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Proje Yönetim Uygulaması'nda seçtiğiniz proje/task/milestone bilgilerini görüntüleyebilir ve kaydedebilirsiniz");
        }

        private void DetailsForm_Load(object sender, EventArgs e)
        {
            richTextBoxDetaylar.Text = detaylar;
        }

        private void checkBoxTamamlandi_CheckedChanged(object sender, EventArgs e)
        {
            if(!checkBoxTamamlandi.Checked)
            {
                MessageBox.Show("Tamamlanmamış bir görev hakkında işlem yapmak istediğinize emin misiniz?");
            }
        }
    }
}
