using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace Karakter_Analiz_Testi
{
    public partial class Form1 : Form
    {
        Thread sure;
        Veriler veri = new Veriler();

        int kacinci = 1,saniye=0, puan=0;
        
        public Form1()
        {
            InitializeComponent();
        }

        void soru()
        {
            label1.Text = "Soru" + kacinci;

            richTextBox1.Text = veri.question(kacinci);
            if(kacinci<10)
                buttonGoster(veri.answers(kacinci).Count(), kacinci);//Sorulardaki Şık sayısını button gonder adlı methoda gönderiyorum.

            if (kacinci > 10)
            {

                sure.Abort();
                label1.Text = "";
                MessageBox.Show("Testiniz bitti Tebrikler!!");
                buttonsEnable(false);
                richTextBox1.ReadOnly = false ;

                richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, richTextBox1.Font.Size -2, richTextBox1.Font.Style);


                richTextBox1.Height = 250;
                richTextBox1.Text = veri.sonuclar(puan) + "\n\n PUANINIZ= " + puan + "\n Bitirme Süreniz = " + label3.Text.Substring(5) + "Saniye";
                label3.Text = "";
            }
            kacinci++;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            buttonsEnable(false);
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            soru();
            puan += veri.puanlama(kacinci, "a");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            soru();
            puan += veri.puanlama(kacinci, "b");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            soru();
            puan += veri.puanlama(kacinci, "c");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            soru();
            puan += veri.puanlama(kacinci, "d");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            soru();
            puan += veri.puanlama(kacinci, "e");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            soru();
            puan += veri.puanlama(kacinci, "f");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            soru();
            puan += veri.puanlama(kacinci, "g");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            sure = new Thread(new ThreadStart(time));
            sure.Start();
            button8.Visible = false;
            buttonsEnable(true);
            soru();
        }
        
        void buttonsEnable(bool buton)
        {
            button1.Enabled = buton;
            button2.Enabled = buton;
            button3.Enabled = buton;
            button4.Enabled = buton;
            button5.Enabled = buton;
            button6.Enabled = buton;
            button7.Enabled = buton;
        }

        private void buttonGoster(int uzunluk, int hangisoru)//cevap şık uzunluğu
        {
            List<string> cevaplar = veri.answers(hangisoru);

            for (int i=1;i<=7;i++)
            {
                Control c = this.Controls["button" + (i)]; // Bu yöntem ile istediğimiz nesnelere döngü yardımı ile işlem yapabiliyoruz.

                if (i <= uzunluk)
                {
                    c.Visible = true;
                    c.Text = cevaplar[i - 1];
                }
                else
                    c.Visible = false;
            }
        }

        void time()
        {
            while (true)
            {
                label3.Text = "Süre= " + saniye.ToString();
                Thread.Sleep(1000);
                saniye++;
            }
        }
    }

    class Veriler
    {
        public string question(int hangisoru)
        {

            string[] questions = { "Kendinizi ne zaman en iyi hissedersiniz?", "Nasıl yürürsünüz?", "İnsanlarla konuşurken", "Dinlenirken nasıl oturursunuz?", "Çok hoşunuza giden bir şey olduğunda ne yaparsınız?", "Bir partiye veya sosyal etkinliğe katıldığınızda", "Çok zor bir işe dikkatinizi vermişken rahatsız ediliyorsunuz. Ne yaparsınız?", "En çok hangi rengi seversiniz?", "Yatakta uyumadan önceki birkaç dakikada", "Rüyanızda genellikle" };

            int index = hangisoru - 1;
            if (index < questions.Length)
                return questions[index];
            else return "Bitti";
        }

        public List<string> answers(int hangisoru)
        {
            Dictionary<int, List<string>> answers = new Dictionary<int, List<string>>();

            answers.Add(1, new List<string> { "Sabahları", "Öğlenden sonra ve akşama doğru", "Gecenin ilerleyen saatlerinde" });
            answers.Add(2, new List<string> { "Hızlı ve uzun adımlarla", "Hızlı ve kısa adımlarla", "Normalden yavaş ve etrafa bakınarak", "Yavaş ve başı eğik", "Çok yavaş" });
            answers.Add(3, new List<string> { "Kollarımı göğsümde katlamış olarak dururum", "Ellerimi sıkarım", "Bir veya iki elimi belime koyarım", "Konuştuğum insanlara dokunur veya ittiririm", "Kulağımla oynar, çeneme dokunur veya saçımı düzeltirim" });
            answers.Add(4, new List<string> { "Dizler katlanmış ve bacaklar birbirine bitişik olarak", "Bacaklar çaprazlanmış olarak", "Bacaklarımı uzatarak", "Bir bacağımı altıma katlayarak" });
            answers.Add(5, new List<string> { "Büyük bir kahkaha atarım", "Gülerim ama fazla sesli değil", "Bir kerelik gülerim", "Sessizce gülümserim" });
            answers.Add(6, new List<string> { "Herkes sizi fark edecek şekilde gürültülü bir giriş mi yaparsınız?", "Sessiz bir giriş yapıp etrafınızda tanıdığınız birilerine mi bakınırsınız?", "Çok sessizce girip kimsenin sizi fark etmemesine mi gayret edersiniz?" });
            answers.Add(7, new List<string> { "Bölünmeyi memnuniyetle karşılarım", "Aşırı derecede rahatsız olurum", "Belli olmaz.Bu iki uç arasında değişken davranışlar gösteririm" });
            answers.Add(8, new List<string> { "Kırmızı veya portakal rengi", "Siyah", "Sarı veya mavi", "Yeşil", "Koyu mavi veya mor", "Beyaz", "Kahverengi veya gri" });
            answers.Add(9, new List<string> { "Sırt üstü yatıp uzanırsınız", "Karnınızın üstüne yatıp uzanırsınız", "Hafif kıvrılmış olarak yan tarafınıza yatarsınız", "Başınızı bir kolunuzun üzerine koyarsınız", "Başınızı yorganın altına kapatırsınız" });
            answers.Add(10, new List<string> { "Düşersiniz", "Kavga eder veya tartışırsınız", "Birilerini veya bir şeyler ararsınız", "Uçar veya yüzersiniz", "Genelde rüya görmezsiniz", "Rüyalarınız daima hoştur" });

            return answers[hangisoru];
        }

        public string sonuclar(int puan)
        {
            string sonuc = "";
            if (puan >= 31 && puan <= 40)
                sonuc = "İnsanlar sizi mantıklı, ihtiyatlı, dikkatli ve pratik birisi olarak görürler. Sizi zeki, yetenekli ve hünerli ama alçak gönüllü olarak tanırlar. Çok hızlı arkadaşlık kurmayan, ama arkadaşlarına karşı çok sadık olan ve onlardan da aynı şeyi bekleyen birisiniz.";
            else if (puan >= 41 && puan <= 50)
                sonuc = "İnsanlar sizi taze, canlı, çekici, eğlendirici, pratik ve daima ilginç birisi olarak görürler; her zaman ilgi odağı olan ama çok aşırıya kaçmayacak kadar da dengeli birisi.. İnsanlar sizi ayrıca iyiliksever, düşünceli, anlayışlı ve kendilerini neşelendiren ve rahatlatan birisi olarak tanırlar.";
            else if (puan >= 51 && puan <= 60)
                sonuc = "insanlar sizi heyecan verici, havai, düşüncesiz yapıda, doğal liderlik özellikleri olan, her zaman doğru olmasa da hızlı karar veren birisi olarak tanırlar. Seni cesur, maceraperest birisi olarak tanırlar; her şeyi bir kez denemek isteyen, macera yaşamak için fırsatları kaçırmayan birisi.. Yaydığınız heyecandan dolayı insanlar sizinle aynı iş yerinde yaşamaktan zevk alırlar.";
            else sonuc = "İnsanlar sana kırılgan bir eşya muamelesi yapıyorlar. Kibirli, bencil ve aşırı baskın birisi olarak görülüyorsun. İnsanlar size hayranlık duyup sizin gibi olmak isteyebilirler ama size her zaman güvenmezler ve sizinle çok yakın ilişkide olmaktan kaçınırlar.";

            return sonuc;
        }
        public int puanlama(int soru, string cevap)
        {
            List<Tuple<int, string, int>> Puan = new List<Tuple<int, string, int>>();//İlk defa denediğim bir şey. 3 veri tipi tutmak için kullanılıyor.
            //Şuan acaba bir veritabanı mı kulansaydım diye düşünüyorum veri tabanını koduma koyamazdım :p
            Puan.Add(Tuple.Create(1, "a", 2));
            Puan.Add(Tuple.Create(1, "b", 4));
            Puan.Add(Tuple.Create(1, "c", 6));

            Puan.Add(Tuple.Create(2, "a", 6));
            Puan.Add(Tuple.Create(2, "b", 4));
            Puan.Add(Tuple.Create(2, "c", 7));
            Puan.Add(Tuple.Create(2, "d", 2));
            Puan.Add(Tuple.Create(2, "e", 1));

            Puan.Add(Tuple.Create(3, "a", 4));
            Puan.Add(Tuple.Create(3, "b", 2));
            Puan.Add(Tuple.Create(3, "c", 5));
            Puan.Add(Tuple.Create(3, "d", 7));
            Puan.Add(Tuple.Create(3, "e", 6));

            Puan.Add(Tuple.Create(4, "a", 4));
            Puan.Add(Tuple.Create(4, "b", 6));
            Puan.Add(Tuple.Create(4, "c", 2));
            Puan.Add(Tuple.Create(4, "d", 1));

            Puan.Add(Tuple.Create(5, "a", 6));
            Puan.Add(Tuple.Create(5, "b", 4));
            Puan.Add(Tuple.Create(5, "c", 3));
            Puan.Add(Tuple.Create(5, "d", 5));
            Puan.Add(Tuple.Create(5, "e", 2));

            Puan.Add(Tuple.Create(6, "a", 6));
            Puan.Add(Tuple.Create(6, "b", 4));
            Puan.Add(Tuple.Create(6, "c", 2));

            Puan.Add(Tuple.Create(7, "a", 6));
            Puan.Add(Tuple.Create(7, "b", 2));
            Puan.Add(Tuple.Create(7, "c", 4));

            Puan.Add(Tuple.Create(8, "a", 6));
            Puan.Add(Tuple.Create(8, "b", 7));
            Puan.Add(Tuple.Create(8, "c", 5));
            Puan.Add(Tuple.Create(8, "d", 4));
            Puan.Add(Tuple.Create(8, "e", 3));
            Puan.Add(Tuple.Create(8, "f", 2));
            Puan.Add(Tuple.Create(8, "g", 1));

            Puan.Add(Tuple.Create(9, "a", 7));
            Puan.Add(Tuple.Create(9, "b", 6));
            Puan.Add(Tuple.Create(9, "c", 4));
            Puan.Add(Tuple.Create(9, "d", 2));
            Puan.Add(Tuple.Create(9, "e", 1));

            Puan.Add(Tuple.Create(10, "a", 4));
            Puan.Add(Tuple.Create(10, "b", 2));
            Puan.Add(Tuple.Create(10, "c", 3));
            Puan.Add(Tuple.Create(10, "d", 5));
            Puan.Add(Tuple.Create(10, "e", 6));
            Puan.Add(Tuple.Create(10, "f", 1));

            int p = 0;
            foreach (var i in Puan)
                if (i.Item1 == soru && i.Item2 == cevap)
                    p = i.Item3;
            return p;
        }
    }
}
