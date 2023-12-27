using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DonemProjesi
{
    public class Employee
    {
        public String ad;
        public String sifre;
        public String soyad;
        public String tamAd;
        public String dogumTarihi;
        public String email;
        public List<String> ekipler;
        public Bitmap fotograf;
        public String tel;
        public Enums.Rol rol;
        

        public Employee(string ad, string soyad, String sifre, string dogumTarihi,
        string email, List<String> ekipler, Bitmap fotograf, String telefonNo, Enums.Rol rol)
        {
            this.ad = ad;
            this.sifre = sifre;
            this.soyad = soyad;
            tamAd = ad + " " + soyad;
            this.dogumTarihi = dogumTarihi;
            this.email = email;
            this.ekipler = ekipler;
            this.fotograf = fotograf;
            this.tel = telefonNo;
            this.rol = rol;
        }

        public override string ToString()
        {
            return $"Tam Ad: {tamAd}, " +
                   $"Doğum Tarihi: {dogumTarihi}, Email: {email}, " +
                   $"Ekipler: {string.Join(", ", ekipler)}, Telefon: {tel}, Rol: {rol}";
        }

    }
}
