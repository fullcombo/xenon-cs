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
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
            string appdata = Environment.GetEnvironmentVariable("AppData");
            datadir.Text = appdata + "\\Stepmania 5";
            uploadmachinestats.Checked = true;

            XmlDocument config = new XmlDocument();
            config.Load("config.xml");
            XmlNode app = config.SelectSingleNode("//app");
            sm5dir.Text = app.Attributes["sm5dir"].Value;
            if (app.Attributes["datadir"].Value != "") { datadir.Text = app.Attributes["datadir"].Value; }
            localprofile.Text = app.Attributes["localprofile"].Value;

            if (app.Attributes["uploadfails"].Value == "1") { uploadfails.Checked = true;  }
            try
            {
                if (app.Attributes["uploadmachinescores"].Value == "0") { uploadmachinestats.Checked = false; }
            }
            catch (NullReferenceException e) { uploadmachinestats.Checked = true; }
        }

        private void button_sm5dir_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = folderBrowserDialog_smdir.ShowDialog();
            if (dialogResult.ToString() == "OK")
            {
                sm5dir.Text = folderBrowserDialog_smdir.SelectedPath;
            }
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void button_datadir_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = folderBrowserDialog_datadir.ShowDialog();
            if (dialogResult.ToString() == "OK")
            {
                datadir.Text = folderBrowserDialog_datadir.SelectedPath;
            }
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            XmlDocument config = new XmlDocument();
            config.Load("config.xml");
            XmlNode app = config.SelectSingleNode("//app");
            app.Attributes["sm5dir"].Value = sm5dir.Text;
            app.Attributes["datadir"].Value = datadir.Text;
            app.Attributes["localprofile"].Value = localprofile.Text;

            if(uploadfails.Checked == true)
            {
                app.Attributes["uploadfails"].Value = "1";
            }
            else { app.Attributes["uploadfails"].Value = "0";  }

            if (uploadmachinestats.Checked == true)
            {
                app.Attributes["uploadmachinestats"].Value = "1";
            }
            else { app.Attributes["uploadmachinestats"].Value = "0";  }
            config.Save("config.xml");

            this.Close();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
