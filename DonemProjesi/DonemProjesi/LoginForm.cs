using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DonemProjesi
{
    public partial class LoginForm : Form
    {

        public static ArrayList employees = new ArrayList(); // kayıtlı employee'ler

        public static Employee loggedInEmployee = null;

        public static Employee emp = new Employee("Şevval", "Gönül", "sg123", new DateTime(2000, 05, 09).ToString("d"), "sg@gmail.com", new List<String>(), null, "1234567891", Enums.Rol.Admin);
        public static Employee emp2 = new Employee("Ali", "Kaya", "ak123", new DateTime(1998, 08, 09).ToString("d"), "ak@gmail.com", new List<String>(), null, "1234567891", Enums.Rol.Manager);
        public static Employee emp3 = new Employee("Ahmet", "Taş", "at123", new DateTime(1996, 08, 09).ToString("d"), "at@gmail.com", new List<String>(), null, "1234567892", Enums.Rol.Employee);
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            if (!(employees.Contains(emp)))
            {
                employees.Add(emp);
                employees.Add(emp2);
                employees.Add(emp3);
            }
            toolStripStatusLabelDate.Text = DateTime.Now.ToLongDateString();
        }

        private void simpleButtonGiris_Click(object sender, EventArgs e)
        {
            //Employee loggedInEmployee = null;

            // Kullanıcı adı ve şifre kontrolü
            for (int i = 0; i < employees.Count; i++)
            {
                Employee employee = (Employee)employees[i];

                if (textEditKullaniciAdi.Text == employee.tamAd && textEditSifre.Text == employee.sifre)
                {
                    loggedInEmployee = employee;
                    break;
                }
            }

            if (loggedInEmployee == null)
            {
                MessageBox.Show("Kullanıcı adı veya şifre yanlış!", "Geçersiz Kullanıcı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //emp = loggedInEmployee;
                
                MainForm.GetForm.Show();
                this.Hide();
            }

            // Kullanıcı adı ve şifre alanlarını temizleme
            textEditKullaniciAdi.Text = "";
            textEditSifre.Text = "";
        }

        private static LoginForm inst;

        public static LoginForm GetForm
        {
            get
            {
                if (inst == null || inst.IsDisposed)
                    inst = new LoginForm();
                return inst;
            }
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
