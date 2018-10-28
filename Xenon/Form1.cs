using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.Xml;
using System.Web;
using Newtonsoft.Json;

namespace Xenon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            bW_ServerStatus.RunWorkerAsync();
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
                    XmlAttribute configAuthUsername = newconfig.CreateAttribute("username");
                    configAuth.Attributes.Append(configAuthUsername);
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
            catch (Exception)
            {
                return 0;
            }
        }

        public int GetServerStatus(TextBox textBoxUsername, TextBox textBoxPassword) // This is run in a thread to verify the server's around and listening.
        {
            // Check for a config file and make one if not present.

            XmlDocument config = new XmlDocument();
            int hasToken = 0;
            try
            {
                config.Load("config.xml");
                XmlNode auth = config.SelectSingleNode("//auth");
                if (auth.Attributes["access_token"].Value.Length > 0)
                {
                    hasToken = 1;
                }
                //textBoxUsername.Text = auth.Attributes["username"].Value;
                //textBoxPassword.Text = "Why do you need Konami Original songs?";
                //hasToken = 1; // lets make sure the token is good next.
            }
            catch (System.IO.FileNotFoundException e)
            {
                int build = BuildConfigFile();
            }
            catch (XmlException e)
            {
                int build = BuildConfigFile();
            }
            bW_ServerStatus.ReportProgress(50);

            HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(BuildURI("api/ok"));
            httpReq.AllowAutoRedirect = false;
            try
            {
                HttpWebResponse httpRes = (HttpWebResponse)httpReq.GetResponse();
                if ((int)httpRes.StatusCode == 200) // it's in the wrong goddamn block
                {
                    if (hasToken == 1) // we have cached creds.
                    {
                        config.Load("config.xml");
                        XmlNode auth = config.SelectSingleNode("//auth");
                        string access_token = auth.Attributes["access_token"].Value;

                        HttpWebRequest validateTokenRequest = (HttpWebRequest)WebRequest.Create(BuildURI("api/user"));
                        WebHeaderCollection validateTokenRequestHeaders = validateTokenRequest.Headers;
                        validateTokenRequestHeaders.Add("Authorization: Bearer " + access_token);

                        HttpWebResponse validateTokenResponse = (HttpWebResponse)validateTokenRequest.GetResponse();
                        if (validateTokenResponse.ResponseUri.ToString() == BuildURI("login")) // access_token is malformed or expired
                        {
                            return 200;
                        }
                        else if (validateTokenResponse.ResponseUri.ToString() == BuildURI("api/user")) // access_token is valid
                        {
                            return 1;
                        }
                    }
                    else return 200;
                }
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

        public class Token
        {
            public string access_token { get; set; }
            public string refresh_token { get; set; }
        }

        public int Login(TextBox textBoxUsername, TextBox textBoxPassword)
        {
            bW_Login.ReportProgress(33);
            // Begin login logic.
            WebRequest loginrequest = WebRequest.Create(BuildURI("oauth/token"));

            var postData = "username=" + HttpUtility.UrlEncode(textBoxUsername.Text);
            postData += "&password=" + HttpUtility.UrlEncode(textBoxPassword.Text);
            postData += "&grant_type=password";
            postData += "&client_id=2";
            postData += "&client_secret=XEh8Q8JKmOvTNX2g7QXtrRLzwSO1XQuWpge04zRB";
            var data = Encoding.ASCII.GetBytes(postData);
            
            loginrequest.Method = "POST";
            loginrequest.ContentType = "application/x-www-form-urlencoded";
            loginrequest.ContentLength = data.Length;

            using (var stream = loginrequest.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            try
            {
                var loginResponse = (HttpWebResponse)loginrequest.GetResponse();
                var loginResponseString = new StreamReader(loginResponse.GetResponseStream()).ReadToEnd();
                Token json_response = JsonConvert.DeserializeObject<Token>(loginResponseString);
                XmlDocument config = new XmlDocument();
                config.Load("config.xml");
                XmlNode auth = config.SelectSingleNode("//auth");
                auth.Attributes["username"].Value = textBoxUsername.Text;
                auth.Attributes["access_token"].Value = json_response.access_token;
                auth.Attributes["refresh_token"].Value = json_response.refresh_token;
                config.Save("config.xml");
                bW_Login.ReportProgress(66);

                return 200;
            }
            catch (WebException we)
            {
                if (((HttpWebResponse)we.Response).StatusCode.ToString() == "Unauthorized")
                {
                    Console.WriteLine("401");
                    return 401;
                }
                else {
                    Console.WriteLine("500");
                    return 500;
                }
            }
        }

        private string GetCatalogHash()
        {
            WebClient client = new WebClient();
            Uri uri = new Uri(BuildURI("api/catalog/sha"));
            return client.DownloadString(uri);
        }

        private void bW_ServerStatus_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = GetServerStatus(textBoxUsername, textBoxPassword);
        }

        private void bW_ServerStatus_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Value = e.ProgressPercentage;
        }

        private void bW_ServerStatus_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((int)e.Result == 200)
            {
                SetProgressBar(100);
                textBoxUsername.Enabled = true;
                textBoxPassword.Enabled = true;
                buttonLogin.Enabled = true;
                SetStatusText("Connected.");
            }

            else if((int)e.Result == 1) // We've got a valid access token to use.
            {
                FormMainMenu formMainMenu = new FormMainMenu();
                formMainMenu.Show();
                this.Visible = false;
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

        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            SetProgressBar(0);
            if (textBoxUsername.TextLength > 0 && textBoxPassword.TextLength > 0)
            {
                buttonLogin.Enabled = false;
                textBoxUsername.Enabled = false;
                textBoxPassword.Enabled = false;
                SetProgressBar(50);
                SetStatusText("Logging in...");
                bW_Login.RunWorkerAsync();
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

        private void bW_Login_DoWork(object sender, DoWorkEventArgs e)
        {
            
            e.Result = Login(textBoxUsername, textBoxPassword);
        }

        private void bW_Login_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Value = e.ProgressPercentage;
        }

        private void bW_Login_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((int)e.Result == 200)
            {
                FormMainMenu formMainMenu = new FormMainMenu();
                formMainMenu.Show();
                this.Visible = false;
                this.Close();
            }
        }
    }
}
