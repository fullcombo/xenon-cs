using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Net;
using System.Xml;
using System.Configuration;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace Xenon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            XmlDocument config = new XmlDocument();
            int hasToken = 0;
            try
            {
                config.Load("config.xml");
                XmlNode auth = config.SelectSingleNode("//auth");
                if (auth.Attributes["access_token"].Value.Length > 0)
                {
                    hasToken = 1;
                    textBoxUsername.Text = auth.Attributes["username"].Value;
                    textBoxPassword.Text = "Why do you need Konami Original songs?";
                }
                labelWelcome.Text = "Welcome, " + auth.Attributes["username"].Value + ".";

            }
            catch (System.IO.FileNotFoundException e)
            {
                int build = BuildConfigFile();
            }
            catch (XmlException e)
            {
                int build = BuildConfigFile();
            }

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
                        HttpWebResponse validateTokenResponse = GetResponse("api/user");
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

        private HttpWebResponse GetResponse(string uri)
        {
            XmlDocument config = new XmlDocument();
            config.Load("config.xml");
            XmlNode auth = config.SelectSingleNode("//auth");
            string access_token = auth.Attributes["access_token"].Value;

            HttpWebRequest validateTokenRequest = (HttpWebRequest)WebRequest.Create(BuildURI(uri));
            WebHeaderCollection validateTokenRequestHeaders = validateTokenRequest.Headers;
            validateTokenRequestHeaders.Add("Authorization: Bearer " + access_token);

            HttpWebResponse validateTokenResponse = (HttpWebResponse)validateTokenRequest.GetResponse();
            return validateTokenResponse;
        }

        private string GetResponseAsString(string uri)
        {
            HttpWebResponse response = GetResponse(uri);
            string manifestResponseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return manifestResponseString;
        }

        public class Token
        {
            public string access_token { get; set; }
            public string refresh_token { get; set; }
        }

        public class ManifestGuid
        {
            public string guid { get; set; }
        }

        public int Login(TextBox textBoxUsername, TextBox textBoxPassword)
        {
            bW_Login.ReportProgress(33);
            // Begin login logic.
            WebRequest loginrequest = WebRequest.Create(BuildURI("oauth/token"));

            var postData = "username=" + System.Uri.EscapeDataString(textBoxUsername.Text);
            postData += "&password=" + System.Uri.EscapeDataString(textBoxPassword.Text);
            postData += "&grant_type=password";
            postData += "&client_id=" + ConfigurationManager.AppSettings.Get("client_id");
            postData += "&client_secret=" + ConfigurationManager.AppSettings.Get("client_secret");
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
                panelForm1.Visible = false;
                SetProgressBar(0);
                SetStatusText("Connected.");
                panel_MainMenu.Visible = true;
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
            if((int)e.Result == 401) // Bad username or password.
            {
                SetProgressBar(0);
                textBoxUsername.Text = "";
                textBoxPassword.Text = "";
                textBoxPassword.Enabled = true;
                textBoxUsername.Enabled = true;
                buttonLogin.Enabled = true;
                SetStatusText("Incorrect username or password.");
            }

            if ((int)e.Result == 200)
            {

                SetProgressBar(0);

                SetStatusText("Connected.");
                panelForm1.Visible = false;

                XmlDocument config = new XmlDocument();
                config.Load("config.xml");
                XmlNode auth = config.SelectSingleNode("//auth");
                labelWelcome.Text = "Welcome, " + auth.Attributes["username"].Value + ".";
                
                panel_MainMenu.Visible = true;
            }
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

        private void settingsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FormSettings formSettings = new FormSettings();
            formSettings.Show();
        }

        private void aboutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            assets.AboutBox1 aboutBox1 = new assets.AboutBox1();
            aboutBox1.Show();
        }

        private void logOutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            XmlDocument config = new XmlDocument();
            config.Load("config.xml");
            XmlNode auth = config.SelectSingleNode("//auth");
            auth.Attributes["username"].Value = "";
            auth.Attributes["access_token"].Value = "";
            auth.Attributes["refresh_token"].Value = "";
            config.Save("config.xml");

            panel_MainMenu.Visible = false;
            panelForm1.Visible = true;
        }

        private void buttonLogin_Click_1(object sender, EventArgs e)
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

        private void buttonSubmitScores_Click(object sender, EventArgs e)
        {
            XmlDocument config = new XmlDocument();
            config.Load("config.xml");
            XmlNode app = config.SelectSingleNode("//app");
            if (app.Attributes["sm5dir"].Value == "" || app.Attributes["datadir"].Value == "")
            {
                FormSettings formSettings = new FormSettings();
                formSettings.Show();
            }

            else // 
            {
                panelSubmitScores.Visible = true;
                panel_MainMenu.Visible = false;
                bW_SubmitScores.RunWorkerAsync();
            }
        }

        public void updateSubmitStatus (string message)
        {
            labelSubmitStatus.Text = message;
        }

        private void bW_SubmitScores_DoWork(object sender, DoWorkEventArgs e)
        {
            bW_SubmitScores.ReportProgress(1); // Downloading score manifest...
            string manifestResponseString = GetResponseAsString("api/manifest");
            List<string> guids = JsonConvert.DeserializeObject<List<string>>(manifestResponseString);
            bW_SubmitScores.ReportProgress(2, guids.Count); // Found n scores.

            bW_SubmitScores.ReportProgress(3); // Downloading simfile catalog...

            WebClient webClient = new WebClient();
            webClient.DownloadFile(BuildURI("catalog.json.zip"), "catalog.json.zip");

            bW_SubmitScores.ReportProgress(4); // Decompressing catalog...

            using (var unzip = new Internals.Unzip("catalog.json.zip"))
            {
                unzip.Extract(@"catalog.json", "catalog.json");
            }

            StreamReader catalogfile = File.OpenText("catalog.json");
            bW_SubmitScores.ReportProgress(6); // Serializing JSON Data...

            JsonSerializer serializer = new JsonSerializer();
            List<Simfile> catalog = (List<Simfile>)serializer.Deserialize(catalogfile, typeof(List<Simfile>));
            File.Delete("catalog.json.zip");

            bW_SubmitScores.ReportProgress(5); // Validating catalog checksum...

            string cataloghash = GetSHA(File.OpenText("catalog.json")).ToUpper();

            if (cataloghash != GetResponseAsString("api/catalog/sha").ToUpper())
            {
                bW_SubmitScores.ReportProgress(18); // Simfile catalog mismatch.
                bW_SubmitScores.CancelAsync();
            }



            bW_SubmitScores.ReportProgress(7, catalog.Count); // Loaded N Simfile hashes.

            //Start iterating scores.
            bW_SubmitScores.ReportProgress(8); // Loading config file...
            // Load App parameters from config file.
            XmlDocument config = new XmlDocument();
            config.Load("config.xml");
            XmlNode app = config.SelectSingleNode("//app");

            // See if there's a LocalProfile to use.

            bW_SubmitScores.ReportProgress(9); // Checking for local profile...

            string PlayerGuid = "";

            if (File.Exists(app.Attributes["datadir"].Value + "/Save/LocalProfiles/" + app.Attributes["localprofile"].Value + "/Stats.xml"))
            {
                XmlDocument profilestats = new XmlDocument();
                profilestats.Load(app.Attributes["datadir"].Value + "/Save/LocalProfiles/" + app.Attributes["localprofile"].Value + "/Stats.xml");
                PlayerGuid = profilestats.SelectSingleNode("//Stats/GeneralData/Guid").InnerText;
                
            }

            // Count scores in Upload folder.
            bW_SubmitScores.ReportProgress(10); // Examining uploads folder...

            string[] uploads = Directory.GetFiles(app.Attributes["datadir"].Value + "/Save/Upload");

            bW_SubmitScores.ReportProgress(11, uploads.Length); // Found N Scores

            //Parse scores.

            var dict = new Dictionary<int, Dictionary<string, string>> { };
            int i = 0;
            foreach (string upload in uploads)
            {
                XmlDocument uploadxml = new XmlDocument();
                uploadxml.Load(upload);

                // If the Guid of this upload is already in the database for this user, skip this file.
                if (guids.Contains(uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/Guid").InnerText)) {
                    bW_SubmitScores.ReportProgress(15, uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/Guid").InnerText);
                    continue;
                }

                // If we're skipping fails and this is a fail, skip this file.
                string HighScore = uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/Grade").InnerText;
                if (HighScore == "Failed" && app.Attributes["uploadfails"].Value == "0")
                {
                    bW_SubmitScores.ReportProgress(12, uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/Guid").InnerText);
                    continue;
                }

                string xPlayerGuid = uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/PlayerGuid").InnerText;
                if (PlayerGuid == "" || PlayerGuid == xPlayerGuid)
                {

                    // Check the hash of the file in the song dir against the catalog.
                    try
                    {
                        string[] stepchart = Directory.GetFiles(app.Attributes["sm5dir"].Value + "\\" + uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/Song").Attributes["Dir"].Value, "*.sm");

                        if (stepchart.Length == 0)
                        { // No .sm, look for a .dwi
                            stepchart = Directory.GetFiles(app.Attributes["sm5dir"].Value + "\\" + uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/Song").Attributes["Dir"].Value, "*.dwi");
                            if (stepchart.Length == 0)
                            { // No .dwi either, can't hash this, skip it.
                                bW_SubmitScores.ReportProgress(13, uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/Guid").InnerText);
                                continue;
                            }
                        }

                        // At this point we should be able to hash whatever stepchart we ended up with in index 0.
                        string simfilesha = GetSHA(File.OpenText(stepchart[0])).ToUpper();

                        string chartID = "";
                        try
                        {
                            Simfile simfile = catalog.Find(x => x.SHA256.Contains(simfilesha));
                            int simfile_id = simfile.ID;
                            string difficulty = uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/Steps").Attributes["Difficulty"].Value.ToUpper();
                            string stepstype = uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/Steps").Attributes["StepsType"].Value.ToUpper();
                            Chart chart = simfile.Catalog.Find(
                                x => (x.Difficulty.ToUpper() == difficulty
                                &&
                                x.StepsType.ToUpper() == stepstype)
                                );
                            chartID = chart.ID.ToString();
                        }
                        // If this errors out it'll be immediate on the assignment of the simfile var because nothing in the Xenon DB has the same hash as this simfile.
                        catch (ArgumentNullException error)
                        {
                            bW_SubmitScores.ReportProgress(14, uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/Guid").InnerText);
                            continue;
                        }
                        catch (NullReferenceException error)
                        {
                            bW_SubmitScores.ReportProgress(14, uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/Guid").InnerText);
                            continue;
                        }
                        // At this point all we have left are files which...
                        // * Are not already in the scores table for this user...
                        // * and have a matching hash in the simfile manifest...
                        // * and have a corresponding chart ID to use.
                        // We can, finally, begin uploading a new score.

                        Dictionary<string, string> score = new Dictionary<string, string>
                    {
                        { "chart_id", chartID },
                        { "W1", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/TapNoteScores/W1").InnerText },
                        { "W2", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/TapNoteScores/W2").InnerText },
                        { "W3", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/TapNoteScores/W3").InnerText },
                        { "W4", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/TapNoteScores/W4").InnerText },
                        { "W5", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/TapNoteScores/W5").InnerText },
                        { "Miss", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/TapNoteScores/Miss").InnerText },
                        { "Held", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/HoldNoteScores/Held").InnerText },
                        { "LetGo", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/HoldNoteScores/LetGo").InnerText },
                        { "x_MachineGuid", uploadxml.SelectSingleNode("//Stats/MachineGuid").InnerText },
                        { "x_Grade", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/Grade").InnerText },
                        { "x_Score", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/Score").InnerText },
                        { "x_PercentDP", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/PercentDP").InnerText },
                        { "x_MaxCombo", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/MaxCombo").InnerText },
                        { "x_StageAward", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/StageAward").InnerText },
                        { "x_PeakComboAward", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/PeakComboAward").InnerText },
                        { "x_Modifiers", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/Modifiers").InnerText },
                        { "x_DateTime", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/DateTime").InnerText },
                        { "x_PlayerGuid", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/PlayerGuid").InnerText },
                        { "x_HitMine", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/TapNoteScores/HitMine").InnerText },
                        { "x_AvoidMine", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/TapNoteScores/AvoidMine").InnerText },
                        { "x_CheckpointMiss", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/TapNoteScores/CheckpointMiss").InnerText },
                        { "x_CheckpointHit", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/TapNoteScores/CheckpointHit").InnerText },
                        { "x_MissedHold", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/HoldNoteScores/MissedHold").InnerText },
                        { "x_Stream", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/RadarValues/Stream").InnerText },
                        { "x_Voltage", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/RadarValues/Voltage").InnerText },
                        { "x_Air", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/RadarValues/Air").InnerText },
                        { "x_Freeze", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/RadarValues/Freeze").InnerText },
                        { "x_Chaos", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/RadarValues/Chaos").InnerText },
                        { "x_Notes", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/RadarValues/Notes").InnerText },
                        { "x_TapsAndHolds", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/RadarValues/TapsAndHolds").InnerText },
                        { "x_Jumps", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/RadarValues/Jumps").InnerText },
                        { "x_Holds", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/RadarValues/Holds").InnerText },
                        { "x_Mines", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/RadarValues/Mines").InnerText },
                        { "x_Hands", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/RadarValues/Hands").InnerText },
                        { "x_Rolls", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/RadarValues/Rolls").InnerText },
                        { "x_Lifts", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/RadarValues/Lifts").InnerText },
                        { "x_Fakes", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/RadarValues/Fakes").InnerText },
                        { "x_Disqualified", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/Disqualified").InnerText },
                        { "x_Pad", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/Pad").InnerText },
                        { "x_StageGuid", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/StageGuid").InnerText },
                        { "x_Guid", uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/Guid").InnerText }
                    };

                        // Add this score to the dictionary we'll serialize and send.
                        dict.Add(i, score);
                        i++;

                        // Report the upload.
                        bW_SubmitScores.ReportProgress(16, uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/Guid").InnerText);

                    }

                    catch (DirectoryNotFoundException error)
                    {
                        bW_SubmitScores.ReportProgress(13, uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/Guid").InnerText);
                        continue;
                    }

                    catch (FileNotFoundException error)
                    {
                        bW_SubmitScores.ReportProgress(13, uploadxml.SelectSingleNode("//Stats/RecentSongScores/HighScoreForASongAndSteps/HighScore/Guid").InnerText);
                        continue;
                    }
                }

            }

            //We've iterated all scores now, let's send the final json dict.
            int scorecount = dict.Count;
            string json = JsonConvert.SerializeObject(dict, Newtonsoft.Json.Formatting.Indented);

            config.Load("config.xml");
            XmlNode auth = config.SelectSingleNode("//auth");

            // Build the web client...
            WebRequest request = WebRequest.Create(BuildURI("api/scores"));
            request.Method = "PUT";
            request.Headers.Add("Authorization: Bearer " + auth.Attributes["access_token"].Value);
            request.ContentType = "application/json";
            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(json);
            Stream stream = request.GetRequestStream();
            // Send the PUT Request.
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();
            var response = (HttpWebResponse)request.GetResponse();
            if ((int)response.StatusCode == 200)
            {
                bW_SubmitScores.ReportProgress(17, scorecount);
            }
        }

        private static string GetSHA(StreamReader file)
        {
            SHA256 sha = SHA256.Create();
            byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(file.ReadToEnd()));
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < bytes.Length; i++)
            {
                sBuilder.Append(bytes[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            string hash = sBuilder.ToString();
            return hash;
        }

        public class Chart
        {
            public int ID { set; get; }
            public string StepsType { set; get; }
            public string Difficulty { set; get; }
            public int Simfile_id { set; get; }
        }

        public class Simfile
        {
            public int ID { set; get; }
            public string SHA256 { set; get; }
            public List<Chart> Catalog { set; get; }
        }

        private void bW_SubmitScores_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string message = "";
            switch (e.ProgressPercentage)
            {
                case 1:
                    message = "Downloading score manifest...";
                    break;
                case 2:
                    message = "Found " + e.UserState.ToString() + " scores.";
                    break;
                case 3:
                    message = "Downloading simfile catalog...";
                    break;
                case 4:
                    message = "Decompressing catalog...";
                    break;
                case 5:
                    message = "Validating catalog checksum...";
                    break;
                case 6:
                    message = "Serializing JSON data...";
                    break;
                case 7:
                    message = "Loaded " + e.UserState.ToString() + " simfile hashes.";
                    break;
                case 8:
                    message = "Loading config file...";
                    break;
                case 9:
                    message = "Checking for local profile...";
                    break;
                case 10:
                    message = "Examining uploads folder...";
                    break;
                case 11:
                    message = "Found " + e.UserState.ToString() + " scores.";
                    break;
                case 12:
                    message = "Skipping failed result " + e.UserState.ToString();
                    break;
                case 13:
                    message = "Skipping " + e.UserState.ToString() + " (no simfile found)";
                    break;
                case 14:
                    message = "Skipping " + e.UserState.ToString() + " (not in Xenon DB)";
                    break;
                case 15:
                    message = "Skipping " + e.UserState.ToString() + " (already uploaded)";
                    break;
                case 16:
                    message = "Uploaded " + e.UserState.ToString() + ".";
                    break;
                case 17:
                    message = e.UserState.ToString() + " scores uploaded successfully.";
                    break;
                case 18:
                    message = "Simfile catalog mismatch.";
                    break;
                default:
                    break;
            }
            updateSubmitStatus(message);
        }

        private void bW_SubmitScores_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            panelSubmitScores.Visible = false;
            labelWelcome.Text = "Scores uploaded successfully.";
            panel_MainMenu.Visible = true;
        }
    }
}
