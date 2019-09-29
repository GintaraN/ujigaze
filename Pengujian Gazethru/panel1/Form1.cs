using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace panel1
{
    public partial class Form1 : Form
    {
        static Form1 objekform;

        public static Form1 Instance
        {
            get
            {
                if(objekform == null)
                {
                    objekform = new Form1();
                }

                return objekform;
            }
        }

        public Panel pnlkontainer
        {
            get { return panelkontainer; }
            set { panelkontainer = value; }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            objekform = this;

            UCHome uc = new UCHome();
            uc.Dock = DockStyle.Fill;
            panelkontainer.Controls.Add(uc);
        }
    }
}
