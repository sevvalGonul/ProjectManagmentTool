using DevExpress.ChartRangeControlClient.Core;
using DevExpress.Utils.CommonDialogs;
using DevExpress.XtraEditors;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DonemProjesi
{
    public partial class MainForm : Form
    {

        List<Project> projectList = new List<Project>();
        Project selectedProje;
        Employee selectedEmp;

        public MainForm()
        {
            InitializeComponent();

        }



        private void MainForm_Load(object sender, EventArgs e)
        {
            // Form yüklendiğinde AutoScroll özelliğini etkinleştirme
            panel1.AutoScroll = true;

            // TabControl'ü gizleme
            tabControl1.Visible = false;

            // TabPage'leri TabControl'den kaldırma
            foreach (TabPage page in tabControl1.TabPages)
            {
                tabControl1.TabPages.Remove(page);
            }

            // ToolTip Özelliklerini Ayarlama
            setToolTipConfig(toolTipPicture, 200, 3000, ToolTipIcon.Info, true, "Fotoğraf Seçimi");
            // Tool Tip'i PictureBox'a Ekleme : 
            toolTipPicture.SetToolTip(pictureBox1, "Lütfen çalışanın fotoğrafını eklemek için bu alana tıklayınız.");

            // Dogum tarihi kısıtlama
            dateTimePickerTarih.MinDate = DateTime.Today.AddYears(-100);
            dateTimePickerTarih.MaxDate = DateTime.Today;



            //listBoxEmployeeKayit.Items.AddRange(LoginForm.employees);

            foreach (Employee emp in LoginForm.employees)
            {
                listBoxEmployeeKayit.Items.Add(emp);
            }

            // Comboboxlara enumları ata
            projectTypeBox.DataSource = Enum.GetValues(typeof(Enums.ProjeTipi));
            comboBoxParasalGetiriTipi.DataSource = Enum.GetValues(typeof(Enums.ParasalGetiriTipi));
            

            // DataGridView'ın setlenmesi
            setDataGridViewProjects();
            

            dataGridViewProjects.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewProjects.ReadOnly = true;


        }



        private void setDataGridViewProjects()
        {
            dataGridViewProjects.ColumnCount = 6;
            dataGridViewProjects.Columns[0].Name = "Proje Adı";
            dataGridViewProjects.Columns[1].Name = "Proje No";
            dataGridViewProjects.Columns[2].Name = "Yönetici";
            dataGridViewProjects.Columns[3].Name = "Açıklama";
            dataGridViewProjects.Columns[4].Name = "Başlangıç Tarihi";
            dataGridViewProjects.Columns[5].Name = "Bitiş Tarihi";
        }

        private void setToolTipConfig(System.Windows.Forms.ToolTip tp, int autoDelay, int popDelay, ToolTipIcon icon, bool isBallon, string title)
        {
            tp.AutomaticDelay = autoDelay;  // Starting time
            tp.AutoPopDelay = popDelay; // Waiting on screen time
            tp.ToolTipTitle = title;
            tp.ToolTipIcon = icon;
            tp.IsBalloon = isBallon;

        }

        private void warnEmptyTextBox(System.Windows.Forms.TextBox textBox, string message)  // TextBox boş ise uyarı verir ve text boxa odaklar
        {
            string text = textBox.Text;
            if (string.IsNullOrWhiteSpace(text))
            {
                MessageBox.Show(message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox.Focus();
            }

        }

        private static MainForm singMain;
        public static MainForm GetForm  // Main Form nesnesini çağıran singleton fonksiyon
        {
            get
            {
                if (singMain == null || singMain.IsDisposed)
                    singMain = new MainForm();
                return singMain;
            }
        }


        private void txtCalisanAdi_Leave(object sender, EventArgs e)
        {
            warnEmptyTextBox(txtCalisanAdi, "Lütfen Çalışan Adı Giriniz");
        }

        private void txtCalisanSoyadi_Leave(object sender, EventArgs e)
        {
            warnEmptyTextBox(txtCalisanSoyadi, "Lütfen Çalışan SoyAdı Giriniz");
        }

        private void txtCalisanAdi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)  // Enter Tuşuna Basılınca soyad textbox'ına yönlendirir
            {
                txtCalisanSoyadi.Focus();
            }
        }

        private void txtCalisanSoyadi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)  // Enter Tuşuna Basılınca Ad ve Soyad girilmiş ise tam ad labelını günceller
            {
                warnEmptyTextBox(txtCalisanSoyadi, "Lütfen Hasta Soyadını Giriniz");
                if (!string.IsNullOrWhiteSpace(txtCalisanAdi.Text))
                {
                    lblTamAdi.Text = txtCalisanAdi.Text + " " + txtCalisanSoyadi.Text;
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "c:\\";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Bitmap bitmap = new Bitmap(ofd.FileName);
                pictureBox1.Image = bitmap;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private bool checkInputsForEmployee()
        {
            if (string.IsNullOrEmpty(txtCalisanAdi.Text) ||
                string.IsNullOrEmpty(txtCalisanSoyadi.Text) ||
                string.IsNullOrEmpty(txtEmail.Text) ||
                !maskedTextBoxPhone.MaskFull ||
                string.IsNullOrEmpty(comboBoxRol.Text) ||
                !txtEmail.Text.Contains("@") ||
                pictureBox1.Image == null)
            {
                return false;
            }

            return true;

        }

        private void btnCalisanEkle_Click(object sender, EventArgs e)
        {
            if (LoginForm.loggedInEmployee.rol.Equals(Enums.Rol.Admin))
            {
                openTabPage(tabPageCalisan);
            }
            else
            {
                MessageBox.Show("Sadece adminler çalışan ekleyebilir!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void openTabPage(TabPage tabPage)
        {
            if (tabControl1.TabPages.Contains(tabPage) == true)
            {
                tabControl1.SelectedTab = tabPage;
            }
            else
            {
                tabControl1.Visible = true;
                tabControl1.TabPages.Add(tabPage);
                //tabControl1.SelectedTab = tabPage;
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {

            if (tabControl1.SelectedTab == tabPageCalisan)
            {
                if (checkInputsForEmployee())
                {
                    // Tüm textboxlar, picturebox, combobox doldurulmussa calisan kaydedilir
                    recordEmployee();
                    clearCalisanEklePage();

                }
                else
                {
                    // Doldurulmamıs alan vardır. Kullaniciya uyarı gösterilir
                    MessageBox.Show("Lütfen çalışan kaydı için tüm alanları doldurun", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (tabControl1.SelectedTab == tabPageProjeEkle)  // Proje eklenmesi islemi burada treeye de kayıt ediliyor
            {
                if (checkProjectFields())
                {
                    recordProject();
                    treeView1.Nodes.Add(projectName.Text);
                    clearProjeEklePage();

                }
            }
        }

        private void recordProject()
        {
            Project project = new Project();

            project.projeAdi = projectName.Text;
            project.projeNo = "PR" + numUpDownprojectNo.Value.ToString();
            project.manager = managerBox.SelectedItem as Employee;
            project.baslangicTarihi = estStartDate.Value;
            project.bitisTarihi = estFinishDate.Value;
            project.projeTipi = (Enums.ProjeTipi)projectTypeBox.SelectedItem;
            
            if (radioButtonDevamEdiyor.Checked)
            {
                project.durum = Enums.ProjeDurumu.DevamEdiyor;
            }
            else if (radioButtonOnayBekliyor.Checked)
            {
                project.durum = Enums.ProjeDurumu.OnayBekliyor;
            }
            else
            {
                project.durum = Enums.ProjeDurumu.Tamamlandı;
            }
            project.getiriTipi = (Enums.ParasalGetiriTipi)comboBoxParasalGetiriTipi.SelectedItem;
            project.kayitTarihi = DateTime.Now;
            if (Double.TryParse(textBoxParasalGetiri.Text, out double kazanc))
            {
                project.parasalGetiri = kazanc;
            }
            ArrayList files = new ArrayList(); // Projedeki dosyalar için oluşturulan ArrayList

            foreach (var listBoxItem in listBoxDokuman.Items)
            {
                files.Add(listBoxItem.ToString()); // projedeki dosyalar files ArrayListine eklenir 
            }
            project.files = files;
            project.aciklama = richTextBoxAciklama.Text;

            projectList.Add(project);
            dataGridViewProjects.Rows.Add(project.projeAdi, project.projeNo, project.manager, project.aciklama, project.baslangicTarihi, project.bitisTarihi);

            //treeView1.Nodes.Add(project.projeAdi);

        }

        private bool checkProjectFields()
        {
            if (string.IsNullOrEmpty(projectName.Text))
            {
                MessageBox.Show("Proje ismi boş olamaz!");
                projectName.Focus();
                return false;
            }
            foreach (Project p in projectList)
            {
                if (p.projeAdi.Equals(projectName.Text))
                {
                    MessageBox.Show("Proje isimleri aynı olamaz!");
                    projectName.Clear();
                    projectName.Focus();
                    return false;
                }
                else if (p.projeNo.Equals("PR" + numUpDownprojectNo.Value))
                {
                    MessageBox.Show("Proje numaraları aynı olamaz!");
                    numUpDownprojectNo.Value = 0;
                    return false;
                }

            }
            if (!(radioButtonDevamEdiyor.Checked || radioButtonOnayBekliyor.Checked || radioButtonTamamlandı.Checked))
            {
                MessageBox.Show("Lütfen proje durumunu işaretleyiniz!");
                return false;
            }

            if(estStartDate.Value > estFinishDate.Value)
            {
                MessageBox.Show("Başlangıç tarihi bitiş tarihinden büyük olamaz!");
                return false;
            }
            return true;
        }

        private void recordEmployee()
        {
            if (comboBoxRol.SelectedItem != null)
            {
                Enums.Rol selectedRol;
                if (Enum.TryParse(comboBoxRol.SelectedItem.ToString(), out selectedRol))
                {
                    // 'selectedRol' değişkeni Enums.Rol türünde:
                    Employee emp = new Employee(txtCalisanAdi.Text, txtCalisanSoyadi.Text, txtSifre.Text, 
                        dateTimePickerTarih.Value.ToShortDateString(), txtEmail.Text,
                    checkedListBox1.CheckedItems.OfType<string>().ToList(), (Bitmap)pictureBox1.Image, maskedTextBoxPhone.Text, selectedRol);
 
                    LoginForm.employees.Add(emp);
                    // ListBox'ta çalısanin görüntülenmesi icin calisan ListBox'a kaydedilir
                    listBoxEmployeeKayit.Items.Add(emp);



                }
                else
                {
                    // Dönüşüm başarısız oldu
                    MessageBox.Show("Geçersiz rol seçimi!");
                }
            }

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void textBoxParasalGetiri_TextChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(textBoxParasalGetiri.Text))
            {
                string enteredText = textBoxParasalGetiri.Text;

                // Metnin sayıya dönüştürülmeye çalışılması:
                if (!double.TryParse(enteredText, out double result))
                {
                    // Eğer dönüştürme başarısız ise, metin sayısal değil demektir
                    MessageBox.Show("Lütfen sadece sayısal değerler girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Text box'un temizlenmesi
                    textBoxParasalGetiri.Text = "";
                }

            }
        }

        private void btnDokumanEkle_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog(); // proje dokumanı eklemek icin
            String filename = openFileDialog1.FileName;
            listBoxDokuman.Items.Add(filename);
        }

        private void btnProjeEkle_Click(object sender, EventArgs e)
        {
            setManagers();
            if (LoginForm.loggedInEmployee.rol.Equals(Enums.Rol.Admin) || LoginForm.loggedInEmployee.rol.Equals(Enums.Rol.Manager))
            {  // Sadece adminler veya yöneticiler proje ekleyebilir
                openTabPage(tabPageProjeEkle);

            }
            else
            {
                MessageBox.Show("Sadece adminler veya yöneticiler proje ekleyebilir!");
            }

        }

        private void setManagers()
        {
            foreach (Employee emp in LoginForm.employees)
            {
                if (emp.rol == Enums.Rol.Manager)
                {
                    if (!managerBox.Items.Contains(emp))
                    {
                        managerBox.Items.Add(emp);
                    }

                }
            }
        }

        private void btnProjeler_Click(object sender, EventArgs e)
        {
            showEmployeesForProjects();
            openTabPage(tabPageProjeler);
        }


        private void btnGoster_Click(object sender, EventArgs e)
        {
            TreeNode secilen = treeView1.SelectedNode;
            string detaylar = "";
            if (secilen == null)
            {
                MessageBox.Show("Lütfen seçim yapınız");
            }
            else
            {
                if (secilen.Parent == null)  // Proje seçilmis
                {
                    Project selectedProject = findSelectedProject(secilen.Text);
                    detaylar = "Proje Bilgileri:\n" + selectedProject.FormatProjectDetails();
                }
                else if (secilen.Parent.Parent == null)  // Milestone seçilmiş
                {
                    Project parentProject = findSelectedProject(secilen.Parent.Text);
                    Milestone selectedMs = findSelectedMilestone(secilen.Text, parentProject);
                    detaylar = "Milestone Bilgileri:\n" + selectedMs.ToString() + "\nMilestone'un ait olduğu Proje bilgileri:\n" + parentProject.FormatProjectDetails();
                }
                else  // Task seçilmiş
                {
                    Project parentProject = findSelectedProject(secilen.Parent.Parent.Text);  // 
                    Milestone parentMs = findSelectedMilestone(secilen.Parent.Text, parentProject);
                    Task selectedTask = findSelectedTask(secilen.Text, parentMs);
                    detaylar = "Task Bilgileri:\n" + selectedTask.ToString() +
                        "\nTask'ın ait olduğu Milestone Bilgileri:\n" + parentMs.ToString() +
                        "\nMilestone'un ait olduğu Proje bilgileri:\n" + parentProject.FormatProjectDetails();
                }

                // Yeni form oluştur:
                DetailsForm detailsForm = new DetailsForm(detaylar);
                detailsForm.Show();
            }
        }

        private Task findSelectedTask(string text, Milestone parentMs)
        {
            foreach (Task task in parentMs.tasks)
            {
                if (text.Equals(task.konu))
                {
                    return task;
                }
            }
            return null;
        }

        private void btnHepsiniGoster_Click(object sender, EventArgs e)
        {
            treeView1.ExpandAll();

        }

        // TreeView'a task veya milestone ekleme
        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (LoginForm.loggedInEmployee.rol.Equals(Enums.Rol.Employee))
            {
                MessageBox.Show("Yetkiniz yok!");
            }
            else if (!string.IsNullOrEmpty(textBoxmtAd.Text))
            {
                TreeNode secilen = treeView1.SelectedNode;
                if (secilen != null)
                {
                    if (secilen.Parent == null)  // Proje secilmis km tası eklenmek istiyor
                    {
                        Project selectedProject = findSelectedProject(secilen.Text);
                        if(dateTimePickerBaslangic.Value.Date < selectedProject.baslangicTarihi.Value.Date)
                        {
                            MessageBox.Show("Eklenecek kilometre taşının başlangıç tarihi projenin başlangıç tarihinden erken olamaz!");
                        }
                        else
                        {
                            Milestone ms = new Milestone();
                            ms.ad = textBoxmtAd.Text;
                            ms.baslangic = dateTimePickerBaslangic.Value;
                            ms.bitis = dateTimePickerBitis.Value;
                            selectedProject.milestoneList.Add(ms);
                            secilen.Nodes.Add(ms.ad);
                            textBoxmtAd.Text = "";
                        }

                    }
                    else if (secilen.Parent.Parent == null)  // Km tası secilmis task eklenmek isteniyor
                    {
                        if (comboBoxEmployee.SelectedIndex == -1)
                        {
                            MessageBox.Show("Lütfen task için bir çalışan seçiniz!");
                        }
                        else
                        {
                            Project parentProject = findSelectedProject(secilen.Parent.Text);
                            Milestone selectedMs = findSelectedMilestone(secilen.Text, parentProject);
                            if(dateTimePickerBaslangic.Value < selectedMs.baslangic)
                            {
                                MessageBox.Show("Eklenecek görevin başlangıç tarihi kilometre taşının başlangıç tarihinden erken olamaz!");
                            } 
                            else
                            {
                                Task task = new Task();
                                task.konu = textBoxmtAd.Text;
                                task.baslangic = dateTimePickerBaslangic.Value;
                                task.bitis = dateTimePickerBitis.Value;
                                task.calisan = comboBoxEmployee.SelectedItem as Employee;
                                selectedMs.tasks.Add(task);
                                secilen.Nodes.Add(task.konu);
                                textBoxmtAd.Text = "";
                            }

                        }
                    }
                    
                }
                else
                {
                    MessageBox.Show("Lütfen seçim yapınız!");
                }
            }
        }

        private Milestone findSelectedMilestone(string text, Project parentProject)
        {
            foreach (Milestone ms in parentProject.milestoneList)
            {
                if (ms.ad.Equals(text))
                {
                    return ms;
                }
            }
            return null;
        }

        private Project findSelectedProject(string name)
        {

            foreach (Project p in projectList)
            {
                if (p.projeAdi.Equals(name))
                {
                    return p;
                }

            }
            return null;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPageProjeler)
            {
                showEmployeesForProjects();
            }
            else if (tabControl1.SelectedTab == tabPageProjeEkle)
            {
                setManagers();
            }
        }

        private void showEmployeesForProjects()
        {
            foreach (Employee emp in LoginForm.employees)
            {
                if (!comboBoxEmployee.Items.Contains(emp))
                {
                    comboBoxEmployee.Items.Add(emp);
                }
            }

        }

        private void btnSec_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPageProjeEkle)
            {
                if (dataGridViewProjects.SelectedRows.Count > 0)
                {
                    selectedProje = null;  // önceki secim varsa temizlemek icin

                    DataGridViewRow selectedRow = dataGridViewProjects.CurrentRow;
                    string selectedProjectName = selectedRow.Cells["Proje Adı"].Value.ToString();
                    
                    selectedProje = findSelectedProject(selectedProjectName);
                    projectName.Text = selectedProje.projeAdi.ToString();
                    richTextBoxAciklama.Text = selectedProje.aciklama.ToString();
                    textBoxParasalGetiri.Text = selectedProje.parasalGetiri.ToString();
                    if(selectedProje.durum != null && selectedProje.durum == Enums.ProjeDurumu.Tamamlandı)
                    {
                        radioButtonTamamlandı.Checked = true;
                    }
                    else if(selectedProje.durum == Enums.ProjeDurumu.DevamEdiyor)
                    {
                        radioButtonDevamEdiyor.Checked = true;
                    }
                    else if(selectedProje.durum == Enums.ProjeDurumu.OnayBekliyor)
                    {
                        radioButtonOnayBekliyor.Checked = true;
                    }

                }
                else
                {
                    MessageBox.Show("Lütfen bir proje seçiniz.");
                }
            }
            else if(tabControl1.SelectedTab == tabPageCalisan)
            {
                // Önceki secimleri temizle
                selectedEmp = null;
                clearCalisanEklePage();

                selectedEmp = listBoxEmployeeKayit.SelectedItem as Employee;
                if(selectedEmp != null)
                {
                    txtCalisanAdi.Text = selectedEmp.ad;
                    txtCalisanSoyadi.Text = selectedEmp.soyad;
                    txtEmail.Text = selectedEmp.email;
                    maskedTextBoxPhone.Text = selectedEmp.tel;
                    txtSifre.Text = selectedEmp.sifre;
                    if(selectedEmp.fotograf != null)
                    {
                        pictureBox1.Image = selectedEmp.fotograf;
                    }

                }
            }
        }

        private void btnCikisYap_Click(object sender, EventArgs e)
        {
            LoginForm.loggedInEmployee = null;  // 

            tabControl1.TabPages.Clear();
            clearProjeEklePage();
            clearCalisanEklePage();
            LoginForm.GetForm.Show();
            this.Hide();
        }

        private void clearProjeEklePage()
        {
            projectName.Text = "";
            numUpDownprojectNo.Value = 0;
            richTextBoxAciklama.Text = "";
            listBoxDokuman.Items.Clear();
            managerBox.SelectedItem = null;
            textBoxParasalGetiri.Text = "";
            radioButtonDevamEdiyor.Checked = false;
            radioButtonOnayBekliyor.Checked = false;
            radioButtonTamamlandı.Checked = false;
            
        }

        private void clearCalisanEklePage()
        {
            txtCalisanAdi.Text = "";
            txtCalisanSoyadi.Text = "";
            lblTamAdi.Text = string.Empty;
            txtEmail.Text = "";
            txtSifre.Text = "";
            maskedTextBoxPhone.Clear();
            pictureBox1.Image = null;
            checkedListBox1.ClearSelected();  // 
        }



        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPageProjeEkle)
            {
                if (selectedProje == null)
                {
                    MessageBox.Show("Lütfen bir proje seçiniz!");
                }
                else
                {
                    if (selectedProje.projeAdi.Equals(projectName.Text))
                    {
                        dataGridViewProjects.Rows.Remove(dataGridViewProjects.SelectedRows[0]);
                        projectList.Remove(selectedProje);
                        selectedProje = null;  // 
                        recordProject();
                        clearProjeEklePage();
                    }
                    else
                    {
                        MessageBox.Show("Lütfen proje adını değiştirmeyiniz");
                    }



                }
            }
            else if(tabControl1.SelectedTab == tabPageCalisan)
            {
                if(selectedEmp == null)
                {
                    MessageBox.Show("Lütfen bir çalışan seçiniz!");
                }
                else
                {
                    listBoxEmployeeKayit.Items.Remove(selectedEmp);
                    comboBoxEmployee.Items.Remove(selectedEmp);
                    LoginForm.employees.Remove(selectedEmp);
                    recordEmployee();
                    clearCalisanEklePage();
                    selectedEmp = null;
                }
            }
        }

        private void btnNodeSil_Click(object sender, EventArgs e)
        {
            TreeNode secilen;
            secilen = treeView1.SelectedNode;

            if (secilen.Parent == null)  // Proje silme
            {
                treeView1.Nodes.Remove(secilen);
                Project silinecek = findSelectedProject(secilen.Text);
                projectList.Remove(silinecek);
                // datagridviewdan kaldırma
                string targetValue = secilen.Text; // Silinmek istenen hücre değeri

                for (int i = dataGridViewProjects.Rows.Count - 1; i >= 0; i--)
                {
                    DataGridViewRow row = dataGridViewProjects.Rows[i];
                    DataGridViewCell cell = row.Cells["Proje Adı"];

                    if (cell.Value != null && cell.Value.ToString() == targetValue)
                    {
                        dataGridViewProjects.Rows.Remove(row);
                    }
                }
            }
            else if (secilen.Parent.Parent == null)  // milestone silme
            {
                //Milestone'un ait oldugu proje:
                Project parentProject = findSelectedProject(secilen.Parent.Text);
                Milestone silinecekMs = findSelectedMilestone(secilen.Text, parentProject);
                // Projenin milestone listesinden kaldırılması
                parentProject.milestoneList.Remove(silinecekMs);
                secilen.Parent.Nodes.Remove(secilen);

            }
            else  // task silme
            {
                // Task'ın ait olduğu milestone 
                Project parentProject = findSelectedProject(secilen.Parent.Parent.Text);
                Milestone parentMilestone = findSelectedMilestone(secilen.Parent.Text, parentProject);
                Task silinecekTask = findSelectedTask(secilen.Text, parentMilestone);
                // Milestone'un task listesinden taskın silinmesi
                parentMilestone.tasks.Remove(silinecekTask);
                secilen.Parent.Nodes.Remove(secilen);

            }

        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            try
            {
                TabPage current_tab = tabControl1.SelectedTab;
                tabControl1.TabPages.Remove(current_tab);

            }
            catch
            {
                MessageBox.Show("Seçili Bir Sayfa Olmadığından Silme İşlemi Gerçekleşmedi.", "Sayfa Kaldırma Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btnGecikmisProjeleriGoster_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewProjects.Rows)
            {
                // "Proje Adı" sütununun indeksini al
                int projeAdiColumnIndex = dataGridViewProjects.Columns["Proje Adı"].Index;

                // Eğer sütun indeksi geçerli ise ve hücre değeri null değilse
                if (projeAdiColumnIndex >= 0 && row.Cells[projeAdiColumnIndex].Value != null)
                {
                    // "Proje Adı" sütunundaki değeri al
                    string projeAdi = row.Cells[projeAdiColumnIndex].Value.ToString();

                    // Projeyi bul
                    Project project = findSelectedProject(projeAdi);

                    if (project != null)
                    {
                        // Proje durumu Onay Bekliyor veya Devam Ediyor ve bitiş tarihi geçmişse
                        if ((project.durum == Enums.ProjeDurumu.OnayBekliyor || project.durum == Enums.ProjeDurumu.DevamEdiyor)
                            && project.bitisTarihi.HasValue && project.bitisTarihi.Value < DateTime.Now)
                        {
                            row.DefaultCellStyle.BackColor = Color.Red; // Satırın arka plan rengini kırmızı yap
                        }
                    }
                }
            }
        }

    }
}
