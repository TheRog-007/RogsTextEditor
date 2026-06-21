using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Security.AccessControl;

/*
  Created 01/04/2025 By Roger Williams

  Reimaginging of a prehistoric Delphi 6 project to create a simple text editor!


  Done for fun, still in development, how hard can it be?

  This version features:

  - Uses the registry for recent files list
  - supports text and RTF
  - converts RTF to Text, Text To RTF!
  - prints
  - recent files list

  RTF Support:

  - bold/italic/underline
  - font size
  - font colour
  - font type
  - left/right/sentre alignment

  To Do:
  - print preview


*/

namespace RogsTextEditor
{
    public partial class FRMMain : Form
    {
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
        //colour etc for font combobox
        Font fntFonts = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular);
        Color colFonts = Color.Black;
        Brush bruFonts = new SolidBrush(Color.Black);
        Rectangle rectFonts;

        //used when loading a file to ignore rich textbox text changed event
        bool blnloading = false;

        public FRMMain()
        {
            InitializeComponent();
        }


        //******* start custom functions*****

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

      
            //make sure not trying to save empty file!
            if (this.RTXTDocument.Lines.Count() == 0) return;

            if (this.SLBLStatus.Text == "Modified" || this.SLBLStatus.Text == "New")
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

            foreach (string strTemp in regKey.GetValueNames())
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

