namespace MegaTAE
{
    partial class GUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI));
            this.TaeListBox = new System.Windows.Forms.ListBox();
            this.AnimListBox = new System.Windows.Forms.ListBox();
            this.AnimDataGrid = new System.Windows.Forms.PropertyGrid();
            this.EventListBox = new System.Windows.Forms.ListBox();
            this.EventDataGrid = new System.Windows.Forms.PropertyGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openANIBNDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveANIBNDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forceInGameReloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.animationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.eventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConsoleBox = new System.Windows.Forms.TextBox();
            this.ClearConsoleBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.CurrentAnimBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.attachProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TaeListBox
            // 
            this.TaeListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TaeListBox.FormattingEnabled = true;
            this.TaeListBox.Location = new System.Drawing.Point(10, 57);
            this.TaeListBox.Margin = new System.Windows.Forms.Padding(2);
            this.TaeListBox.Name = "TaeListBox";
            this.TaeListBox.Size = new System.Drawing.Size(74, 615);
            this.TaeListBox.TabIndex = 0;
            this.TaeListBox.SelectedIndexChanged += new System.EventHandler(this.TaeListBox_SelectedIndexChanged);
            // 
            // AnimListBox
            // 
            this.AnimListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.AnimListBox.FormattingEnabled = true;
            this.AnimListBox.Location = new System.Drawing.Point(87, 57);
            this.AnimListBox.Margin = new System.Windows.Forms.Padding(2);
            this.AnimListBox.Name = "AnimListBox";
            this.AnimListBox.Size = new System.Drawing.Size(74, 615);
            this.AnimListBox.TabIndex = 1;
            this.AnimListBox.SelectedIndexChanged += new System.EventHandler(this.AnimListBox_SelectedIndexChanged);
            // 
            // AnimDataGrid
            // 
            this.AnimDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AnimDataGrid.HelpVisible = false;
            this.AnimDataGrid.Location = new System.Drawing.Point(6, 19);
            this.AnimDataGrid.Margin = new System.Windows.Forms.Padding(2);
            this.AnimDataGrid.Name = "AnimDataGrid";
            this.AnimDataGrid.Size = new System.Drawing.Size(437, 171);
            this.AnimDataGrid.TabIndex = 2;
            this.AnimDataGrid.ToolbarVisible = false;
            this.AnimDataGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.AnimDataGrid_PropertyValueChanged);
            // 
            // EventListBox
            // 
            this.EventListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EventListBox.FormattingEnabled = true;
            this.EventListBox.Location = new System.Drawing.Point(6, 19);
            this.EventListBox.Margin = new System.Windows.Forms.Padding(2);
            this.EventListBox.Name = "EventListBox";
            this.EventListBox.Size = new System.Drawing.Size(191, 412);
            this.EventListBox.TabIndex = 3;
            this.EventListBox.SelectedIndexChanged += new System.EventHandler(this.EventListBox_SelectedIndexChanged);
            // 
            // EventDataGrid
            // 
            this.EventDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EventDataGrid.HelpVisible = false;
            this.EventDataGrid.Location = new System.Drawing.Point(6, 19);
            this.EventDataGrid.Margin = new System.Windows.Forms.Padding(2);
            this.EventDataGrid.Name = "EventDataGrid";
            this.EventDataGrid.Size = new System.Drawing.Size(232, 412);
            this.EventDataGrid.TabIndex = 4;
            this.EventDataGrid.ToolbarVisible = false;
            this.EventDataGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.EventDataGrid_PropertyValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "TAE";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(95, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Animation";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.AnimDataGrid);
            this.groupBox1.Location = new System.Drawing.Point(166, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox1.Size = new System.Drawing.Size(449, 196);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Animation Data";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.EventListBox);
            this.groupBox2.Location = new System.Drawing.Point(166, 239);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox2.Size = new System.Drawing.Size(203, 437);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Events";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.EventDataGrid);
            this.groupBox3.Location = new System.Drawing.Point(371, 239);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox3.Size = new System.Drawing.Size(244, 437);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Event Data";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.animationToolStripMenuItem,
            this.eventsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1078, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openANIBNDToolStripMenuItem,
            this.saveANIBNDToolStripMenuItem,
            this.attachProcessToolStripMenuItem,
            this.forceInGameReloadToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openANIBNDToolStripMenuItem
            // 
            this.openANIBNDToolStripMenuItem.Name = "openANIBNDToolStripMenuItem";
            this.openANIBNDToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openANIBNDToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
            this.openANIBNDToolStripMenuItem.Text = "Open ANIBND";
            this.openANIBNDToolStripMenuItem.Click += new System.EventHandler(this.openANIBNDToolStripMenuItem_Click);
            // 
            // saveANIBNDToolStripMenuItem
            // 
            this.saveANIBNDToolStripMenuItem.Name = "saveANIBNDToolStripMenuItem";
            this.saveANIBNDToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveANIBNDToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
            this.saveANIBNDToolStripMenuItem.Text = "Save ANIBND";
            this.saveANIBNDToolStripMenuItem.Click += new System.EventHandler(this.saveANIBNDToolStripMenuItem_Click);
            // 
            // forceInGameReloadToolStripMenuItem
            // 
            this.forceInGameReloadToolStripMenuItem.Name = "forceInGameReloadToolStripMenuItem";
            this.forceInGameReloadToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.forceInGameReloadToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
            this.forceInGameReloadToolStripMenuItem.Text = "Force In-Game Reload";
            this.forceInGameReloadToolStripMenuItem.Click += new System.EventHandler(this.forceInGameReloadToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(264, 22);
            this.toolStripMenuItem2.Text = "--------------------------------------";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // animationToolStripMenuItem
            // 
            this.animationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewToolStripMenuItem1});
            this.animationToolStripMenuItem.Name = "animationToolStripMenuItem";
            this.animationToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.animationToolStripMenuItem.Text = "Animation";
            // 
            // addNewToolStripMenuItem1
            // 
            this.addNewToolStripMenuItem1.Name = "addNewToolStripMenuItem1";
            this.addNewToolStripMenuItem1.Size = new System.Drawing.Size(123, 22);
            this.addNewToolStripMenuItem1.Text = "Add New";
            this.addNewToolStripMenuItem1.Click += new System.EventHandler(this.addNewToolAnimationStripMenuItem1_Click);
            // 
            // eventsToolStripMenuItem
            // 
            this.eventsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewToolStripMenuItem,
            this.deleteSelectedToolStripMenuItem});
            this.eventsToolStripMenuItem.Name = "eventsToolStripMenuItem";
            this.eventsToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.eventsToolStripMenuItem.Text = "Events";
            // 
            // addNewToolStripMenuItem
            // 
            this.addNewToolStripMenuItem.Name = "addNewToolStripMenuItem";
            this.addNewToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.addNewToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.addNewToolStripMenuItem.Text = "Add New";
            this.addNewToolStripMenuItem.Click += new System.EventHandler(this.addNewEventToolStripMenuItem_Click);
            // 
            // deleteSelectedToolStripMenuItem
            // 
            this.deleteSelectedToolStripMenuItem.Name = "deleteSelectedToolStripMenuItem";
            this.deleteSelectedToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.deleteSelectedToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.deleteSelectedToolStripMenuItem.Text = "Delete Selected";
            this.deleteSelectedToolStripMenuItem.Click += new System.EventHandler(this.deleteSelectedEventToolStripMenuItem_Click);
            // 
            // ConsoleBox
            // 
            this.ConsoleBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConsoleBox.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ConsoleBox.Font = new System.Drawing.Font("Consolas", 9F);
            this.ConsoleBox.ForeColor = System.Drawing.SystemColors.Window;
            this.ConsoleBox.Location = new System.Drawing.Point(624, 68);
            this.ConsoleBox.Multiline = true;
            this.ConsoleBox.Name = "ConsoleBox";
            this.ConsoleBox.ReadOnly = true;
            this.ConsoleBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ConsoleBox.Size = new System.Drawing.Size(442, 608);
            this.ConsoleBox.TabIndex = 11;
            this.ConsoleBox.WordWrap = false;
            // 
            // ClearConsoleBtn
            // 
            this.ClearConsoleBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearConsoleBtn.Location = new System.Drawing.Point(981, 37);
            this.ClearConsoleBtn.Name = "ClearConsoleBtn";
            this.ClearConsoleBtn.Size = new System.Drawing.Size(85, 25);
            this.ClearConsoleBtn.TabIndex = 12;
            this.ClearConsoleBtn.Text = "Clear";
            this.ClearConsoleBtn.UseVisualStyleBackColor = true;
            this.ClearConsoleBtn.Click += new System.EventHandler(this.ClearConsoleBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(895, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Console Output";
            // 
            // CurrentAnimBox
            // 
            this.CurrentAnimBox.Location = new System.Drawing.Point(745, 43);
            this.CurrentAnimBox.Name = "CurrentAnimBox";
            this.CurrentAnimBox.ReadOnly = true;
            this.CurrentAnimBox.Size = new System.Drawing.Size(100, 20);
            this.CurrentAnimBox.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(621, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Active Player Animation";
            // 
            // attachProcessToolStripMenuItem
            // 
            this.attachProcessToolStripMenuItem.Name = "attachProcessToolStripMenuItem";
            this.attachProcessToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.attachProcessToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
            this.attachProcessToolStripMenuItem.Text = "Attach Process";
            this.attachProcessToolStripMenuItem.Click += new System.EventHandler(this.attachProcessToolStripMenuItem_Click);
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 696);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CurrentAnimBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ClearConsoleBtn);
            this.Controls.Add(this.ConsoleBox);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AnimListBox);
            this.Controls.Add(this.TaeListBox);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GUI";
            this.Text = "MegaTAE";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox AnimListBox;
        private System.Windows.Forms.PropertyGrid AnimDataGrid;
        private System.Windows.Forms.ListBox EventListBox;
        private System.Windows.Forms.PropertyGrid EventDataGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openANIBNDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveANIBNDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        public System.Windows.Forms.ListBox TaeListBox;
        private System.Windows.Forms.ToolStripMenuItem eventsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedToolStripMenuItem;
        public System.Windows.Forms.TextBox ConsoleBox;
        private System.Windows.Forms.Button ClearConsoleBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem animationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem forceInGameReloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.TextBox CurrentAnimBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem attachProcessToolStripMenuItem;
    }
}

