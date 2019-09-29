using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tobii.EyeX.Framework;
using EyeXFramework;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace panel1
{  
    public delegate void FungsiTombol(ArgumenKendaliTombol e);   // fungsi delegate untuk merepresentasikan fungsi tombol pada form dalam pengendali

    public enum MetodeDeteksi
    {
        ED,
        PPMCX,
        PPMCY,
        PPMCXY
    }
    public class ArgumenKendaliTombol  //class untuk mengelompokkan hasil pengecekan status tombol
    {
        public double korelasix;
        public double korelasiy;
        public bool status;
        public double jarak;

        public double waktuRespon;

        public ArgumenKendaliTombol(bool state, double[] data)
        {            
            status = state;
            waktuRespon = data[0];
            jarak = data[1];
            korelasix = data[2];
            korelasiy = data[3];
        }
    }

    public class KendaliTombol
    {
        MetodeDeteksi mode;
        int ukuranKorelasi = 5;             //ukuran buffer/ sliding wondow, korelasi ppmc
        int ukuranFilterMata = 30;           //ukuran MA Filter

        double ThresholdJarak = 300;        //Threshold batas klasifikasi
        double ThresholdKorelasi = 0.8;

        int DurasiJarakEuclidean = 80;     //durasi terpilih sampai terseleksi
        int DurasiKorelasiPearson = 80;

        EyeXHost Host;
        int[][] PosisiMata;
        int[][] PosisiMataAsli;
        GazePointDataStream DataStream;

        List<Control> DaftarTombol;
        List<FungsiTombol> DaftarFungsi;

        List<int[][]> DaftarPosisiTombol;
        List<Stopwatch> daftarTimerTombol;

        List<int> daftarHasilJarak;
        List<int> daftarHasilKorelasiX;
        List<int> daftarHasilKorelasiY;
        List<int> daftarHasilKorelasiXY;

        List<double[]> daftarJarak;
        List<double[]> daftarKorelasiX;
        List<double[]> daftarKorelasiY;
        List<double[]> daftarKorelasiXY;

        public KendaliTombol(MetodeDeteksi metode)
        {
            mode = metode;
            DaftarTombol = new List<Control>();
            DaftarFungsi = new List<FungsiTombol>();
            DaftarPosisiTombol = new List<int[][]>();
            daftarTimerTombol = new List<Stopwatch>();

            PosisiMata = new int[2][];
            PosisiMata[0] = new int[ukuranKorelasi];
            PosisiMata[1] = new int[ukuranKorelasi];

            PosisiMataAsli = new int[2][];
            PosisiMataAsli[0] = new int[ukuranFilterMata];
            PosisiMataAsli[1] = new int[ukuranFilterMata];

            daftarHasilJarak = new List<int>();
            daftarHasilKorelasiX = new List<int>();
            daftarHasilKorelasiY = new List<int>();
            daftarHasilKorelasiXY = new List<int>();

            daftarJarak = new List<double[]>();
            daftarKorelasiX = new List<double[]>();
            daftarKorelasiY = new List<double[]>();
            daftarKorelasiXY = new List<double[]>();                 

        }
          
        public void TambahTombol(Control tombol, FungsiTombol fungsi)
        {
            DaftarTombol.Add(tombol);
            DaftarFungsi.Add(fungsi);

            daftarTimerTombol.Add(new Stopwatch());

            int indexDaftarPosisiTombol = DaftarPosisiTombol.Count;
	        DaftarPosisiTombol.Add(new int[2][]);
            DaftarPosisiTombol[indexDaftarPosisiTombol][0] = new int[ukuranKorelasi]; 
            DaftarPosisiTombol[indexDaftarPosisiTombol][1] = new int[ukuranKorelasi];

            daftarHasilJarak.Add(0);
            daftarHasilKorelasiX.Add(0);
            daftarHasilKorelasiY.Add(0);
            daftarHasilKorelasiXY.Add(0);

            daftarJarak.Add(new double[DurasiJarakEuclidean + 1]);
            daftarKorelasiX.Add(new double[DurasiKorelasiPearson + 1]);
            daftarKorelasiY.Add(new double[DurasiKorelasiPearson + 1]);
            daftarKorelasiXY.Add(new double[DurasiKorelasiPearson + 1]);
        }

        public void Start()         //menyalakan Eyetracker dan mulai tracking
        {
            Host = new EyeXHost();
            DataStream = Host.CreateGazePointDataStream(GazePointDataMode.LightlyFiltered);

            Host.Start();                               //mulai tracking mata
            DataStream.Next += SimpanPosisiMata;        //menyimpan data mata                      
        }

        void SimpanPosisiMata(object sender, GazePointEventArgs e)
        {
            int[] PosisiMataSekarang = { (int)e.X, (int)e.Y };          //Menyimpan data gaze

            for (int i = ukuranFilterMata - 1; i > 0; i--)
            {
                PosisiMataAsli[0][i] = PosisiMataAsli[0][i - 1];
                PosisiMataAsli[1][i] = PosisiMataAsli[1][i - 1];
            }

            PosisiMataAsli[0][0] = PosisiMataSekarang[0];
            PosisiMataAsli[1][0] = PosisiMataSekarang[1];

            for (int i = ukuranKorelasi - 1; i > 0; i--)                //Gaze difilter
            {
                PosisiMata[0][i] = PosisiMata[0][i - 1];
                PosisiMata[1][i] = PosisiMata[1][i - 1];
            }

            PosisiMata[0][0] = AverageFilter(PosisiMataAsli[0]);
            PosisiMata[1][0] = AverageFilter(PosisiMataAsli[1]);

            GeserPosisiTombol();
        }

        void GeserPosisiTombol()  //kalo ada multitombol
        {
            for (int i = 0; i < DaftarTombol.Count; i++)
            {
                for (int j = ukuranKorelasi - 1; j > 0; j--)
                {
                    DaftarPosisiTombol[i][0][j] = DaftarPosisiTombol[i][0][j - 1];
                    DaftarPosisiTombol[i][1][j] = DaftarPosisiTombol[i][1][j - 1];

                }
            }
        }

        void SimpanPosisiTombol()
        {
            GeserPosisiTombol();
            for (int i = 0; i < DaftarTombol.Count; i++)
            {
                int[] PosisiTombolSekarang = PosisiTombol(i);
                DaftarPosisiTombol[i][0][0] = PosisiTombolSekarang[0];
                DaftarPosisiTombol[i][1][0] = PosisiTombolSekarang[1];

            }
        }

        int AverageFilter(int[] data)   //Algoritme M.A Filter
        {
            int posisi = 0;
            for (int i = 0; i < data.Length; i++)
            {
                posisi += data[i];
            }
            return posisi / data.Length;
        }

        int[] PosisiTombol(int index)   //mendata posisi tombol dalam array
        {
            Point posisi = DaftarTombol[index].Parent.PointToScreen(DaftarTombol[index].Location);
            return new int[] { posisi.X, posisi.Y };
        }

        double JarakEuclidean(int titik1x, int titik1y, int titik2x, int titik2y)  //Algoritme ED
        {
            int dx = titik1x - titik2x;
            int dy = titik1y - titik2y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        double KorelasiPearson(int[] x, int[] y)   //Algoritme PPMC
        {
            double ux = 0;
            double uy = 0;
            double uxx = 0;
            double uyy = 0;
            double uxy = 0;
            for (int i = 0; i < x.Length; i++)
            {
                ux += x[i];
                uy += y[i];
                uxx += x[i] * x[i];
                uxy += x[i] * y[i];
                uyy += y[i] * y[i];
            }
            return (ukuranKorelasi * uxy - ux * uy) / (Math.Sqrt((ukuranKorelasi * uxx - ux * ux) * (ukuranKorelasi * uyy - uy * uy)) + 1e-10);
        }

        public void RestartTimer()  // mengembalikan nilai awal atau restart
        {
            for (int ii = 0; ii < daftarTimerTombol.Count; ii++)
            {
                daftarTimerTombol[ii].Restart();
            }
            for (int ii = 0; ii < daftarHasilJarak.Count; ii++)
            {
                daftarHasilJarak[ii] = 0;
            }
            for (int ii = 0; ii < daftarHasilKorelasiX.Count; ii++)
            {
                daftarHasilKorelasiX[ii] = 0;
            }
            for (int ii = 0; ii < daftarHasilKorelasiY.Count; ii++)
            {
                daftarHasilKorelasiY[ii] = 0;
            }
            for (int ii = 0; ii < daftarHasilKorelasiXY.Count; ii++)
            {
                daftarHasilKorelasiXY[ii] = 0;
            }
        }

        public void CekTombol()
        {
            SimpanPosisiTombol();

            for (int i = 0; i < DaftarTombol.Count; i++)
            {
                //mulai proses similarity measurement ED
                double jarak = JarakEuclidean
                    (
                    DaftarPosisiTombol[i][0][0],
                    DaftarPosisiTombol[i][1][0],
                    PosisiMata[0][0],
                    PosisiMata[1][0]
                );
                //mulai proses similarity measurement PPMC
                double korelasix = KorelasiPearson(DaftarPosisiTombol[i][0], PosisiMata[0]);
                double korelasiy = KorelasiPearson(DaftarPosisiTombol[i][1], PosisiMata[1]);

                //Proses ketika hasil PPMC dan ED sampai durasi terpenuhi
                daftarJarak[i][daftarHasilJarak[i]] = jarak;
                daftarKorelasiX[i][daftarHasilKorelasiX[i]] = korelasix;
                daftarKorelasiY[i][daftarHasilKorelasiY[i]] = korelasiy;

                if (jarak < ThresholdJarak)
                {
                    daftarHasilJarak[i] += 1;
                }
                else
                {
                    daftarHasilJarak[i] = 0;
                    if (mode.Equals(MetodeDeteksi.ED))
                    {
                        daftarTimerTombol[i].Restart();
                    }
                }

                if (korelasix > ThresholdKorelasi)
                {
                    daftarHasilKorelasiX[i] += 1;
                }
                else
                {
                    daftarHasilKorelasiX[i] = 0;
                    if (mode.Equals(MetodeDeteksi.PPMCX))
                    {
                        daftarTimerTombol[i].Restart();
                    }
                }

                if (korelasiy > ThresholdKorelasi)
                {
                    daftarHasilKorelasiY[i] += 1;
                }
                else
                {
                    daftarHasilKorelasiY[i] = 0;
                    if (mode.Equals(MetodeDeteksi.PPMCY))
                    {
                        daftarTimerTombol[i].Restart();
                    }
                }

                if (korelasix >= ThresholdKorelasi || korelasiy >= ThresholdKorelasi)
                {
                    daftarHasilKorelasiXY[i] += 1;
                }
                else
                {
                    daftarHasilKorelasiXY[i] = 0;
                    if (mode.Equals(MetodeDeteksi.PPMCXY))
                    {
                        daftarTimerTombol[i].Restart();
                    }
                }

                bool statusjarak = daftarHasilJarak[i] >= DurasiJarakEuclidean;
                bool statusKorelasiX = daftarHasilKorelasiX[i] >= DurasiKorelasiPearson;
                bool statusKorelasiY = daftarHasilKorelasiY[i] >= DurasiKorelasiPearson;
                bool statusKorelasiXY = daftarHasilKorelasiXY[i] >= DurasiKorelasiPearson;

                //Proses jika button mulai terdeteksi jaraknya sama dengan gaze

                if (statusjarak)
                {
                    daftarHasilJarak[i] = DurasiJarakEuclidean;
                }

                //proses jika button mulai terdeteksi korelasinya sama dengan gaze

                if (statusKorelasiX)
                {
                    daftarHasilKorelasiX[i] = DurasiKorelasiPearson;
                }

                if (statusKorelasiY)
                {
                    daftarHasilKorelasiY[i] = DurasiKorelasiPearson;
                }

                if (statusKorelasiXY)
                {
                    daftarHasilKorelasiXY[i] = DurasiKorelasiPearson;
                }

                bool status = false;

                switch (mode)
                {
                    case MetodeDeteksi.ED:
                        status = statusjarak;
                        break;
                    case MetodeDeteksi.PPMCX:
                        status = statusKorelasiX;
                        break;
                    case MetodeDeteksi.PPMCY:
                        status = statusKorelasiY;
                        break;
                    case MetodeDeteksi.PPMCXY:
                        status = statusKorelasiXY;
                        break;
                }

                if (status)
                {
                    daftarTimerTombol[i].Stop();
                }
                double[] data =
                {
                daftarTimerTombol[i].ElapsedMilliseconds,
                jarak,
                korelasix,
                korelasiy
                };
                ArgumenKendaliTombol hasilCekTombol = new ArgumenKendaliTombol(status, data);

                DaftarFungsi[i](hasilCekTombol);

                //Debug.WriteLine(korelasix);
            }
        }

    }
}
