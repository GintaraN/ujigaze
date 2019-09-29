using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace panel1
{
    public partial class UCbuttontutorial : UserControl
    {
        public int klik = 0;
        public UCbuttontutorial()
        {
            InitializeComponent();
            tutor0.Show();
            tutor1.Hide();
            tutor2.Hide();
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

        private void UCbuttontutorial_Click(object sender, EventArgs e)
        {            
            klik += 1;

            if(klik ==1)
            {
                tutor0.Hide();
                tutor1.Show();                
            }
            if(klik ==2)
            {
                tutor1.Hide();
                tutor2.Show();
            }
            if(klik ==3)
            {
                tutor2.Hide();
                if (!Form1.Instance.pnlkontainer.Controls.ContainsKey("UCHome"))
                {
                    UCHome uh = new UCHome();
                    uh.Dock = DockStyle.Fill;
                    Form1.Instance.pnlkontainer.Controls.Add(uh);
                }
                Form1.Instance.pnlkontainer.Controls["UCHome"].BringToFront();
                klik = 0;

                if (klik == 0)
                {
                    tutor0.Show();
                }
            }
        }

        private void tutor0_Click(object sender, EventArgs e)
        {
            tutor0.Hide();
            tutor1.Show();
        }

        private void tutor1_Click(object sender, EventArgs e)
        {
            tutor1.Hide();
            tutor2.Show();
        }

        private void tutor2_Click(object sender, EventArgs e)
        {
            tutor2.Hide();
            if (!Form1.Instance.pnlkontainer.Controls.ContainsKey("UCHome"))
            {
                UCHome uh = new UCHome();
                uh.Dock = DockStyle.Fill;
                Form1.Instance.pnlkontainer.Controls.Add(uh);
            }
            Form1.Instance.pnlkontainer.Controls["UCHome"].BringToFront();
            klik = 0;

            if (klik == 0)
            {
                tutor0.Show();
            }
        }
    }
}
