namespace IniciacaoCientifica
{
    partial class teste
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.original = new System.Windows.Forms.PictureBox();
            this.final = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.original)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.final)).BeginInit();
            this.SuspendLayout();
            // 
            // original
            // 
            this.original.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.original.Location = new System.Drawing.Point(12, 12);
            this.original.Name = "original";
            this.original.Size = new System.Drawing.Size(579, 567);
            this.original.TabIndex = 0;
            this.original.TabStop = false;
            this.original.Click += new System.EventHandler(this.original_Click);
            // 
            // final
            // 
            this.final.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.final.Location = new System.Drawing.Point(597, 12);
            this.final.Name = "final";
            this.final.Size = new System.Drawing.Size(553, 567);
            this.final.TabIndex = 1;
            this.final.TabStop = false;
            // 
            // teste
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1162, 529);
            this.Controls.Add(this.final);
            this.Controls.Add(this.original);
            this.Name = "teste";
            this.Text = "teste";
            ((System.ComponentModel.ISupportInitialize)(this.original)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.final)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox original;
        private System.Windows.Forms.PictureBox final;
    }
}