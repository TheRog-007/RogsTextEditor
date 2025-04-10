namespace RogsTextEditor
{
    partial class FRMMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRMMain));
            this.MNUSMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MNUNew = new System.Windows.Forms.ToolStripMenuItem();
            this.MNUOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.MNURecentFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.MNUSave = new System.Windows.Forms.ToolStripMenuItem();
            this.MNUSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.MNUClose = new System.Windows.Forms.ToolStripMenuItem();
            this.MNUSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.MNUPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.MNUSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.MNUExit = new System.Windows.Forms.ToolStripMenuItem();
            this.TLSMenu = new System.Windows.Forms.ToolStrip();
            this.TBTNBold = new System.Windows.Forms.ToolStripButton();
            this.TBTNItalics = new System.Windows.Forms.ToolStripButton();
            this.TBTNUnderline = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.TBTNLeft = new System.Windows.Forms.ToolStripButton();
            this.TBTCentre = new System.Windows.Forms.ToolStripButton();
            this.TBTNRight = new System.Windows.Forms.ToolStripButton();
            this.TBTNJustify = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.STAStatus = new System.Windows.Forms.StatusStrip();
            this.SLBLColRow = new System.Windows.Forms.ToolStripStatusLabel();
            this.SLBLStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.RTXTDocument = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CHKWordWrap = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CMBType = new System.Windows.Forms.ComboBox();
            this.CMBColours = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CMBSize = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CMBFonts = new System.Windows.Forms.ComboBox();
            this.printDoc = new System.Drawing.Printing.PrintDocument();
            this.TOOLTIPText = new System.Windows.Forms.ToolTip(this.components);
            this.MNUSMenu.SuspendLayout();
            this.TLSMenu.SuspendLayout();
            this.STAStatus.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MNUSMenu
            // 
            this.MNUSMenu.BackColor = System.Drawing.Color.Olive;
            this.MNUSMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.MNUSMenu.Location = new System.Drawing.Point(0, 0);
            this.MNUSMenu.Name = "MNUSMenu";
            this.MNUSMenu.Size = new System.Drawing.Size(993, 24);
            this.MNUSMenu.TabIndex = 0;
            this.MNUSMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MNUNew,
            this.MNUOpen,
            this.MNURecentFiles,
            this.MNUSep1,
            this.MNUSave,
            this.MNUSaveAs,
            this.MNUPrint,
            this.MNUClose,
            this.MNUSep2,
            this.MNUExit});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // MNUNew
            // 
            this.MNUNew.BackColor = System.Drawing.Color.DarkGreen;
            this.MNUNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MNUNew.ForeColor = System.Drawing.Color.White;
            this.MNUNew.Name = "MNUNew";
            this.MNUNew.Size = new System.Drawing.Size(180, 22);
            this.MNUNew.Text = "New";
            this.MNUNew.Click += new System.EventHandler(this.MNUNew_Click);
            // 
            // MNUOpen
            // 
            this.MNUOpen.BackColor = System.Drawing.Color.DarkGreen;
            this.MNUOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MNUOpen.ForeColor = System.Drawing.Color.White;
            this.MNUOpen.Name = "MNUOpen";
            this.MNUOpen.Size = new System.Drawing.Size(180, 22);
            this.MNUOpen.Text = "Open";
            this.MNUOpen.Click += new System.EventHandler(this.MNUOpen_Click);
            // 
            // MNURecentFiles
            // 
            this.MNURecentFiles.BackColor = System.Drawing.Color.DarkGreen;
            this.MNURecentFiles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MNURecentFiles.ForeColor = System.Drawing.Color.White;
            this.MNURecentFiles.Name = "MNURecentFiles";
            this.MNURecentFiles.Size = new System.Drawing.Size(180, 22);
            this.MNURecentFiles.Text = "Recent Files";
            // 
            // MNUSave
            // 
            this.MNUSave.BackColor = System.Drawing.Color.DarkGreen;
            this.MNUSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MNUSave.ForeColor = System.Drawing.Color.White;
            this.MNUSave.Name = "MNUSave";
            this.MNUSave.Size = new System.Drawing.Size(180, 22);
            this.MNUSave.Text = "Save";
            this.MNUSave.Click += new System.EventHandler(this.MNUSave_Click);
            // 
            // MNUSaveAs
            // 
            this.MNUSaveAs.BackColor = System.Drawing.Color.DarkGreen;
            this.MNUSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MNUSaveAs.ForeColor = System.Drawing.Color.White;
            this.MNUSaveAs.Name = "MNUSaveAs";
            this.MNUSaveAs.Size = new System.Drawing.Size(180, 22);
            this.MNUSaveAs.Text = "Save As";
            this.MNUSaveAs.Click += new System.EventHandler(this.MNUSaveAs_Click);
            // 
            // MNUClose
            // 
            this.MNUClose.BackColor = System.Drawing.Color.DarkGreen;
            this.MNUClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MNUClose.ForeColor = System.Drawing.Color.White;
            this.MNUClose.Name = "MNUClose";
            this.MNUClose.Size = new System.Drawing.Size(180, 22);
            this.MNUClose.Text = "Close";
            this.MNUClose.Click += new System.EventHandler(this.MNUClose_Click);
            // 
            // MNUSep1
            // 
            this.MNUSep1.BackColor = System.Drawing.Color.DarkGreen;
            this.MNUSep1.ForeColor = System.Drawing.Color.Green;
            this.MNUSep1.Name = "MNUSep1";
            this.MNUSep1.Size = new System.Drawing.Size(177, 6);
            // 
            // MNUPrint
            // 
            this.MNUPrint.BackColor = System.Drawing.Color.DarkGreen;
            this.MNUPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MNUPrint.ForeColor = System.Drawing.Color.White;
            this.MNUPrint.Name = "MNUPrint";
            this.MNUPrint.Size = new System.Drawing.Size(180, 22);
            this.MNUPrint.Text = "Print";
            this.MNUPrint.Click += new System.EventHandler(this.MNUPrint_Click);
            // 
            // MNUSep2
            // 
            this.MNUSep2.BackColor = System.Drawing.Color.DarkGreen;
            this.MNUSep2.ForeColor = System.Drawing.Color.Green;
            this.MNUSep2.Name = "MNUSep2";
            this.MNUSep2.Size = new System.Drawing.Size(177, 6);
            // 
            // MNUExit
            // 
            this.MNUExit.BackColor = System.Drawing.Color.DarkGreen;
            this.MNUExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MNUExit.ForeColor = System.Drawing.Color.White;
            this.MNUExit.Name = "MNUExit";
            this.MNUExit.Size = new System.Drawing.Size(180, 22);
            this.MNUExit.Text = "Exit";
            this.MNUExit.Click += new System.EventHandler(this.MNUExit_Click);
            // 
            // TLSMenu
            // 
            this.TLSMenu.AutoSize = false;
            this.TLSMenu.BackColor = System.Drawing.Color.Olive;
            this.TLSMenu.Dock = System.Windows.Forms.DockStyle.None;
            this.TLSMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TBTNBold,
            this.TBTNItalics,
            this.TBTNUnderline,
            this.toolStripSeparator1,
            this.TBTNLeft,
            this.TBTCentre,
            this.TBTNRight,
            this.TBTNJustify,
            this.toolStripSeparator3});
            this.TLSMenu.Location = new System.Drawing.Point(0, 24);
            this.TLSMenu.Name = "TLSMenu";
            this.TLSMenu.Size = new System.Drawing.Size(200, 30);
            this.TLSMenu.TabIndex = 1;
            this.TLSMenu.Text = "toolStrip1";
            // 
            // TBTNBold
            // 
            this.TBTNBold.CheckOnClick = true;
            this.TBTNBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TBTNBold.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.TBTNBold.ForeColor = System.Drawing.Color.White;
            this.TBTNBold.Image = ((System.Drawing.Image)(resources.GetObject("TBTNBold.Image")));
            this.TBTNBold.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TBTNBold.Name = "TBTNBold";
            this.TBTNBold.Size = new System.Drawing.Size(23, 27);
            this.TBTNBold.Text = "B";
            this.TBTNBold.Click += new System.EventHandler(this.TBTNBold_Click);
            // 
            // TBTNItalics
            // 
            this.TBTNItalics.CheckOnClick = true;
            this.TBTNItalics.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TBTNItalics.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.TBTNItalics.ForeColor = System.Drawing.Color.White;
            this.TBTNItalics.Image = ((System.Drawing.Image)(resources.GetObject("TBTNItalics.Image")));
            this.TBTNItalics.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TBTNItalics.Name = "TBTNItalics";
            this.TBTNItalics.Size = new System.Drawing.Size(23, 27);
            this.TBTNItalics.Text = "I";
            this.TBTNItalics.Click += new System.EventHandler(this.TBTNItalics_Click);
            // 
            // TBTNUnderline
            // 
            this.TBTNUnderline.CheckOnClick = true;
            this.TBTNUnderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TBTNUnderline.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline);
            this.TBTNUnderline.ForeColor = System.Drawing.Color.White;
            this.TBTNUnderline.Image = ((System.Drawing.Image)(resources.GetObject("TBTNUnderline.Image")));
            this.TBTNUnderline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TBTNUnderline.Name = "TBTNUnderline";
            this.TBTNUnderline.Size = new System.Drawing.Size(23, 27);
            this.TBTNUnderline.Text = "U";
            this.TBTNUnderline.Click += new System.EventHandler(this.TBTNUnderline_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 30);
            // 
            // TBTNLeft
            // 
            this.TBTNLeft.Checked = true;
            this.TBTNLeft.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TBTNLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TBTNLeft.Image = ((System.Drawing.Image)(resources.GetObject("TBTNLeft.Image")));
            this.TBTNLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TBTNLeft.Name = "TBTNLeft";
            this.TBTNLeft.Size = new System.Drawing.Size(23, 27);
            this.TBTNLeft.Text = "toolStripButton1";
            this.TBTNLeft.ToolTipText = "Left";
            this.TBTNLeft.Click += new System.EventHandler(this.TBTNLeft_Click);
            // 
            // TBTCentre
            // 
            this.TBTCentre.Checked = true;
            this.TBTCentre.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TBTCentre.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TBTCentre.Image = ((System.Drawing.Image)(resources.GetObject("TBTCentre.Image")));
            this.TBTCentre.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TBTCentre.Name = "TBTCentre";
            this.TBTCentre.Size = new System.Drawing.Size(23, 27);
            this.TBTCentre.Text = "toolStripButton2";
            this.TBTCentre.ToolTipText = "Centre";
            this.TBTCentre.Click += new System.EventHandler(this.TBTCentre_Click);
            // 
            // TBTNRight
            // 
            this.TBTNRight.Checked = true;
            this.TBTNRight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TBTNRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TBTNRight.Image = ((System.Drawing.Image)(resources.GetObject("TBTNRight.Image")));
            this.TBTNRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TBTNRight.Name = "TBTNRight";
            this.TBTNRight.Size = new System.Drawing.Size(23, 27);
            this.TBTNRight.Text = "toolStripButton3";
            this.TBTNRight.ToolTipText = "Right";
            this.TBTNRight.Click += new System.EventHandler(this.TBTNRight_Click);
            // 
            // TBTNJustify
            // 
            this.TBTNJustify.Checked = true;
            this.TBTNJustify.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TBTNJustify.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TBTNJustify.Enabled = false;
            this.TBTNJustify.Image = ((System.Drawing.Image)(resources.GetObject("TBTNJustify.Image")));
            this.TBTNJustify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TBTNJustify.Name = "TBTNJustify";
            this.TBTNJustify.Size = new System.Drawing.Size(23, 27);
            this.TBTNJustify.Text = "toolStripButton1";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 30);
            // 
            // STAStatus
            // 
            this.STAStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SLBLColRow,
            this.SLBLStatus});
            this.STAStatus.Location = new System.Drawing.Point(0, 516);
            this.STAStatus.Name = "STAStatus";
            this.STAStatus.Size = new System.Drawing.Size(993, 22);
            this.STAStatus.TabIndex = 3;
            this.STAStatus.Text = "statusStrip1";
            this.STAStatus.Paint += new System.Windows.Forms.PaintEventHandler(this.STAStatus_Paint);
            // 
            // SLBLColRow
            // 
            this.SLBLColRow.BackColor = System.Drawing.Color.DarkGreen;
            this.SLBLColRow.ForeColor = System.Drawing.Color.White;
            this.SLBLColRow.Name = "SLBLColRow";
            this.SLBLColRow.Size = new System.Drawing.Size(10, 17);
            this.SLBLColRow.Text = " ";
            this.SLBLColRow.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // SLBLStatus
            // 
            this.SLBLStatus.BackColor = System.Drawing.Color.DarkGreen;
            this.SLBLStatus.ForeColor = System.Drawing.Color.White;
            this.SLBLStatus.Name = "SLBLStatus";
            this.SLBLStatus.Size = new System.Drawing.Size(118, 17);
            this.SLBLStatus.Text = "toolStripStatusLabel1";
            // 
            // RTXTDocument
            // 
            this.RTXTDocument.AcceptsTab = true;
            this.RTXTDocument.Location = new System.Drawing.Point(0, 60);
            this.RTXTDocument.Name = "RTXTDocument";
            this.RTXTDocument.Size = new System.Drawing.Size(989, 453);
            this.RTXTDocument.TabIndex = 2;
            this.RTXTDocument.Text = "";
            this.RTXTDocument.SelectionChanged += new System.EventHandler(this.RTXTDocument_SelectionChanged);
            this.RTXTDocument.ModifiedChanged += new System.EventHandler(this.RTXTDocument_ModifiedChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Olive;
            this.panel1.Controls.Add(this.CHKWordWrap);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.CMBType);
            this.panel1.Controls.Add(this.CMBColours);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.CMBSize);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.CMBFonts);
            this.panel1.Location = new System.Drawing.Point(193, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(798, 30);
            this.panel1.TabIndex = 6;
            // 
            // CHKWordWrap
            // 
            this.CHKWordWrap.AutoSize = true;
            this.CHKWordWrap.BackColor = System.Drawing.Color.Olive;
            this.CHKWordWrap.Checked = true;
            this.CHKWordWrap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKWordWrap.ForeColor = System.Drawing.Color.White;
            this.CHKWordWrap.Location = new System.Drawing.Point(715, 7);
            this.CHKWordWrap.Name = "CHKWordWrap";
            this.CHKWordWrap.Size = new System.Drawing.Size(81, 17);
            this.CHKWordWrap.TabIndex = 16;
            this.CHKWordWrap.Text = "Word Wrap";
            this.CHKWordWrap.UseVisualStyleBackColor = false;
            this.CHKWordWrap.CheckedChanged += new System.EventHandler(this.CHKWordWrap_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(597, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Text Fomat:";
            // 
            // CMBType
            // 
            this.CMBType.BackColor = System.Drawing.Color.DarkGreen;
            this.CMBType.ForeColor = System.Drawing.Color.White;
            this.CMBType.FormattingEnabled = true;
            this.CMBType.Items.AddRange(new object[] {
            "Text",
            "RTF"});
            this.CMBType.Location = new System.Drawing.Point(661, 4);
            this.CMBType.Name = "CMBType";
            this.CMBType.Size = new System.Drawing.Size(45, 21);
            this.CMBType.TabIndex = 14;
            this.CMBType.Text = "Text";
            this.CMBType.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.CMBType_DrawItem);
            // 
            // CMBColours
            // 
            this.CMBColours.BackColor = System.Drawing.Color.DarkGreen;
            this.CMBColours.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.CMBColours.DropDownWidth = 200;
            this.CMBColours.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMBColours.ForeColor = System.Drawing.Color.White;
            this.CMBColours.FormattingEnabled = true;
            this.CMBColours.Location = new System.Drawing.Point(398, 5);
            this.CMBColours.Name = "CMBColours";
            this.CMBColours.Size = new System.Drawing.Size(197, 21);
            this.CMBColours.TabIndex = 13;
            this.CMBColours.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.CMBColours_DrawItem);
            this.CMBColours.SelectedValueChanged += new System.EventHandler(this.CMBColours_SelectedValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(358, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Colour:";
            // 
            // CMBSize
            // 
            this.CMBSize.BackColor = System.Drawing.Color.DarkGreen;
            this.CMBSize.ForeColor = System.Drawing.Color.White;
            this.CMBSize.FormattingEnabled = true;
            this.CMBSize.Items.AddRange(new object[] {
            "8.25",
            "10",
            "11",
            "12",
            "14",
            "16",
            "18",
            "20",
            "22",
            "24",
            "26",
            "28",
            "30"});
            this.CMBSize.Location = new System.Drawing.Point(301, 5);
            this.CMBSize.MaxLength = 2;
            this.CMBSize.Name = "CMBSize";
            this.CMBSize.Size = new System.Drawing.Size(52, 21);
            this.CMBSize.TabIndex = 10;
            this.CMBSize.Text = "8.25";
            this.CMBSize.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.CMBSize_DrawItem);
            this.CMBSize.SelectedValueChanged += new System.EventHandler(this.CMBSize_SelectedValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(269, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Size:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Font:";
            // 
            // CMBFonts
            // 
            this.CMBFonts.BackColor = System.Drawing.Color.DarkGreen;
            this.CMBFonts.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.CMBFonts.ForeColor = System.Drawing.Color.White;
            this.CMBFonts.FormattingEnabled = true;
            this.CMBFonts.Location = new System.Drawing.Point(34, 6);
            this.CMBFonts.Name = "CMBFonts";
            this.CMBFonts.Size = new System.Drawing.Size(230, 21);
            this.CMBFonts.TabIndex = 6;
            this.CMBFonts.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.CMBFonts_DrawItem);
            this.CMBFonts.SelectedValueChanged += new System.EventHandler(this.CMBFonts_SelectedValueChanged);
            // 
            // TOOLTIPText
            // 
            this.TOOLTIPText.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.TOOLTIPText_Draw);
            // 
            // FRMMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Olive;
            this.ClientSize = new System.Drawing.Size(993, 538);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.STAStatus);
            this.Controls.Add(this.RTXTDocument);
            this.Controls.Add(this.TLSMenu);
            this.Controls.Add(this.MNUSMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MNUSMenu;
            this.MaximizeBox = false;
            this.Name = "FRMMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rog\'s Text Editor 2025";
            this.Load += new System.EventHandler(this.FRMMain_Load);
            this.MNUSMenu.ResumeLayout(false);
            this.MNUSMenu.PerformLayout();
            this.TLSMenu.ResumeLayout(false);
            this.TLSMenu.PerformLayout();
            this.STAStatus.ResumeLayout(false);
            this.STAStatus.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MNUSMenu;
        private System.Windows.Forms.ToolStrip TLSMenu;
        private System.Windows.Forms.StatusStrip STAStatus;
        private System.Windows.Forms.ToolStripStatusLabel SLBLColRow;
        private System.Windows.Forms.ToolStripStatusLabel SLBLStatus;
        private System.Windows.Forms.ToolStripButton TBTNBold;
        private System.Windows.Forms.ToolStripButton TBTNItalics;
        private System.Windows.Forms.ToolStripButton TBTNUnderline;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.RichTextBox RTXTDocument;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MNUNew;
        private System.Windows.Forms.ToolStripMenuItem MNUSave;
        private System.Windows.Forms.ToolStripMenuItem MNUSaveAs;
        private System.Windows.Forms.ToolStripMenuItem MNUClose;
        private System.Windows.Forms.ToolStripSeparator MNUSep1;
        private System.Windows.Forms.ToolStripMenuItem MNUPrint;
        private System.Windows.Forms.ToolStripSeparator MNUSep2;
        private System.Windows.Forms.ToolStripMenuItem MNUExit;
        private System.Windows.Forms.ToolStripMenuItem MNUOpen;
        private System.Windows.Forms.ToolStripButton TBTNLeft;
        private System.Windows.Forms.ToolStripButton TBTCentre;
        private System.Windows.Forms.ToolStripButton TBTNRight;
        private System.Windows.Forms.ToolStripButton TBTNJustify;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox CMBColours;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox CMBSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CMBFonts;
        private System.Windows.Forms.CheckBox CHKWordWrap;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CMBType;
        private System.Drawing.Printing.PrintDocument printDoc;
        private System.Windows.Forms.ToolStripMenuItem MNURecentFiles;
        private System.Windows.Forms.ToolTip TOOLTIPText;
    }
}

