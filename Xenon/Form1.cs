using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.Xml;

namespace Xenon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            backgroundWorker1.RunWorkerAsync();
        }

        private void SetStatusText(string text)
        {
            toolStripStatusLabel1.Text = text;
        }

        private void SetProgressBar(int value)
        {
            toolStripProgressBar1.Value = value;
        }

        public string BuildURI(string path) // We're doing this for the move to full.com.bo so we only need to change the path in one place.
        {
            string CurrentHost = "http://xenon.laravel/";
            return (CurrentHost + path);
        }

        private int BuildConfigFile()
        {
            XmlDocument newconfig = new XmlDocument();
            XmlNode configRoot = newconfig.CreateElement("xenon");
                XmlElement configAuth = newconfig.CreateElement("auth");
                    XmlAttribute configAuthAccessToken = newconfig.CreateAttribute("access_token");
                    configAuth.Attributes.Append(configAuthAccessToken);
                    XmlAttribute configAuthRefreshToken = newconfig.CreateAttribute("refresh_token");
                    configAuth.Attributes.Append(configAuthRefreshToken);
                XmlElement configApp = newconfig.CreateElement("app");
                    XmlAttribute configAppSM5Dir = newconfig.CreateAttribute("sm5dir");
                    configApp.Attributes.Append(configAppSM5Dir);
                    XmlAttribute configAppDataDir = newconfig.CreateAttribute("datadir");
                    configApp.Attributes.Append(configAppDataDir);
                    XmlAttribute configAppLocalProfile = newconfig.CreateAttribute("localprofile");
                    configApp.Attributes.Append(configAppLocalProfile);
                    XmlAttribute configAppUploadFails = newconfig.CreateAttribute("uploadfails");
                    configApp.Attributes.Append(configAppUploadFails);
                    XmlAttribute configAppUploadMachineStats = newconfig.CreateAttribute("uploadmachinestats");
                    configApp.Attributes.Append(configAppUploadMachineStats);
            configRoot.AppendChild(configAuth);
            configRoot.AppendChild(configApp);
            newconfig.AppendChild(configRoot);
            try
            {
                newconfig.Save("config.xml");
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int GetServerStatus() // This is run in a thread to verify the server's around and listening.
        {
            // Check for a config file and make one if not present.

            XmlDocument config = new XmlDocument();
            try
            {
                config.Load("config.xml");
            }
            catch (System.IO.FileNotFoundException e)
            {
                int build = BuildConfigFile();
            }
            catch (XmlException e)
            {
                int build = BuildConfigFile();
            }
            backgroundWorker1.ReportProgress(50);

            HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(BuildURI("api/ok"));
            httpReq.AllowAutoRedirect = false;
            try
            {
                HttpWebResponse httpRes = (HttpWebResponse)httpReq.GetResponse();
                return (int)httpRes.StatusCode;
            }
            catch (WebException e) // 4xx and 5xx status codes return as an Exception
            {
                HttpWebResponse httpRes = (HttpWebResponse)e.Response;
                if (e.Status.ToString() == "ConnectFailure") // No response at all isn't handled via an HTTP status code.
                { return 0; }
                return (int)httpRes.StatusCode;
            }
        }

        private string GetCatalogHash()
        {
            WebClient client = new WebClient();
            Uri uri = new Uri(BuildURI("api/catalog/sha"));
            return client.DownloadString(uri);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = GetServerStatus();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((int)e.Result == 200)
            {
                SetProgressBar(100);
                textBoxUsername.Enabled = true;
                textBoxPassword.Enabled = true;
                buttonLogin.Enabled = true;
                SetStatusText("Connected.");
            }

            else if((int)e.Result == 0) // Server's down or no network.
            {
                SetProgressBar(0);
                SetStatusText("Server unavailable.");
            }

            else
            {
                SetProgressBar(0);
                SetStatusText("Server returned status " + ((int)e.Result) + ".");
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            SetProgressBar(0);
            if (textBoxUsername.TextLength > 0 && textBoxPassword.TextLength > 0)
            {
                buttonLogin.Enabled = false;
                textBoxUsername.Enabled = false;
                textBoxPassword.Enabled = false;
                SetProgressBar(50);
                SetStatusText("Logging in...");
            }
            else if (textBoxUsername.TextLength == 0)
            {
                SetStatusText("Username required.");
            }
            else { SetStatusText("Password required."); }
        }

        private void textBoxUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                buttonLogin.PerformClick();
            }
        }

        private void textBoxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                buttonLogin.PerformClick();
            }
        }
    }
}
