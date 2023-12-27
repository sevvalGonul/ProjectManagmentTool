using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonemProjesi
{
    public class Milestone
    {
        public string ad;
        public DateTime baslangic;
        public DateTime bitis;
        public List<Task> tasks = new List<Task>();


        public Milestone(string ad, DateTime baslangic, DateTime bitis, List<Task> tasks)
        {
            this.ad = ad;
            this.baslangic = baslangic;
            this.bitis = bitis;
            this.tasks = tasks;
        }

        public Milestone() { }

        public override string ToString() 
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine($"Ad: {ad}");
            result.AppendLine($"Başlangıç Tarihi: {baslangic}");
            result.AppendLine($"Bitiş Tarihi: {bitis}");

            if (tasks.Count > 0)
            {
                result.AppendLine("Görevler:");
                foreach (var task in tasks)
                {
                    result.AppendLine($"  - {task}");
                }
            }

            return result.ToString();
        }
    }
}
