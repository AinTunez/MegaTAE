namespace MegaTAE
{
    partial class ArrayEditor
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
            this.CloseBtn = new System.Windows.Forms.Button();
            this.ArrayView = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // CloseBtn
            // 
            this.CloseBtn.Location = new System.Drawing.Point(160, 337);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(75, 23);
            this.CloseBtn.TabIndex = 1;
            this.CloseBtn.Text = "Done";
            this.CloseBtn.UseVisualStyleBackColor = true;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // ArrayView
            // 
            this.ArrayView.HelpVisible = false;
            this.ArrayView.Location = new System.Drawing.Point(25, 12);
            this.ArrayView.Name = "ArrayView";
            this.ArrayView.Size = new System.Drawing.Size(346, 319);
            this.ArrayView.TabIndex = 2;
            this.ArrayView.ToolbarVisible = false;
            // 
            // ArrayEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 372);
            this.ControlBox = false;
            this.Controls.Add(this.ArrayView);
            this.Controls.Add(this.CloseBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ArrayEditor";
            this.ShowIcon = false;
            this.Text = "Array Editor";
            this.Load += new System.EventHandler(this.ArrayEditor_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button CloseBtn;
        public System.Windows.Forms.PropertyGrid ArrayView;
    }
}