using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace panel1
{
    public partial class UCbutton1 : UserControl
    {
        
        Action<HasilPengujian> selesaiUji;
        
        KendaliTombol kendali;
        Stopwatch timer;
        Timer updater;

        bool[] hasilTekan;
        double[] hasilDurasiRespon;

        int sesi = 0;
        int lap = 0;
        const int jumlahSesi = 10;
        const long durasiSesi = 3000;  // 3 sekon
        const double duaPi = Math.PI * 2;
        const double frekuensi = 0.05;
        const double skala = 0.85;
        const int timeStep = 14;  //sampling

        List<double> px;
        List<double> py;
        int metod;

        public UCbutton1(Action<HasilPengujian> fungsiSelesai, MetodeDeteksi metode, ArahTombol arah)
        {
            InitializeComponent();
            selesaiUji = fungsiSelesai;
            hasilTekan = new bool[jumlahSesi];
            hasilDurasiRespon = new double[jumlahSesi];

            kendali = new KendaliTombol(metode);
            kendali.TambahTombol(tombol, new FungsiTombol(HasilCekTombol));

            px = new List<double>();
            py = new List<double>();
            px.Add(0);
            py.Add(0);
            px.Add(0);
            py.Add(0);
            px.Add(0);
            py.Add(0);

            px[0] = 240;
            py[1] = 110;
            px[2] = 150;
            py[2] = 540;

            switch(arah)
            {                
                case ArahTombol.Horizontal:
                    metod = 0;
                    break;
                case ArahTombol.Vertikal:
                    metod = 1;
                    break;
                case ArahTombol.Diagonal:
                    metod = 2;
                    break;
            }

            timer = new Stopwatch();
            timer.Start();

            updater = new Timer();
            updater.Tick += Update;
            updater.Interval = timeStep;
            updater.Start();
            kendali.Start();
        }
        public void HasilCekTombol(ArgumenKendaliTombol args)
        {
            if (sesi < 10)
            {
                if (args.status && !hasilTekan[sesi])     //ngecek setiap tombol untuk dilakukan testing
                {
                    hasilTekan[sesi] = true;
                    hasilDurasiRespon[sesi] = args.waktuRespon;
                }
            }
        }
        private void backbutton_Click(object sender, EventArgs e)
        {
            if (!Form1.Instance.pnlkontainer.Controls.ContainsKey("UCHome"))
            {
                UCHome uh = new UCHome();
                uh.Dock = DockStyle.Fill;
                Form1.Instance.pnlkontainer.Controls.Add(uh);
            }
            Form1.Instance.pnlkontainer.Controls["UCHome"].BringToFront();
        }
        void Update(object sender, EventArgs e)
        {
            if (timer.ElapsedMilliseconds > durasiSesi)  // setiap 3 sekon, terupdate ke sesi selanjutnya
            {
                kendali.RestartTimer();
                timer.Restart();
                sesi += 1;
            }

            if (sesi >= jumlahSesi)     // ketika sampai 30 sekon semuaya berhenti
            {
                timer.Stop();
                updater.Stop();
                selesaiUji(new HasilPengujian(hasilTekan, hasilDurasiRespon));
            }

            py[0] = 330;
            px[1] = 690;

            if (lap == 0)
            {
                px[0] = px[0] + 0.8f;
                py[1] = py[1] + 0.8f;
                px[2]++;
                py[2] = py[2] - 0.60f;
            }
            if (lap == 1)
            {
                px[0] = px[0] - 0.8f;
                py[1] = py[1] - 0.8f;
                px[2]--;
                py[2] = py[2] + 0.60f;
            }
            if (px[0] == 875 || py[1] == 610)
            {
                lap = 1;
            }
            if (px[2] == 685)
            {
                lap = 1;
            }
            if (px[0] == 140 || py[1] == 110)
            {
                lap = 0;
            }
            if (px[2] == 150)
            {
                lap = 0;
            }

            tombol.Location = new Point((int)px[metod], (int)py[metod]);
            kendali.CekTombol();
                        
        }
    }
    public class HasilPengujian
    {
        public readonly bool[] tertekan;
        public readonly double[] durasiRespon;
        public HasilPengujian(bool[] hasilTekan, double[] hasilDurasiRespon)
        {
            tertekan = new bool[hasilTekan.Length];
            durasiRespon = new double[hasilDurasiRespon.Length];
            for (int ii = 0; ii < hasilTekan.Length; ii++)
            {
                tertekan[ii] = hasilTekan[ii];
            }
            for (int jj = 0; jj < hasilDurasiRespon.Length; jj++)
            {
                durasiRespon[jj] = hasilDurasiRespon[jj];
            }
        }
    }
    public enum ArahTombol
    {
        Horizontal,
        Vertikal,
        Diagonal
    }
}

