namespace WindowsFormsApp1
{
    partial class Stage3Helper
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
            this.components = new System.ComponentModel.Container();
            this.Stage3HelperShakeTimer = new System.Windows.Forms.Timer(this.components);
            this.beltPicturebox = new System.Windows.Forms.PictureBox();
            this.dumpPicturebox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.beltPicturebox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dumpPicturebox)).BeginInit();
            this.SuspendLayout();
            // 
            // Stage3HelperShakeTimer
            // 
            this.Stage3HelperShakeTimer.Interval = 30;
            this.Stage3HelperShakeTimer.Tick += new System.EventHandler(this.Stage3HelperShakeTimer_Tick);
            // 
            // beltPicturebox
            // 
            this.beltPicturebox.BackColor = System.Drawing.Color.Transparent;
            this.beltPicturebox.Image = global::WindowsFormsApp1.Properties.Resources.belt;
            this.beltPicturebox.Location = new System.Drawing.Point(0, 0);
            this.beltPicturebox.Name = "beltPicturebox";
            this.beltPicturebox.Size = new System.Drawing.Size(800, 80);
            this.beltPicturebox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.beltPicturebox.TabIndex = 0;
            this.beltPicturebox.TabStop = false;
            // 
            // dumpPicturebox
            // 
            this.dumpPicturebox.BackColor = System.Drawing.Color.Transparent;
            this.dumpPicturebox.Image = global::WindowsFormsApp1.Properties.Resources.dump3;
            this.dumpPicturebox.Location = new System.Drawing.Point(751, 0);
            this.dumpPicturebox.Name = "dumpPicturebox";
            this.dumpPicturebox.Size = new System.Drawing.Size(50, 40);
            this.dumpPicturebox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.dumpPicturebox.TabIndex = 1;
            this.dumpPicturebox.TabStop = false;
            // 
            // Stage3Helper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(17F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Snow;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(800, 80);
            this.ControlBox = false;
            this.Controls.Add(this.dumpPicturebox);
            this.Controls.Add(this.beltPicturebox);
            this.Font = new System.Drawing.Font("굴림", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ForeColor = System.Drawing.Color.Red;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "Stage3Helper";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Stage3Helper";
            this.TransparencyKey = System.Drawing.Color.Snow;
            ((System.ComponentModel.ISupportInitialize)(this.beltPicturebox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dumpPicturebox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox beltPicturebox;
        private System.Windows.Forms.Timer Stage3HelperShakeTimer;
        private System.Windows.Forms.PictureBox dumpPicturebox;
    }
}