            foreach (ToolStripMenuItem MNUTemp in this.MNURecentFiles.DropDownItems)
            {
                if (MNUTemp.Tag.ToString() == strFileName)
                {
                    this.MNURecentFiles.DropDownItems.Remove(MNUTemp);
                    //remove from registry
                    //check registry for item
                    regKey = Registry.CurrentUser.OpenSubKey(CNST_STR_REGISTRYKEY, true);

                    foreach (string strTemp in regKey.GetValueNames())
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
            }

            //renumber existing recent menu items
            ReNumberRecent();
        }
        private void OpenFile(bool blnJustOpen, string strFile)
        {
            /*
              Modified 21/06/2026 By Roger Williams
             
              trying wizard new idea for absolute text changes by loading the file
              into a hidden richtextbox and comparing the lines!
              
             
              Created 07/04/2025 By Roger Williams
            
              Open file

              Will load as RTF or plain text depending on file dialog filter

              VARS

              blnJustOpen - if TRUE recent file so skip open file dialog
              strFile     - if recent file this is the file to open
            */


            /*
             OpenFileDialog fdlgOpen = new OpenFileDialog();
            string strData = "";
            int intLine = 1;

            fdlgOpen.Title = "Open File";
            fdlgOpen.InitialDirectory = Application.StartupPath;
            fdlgOpen.DefaultExt = "txt";
            fdlgOpen.Filter = "Text|*.txt|Rich Text File|*.rtf";
            fdlgOpen = new OpenFileDialog();

            if (fdlgOpen.ShowDialog() == DialogResult.OK)
            {
                strFileName = fdlgOpen.FileName;
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

                            AddToRecentList(fdlgOpen.FileName);
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
                            AddToRecentList(fdlgOpen.FileName);
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
            }
            */
            OpenFileDialog fdlgOpen = new OpenFileDialog();
            string strData = "";
            int intLine = 1;

            //disable rich textbox text changed event
            blnloading = true;

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
                else
                    return;
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
                                    while (!strmRead.EndOfStream)
                                    {
                                        strData = strmRead.ReadLine() + "\n";
                                        this.RTXTDocument.AppendText(strData);
                                        intLine++;
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

                 }
                catch (Exception ex)
                {
                    MessageBox.Show("Error!\n\n" + ex.Message, "File Open Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            AddToRecentList(strFileName);
            //set title
            this.Text = CNST_STR_TITLE + " - " + Path.GetFileName(strFileName);
            //enable rich textbox text changed event
            blnloading = false;
            //enable save as
            this.MNUSaveAs.Enabled = true;
            //set status
            this.SLBLStatus.Text = "Edit";
            //activate editor
            this.RTXTDocument.Enabled = true;
        }

        private void SaveFile()
        {
            /*
              Created 07/04/2025 By Roger Williams
            
              Save file

              Will save as RTF or plain text
              If text saves ines to stream
            */
            FileMode fileModeType;

            if (this.CMBType.Text == "RTF")
            {
                try
                {
                    //if extension NOT RTF change it!
                    if (Path.GetExtension(strFileName).ToLower() != "RTF")
                    {
                        strFileName = Path.ChangeExtension(strFileName, "rtf");
                    }

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
                    if (File.Exists(strFileName))
                    {
                        //opens file sets contents to null
                        fileModeType = FileMode.Truncate;
                    }
                    else
                        fileModeType = FileMode.Create;


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

                    ////disable editor
                    //this.RTXTDocument.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error!\n\n" + ex.Message, "File Save Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            AddToRecentList(strFileName);

            if (this.SLBLStatus.Text == "New")
            {
                this.SLBLStatus.Text = "Saved";
                //enabled save as function
                this.MNUSaveAs.Enabled = true;
            }
        
            MessageBox.Show("Saved!", "File Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void EnableDisableRTFControls(bool blnEnable)
        {
            /*
              Created 30/06/2026 By Roger Williams
            
              enables/disables RTF controls

              VARS

              blnenable - enables/disables RTF controls


            */

            this.TBTCentre.Enabled = blnEnable;
            this.TBTNBold.Enabled = blnEnable;
            this.TBTNItalics.Enabled = blnEnable;
            this.TBTNRight.Enabled = blnEnable;
            this.TBTNUnderline.Enabled = blnEnable;
            this.CMBColours.Enabled = blnEnable;
            this.CMBFonts.Enabled = blnEnable;
            this.CMBSize.Enabled = blnEnable;
            this.CHKWordWrap.Enabled = blnEnable;
        }

        //*****end custom functions*****

        //form events etc
        private void FRMMain_Load(object sender, EventArgs e)
        {

            //init statusbar
            this.SLBLColRow.Text = "Row: " + intRow.ToString() + " Col: " + intCol.ToString();
            this.SLBLStatus.Text = "";

            //init comboboxes
            GetSystemFonts();
            GetSystemColours();
            //set combbox events
            SetComboBoxKeyPress();
            //get recent files list
            GetRecentRegistryData();
            //disable RTF controls
            EnableDisableRTFControls(false);
            //disable editor
            this.RTXTDocument.Enabled = false;
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
           // this.SLBLStatus.Text = this.RTXTDocument.Modified == true ? "Modified" : "";
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
                SaveFile();
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
                    SaveFile();
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
            //disable editor
            this.RTXTDocument.Enabled = false;
            this.MNUSaveAs.Enabled = false;
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
            //enable rich textbox
            this.RTXTDocument.Enabled = true;
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

            if (e.Index >= 0)
            {
                colColours = Color.FromName(((ComboBox)sender).Items[e.Index].ToString());
                bruColours = new SolidBrush(colColours);
                e.Graphics.DrawString(((ComboBox)sender).Items[e.Index].ToString(), fntColours, Brushes.Black, rectColours.X, rectColours.Top);
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

            if (e.Index >= 0)
            {
                fntColours = new Font(((ComboBox)sender).Items[e.Index].ToString(), 8.25f, FontStyle.Regular);
                e.Graphics.DrawString(((ComboBox)sender).Items[e.Index].ToString(), fntColours, Brushes.Black, rectFonts.X, rectFonts.Top);
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

        }

        private void MNUPrint_Click(object sender, EventArgs e)
        {
            this.printDoc.DocumentName = strFileName;
            this.printDoc.Print();
        }

        private void RTXTDocument_TextChanged(object sender, EventArgs e)
        {
            if (this.RTXTDocument.Lines.Count() != 0)
            {
                if (!blnloading)
                {
                 if (this.SLBLStatus.Text != "New")
                    { 
                        this.SLBLStatus.Text = "Modified";
                    }
                }
            }
        }
    }
}
