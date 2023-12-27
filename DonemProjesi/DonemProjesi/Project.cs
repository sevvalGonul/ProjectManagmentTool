using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonemProjesi
{
    public class Project
    {
        public string projeAdi;
        public Employee manager;
        public string projeNo;
        public string aciklama;
        public Enums.ProjeDurumu durum;
        public Enums.ParasalGetiriTipi getiriTipi;
        public double parasalGetiri;
        public DateTime kayitTarihi;
        public DateTime? baslangicTarihi;
        public DateTime? bitisTarihi;
        public Enums.ProjeTipi projeTipi;
        public ArrayList files;
        public List<Milestone> milestoneList = new List<Milestone>();

        public Project(string projeAdi, Employee manager, string projeNo, string aciklama, Enums.ProjeDurumu durum, Enums.ParasalGetiriTipi getiriTipi, double kazanc, DateTime kayitTarihi, DateTime? baslangicTarihi, DateTime? bitisTarihi, Enums.ProjeTipi projeTipi, ArrayList files)
        {
            this.projeAdi = projeAdi;
            this.manager = manager;
            this.projeNo = "PRJ" + projeNo;
            this.aciklama = aciklama;
            this.durum = durum;
            this.getiriTipi = getiriTipi;
            this.parasalGetiri = kazanc;
            this.kayitTarihi = kayitTarihi;
            this.baslangicTarihi = baslangicTarihi;
            this.bitisTarihi = bitisTarihi;
            this.projeTipi = projeTipi;
            this.files = files;
        }

        public Project()
        {

        }

        public string FormatProjectDetails()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine($"Proje Adı: {projeAdi}");
            result.AppendLine($"Yönetici: {manager}");
            result.AppendLine($"Proje No: {projeNo}");
            result.AppendLine($"Açıklama: {aciklama}");
            result.AppendLine($"Proje Durumu: {durum}");
            result.AppendLine($"Parasal Getiri Tipi: {getiriTipi}");
            result.AppendLine($"Parasal Getiri: {parasalGetiri}");
            result.AppendLine($"Kayıt Tarihi: {kayitTarihi}");
            result.AppendLine($"Başlangıç Tarihi: {baslangicTarihi}");
            result.AppendLine($"Bitiş Tarihi: {bitisTarihi}");
            result.AppendLine($"Proje Tipi: {projeTipi}");

            // Dosya ve Milestone bilgilerini ekleyin
            result.AppendLine("Dosyalar:");
            foreach (var file in files)
            {
                result.AppendLine($"  - {file}");
            }

            result.AppendLine("Milestone Listesi:");
            foreach (var milestone in milestoneList)
            {
                result.AppendLine($"  - {milestone}");
            }

            return result.ToString();
        }
    }
}
