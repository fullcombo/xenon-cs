namespace Xenon
{
    partial class FormSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
            this.label1 = new System.Windows.Forms.Label();
            this.sm5dir = new System.Windows.Forms.TextBox();
            this.button_sm5dir = new System.Windows.Forms.Button();
            this.folderBrowserDialog_smdir = new System.Windows.Forms.FolderBrowserDialog();
            this.datadir = new System.Windows.Forms.TextBox();
            this.label_datadir = new System.Windows.Forms.Label();
            this.button_datadir = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.localprofile = new System.Windows.Forms.TextBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.uploadfails = new System.Windows.Forms.CheckBox();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.folderBrowserDialog_datadir = new System.Windows.Forms.FolderBrowserDialog();
            this.uploadmachinestats = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(274, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Path to SM5 Folder (e.g., C:\\Program Files\\StepMania 5)";
            // 
            // sm5dir
            // 
            this.sm5dir.Location = new System.Drawing.Point(13, 30);
            this.sm5dir.Name = "sm5dir";
            this.sm5dir.Size = new System.Drawing.Size(380, 20);
            this.sm5dir.TabIndex = 1;
            // 
            // button_sm5dir
            // 
            this.button_sm5dir.Location = new System.Drawing.Point(399, 28);
            this.button_sm5dir.Name = "button_sm5dir";
            this.button_sm5dir.Size = new System.Drawing.Size(75, 23);
            this.button_sm5dir.TabIndex = 2;
            this.button_sm5dir.Text = "Browse";
            this.button_sm5dir.UseVisualStyleBackColor = true;
            this.button_sm5dir.Click += new System.EventHandler(this.button_sm5dir_Click);
            // 
            // folderBrowserDialog_smdir
            // 
            this.folderBrowserDialog_smdir.Description = "Path to StepMania 5 Folder:";
            this.folderBrowserDialog_smdir.ShowNewFolderButton = false;
            this.folderBrowserDialog_smdir.HelpRequest += new System.EventHandler(this.folderBrowserDialog1_HelpRequest);
            // 
            // datadir
            // 
            this.datadir.Location = new System.Drawing.Point(13, 69);
            this.datadir.Name = "datadir";
            this.datadir.Size = new System.Drawing.Size(380, 20);
            this.datadir.TabIndex = 3;
            // 
            // label_datadir
            // 
            this.label_datadir.AutoSize = true;
            this.label_datadir.Location = new System.Drawing.Point(10, 53);
            this.label_datadir.Name = "label_datadir";
            this.label_datadir.Size = new System.Drawing.Size(335, 13);
            this.label_datadir.TabIndex = 4;
            this.label_datadir.Text = "Path to Upload Data (Only change this if you know what you\'re doing)";
            // 
            // button_datadir
            // 
            this.button_datadir.Location = new System.Drawing.Point(399, 69);
            this.button_datadir.Name = "button_datadir";
            this.button_datadir.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.button_datadir.Size = new System.Drawing.Size(75, 23);
            this.button_datadir.TabIndex = 5;
            this.button_datadir.Text = "Browse";
            this.button_datadir.UseVisualStyleBackColor = true;
            this.button_datadir.Click += new System.EventHandler(this.button_datadir_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(400, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "LocalProfile ID (e.g., 00000001, Leave blank to upload all scores found on this P" +
    "C.)";
            // 
            // localprofile
            // 
            this.localprofile.Location = new System.Drawing.Point(13, 109);
            this.localprofile.Name = "localprofile";
            this.localprofile.Size = new System.Drawing.Size(380, 20);
            this.localprofile.TabIndex = 7;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(396, 112);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(76, 13);
            this.linkLabel1.TabIndex = 8;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "What\'s my ID?";
            // 
            // uploadfails
            // 
            this.uploadfails.AutoSize = true;
            this.uploadfails.Location = new System.Drawing.Point(13, 135);
            this.uploadfails.Name = "uploadfails";
            this.uploadfails.Size = new System.Drawing.Size(145, 17);
            this.uploadfails.TabIndex = 10;
            this.uploadfails.Text = "Upload Grade F Results?";
            this.uploadfails.UseVisualStyleBackColor = true;
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(246, 165);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 11;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point(165, 165);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 12;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // uploadmachinestats
            // 
            this.uploadmachinestats.AutoSize = true;
            this.uploadmachinestats.Location = new System.Drawing.Point(218, 135);
            this.uploadmachinestats.Name = "uploadmachinestats";
            this.uploadmachinestats.Size = new System.Drawing.Size(254, 17);
            this.uploadmachinestats.TabIndex = 13;
            this.uploadmachinestats.Text = "Upload machine stats? (Calories, total plays etc.)";
            this.uploadmachinestats.UseVisualStyleBackColor = true;
            // 
            // FormSettings
            // 
            this.AcceptButton = this.button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancel;
            this.ClientSize = new System.Drawing.Size(487, 200);
            this.ControlBox = false;
            this.Controls.Add(this.uploadmachinestats);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.uploadfails);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.localprofile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_datadir);
            this.Controls.Add(this.label_datadir);
            this.Controls.Add(this.datadir);
            this.Controls.Add(this.button_sm5dir);
            this.Controls.Add(this.sm5dir);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSettings";
            this.Text = "Xenon Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox sm5dir;
        private System.Windows.Forms.Button button_sm5dir;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog_smdir;
        private System.Windows.Forms.TextBox datadir;
        private System.Windows.Forms.Label label_datadir;
        private System.Windows.Forms.Button button_datadir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox localprofile;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.CheckBox uploadfails;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog_datadir;
        private System.Windows.Forms.CheckBox uploadmachinestats;
    }
}