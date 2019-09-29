using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace panel1
{
    public partial class UCHome : UserControl
    {
        UCbutton1 pengukur;
        HasilPengujian[] hasilUji;
        int indexUji = 0;
        string[] metode =
        {
            "ED", "ED", "ED", "PPMC", "PPMC", "PPMC"
        };

        string[] arah =
        {
            "Horizontal", "Vertikal", "Diagonal", "Horizontal", "Vertikal", "Diagonal"
        };
        public UCHome()
        {
            InitializeComponent();

            hasilUji = new HasilPengujian[6];

            for (int i=0; i < 6; i++)
            {
                hasilUji[i] = new HasilPengujian(new bool[10], new double[10]);
            }
        }
        
        void UkurBaru(MetodeDeteksi metode, ArahTombol arah, int index)
        {
            pengukur = new UCbutton1(new Action<HasilPengujian>(SelesaiUkur), metode, arah);
            pengukur.Parent = this;
            pengukur.Dock = DockStyle.Fill;
            pengukur.BringToFront();
            indexUji = index;
        }

        public void SelesaiUkur(HasilPengujian args)
        {
            hasilUji[indexUji] = args;
            Controls.Remove(pengukur);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            UkurBaru(MetodeDeteksi.ED, ArahTombol.Horizontal, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UkurBaru(MetodeDeteksi.ED, ArahTombol.Vertikal, 1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UkurBaru(MetodeDeteksi.ED, ArahTombol.Diagonal, 2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UkurBaru(MetodeDeteksi.PPMCXY, ArahTombol.Horizontal, 3);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            UkurBaru(MetodeDeteksi.PPMCXY, ArahTombol.Vertikal, 4);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            UkurBaru(MetodeDeteksi.PPMCXY, ArahTombol.Diagonal, 5);
        }

        private void buttontutor_Click(object sender, EventArgs e)
        {
            if (!Form1.Instance.pnlkontainer.Controls.ContainsKey("UCbuttontutorial"))
            {
                UCbuttontutorial ubt = new UCbuttontutorial();
                ubt.Dock = DockStyle.Fill;
                Form1.Instance.pnlkontainer.Controls.Add(ubt);
            }
            Form1.Instance.pnlkontainer.Controls["UCbuttontutorial"].BringToFront();
        }

        private void SaveData(object sender, EventArgs e)        //nyimpen data
        {
            List<string> data = new List<string>();
            data.Add("Metode,Arah,Pengujian,Terdeteksi,Waktu Respon");

            for (int i = 0; i < hasilUji.Length; i++)
            {
                for (int j = 0; j < hasilUji[i].durasiRespon.Length; j++)
                {
                    data.Add(
                        metode[i] + "," +
                        arah[i] + "," +
                        j + "," +
                        (hasilUji[i].tertekan[j] ? "Berhasil" : "Gagal") + "," +
                        (hasilUji[i].durasiRespon[j].ToString("0.00"))
                        );                   
                }
            }
            File.WriteAllLines("Hasil Uji" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + ".csv", data.ToArray());
        }
    }
}
