namespace panel1
{
    partial class UCbutton1
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
            this.backbutton = new System.Windows.Forms.Button();
            this.tombol = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.tombol)).BeginInit();
            this.SuspendLayout();
            // 
            // backbutton
            // 
            this.backbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backbutton.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backbutton.ForeColor = System.Drawing.Color.Black;
            this.backbutton.Location = new System.Drawing.Point(30, 32);
            this.backbutton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.backbutton.Name = "backbutton";
            this.backbutton.Size = new System.Drawing.Size(79, 32);
            this.backbutton.TabIndex = 2;
            this.backbutton.Text = "<  Back";
            this.backbutton.UseVisualStyleBackColor = true;
            this.backbutton.Click += new System.EventHandler(this.backbutton_Click);
            // 
            // tombol
            // 
            this.tombol.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(0)))), ((int)(((byte)(125)))));
            this.tombol.Location = new System.Drawing.Point(105, 284);
            this.tombol.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tombol.Name = "tombol";
            this.tombol.Size = new System.Drawing.Size(28, 28);
            this.tombol.TabIndex = 3;
            this.tombol.TabStop = false;
            // 
            // UCbutton1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.tombol);
            this.Controls.Add(this.backbutton);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "UCbutton1";
            this.Size = new System.Drawing.Size(833, 569);
            ((System.ComponentModel.ISupportInitialize)(this.tombol)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button backbutton;
        private System.Windows.Forms.PictureBox tombol;
    }
}
