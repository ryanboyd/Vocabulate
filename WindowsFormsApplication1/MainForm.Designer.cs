namespace VocabulateApplication
{
    partial class VocabulateMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VocabulateMainForm));
            this.BgWorker = new System.ComponentModel.BackgroundWorker();
            this.ScanSubfolderCheckbox = new System.Windows.Forms.CheckBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.FolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.FilenameLabel = new System.Windows.Forms.Label();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.EncodingDropdown = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.RawWCCheckbox = new System.Windows.Forms.CheckBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.LoadDictionaryButton = new System.Windows.Forms.Button();
            this.StopListLabel = new System.Windows.Forms.Label();
            this.StopListTextBox = new System.Windows.Forms.TextBox();
            this.CSVQuoteTextbox = new System.Windows.Forms.TextBox();
            this.CSVDelimiterTextbox = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.DictStructureTextBox = new System.Windows.Forms.TextBox();
            this.DictionaryStructureLabel = new System.Windows.Forms.Label();
            this.DictionarySettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.OutputCapturedWordsCheckbox = new System.Windows.Forms.CheckBox();
            this.DictionarySettingsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // BgWorker
            // 
            this.BgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgWorkerClean_DoWork);
            this.BgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BgWorker_RunWorkerCompleted);
            // 
            // ScanSubfolderCheckbox
            // 
            this.ScanSubfolderCheckbox.AutoSize = true;
            this.ScanSubfolderCheckbox.Location = new System.Drawing.Point(681, 294);
            this.ScanSubfolderCheckbox.Name = "ScanSubfolderCheckbox";
            this.ScanSubfolderCheckbox.Size = new System.Drawing.Size(108, 17);
            this.ScanSubfolderCheckbox.TabIndex = 2;
            this.ScanSubfolderCheckbox.Text = "Scan subfolders?";
            this.ScanSubfolderCheckbox.UseVisualStyleBackColor = true;
            // 
            // StartButton
            // 
            this.StartButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartButton.Location = new System.Drawing.Point(670, 256);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(129, 32);
            this.StartButton.TabIndex = 3;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // FolderBrowser
            // 
            this.FolderBrowser.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.FolderBrowser.ShowNewFolderButton = false;
            // 
            // FilenameLabel
            // 
            this.FilenameLabel.AutoEllipsis = true;
            this.FilenameLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FilenameLabel.Location = new System.Drawing.Point(13, 378);
            this.FilenameLabel.Name = "FilenameLabel";
            this.FilenameLabel.Size = new System.Drawing.Size(859, 25);
            this.FilenameLabel.TabIndex = 6;
            this.FilenameLabel.Text = "Waiting to analyze texts...";
            this.FilenameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.FileName = "VocabulateOutput.csv";
            this.saveFileDialog.Filter = "CSV Files|*.csv";
            this.saveFileDialog.Title = "Please choose where to save your output";
            // 
            // EncodingDropdown
            // 
            this.EncodingDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EncodingDropdown.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EncodingDropdown.FormattingEnabled = true;
            this.EncodingDropdown.Location = new System.Drawing.Point(22, 53);
            this.EncodingDropdown.Name = "EncodingDropdown";
            this.EncodingDropdown.Size = new System.Drawing.Size(221, 23);
            this.EncodingDropdown.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(33, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(198, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "Encoding of Dictionary && Text Files:";
            // 
            // RawWCCheckbox
            // 
            this.RawWCCheckbox.AutoSize = true;
            this.RawWCCheckbox.Checked = true;
            this.RawWCCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RawWCCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RawWCCheckbox.Location = new System.Drawing.Point(657, 318);
            this.RawWCCheckbox.Name = "RawWCCheckbox";
            this.RawWCCheckbox.Size = new System.Drawing.Size(164, 17);
            this.RawWCCheckbox.TabIndex = 21;
            this.RawWCCheckbox.Text = "Output Raw Category Counts";
            this.RawWCCheckbox.UseVisualStyleBackColor = true;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "DictionaryFile.csv";
            this.openFileDialog.Filter = "Dictionary Files|*.csv";
            this.openFileDialog.RestoreDirectory = true;
            // 
            // LoadDictionaryButton
            // 
            this.LoadDictionaryButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadDictionaryButton.Location = new System.Drawing.Point(68, 182);
            this.LoadDictionaryButton.Name = "LoadDictionaryButton";
            this.LoadDictionaryButton.Size = new System.Drawing.Size(129, 32);
            this.LoadDictionaryButton.TabIndex = 20;
            this.LoadDictionaryButton.Text = "Load Dictionary";
            this.LoadDictionaryButton.UseVisualStyleBackColor = true;
            this.LoadDictionaryButton.Click += new System.EventHandler(this.LoadDictionaryButton_Click);
            // 
            // StopListLabel
            // 
            this.StopListLabel.AutoSize = true;
            this.StopListLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StopListLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.StopListLabel.Location = new System.Drawing.Point(95, 9);
            this.StopListLabel.Name = "StopListLabel";
            this.StopListLabel.Size = new System.Drawing.Size(68, 16);
            this.StopListLabel.TabIndex = 21;
            this.StopListLabel.Text = "Stop List";
            // 
            // StopListTextBox
            // 
            this.StopListTextBox.AcceptsReturn = true;
            this.StopListTextBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StopListTextBox.Location = new System.Drawing.Point(13, 32);
            this.StopListTextBox.MaxLength = 2147483647;
            this.StopListTextBox.Multiline = true;
            this.StopListTextBox.Name = "StopListTextBox";
            this.StopListTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.StopListTextBox.Size = new System.Drawing.Size(243, 330);
            this.StopListTextBox.TabIndex = 0;
            this.StopListTextBox.WordWrap = false;
            // 
            // CSVQuoteTextbox
            // 
            this.CSVQuoteTextbox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CSVQuoteTextbox.Location = new System.Drawing.Point(144, 137);
            this.CSVQuoteTextbox.MaxLength = 1;
            this.CSVQuoteTextbox.Name = "CSVQuoteTextbox";
            this.CSVQuoteTextbox.Size = new System.Drawing.Size(65, 20);
            this.CSVQuoteTextbox.TabIndex = 25;
            this.CSVQuoteTextbox.Text = "\"";
            this.CSVQuoteTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CSVDelimiterTextbox
            // 
            this.CSVDelimiterTextbox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CSVDelimiterTextbox.Location = new System.Drawing.Point(57, 137);
            this.CSVDelimiterTextbox.MaxLength = 1;
            this.CSVDelimiterTextbox.Name = "CSVDelimiterTextbox";
            this.CSVDelimiterTextbox.Size = new System.Drawing.Size(65, 20);
            this.CSVDelimiterTextbox.TabIndex = 24;
            this.CSVDelimiterTextbox.Text = ",";
            this.CSVDelimiterTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(141, 119);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(43, 15);
            this.label42.TabIndex = 23;
            this.label42.Text = "Quote:";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.Location = new System.Drawing.Point(56, 119);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(60, 15);
            this.label41.TabIndex = 22;
            this.label41.Text = "Delimiter:";
            // 
            // DictStructureTextBox
            // 
            this.DictStructureTextBox.AcceptsReturn = true;
            this.DictStructureTextBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DictStructureTextBox.Location = new System.Drawing.Point(270, 32);
            this.DictStructureTextBox.MaxLength = 2147483647;
            this.DictStructureTextBox.Multiline = true;
            this.DictStructureTextBox.Name = "DictStructureTextBox";
            this.DictStructureTextBox.ReadOnly = true;
            this.DictStructureTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.DictStructureTextBox.Size = new System.Drawing.Size(313, 330);
            this.DictStructureTextBox.TabIndex = 26;
            this.DictStructureTextBox.WordWrap = false;
            // 
            // DictionaryStructureLabel
            // 
            this.DictionaryStructureLabel.AutoSize = true;
            this.DictionaryStructureLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DictionaryStructureLabel.Location = new System.Drawing.Point(352, 9);
            this.DictionaryStructureLabel.Name = "DictionaryStructureLabel";
            this.DictionaryStructureLabel.Size = new System.Drawing.Size(143, 16);
            this.DictionaryStructureLabel.TabIndex = 27;
            this.DictionaryStructureLabel.Text = "Dictionary Structure";
            // 
            // DictionarySettingsGroupBox
            // 
            this.DictionarySettingsGroupBox.Controls.Add(this.label1);
            this.DictionarySettingsGroupBox.Controls.Add(this.CSVQuoteTextbox);
            this.DictionarySettingsGroupBox.Controls.Add(this.label4);
            this.DictionarySettingsGroupBox.Controls.Add(this.EncodingDropdown);
            this.DictionarySettingsGroupBox.Controls.Add(this.label41);
            this.DictionarySettingsGroupBox.Controls.Add(this.CSVDelimiterTextbox);
            this.DictionarySettingsGroupBox.Controls.Add(this.LoadDictionaryButton);
            this.DictionarySettingsGroupBox.Controls.Add(this.label42);
            this.DictionarySettingsGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DictionarySettingsGroupBox.Location = new System.Drawing.Point(602, 13);
            this.DictionarySettingsGroupBox.Name = "DictionarySettingsGroupBox";
            this.DictionarySettingsGroupBox.Size = new System.Drawing.Size(264, 231);
            this.DictionarySettingsGroupBox.TabIndex = 28;
            this.DictionarySettingsGroupBox.TabStop = false;
            this.DictionarySettingsGroupBox.Text = "Dictionary / File Settings";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(50, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 15);
            this.label1.TabIndex = 26;
            this.label1.Text = "Dictionary File CSV Properties";
            // 
            // OutputCapturedWordsCheckbox
            // 
            this.OutputCapturedWordsCheckbox.AutoSize = true;
            this.OutputCapturedWordsCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputCapturedWordsCheckbox.Location = new System.Drawing.Point(657, 341);
            this.OutputCapturedWordsCheckbox.Name = "OutputCapturedWordsCheckbox";
            this.OutputCapturedWordsCheckbox.Size = new System.Drawing.Size(169, 17);
            this.OutputCapturedWordsCheckbox.TabIndex = 29;
            this.OutputCapturedWordsCheckbox.Text = "Output Captured Text (Debug)";
            this.OutputCapturedWordsCheckbox.UseVisualStyleBackColor = true;
            // 
            // VocabulateMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(884, 411);
            this.Controls.Add(this.OutputCapturedWordsCheckbox);
            this.Controls.Add(this.DictionarySettingsGroupBox);
            this.Controls.Add(this.DictionaryStructureLabel);
            this.Controls.Add(this.RawWCCheckbox);
            this.Controls.Add(this.StopListTextBox);
            this.Controls.Add(this.StopListLabel);
            this.Controls.Add(this.FilenameLabel);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.ScanSubfolderCheckbox);
            this.Controls.Add(this.DictStructureTextBox);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(900, 450);
            this.MinimumSize = new System.Drawing.Size(900, 450);
            this.Name = "VocabulateMainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vocabulate";
            this.DictionarySettingsGroupBox.ResumeLayout(false);
            this.DictionarySettingsGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.ComponentModel.BackgroundWorker BgWorker;
        private System.Windows.Forms.CheckBox ScanSubfolderCheckbox;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowser;
        private System.Windows.Forms.Label FilenameLabel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ComboBox EncodingDropdown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button LoadDictionaryButton;
        private System.Windows.Forms.CheckBox RawWCCheckbox;
        private System.Windows.Forms.Label StopListLabel;
        private System.Windows.Forms.TextBox StopListTextBox;
        private System.Windows.Forms.TextBox CSVQuoteTextbox;
        private System.Windows.Forms.TextBox CSVDelimiterTextbox;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.TextBox DictStructureTextBox;
        private System.Windows.Forms.Label DictionaryStructureLabel;
        private System.Windows.Forms.GroupBox DictionarySettingsGroupBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox OutputCapturedWordsCheckbox;
    }
}

