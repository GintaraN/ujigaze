namespace panel1
{
    partial class UCbuttontutorial
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCbuttontutorial));
            this.label1 = new System.Windows.Forms.Label();
            this.backbutton = new System.Windows.Forms.Button();
            this.tutor0 = new System.Windows.Forms.PictureBox();
            this.tutor1 = new System.Windows.Forms.PictureBox();
            this.tutor2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.tutor0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tutor1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tutor2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Futura Bk BT", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(510, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 34);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tutorial";
            // 
            // backbutton
            // 
            this.backbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backbutton.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backbutton.ForeColor = System.Drawing.Color.Black;
            this.backbutton.Location = new System.Drawing.Point(40, 40);
            this.backbutton.Name = "backbutton";
            this.backbutton.Size = new System.Drawing.Size(105, 39);
            this.backbutton.TabIndex = 1;
            this.backbutton.Text = "<  Back";
            this.backbutton.UseVisualStyleBackColor = true;
            this.backbutton.Click += new System.EventHandler(this.backbutton_Click);
            // 
            // tutor0
            // 
            this.tutor0.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tutor0.Cursor = System.Windows.Forms.Cursors.Default;
            this.tutor0.Image = ((System.Drawing.Image)(resources.GetObject("tutor0.Image")));
            this.tutor0.InitialImage = null;
            this.tutor0.Location = new System.Drawing.Point(40, 123);
            this.tutor0.Name = "tutor0";
            this.tutor0.Size = new System.Drawing.Size(1044, 552);
            this.tutor0.TabIndex = 2;
            this.tutor0.TabStop = false;
            this.tutor0.Click += new System.EventHandler(this.tutor0_Click);
            // 
            // tutor1
            // 
            this.tutor1.Image = ((System.Drawing.Image)(resources.GetObject("tutor1.Image")));
            this.tutor1.InitialImage = null;
            this.tutor1.Location = new System.Drawing.Point(40, 123);
            this.tutor1.Name = "tutor1";
            this.tutor1.Size = new System.Drawing.Size(1044, 552);
            this.tutor1.TabIndex = 3;
            this.tutor1.TabStop = false;
            this.tutor1.Click += new System.EventHandler(this.tutor1_Click);
            // 
            // tutor2
            // 
            this.tutor2.Image = ((System.Drawing.Image)(resources.GetObject("tutor2.Image")));
            this.tutor2.InitialImage = null;
            this.tutor2.Location = new System.Drawing.Point(40, 123);
            this.tutor2.Name = "tutor2";
            this.tutor2.Size = new System.Drawing.Size(1044, 552);
            this.tutor2.TabIndex = 4;
            this.tutor2.TabStop = false;
            this.tutor2.Click += new System.EventHandler(this.tutor2_Click);
            // 
            // UCbuttontutorial
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.tutor2);
            this.Controls.Add(this.tutor1);
            this.Controls.Add(this.tutor0);
            this.Controls.Add(this.backbutton);
            this.Controls.Add(this.label1);
            this.Name = "UCbuttontutorial";
            this.Size = new System.Drawing.Size(1111, 700);
            this.Click += new System.EventHandler(this.UCbuttontutorial_Click);
            ((System.ComponentModel.ISupportInitialize)(this.tutor0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tutor1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tutor2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button backbutton;
        private System.Windows.Forms.PictureBox tutor0;
        private System.Windows.Forms.PictureBox tutor1;
        private System.Windows.Forms.PictureBox tutor2;
    }
}
