using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonemProjesi
{
    public class Enums
    {
        public enum ProjeTipi
        {
            Yurtdisi,
            Tubitak,
            Kobi            
        }

        public enum Rol
        {
            Admin,
            Manager,
            Employee
        }

        public enum ProjeDurumu 
        { 
            OnayBekliyor,
            DevamEdiyor,
            Tamamlandı
        }

        public enum ParasalGetiriTipi
        {
            Günlük,
            Aylık,
            Yıllık
        }
    }
}
