namespace MegaTAE
{
    partial class GameChooser
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
            this.ds3Btn = new System.Windows.Forms.Button();
            this.sekiroBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ds3Btn
            // 
            this.ds3Btn.Location = new System.Drawing.Point(37, 56);
            this.ds3Btn.Name = "ds3Btn";
            this.ds3Btn.Size = new System.Drawing.Size(147, 23);
            this.ds3Btn.TabIndex = 0;
            this.ds3Btn.TabStop = false;
            this.ds3Btn.Text = "Dark Souls III";
            this.ds3Btn.UseVisualStyleBackColor = true;
            this.ds3Btn.Click += new System.EventHandler(this.ds3Btn_Click);
            // 
            // sekiroBtn
            // 
            this.sekiroBtn.Location = new System.Drawing.Point(37, 27);
            this.sekiroBtn.Name = "sekiroBtn";
            this.sekiroBtn.Size = new System.Drawing.Size(147, 23);
            this.sekiroBtn.TabIndex = 1;
            this.sekiroBtn.TabStop = false;
            this.sekiroBtn.Text = "Sekiro: Shadows Die Twice";
            this.sekiroBtn.UseVisualStyleBackColor = true;
            this.sekiroBtn.Click += new System.EventHandler(this.sekiroBtn_Click);
            // 
            // GameChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(228, 113);
            this.Controls.Add(this.sekiroBtn);
            this.Controls.Add(this.ds3Btn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameChooser";
            this.Text = "Select Game";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ds3Btn;
        private System.Windows.Forms.Button sekiroBtn;
    }
}