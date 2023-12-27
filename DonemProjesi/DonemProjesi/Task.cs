using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonemProjesi
{
    public class Task
    {
        public string konu;
        public Employee calisan;
        public DateTime baslangic;
        public DateTime bitis;

        public Task(string konu, Employee calisan, DateTime baslangic, DateTime bitis)
        {
            this.konu = konu;
            this.calisan = calisan;
            this.baslangic = baslangic;
            this.bitis = bitis;
        }

        public Task() { }

        public override string ToString()
        {
            return $"Konu: {konu}, Çalışan: {calisan}, Başlangıç Tarihi: {baslangic}, Bitiş Tarihi: {bitis}";
        }
    }
}
