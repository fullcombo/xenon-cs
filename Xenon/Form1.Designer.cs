namespace Xenon
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.bW_ServerStatus = new System.ComponentModel.BackgroundWorker();
            this.bW_Login = new System.ComponentModel.BackgroundWorker();
            this.panelForm1 = new System.Windows.Forms.Panel();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelUsername = new System.Windows.Forms.Label();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panel_MainMenu = new System.Windows.Forms.Panel();
            this.labelWelcome = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.buttonSubmitScores = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bW_SubmitScores = new System.ComponentModel.BackgroundWorker();
            this.panelSubmitScores = new System.Windows.Forms.Panel();
            this.labelSubmitStatus = new System.Windows.Forms.Label();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.panelForm1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.panel_MainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.panelSubmitScores.SuspendLayout();
            this.SuspendLayout();
            // 
            // bW_ServerStatus
            // 
            this.bW_ServerStatus.WorkerReportsProgress = true;
            this.bW_ServerStatus.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bW_ServerStatus_DoWork);
            this.bW_ServerStatus.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bW_ServerStatus_ProgressChanged);
            this.bW_ServerStatus.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bW_ServerStatus_RunWorkerCompleted);
            // 
            // bW_Login
            // 
            this.bW_Login.WorkerReportsProgress = true;
            this.bW_Login.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bW_Login_DoWork);
            this.bW_Login.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bW_Login_ProgressChanged);
            this.bW_Login.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bW_Login_RunWorkerCompleted);
            // 
            // panelForm1
            // 
            this.panelForm1.Controls.Add(this.buttonLogin);
            this.panelForm1.Controls.Add(this.labelPassword);
            this.panelForm1.Controls.Add(this.textBoxPassword);
            this.panelForm1.Controls.Add(this.labelUsername);
            this.panelForm1.Controls.Add(this.textBoxUsername);
            this.panelForm1.Controls.Add(this.pictureBox1);
            this.panelForm1.Location = new System.Drawing.Point(0, 25);
            this.panelForm1.Name = "panelForm1";
            this.panelForm1.Size = new System.Drawing.Size(484, 311);
            this.panelForm1.TabIndex = 7;
            // 
            // buttonLogin
            // 
            this.buttonLogin.Enabled = false;
            this.buttonLogin.Location = new System.Drawing.Point(197, 258);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(106, 23);
            this.buttonLogin.TabIndex = 11;
            this.buttonLogin.Text = "Log On";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click_1);
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPassword.ForeColor = System.Drawing.Color.White;
            this.labelPassword.Location = new System.Drawing.Point(8, 165);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(76, 21);
            this.labelPassword.TabIndex = 12;
            this.labelPassword.Text = "Password";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Enabled = false;
            this.textBoxPassword.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPassword.Location = new System.Drawing.Point(12, 189);
            this.textBoxPassword.MaxLength = 2048;
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(460, 29);
            this.textBoxPassword.TabIndex = 9;
            this.textBoxPassword.UseSystemPasswordChar = true;
            this.textBoxPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxPassword_KeyDown);
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUsername.ForeColor = System.Drawing.Color.White;
            this.labelUsername.Location = new System.Drawing.Point(8, 88);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(81, 21);
            this.labelUsername.TabIndex = 10;
            this.labelUsername.Text = "Username";
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Enabled = false;
            this.textBoxUsername.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxUsername.Location = new System.Drawing.Point(12, 112);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(460, 29);
            this.textBoxUsername.TabIndex = 8;
            this.textBoxUsername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxUsername_KeyDown);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Xenon.Properties.Resources.logo;
            this.pictureBox1.ImageLocation = "";
            this.pictureBox1.Location = new System.Drawing.Point(122, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(256, 36);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ActiveLinkColor = System.Drawing.Color.Blue;
            this.toolStripStatusLabel1.AutoSize = false;
            this.toolStripStatusLabel1.BackColor = System.Drawing.Color.Transparent;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(250, 17);
            this.toolStripStatusLabel1.Text = "Establishing connection to server...";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 339);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(484, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 13;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // panel_MainMenu
            // 
            this.panel_MainMenu.Controls.Add(this.labelWelcome);
            this.panel_MainMenu.Controls.Add(this.pictureBox2);
            this.panel_MainMenu.Controls.Add(this.buttonSubmitScores);
            this.panel_MainMenu.Location = new System.Drawing.Point(0, 25);
            this.panel_MainMenu.Name = "panel_MainMenu";
            this.panel_MainMenu.Size = new System.Drawing.Size(484, 311);
            this.panel_MainMenu.TabIndex = 14;
            this.panel_MainMenu.Visible = false;
            // 
            // labelWelcome
            // 
            this.labelWelcome.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWelcome.ForeColor = System.Drawing.Color.White;
            this.labelWelcome.Location = new System.Drawing.Point(0, 138);
            this.labelWelcome.Name = "labelWelcome";
            this.labelWelcome.Size = new System.Drawing.Size(484, 30);
            this.labelWelcome.TabIndex = 13;
            this.labelWelcome.Text = "Welcome.";
            this.labelWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Xenon.Properties.Resources.logo;
            this.pictureBox2.Location = new System.Drawing.Point(122, 52);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(256, 36);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 12;
            this.pictureBox2.TabStop = false;
            // 
            // buttonSubmitScores
            // 
            this.buttonSubmitScores.AutoSize = true;
            this.buttonSubmitScores.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSubmitScores.Location = new System.Drawing.Point(94, 211);
            this.buttonSubmitScores.Name = "buttonSubmitScores";
            this.buttonSubmitScores.Size = new System.Drawing.Size(300, 31);
            this.buttonSubmitScores.TabIndex = 11;
            this.buttonSubmitScores.Text = "Submit Scores";
            this.buttonSubmitScores.UseVisualStyleBackColor = true;
            this.buttonSubmitScores.Click += new System.EventHandler(this.buttonSubmitScores_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.logOutToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.menuStrip1.Size = new System.Drawing.Size(484, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 24);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click_1);
            // 
            // logOutToolStripMenuItem
            // 
            this.logOutToolStripMenuItem.Name = "logOutToolStripMenuItem";
            this.logOutToolStripMenuItem.Size = new System.Drawing.Size(62, 24);
            this.logOutToolStripMenuItem.Text = "Log Out";
            this.logOutToolStripMenuItem.Click += new System.EventHandler(this.logOutToolStripMenuItem_Click_1);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 24);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click_1);
            // 
            // bW_SubmitScores
            // 
            this.bW_SubmitScores.WorkerReportsProgress = true;
            this.bW_SubmitScores.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bW_SubmitScores_DoWork);
            this.bW_SubmitScores.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bW_SubmitScores_ProgressChanged);
            this.bW_SubmitScores.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bW_SubmitScores_RunWorkerCompleted);
            // 
            // panelSubmitScores
            // 
            this.panelSubmitScores.Controls.Add(this.labelSubmitStatus);
            this.panelSubmitScores.Location = new System.Drawing.Point(0, 25);
            this.panelSubmitScores.Name = "panelSubmitScores";
            this.panelSubmitScores.Size = new System.Drawing.Size(484, 311);
            this.panelSubmitScores.TabIndex = 14;
            this.panelSubmitScores.Visible = false;
            // 
            // labelSubmitStatus
            // 
            this.labelSubmitStatus.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSubmitStatus.ForeColor = System.Drawing.Color.White;
            this.labelSubmitStatus.Location = new System.Drawing.Point(0, 156);
            this.labelSubmitStatus.Name = "labelSubmitStatus";
            this.labelSubmitStatus.Size = new System.Drawing.Size(484, 30);
            this.labelSubmitStatus.TabIndex = 14;
            this.labelSubmitStatus.Text = "Entering score submission routine.";
            this.labelSubmitStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(200, 16);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(119)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.Controls.Add(this.panelSubmitScores);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel_MainMenu);
            this.Controls.Add(this.panelForm1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "full.com.bo Xenon";
            this.panelForm1.ResumeLayout(false);
            this.panelForm1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel_MainMenu.ResumeLayout(false);
            this.panel_MainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelSubmitScores.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.ComponentModel.BackgroundWorker bW_ServerStatus;
        private System.ComponentModel.BackgroundWorker bW_Login;
        private System.Windows.Forms.Panel panelForm1;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Panel panel_MainMenu;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label labelWelcome;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button buttonSubmitScores;
        private System.ComponentModel.BackgroundWorker bW_SubmitScores;
        private System.Windows.Forms.Panel panelSubmitScores;
        private System.Windows.Forms.Label labelSubmitStatus;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
    }
}

