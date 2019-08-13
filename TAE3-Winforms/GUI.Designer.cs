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
            this.attachProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forceInGameReloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.animationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copySelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.consoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreBackupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConsoleBox = new System.Windows.Forms.TextBox();
            this.ClearConsoleBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.CurrentAnimBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.EditArrayBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TaeListBox
            // 
            this.TaeListBox.FormattingEnabled = true;
            this.TaeListBox.Location = new System.Drawing.Point(10, 57);
            this.TaeListBox.Margin = new System.Windows.Forms.Padding(2);
            this.TaeListBox.Name = "TaeListBox";
            this.TaeListBox.Size = new System.Drawing.Size(74, 589);
            this.TaeListBox.TabIndex = 0;
            this.TaeListBox.SelectedIndexChanged += new System.EventHandler(this.TaeListBox_SelectedIndexChanged);
            // 
            // AnimListBox
            // 
            this.AnimListBox.FormattingEnabled = true;
            this.AnimListBox.Location = new System.Drawing.Point(87, 57);
            this.AnimListBox.Margin = new System.Windows.Forms.Padding(2);
            this.AnimListBox.Name = "AnimListBox";
            this.AnimListBox.Size = new System.Drawing.Size(80, 589);
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
            this.AnimDataGrid.Size = new System.Drawing.Size(431, 171);
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
            this.EventListBox.Size = new System.Drawing.Size(195, 412);
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
            this.EventDataGrid.Size = new System.Drawing.Size(222, 370);
            this.EventDataGrid.TabIndex = 4;
            this.EventDataGrid.ToolbarVisible = false;
            this.EventDataGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.EventDataGrid_PropertyValueChanged);
            this.EventDataGrid.SelectedGridItemChanged += new System.Windows.Forms.SelectedGridItemChangedEventHandler(this.EventDataGrid_SelectedGridItemChanged);
            this.EventDataGrid.SelectedObjectsChanged += new System.EventHandler(this.EventDataGrid_SelectedObjectsChanged);
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
            this.groupBox1.Location = new System.Drawing.Point(172, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox1.Size = new System.Drawing.Size(443, 196);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Animation Data";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.EventListBox);
            this.groupBox2.Location = new System.Drawing.Point(172, 239);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox2.Size = new System.Drawing.Size(207, 437);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Events";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.EventDataGrid);
            this.groupBox3.Location = new System.Drawing.Point(381, 239);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox3.Size = new System.Drawing.Size(234, 395);
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
            this.eventsToolStripMenuItem,
            this.consoleToolStripMenuItem});
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
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openANIBNDToolStripMenuItem
            // 
            this.openANIBNDToolStripMenuItem.Name = "openANIBNDToolStripMenuItem";
            this.openANIBNDToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openANIBNDToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.openANIBNDToolStripMenuItem.Text = "Open ANIBND";
            this.openANIBNDToolStripMenuItem.Click += new System.EventHandler(this.openANIBNDToolStripMenuItem_Click);
            // 
            // saveANIBNDToolStripMenuItem
            // 
            this.saveANIBNDToolStripMenuItem.Name = "saveANIBNDToolStripMenuItem";
            this.saveANIBNDToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveANIBNDToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.saveANIBNDToolStripMenuItem.Text = "Save ANIBND";
            this.saveANIBNDToolStripMenuItem.Click += new System.EventHandler(this.saveANIBNDToolStripMenuItem_Click);
            // 
            // attachProcessToolStripMenuItem
            // 
            this.attachProcessToolStripMenuItem.Name = "attachProcessToolStripMenuItem";
            this.attachProcessToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.attachProcessToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.attachProcessToolStripMenuItem.Text = "Attach Process";
            this.attachProcessToolStripMenuItem.Click += new System.EventHandler(this.attachProcessToolStripMenuItem_Click);
            // 
            // forceInGameReloadToolStripMenuItem
            // 
            this.forceInGameReloadToolStripMenuItem.Name = "forceInGameReloadToolStripMenuItem";
            this.forceInGameReloadToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.forceInGameReloadToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.forceInGameReloadToolStripMenuItem.Text = "Force In-Game Reload";
            this.forceInGameReloadToolStripMenuItem.Click += new System.EventHandler(this.forceInGameReloadToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(229, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // animationToolStripMenuItem
            // 
            this.animationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewToolStripMenuItem1,
            this.deleteToolStripMenuItem});
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
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // eventsToolStripMenuItem
            // 
            this.eventsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewToolStripMenuItem,
            this.deleteSelectedToolStripMenuItem,
            this.copySelectedToolStripMenuItem,
            this.pasteSelectedToolStripMenuItem,
            this.copyAllToolStripMenuItem,
            this.pasteAllToolStripMenuItem});
            this.eventsToolStripMenuItem.Name = "eventsToolStripMenuItem";
            this.eventsToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.eventsToolStripMenuItem.Text = "Events";
            // 
            // addNewToolStripMenuItem
            // 
            this.addNewToolStripMenuItem.Name = "addNewToolStripMenuItem";
            this.addNewToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.addNewToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.addNewToolStripMenuItem.Text = "Add New";
            this.addNewToolStripMenuItem.Click += new System.EventHandler(this.addNewEventToolStripMenuItem_Click);
            // 
            // deleteSelectedToolStripMenuItem
            // 
            this.deleteSelectedToolStripMenuItem.Name = "deleteSelectedToolStripMenuItem";
            this.deleteSelectedToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.deleteSelectedToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.deleteSelectedToolStripMenuItem.Text = "Delete Selected";
            this.deleteSelectedToolStripMenuItem.Click += new System.EventHandler(this.deleteSelectedEventToolStripMenuItem_Click);
            // 
            // copySelectedToolStripMenuItem
            // 
            this.copySelectedToolStripMenuItem.Name = "copySelectedToolStripMenuItem";
            this.copySelectedToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.copySelectedToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.copySelectedToolStripMenuItem.Text = "Copy Selected";
            this.copySelectedToolStripMenuItem.Click += new System.EventHandler(this.copySelectedToolStripMenuItem_Click);
            // 
            // pasteSelectedToolStripMenuItem
            // 
            this.pasteSelectedToolStripMenuItem.Name = "pasteSelectedToolStripMenuItem";
            this.pasteSelectedToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.V)));
            this.pasteSelectedToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.pasteSelectedToolStripMenuItem.Text = "Paste Selected";
            this.pasteSelectedToolStripMenuItem.Click += new System.EventHandler(this.pasteSelectedToolStripMenuItem_Click);
            // 
            // copyAllToolStripMenuItem
            // 
            this.copyAllToolStripMenuItem.Name = "copyAllToolStripMenuItem";
            this.copyAllToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.copyAllToolStripMenuItem.Text = "Copy All";
            this.copyAllToolStripMenuItem.Click += new System.EventHandler(this.copyAllToolStripMenuItem_Click);
            // 
            // pasteAllToolStripMenuItem
            // 
            this.pasteAllToolStripMenuItem.Name = "pasteAllToolStripMenuItem";
            this.pasteAllToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.pasteAllToolStripMenuItem.Text = "Paste All";
            this.pasteAllToolStripMenuItem.Click += new System.EventHandler(this.pasteAllToolStripMenuItem_Click);
            // 
            // consoleToolStripMenuItem
            // 
            this.consoleToolStripMenuItem.DoubleClickEnabled = true;
            this.consoleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toggleToolStripMenuItem,
            this.restoreBackupToolStripMenuItem});
            this.consoleToolStripMenuItem.Name = "consoleToolStripMenuItem";
            this.consoleToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.consoleToolStripMenuItem.Text = "Misc";
            // 
            // toggleToolStripMenuItem
            // 
            this.toggleToolStripMenuItem.Name = "toggleToolStripMenuItem";
            this.toggleToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.toggleToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.toggleToolStripMenuItem.Text = "Toggle Console";
            this.toggleToolStripMenuItem.Click += new System.EventHandler(this.toggleToolStripMenuItem_Click);
            // 
            // restoreBackupToolStripMenuItem
            // 
            this.restoreBackupToolStripMenuItem.Name = "restoreBackupToolStripMenuItem";
            this.restoreBackupToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.restoreBackupToolStripMenuItem.Text = "Restore Backup";
            this.restoreBackupToolStripMenuItem.Click += new System.EventHandler(this.restoreBackupToolStripMenuItem_Click);
            // 
            // ConsoleBox
            // 
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
            this.CurrentAnimBox.Location = new System.Drawing.Point(75, 654);
            this.CurrentAnimBox.Name = "CurrentAnimBox";
            this.CurrentAnimBox.ReadOnly = true;
            this.CurrentAnimBox.Size = new System.Drawing.Size(92, 20);
            this.CurrentAnimBox.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 657);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "CURRENT";
            // 
            // EditArrayBtn
            // 
            this.EditArrayBtn.Enabled = false;
            this.EditArrayBtn.Location = new System.Drawing.Point(434, 637);
            this.EditArrayBtn.Name = "EditArrayBtn";
            this.EditArrayBtn.Size = new System.Drawing.Size(119, 33);
            this.EditArrayBtn.TabIndex = 16;
            this.EditArrayBtn.Text = "Edit Array";
            this.EditArrayBtn.UseVisualStyleBackColor = true;
            this.EditArrayBtn.Click += new System.EventHandler(this.EditArrayBtn_Click);
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1078, 718);
            this.Controls.Add(this.EditArrayBtn);
            this.Controls.Add(this.CurrentAnimBox);
            this.Controls.Add(this.label4);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GUI";
            this.Text = "MegaTAE";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GUI_FormClosing);
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
        private System.Windows.Forms.ToolStripMenuItem attachProcessToolStripMenuItem;
        private System.Windows.Forms.TextBox CurrentAnimBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem consoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restoreBackupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copySelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem copyAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteAllToolStripMenuItem;
        private System.Windows.Forms.Button EditArrayBtn;
    }
}

