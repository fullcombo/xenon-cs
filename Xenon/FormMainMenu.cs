using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Xenon
{
    public partial class FormMainMenu : Form
    {
        public FormMainMenu()
        {
            InitializeComponent();
            bW_GetUsername.RunWorkerAsync();
        }

        private void bW_GetUsername_DoWork(object sender, DoWorkEventArgs e)
        {
            XmlDocument config = new XmlDocument();
            config.Load("config.xml");
            XmlNode auth = config.SelectSingleNode("//auth");
            labelWelcome.Text = "Welcome, " + auth.Attributes["username"].Value + ".";
        }

        private void bW_GetUsername_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void bW_GetUsername_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
