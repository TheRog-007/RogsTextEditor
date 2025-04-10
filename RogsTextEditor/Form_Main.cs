using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;

namespace RogsTextEditor
{

    public partial class FRMMain : Form
    {
        //used for menu to get colours
        class MyColours : ProfessionalColorTable
        {
            public override Color ToolStripDropDownBackground 
            {
                get { return Color.DarkGreen; }
            }

            public override Color MenuItemSelected
            {
                get { return Color.Green; }
            }

            //public override Color MenuItemSelectedGradientBegin
            //{
            //    get { return Color.DarkCyan; }
            //}
            //public override Color MenuItemSelectedGradientEnd
            //{
            //    get { return Color.Cyan; }
            //}

            public override Color MenuItemPressedGradientBegin
            {
                get { return Color.Green; }
            }

            public override Color MenuItemPressedGradientEnd
            {
                get { return Color.Green; }
            }
        }
        //used by above to set menu colours
        class NewColourRenderer : ToolStripProfessionalRenderer
        {
            public NewColourRenderer() : base(new MyColours()) { }
        }


        readonly string CNST_STR_TITLE = "Rog's Text Editor 2025";
        readonly string CNST_STR_REGISTRYKEY =  @"Software\RogsTextEditor2025";

        //last number of recent files list
        int intRecent = 1;
        //row/col calculations
        int intRow = 1;
        int intCol = 1;
        int intIndex = 0;
        //current filename
        string strFileName = "";
        //font etc for colours combobox
        Font fntColours = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular);
        Brush bruColours = new SolidBrush(Color.Black);
        Color colColours = Color.Black;
        Rectangle rectColours;
        Rectangle rectFontSize;
        Rectangle rectType;
        //colour etc for comboboxes
        Font fntFonts = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular);
        Rectangle rectFonts;
        //for combobox background and font colour
        Pen penTemp = new Pen(Color.DarkGreen);
        // Create solid brush.
        SolidBrush bruTemp = new SolidBrush(Color.DarkGreen);

        public FRMMain()
        {
            InitializeComponent();
        }

        private void SetTextFont()
        {
            /*
              Created 07/04/2025 By Roger Williams
            
              Sets font for text using TCMBize for font size
              
            */
            FontFamily family = new FontFamily(CMBFonts.Text);
            Font fntFontNew = new Font(family, float.Parse(this.CMBSize.Text), FontStyle.Regular);
            Font fntFontCur = this.RTXTDocument.Font;

            this.RTXTDocument.SelectionFont = fntFontNew;
            this.RTXTDocument.Font = fntFontCur;
        }

        private void CheckForSave(object sender, EventArgs e)
        {
            /*
              Created 07/04/2025 By Roger Williams

              Check if file saved before close

            */

            if (this.STAStatus.Text == "Modified" || this.STAStatus.Text == "New")
            {
                if (MessageBox.Show("Save Changes?", "Lose Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    MNUSave_Click(sender, e);
                }
            }
        }

        private void GetSystemColours()
        {
            /*
              Created 08/04/2025 By Roger Williams
            
              Gets colourss list and puts into combobox
              
            */

            this.CMBColours.Items.Clear();

            foreach (string strName in Enum.GetNames(typeof(System.Drawing.KnownColor)))
            {
                this.CMBColours.Items.Add(strName);
            }

            this.CMBColours.Text = "Black";
        }

        private void GetSystemFonts()
        {
            /*
              Created 08/04/2025 By Roger Williams
            
              Gets fonts list and puts into combobox
              
            */

            InstalledFontCollection fntCol;

            fntCol = new InstalledFontCollection();
            this.CMBFonts.Items.Clear();

            foreach (FontFamily fntFamily in fntCol.Families)
            {
                this.CMBFonts.Items.Add(fntFamily.Name);
            }

            this.CMBFonts.Text = this.RTXTDocument.Font.Name;
            this.CMBSize.Text = this.RTXTDocument.Font.Size.ToString();

        }

        private void HandleComboBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            /*
              Created 08/04/2025 By Roger Williams
            
              Makes sure no keypresses are accepted
              
            */

            e.Handled = true;

        }

        private void SetComboBoxKeyPress()
        {
            /*
              Created 08/04/2025 By Roger Williams
            
              Sets all combobox keypress events to global handler
              
            */

            this.CMBColours.KeyPress += HandleComboBoxKeyPress;
            this.CMBFonts.KeyPress += HandleComboBoxKeyPress;
            this.CMBSize.KeyPress += HandleComboBoxKeyPress;
            this.CMBType.KeyPress += HandleComboBoxKeyPress;
        }

        private void RecentMenuItem_Click(object sender, EventArgs e)
        {
            /*
               Created 09/04/2025 By Roger Williams
               
               Opens recent file selected

               Uses tag for actual full path + name
            */
            ToolStripMenuItem MNUTemp;

            MNUTemp = (ToolStripMenuItem)sender;
            CheckForSave(sender, e);
            OpenFile(true,MNUTemp.Tag.ToString());
        }

        private void GetRecentRegistryData()
        {
            /*
              Created 09/04/2025 By Roger Williams
            
              Gets registry entries
              Creates recent file menu items 

            */
            RegistryKey regKey;
            RegistryKey regSubKey;
            ToolStripMenuItem MNUFile;
            string strTemp = "";

            //check registry for item
            regKey = Registry.CurrentUser.OpenSubKey(CNST_STR_REGISTRYKEY, true);

            //check if no entries
            if (regKey == null)
            {
                return;
            }
            else
            {
                foreach (string strRegKey in regKey.GetSubKeyNames())
                {
                    regSubKey = regKey.OpenSubKey(strRegKey);
                    strTemp = regSubKey.GetValue("FileName").ToString();
                    MNUFile = new ToolStripMenuItem();
                    MNUFile.Text = Path.GetFileName(regSubKey.Name) + ":" + Path.GetFileName(strTemp);
                    MNUFile.Tag = strTemp;
                    MNUFile.DisplayStyle = ToolStripItemDisplayStyle.Text;
                    MNUFile.ForeColor = Color.White;
                  
                    //add click handler
                    MNUFile.Click += RecentMenuItem_Click;
                    //add to recent files menu
                    this.MNURecentFiles.DropDownItems.Add(MNUFile);
                    //inc intrecent
                    intRecent++;
                }
            }

            regKey.Close();
        }
        private void AddToRecentRegistryData(string strFileName)
        {
            /*
              Created 09/04/2025 By Roger Williams
            
              Creates registry entry

              VARS

              strFileName - file to add
            */
            RegistryKey regKey;
            RegistryKey regSubKey;
            int intNumKeys = 0;

            //check registry for item
            regKey = Registry.CurrentUser.OpenSubKey(CNST_STR_REGISTRYKEY, true);

            //check if no entries
            if (regKey == null)
            {
                //create new key and data
                regKey = Registry.CurrentUser.CreateSubKey(CNST_STR_REGISTRYKEY, true);
                regSubKey = regKey.CreateSubKey("1");
                regSubKey.SetValue("FileName", strFileName);
                regSubKey.Close();
            }
            else
            {
                //make sure does not already exist by checking FileName value
                foreach (string strTemp in regKey.GetSubKeyNames())
                {
                    //open subkey
                    regSubKey = regKey.OpenSubKey(strTemp);
                    //check filename value against strfilename
                    if (regSubKey.GetValue("FileName").ToString() == strFileName)
                    {
                        return;
                    }

                    regSubKey.Close();
                }

                intNumKeys = regKey.SubKeyCount+1;
                //add new file data
                regSubKey = regKey.CreateSubKey(intNumKeys.ToString(), true);
                regSubKey.SetValue("FileName", strFileName);
                regSubKey.Close();
            }
            regKey.Close();
        }

        private void AddToRecentList(string strFileName)
        {
            /*
              Created 09/04/2025 By Roger Williams
            
              Adds passed filename if unique to recent files list
              Saves in registry with unique number as key and full path as string value
              Creates menuitem with same number but only filename, uses tag for full path + name
              
              Each menu item has a unique number this is the registry key 
            */

            ToolStripMenuItem MNUFile;

            //check if already exists
            foreach (ToolStripMenuItem MNUTemp in this.MNURecentFiles.DropDownItems)
            {
                if (MNUTemp.Tag.ToString() == strFileName)
                {
                    return;
                }
            }

            //add new menu item
            MNUFile = new ToolStripMenuItem();
            MNUFile.Text = intRecent.ToString() + ":" + Path.GetFileName(strFileName);
            MNUFile.Tag = strFileName;
            MNUFile.DisplayStyle = ToolStripItemDisplayStyle.Text;
            MNUFile.ForeColor = Color.White;

            //add click handler
            MNUFile.Click += RecentMenuItem_Click;
            //add to recent files menu
            this.MNURecentFiles.DropDownItems.Add(MNUFile);
            //add to registry
            AddToRecentRegistryData(strFileName);
            //inc intrecent
            intRecent++;
        }

        private void ReCreateRecentRegistryData()
        {
            /*
              Created 09/04/2025 By Roger Williams
            
              Recreates registry entries based on recent menu sub items

            */

            RegistryKey regKey;
            RegistryKey regSubKey;
            int intNum = 1;

            //delete existing sub keys
            regKey = Registry.CurrentUser.OpenSubKey(CNST_STR_REGISTRYKEY, true);

            foreach (string strTemp in regKey.GetSubKeyNames())
            {
              regKey.DeleteSubKey(strTemp);
            }

            //recreate subkeys
            foreach (ToolStripMenuItem MNUTemp in this.MNURecentFiles.DropDownItems)
            {
                //add new file data
                regSubKey = regKey.CreateSubKey(intNum.ToString(), true);
                regSubKey.SetValue("FileName", MNUTemp.Tag.ToString());
                regSubKey.Close();
                intNum++;
            }

            regKey.Close();
        }
        private void ReNumberRecent()
        {
            /*
              Created 09/04/2025 By Roger Williams
            
              Renumbers recent menu items
              Recreates registry entries based on recent menu sub items

            */
            intRecent = 1;

            //update registry
            ReCreateRecentRegistryData();

            //check if any recent menu items
            if (this.MNURecentFiles.DropDownItems.Count == 0)
            {
                return;
            }
 
            foreach (ToolStripMenuItem MNUTemp in this.MNURecentFiles.DropDownItems)
            {
                 MNUTemp.Text = intRecent.ToString() + ":" + Path.GetFileName(MNUTemp.Tag.ToString());
            }

        }
        private void RemoveFromRecent(string strFileName)
        {
            /*
              Created 09/04/2025 By Roger Williams
            
              Removes file from recent list and renumbers remaining items
              Removes from registry

              VARS

              strFilename     - file to remove
            */

            RegistryKey regKey;
            RegistryKey regSubKey;
            int intNum = 0;
            ToolStripItem MNUTemp;

            while (intNum != this.MNURecentFiles.DropDownItems.Count)
            {
                MNUTemp = this.MNURecentFiles.DropDownItems[intNum];

                if (MNUTemp.Tag.ToString() == strFileName)
                {
                    intNum = 0;
                    this.MNURecentFiles.DropDownItems.Remove(MNUTemp);
                    //remove from registry
                    //check registry for item
                    regKey = Registry.CurrentUser.OpenSubKey(CNST_STR_REGISTRYKEY, true);

                    foreach (string strTemp in regKey.GetSubKeyNames())
                    {
                        regSubKey = regKey.OpenSubKey(strTemp);

                        if (regSubKey.GetValue("FileName").ToString() == strFileName)
                        {
                            regSubKey.Close();
                            regKey.DeleteSubKey(strTemp);
                        }
                    }

                    regKey.Close();
                }
                else
                {
                    intNum++;
                }
            }

            //renumber existing recent menu items
            ReNumberRecent();
        }
        private void OpenFile(bool blnJustOpen, string strFile)
        {
            /*
              Created 07/04/2025 By Roger Williams
            
              Open file

              Will load as RTF or plain text depending on file dialog filter

              VARS

              blnJustOpen - if TRUE recent file so skip open file dialog
              strFile     - if recent file this is the file to open
            */

            OpenFileDialog fdlgOpen = new OpenFileDialog();
            string strData = "";
            int intLine = 1;

            if (!blnJustOpen)
            {
                fdlgOpen.Title = "Open File";
                fdlgOpen.InitialDirectory = Application.StartupPath;
                fdlgOpen.DefaultExt = "txt";
                fdlgOpen.Filter = "Text|*.txt|Rich Text File|*.rtf";
                fdlgOpen = new OpenFileDialog();

                if (fdlgOpen.ShowDialog() == DialogResult.OK)
                {
                    strFileName = fdlgOpen.FileName;
                }
            }
            else
            {
                //open recent file
                strFileName = strFile;
                //check if file exists
                if (!File.Exists(strFileName))
                {
                    MessageBox.Show("Error Accessing File - File Not Found", "File Open Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RemoveFromRecent(strFileName);
                    return;
                }
            }
            this.RTXTDocument.Clear();

            try
            {
                if (Path.GetExtension(strFileName.ToLower()) == ".txt")
                {
                    //load plain text file
                    try
                    {
                        using (FileStream filRead = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
                        {
                            using (StreamReader strmRead = new StreamReader(filRead))
                            {
                                strData = strmRead.ReadLine();
                                this.RTXTDocument.AppendText(strData);
                                intLine++;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error!\n\n" + ex.Message, "File Save Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    try
                    {
                        this.RTXTDocument.LoadFile(strFileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error!\n\n" + ex.Message, "File Save Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                this.SLBLStatus.Text = "Edit";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!\n\n" + ex.Message, "File Open Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (Path.GetExtension(strFileName).ToLower() == "rtf")
            { 
                this.CMBType.Text = "RTF";
            }
            else
            {
                this.CMBType.Text = "Text";
            }

            this.SLBLStatus.Text = "Edit";
            AddToRecentList(strFileName);
            //set title
            this.Text = CNST_STR_TITLE + " - " + Path.GetFileName(strFileName);
        }
        private void SaveFile(bool blnSilent)
        {
            /*
              Created 07/04/2025 By Roger Williams
            
              Save file

              Will save as RTF or plain text
              If text saves lines to stream
              

              VARS

              blnSilent - used by print skip messages and registry/menu entries creation
            */
            FileMode fileModeType;

            if (this.CMBType.Text == "RTF")
            {
                try
                {
                    this.RTXTDocument.SaveFile(strFileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error!\n\n" + ex.Message, "File Save Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                //save as plain text  
                try
                {
                    //opens file sets contents to null
                    fileModeType = FileMode.Truncate;

                    using (FileStream filWrite = new FileStream(strFileName, fileModeType, FileAccess.Write))
                    {
                        using (StreamWriter strmWriter = new StreamWriter(filWrite))
                        {
                            foreach (string strItem in this.RTXTDocument.Lines)
                            {
                                strmWriter.WriteLine(strItem);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error!\n\n" + ex.Message, "File Save Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (!blnSilent)
            {
                this.SLBLStatus.Text = "Modified";
                AddToRecentList(strFileName);
                MessageBox.Show("Saved!", "File Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SetValuesOnSubItems(List<ToolStripMenuItem> MNUItems)
        {
            /*
              Created 010/04/2025 By Roger Williams
            
              Removes the image block from all menu items

              Copied from:

              https://stackoverflow.com/questions/32577447/c-sharp-removing-submenu-item-image-margins
              
            */

            MNUItems.ForEach(item =>
            {
                var dropdown = (ToolStripDropDownMenu)item.DropDown;
             
                if (dropdown != null)
                {
                    dropdown.ShowImageMargin = false;
                    SetValuesOnSubItems(item.DropDownItems.OfType<ToolStripMenuItem>().ToList());
                }
            });
        }
        //form events etc
        private void FRMMain_Load(object sender, EventArgs e)
        {
            this.TOOLTIPText.BackColor = Color.LightGreen;
            this.TOOLTIPText.ForeColor = Color.DarkBlue;
            this.TOOLTIPText.IsBalloon = false; 
            this.TOOLTIPText.UseFading = true;
            this.TOOLTIPText.OwnerDraw = true;

            //set tooltips
            this.TOOLTIPText.SetToolTip(this.CMBColours, "Font Colours");
            this.TOOLTIPText.SetToolTip(this.CMBFonts, "Fonts");
            this.TOOLTIPText.SetToolTip(this.CMBType, "Plain Text or RTF Format?");
            this.TOOLTIPText.SetToolTip(this.CMBSize, "Font Size");
            this.TOOLTIPText.SetToolTip(this.CHKWordWrap, "Word Wrap?");

            //init statusbar
            this.SLBLColRow.Text = "Row: " + intRow.ToString() + " Col: " + intCol.ToString();
            this.SLBLStatus.Text = "";

            //init comboboxs
            GetSystemFonts();
            GetSystemColours();
            //set combbox events
            SetComboBoxKeyPress();
            //get recent files list
            GetRecentRegistryData();
            //set custom colours for menu
            this.MNUSMenu.Renderer = new NewColourRenderer();
            //remove image block from menu
            SetValuesOnSubItems(this.MNUSMenu.Items.OfType<ToolStripMenuItem>().ToList());
        }

        private void RTXTDocument_SelectionChanged(object sender, EventArgs e)
        {
            /*
              Created 07/04/2025 By Roger Williams
            
              Gets current row and col and shows in statusbar
              
            */

            // Get the line.
            intIndex = RTXTDocument.SelectionStart;
            intRow = RTXTDocument.GetLineFromCharIndex(intIndex);

            // Get the column.
            int firstChar = RTXTDocument.GetFirstCharIndexFromLine(intRow);
            intCol = intIndex - firstChar;
            intCol++;
            intRow++;
            this.SLBLColRow.Text = "Row: " + intRow.ToString() + " Col: " + intCol.ToString();
            this.SLBLStatus.Text = this.RTXTDocument.Modified == true ? "Modified" : "";
        }

        
            private void TBTNBold_Click(object sender, EventArgs e)
        {
            if (!this.RTXTDocument.SelectionFont.Bold)
            {
                this.RTXTDocument.SelectionFont = new Font(this.RTXTDocument.SelectionFont, this.RTXTDocument.SelectionFont.Style | FontStyle.Bold);
            }
            else
            {
                this.RTXTDocument.SelectionFont = new Font(this.RTXTDocument.SelectionFont, this.RTXTDocument.SelectionFont.Style & FontStyle.Regular);
            }
        }

        private void TBTNItalics_Click(object sender, EventArgs e)
        {
            if (!this.RTXTDocument.SelectionFont.Italic)
            {
                this.RTXTDocument.SelectionFont = new Font(this.RTXTDocument.SelectionFont, this.RTXTDocument.SelectionFont.Style | FontStyle.Italic);
            }
            else
            {
                this.RTXTDocument.SelectionFont = new Font(this.RTXTDocument.SelectionFont, this.RTXTDocument.SelectionFont.Style & FontStyle.Regular);
            }
        }

        private void TBTNUnderline_Click(object sender, EventArgs e)
        {
            if (!this.RTXTDocument.SelectionFont.Underline)
            {
                this.RTXTDocument.SelectionFont = new Font(this.RTXTDocument.SelectionFont, this.RTXTDocument.SelectionFont.Style | FontStyle.Underline);
            }
            else
            {
                this.RTXTDocument.SelectionFont = new Font(this.RTXTDocument.SelectionFont, this.RTXTDocument.SelectionFont.Style & FontStyle.Regular);
            }
        }

        private void MNUSave_Click(object sender, EventArgs e)
        {
            /*
              Created 07/04/2025 By Roger Williams
            
              Save file

              Handles if existing file (strfilename != "") or not
              Calls savefile to actually save the data
              
            */
            ToolStripMenuItem MNUTemp = null;
            SaveFileDialog fdlgSave = new SaveFileDialog();

            if (sender.GetType() == typeof(ToolStripMenuItem))
            {
                MNUTemp = (ToolStripMenuItem)sender;
            }

            if (strFileName.Length != 0 && MNUTemp.Name != "MNUSaveAs")
            {
                SaveFile(false);
            }
            else
            {
                fdlgSave.Title = "Save File";
                fdlgSave.InitialDirectory = Application.StartupPath;
                fdlgSave.DefaultExt = "txt";
                fdlgSave.Filter = "Text|*.txt|Rich Text File|*.rtf";
  
                if (this.CMBType.Text == "RTF")
                {
                    fdlgSave.FilterIndex = 2;
                }

                if (fdlgSave.ShowDialog() == DialogResult.OK)
                {
                    strFileName = fdlgSave.FileName;
                    SaveFile(false);
                }
            }
        }

        private void MNUExit_Click(object sender, EventArgs e)
        {
            /*
              Created 07/04/2025 By Roger Williams
            
              Check if file saved before close
              
            */

            CheckForSave(sender, e);
            this.Close();
        }

        private void MNUClose_Click(object sender, EventArgs e)
        {
            /*
              Created 07/04/2025 By Roger Williams
            
              Check if file saved before close
              
            */

            CheckForSave(sender, e);
            this.RTXTDocument.Clear();
            strFileName = "";
            this.Text = CNST_STR_TITLE;
        }

        private void MNUNew_Click(object sender, EventArgs e)
        {
            CheckForSave(sender, e);
            this.RTXTDocument.Clear();
            this.SLBLStatus.Text = "New";
            strFileName = "";
            //reset richtextbox font
            this.RTXTDocument.Font = this.Font;
            this.CMBFonts.Text = this.RTXTDocument.Font.Name;
            this.CMBSize.Text = this.RTXTDocument.Font.Size.ToString();
            this.RTXTDocument.Focus();
        }

        private void MNUSaveAs_Click(object sender, EventArgs e)
        {
            
            MNUSave_Click(sender, e);
        }

        private void TBTNLeft_Click(object sender, EventArgs e)
        {
            this.RTXTDocument.SelectionAlignment = HorizontalAlignment.Left;
            this.RTXTDocument.Focus();
        }

        private void TBTCentre_Click(object sender, EventArgs e)
        {
            this.RTXTDocument.SelectionAlignment = HorizontalAlignment.Center;
            this.RTXTDocument.Focus();
        }

        private void TBTNRight_Click(object sender, EventArgs e)
        {
            this.RTXTDocument.SelectionAlignment = HorizontalAlignment.Right;
            this.RTXTDocument.Focus();
        }

        private void MNUOpen_Click(object sender, EventArgs e)
        {
            OpenFile(false,"");
        }
        private void CMBColours_DrawItem(object sender, DrawItemEventArgs e)
        {
            /*
              Created 08/04/2025 By Roger Williams
            
              Shows item in combobox with actual colour!
              
            */
            rectColours = e.Bounds;
            e.Graphics.FillRectangle(bruTemp, rectColours);

            if (e.Index >= 0)
            {
                colColours = Color.FromName(((ComboBox)sender).Items[e.Index].ToString());
                bruColours = new SolidBrush(colColours);
                e.Graphics.DrawString(((ComboBox)sender).Items[e.Index].ToString(), fntColours, Brushes.White, rectColours.X, rectColours.Top);
                //draw bar in colour
                e.Graphics.FillRectangle(bruColours, rectColours.X + 130, rectColours.Y + 5, rectColours.Width - 60, rectColours.Height - 10);
            }

        }

        private void CMBFonts_DrawItem(object sender, DrawItemEventArgs e)
        {
            /*
              Created 08/04/2025 By Roger Williams
            
              Shows font item in combobox in actual font!
              
            */

            rectFonts = e.Bounds;
            e.Graphics.FillRectangle(bruTemp, rectFonts);

            if (e.Index >= 0)
            {
                fntColours = new Font(((ComboBox)sender).Items[e.Index].ToString(), 8.25f, FontStyle.Regular);
                e.Graphics.DrawString(((ComboBox)sender).Items[e.Index].ToString(), fntColours, Brushes.White, rectFonts.X, rectFonts.Top);
            }
        }

        private void CMBFonts_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
              Created 07/04/2025 By Roger Williams

              Sets font for text using TCMBize for font size

            */

            SetTextFont();
            this.RTXTDocument.Focus();
        }

        private void CMBSize_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
              Created 07/04/2025 By Roger Williams
            
              Sets font size
              
            */

            SetTextFont();
            this.RTXTDocument.Focus();
        }

        private void CHKWordWrap_CheckedChanged(object sender, EventArgs e)
        {
            this.RTXTDocument.WordWrap = this.CHKWordWrap.Checked;
            this.RTXTDocument.Focus();
        }

        private void CMBColours_SelectedValueChanged(object sender, EventArgs e)
        {
            this.RTXTDocument.SelectionColor = Color.FromName(this.CMBColours.Text);
            this.RTXTDocument.Focus();
        }

        private void RTXTDocument_ModifiedChanged(object sender, EventArgs e)
        {
            /*
              Created 08/04/2025 By Roger Williams
            
              Sets status bar status to modified if not new file
              
            */

            if (this.RTXTDocument.Modified)
            {
                if (strFileName.Length != 0)
                {
                    this.SLBLStatus.Text = "Modified";
                }
            }
        }

        private void MNUPrint_Click(object sender, EventArgs e)
        {
            /*
              Created 09/04/2025 By Roger Williams

              Prints file
              
              First saves to temp file (in case edited but not saved)
              Second creates a processes to open temp file and print it
              Third deletes temp file

            */

            Process pro;
            string strStatus = this.SLBLStatus.Text;
            string strRename = "";
            string strTemp = Path.GetTempFileName();  //create temp file
            string strFileNameCur = strFileName;      //save cur filename if any

            //if unsaved changes saves as temp file
            if (this.SLBLStatus.Text == "New" || this.SLBLStatus.Text == "Modified")
                { 
                strRename = Path.GetDirectoryName(strTemp) + "\\" + Path.GetFileNameWithoutExtension(strTemp);

                //check if txt or RTF
                if (this.CMBType.Text == "RTF")
                {
                    strRename += ".rtf";
                }
                else 
                {
                    strRename += ".txt";
                }

                //rename file
                File.Move(strTemp, strRename);
                strFileName = strRename;
                strTemp = strRename;
                //save data to temp file
                SaveFile(true);
            }
            else
            {
                //if unedited use current filename
                strTemp = strFileName;
            }

            //print
            this.SLBLStatus.Text = "Printing";
            pro = new Process();
            pro.StartInfo.UseShellExecute = true;
            pro.StartInfo.Verb = "print";
            pro.StartInfo.FileName = strTemp;
            pro.StartInfo.WorkingDirectory = Path.GetDirectoryName(strTemp);
            pro.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            pro.Start();
            //wait till finished
            pro.WaitForExit();
            this.SLBLStatus.Text = strStatus;

            //check if temp file
            if (strFileName != strFileNameCur)
            {
                //delete temp file
                File.Delete(strFileName);
                //reset filename to pre-print status
                strFileName = strFileNameCur;
            }
        }

        private void TOOLTIPText_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }

        private void CMBSize_DrawItem(object sender, DrawItemEventArgs e)
        {
            rectFontSize = e.Bounds;
            e.Graphics.FillRectangle(bruTemp, rectFontSize);
        }

        private void CMBType_DrawItem(object sender, DrawItemEventArgs e)
        {
            rectType = e.Bounds;
            e.Graphics.FillRectangle(bruTemp, rectType);
        }
    }
}
 