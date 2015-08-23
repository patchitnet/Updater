using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using PatchIT.Core;

/* Steps:
 * Check if update exists
 * If not, go to last step
 * If yes, download difference package if exists
 * Create backup of files going to be affected so we can rollback
 * 
 * Cleanup patch-data, backup-files
 * Start the main executable and close us
 */

namespace PatchIT.Updater
{
    public partial class Patcher : Form
    {
        /* PatchIT */
        PatchingSystem PatchSys = null;

        /* Saved from config */
        private String ApiKey = "";
        private String ApiTitle = "";
        private String ApiChannel = "";
        private String ApiExecPath = "";
        private String ApiArgs = "";
        private String ApiExecVersion = "";
        private String ApiRelativePos = "";

        private String PatchITRegion = "";
        private String PatchITDataCenter = "";
        private Boolean UpdateInProgress;
        private Boolean FullUpdate = false;
        private String BasePath = "";

        /* Reflection, we cheat */
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        /* Constructor */
        public Patcher()
        {
            /* Ui Init */
            InitializeComponent();

            /* Setup Colors */
            this.BackColor = Color.FromArgb(236, 240, 241); //52, 152, 219

            /* Setup Titlebar */
            this.TitleBar.BackColor = Color.FromArgb(44, 62, 80); //40, 40, 40

            /* Reset Controls */
            ProgressBarStep.Value = 0;
            ProgressBarOverall.Value = 0;
            LabelStep.Text = "Starting Up...";
            LabelOverall.Text = "Initializing";
            TbLog.Text = "Fetching Changelog...";
            UpdateInProgress = false;
        }

        #region Tools

        /* Load config file */
        private void LoadConfigurationFile()
        {
            try
            {
                /* Load file */
                StreamReader Reader = new StreamReader("Updater.conf");
                String Line = "";

                /* Read it in */
                while ((Line = Reader.ReadLine()) != null)
                {
                    /* Comment ? */
                    if (Line.StartsWith("#") || String.IsNullOrEmpty(Line.Trim()))
                        continue;

                    /* Ok not funny, no comment */
                    String[] Tokens = Line.Split(new Char[] { '=' });

                    /* Check */
                    if (Tokens[0].Trim() == "ProductKey")
                        ApiKey = Tokens[1].Trim().ToLower();
                    if (Tokens[0].Trim() == "ProductChannel")
                        ApiChannel = Tokens[1].Trim();
                    if (Tokens[0].Trim() == "ProductName")
                        ApiTitle = Tokens[1].Trim();
                    if (Tokens[0].Trim() == "ExePath")
                        ApiExecPath = Tokens[1].Trim();
                    if (Tokens[0].Trim() == "ExeArguments" && Tokens.Length > 1)
                        ApiArgs = Tokens[1].Trim();
                    if (Tokens[0].Trim() == "RootDirectoryPath" && Tokens.Length > 1)
                        ApiRelativePos = Tokens[1].Trim();
                }

                /* Cleanup */
                Reader.Close();

                /* Validate */
                if (ApiKey == "")
                {
                    /* Inform & Quit */
                    MessageBox.Show("Product Key was missing from configuration file, inform the software developer.\n The updater cannot continue.");
                    this.Close();
                }
                if (ApiChannel == "")
                {
                    /* Inform & Quit */
                    MessageBox.Show("Product Update Channel was missing from configuration file, inform the software developer.\n The updater cannot continue.");
                    this.Close();
                }
                if (ApiTitle != "")
                    LabelTitle.Text = ApiTitle;
                if (ApiExecPath == "")
                    MessageBox.Show("Executable Path was missing from configuration file, inform the software developer.\n When updating is complete you must start the main program manually.");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString());
            }
        }

        #endregion

        /* Do validation and setup */
        private async void Patcher_Shown(object sender, EventArgs e)
        {
            /* Hide */
            this.Hide();

            /* Validate the config file */
            LoadConfigurationFile();

            /* Try to load executable */
            if (File.Exists(ApiExecPath))
            {
                ApiExecVersion = FileVersionInfo.GetVersionInfo(Path.GetFullPath(ApiExecPath)).FileVersion;
                String[] Length = ApiExecVersion.Split(new Char[] { '.' });
                String[] NewLen = VersionTools.Extend(Length);
                ApiExecVersion = NewLen[0] + "." + NewLen[1] + "." + NewLen[2] + "." + NewLen[3];
            }
            else
                this.Close();

            /* Calculate Base Path */
            BasePath = Path.GetFullPath(Application.StartupPath + "\\" + ApiRelativePos);

            try
            {
                /* Setup */
                PatchSys = new PatchingSystem(ApiKey, int.Parse(ApiChannel),
                    BasePath, ApiExecVersion, null, (err, msg) => 
                    { 
                        /* Depending on error type */

                        MessageBox.Show(msg); 
                    });

                /* Check for updates */
                UpdateDescriptor UpdInfo = await PatchSys.CheckForUpdates();

                /* Sanity */
                if (UpdInfo == null)
                    this.Close();

                /* Yay there is an update! */
                this.Show();
                this.Refresh();

                /* Do Events */
                Application.DoEvents();

                /* Retrieve changelog and set it once it downloads */
                TbLog.ResetText();
                TbLog.AppendText("Retrieving Changelog...");
                await PatchSys.GetFullChangelog().ContinueWith((t) =>
                {
                    /* Get log */
                    String Changelog = t.Result;

                    /* Ok, lets build! */
                    StaticCall.SetText(this, TbLog, Changelog);

                    /* Scroll to top */
                    TbLog.SelectionStart = 0;
                    TbLog.ScrollToCaret();

                }).ConfigureAwait(false);

                /* Clear step bar */
                StaticCall.SetValue(this, ProgressBarStep, 0);
                StaticCall.SetText(this, LabelStep, "");

                /* Set total bar */
                StaticCall.SetValue(this, ProgressBarOverall, 50);
                StaticCall.SetText(this, LabelOverall, "Downloading Update...");

                /* Initiate */
                PatchSys.DownloadUpdate(null, DownloadProgress, DownloadComplete);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString());
                this.Close();
            }
        }

        #region Download Step

        /* Occurs on download progress */
        private void DownloadProgress(Int64 BytesDownloaded, Int64 BytesTotal, Double SpeedInBytes)
        {
            /* Calculate */
            long KbSent = BytesDownloaded / 1024;
            long KbTotal = BytesTotal / 1024;
            double KbSpeed = SpeedInBytes / 1024;
            int Percentage = (int)(((double)KbSent / KbTotal) * 100);

            /* Update Bar */
            StaticCall.SetValue(this, ProgressBarStep, Percentage);

            /* Update */
            StaticCall.SetText(this, LabelSpeed, KbSent.ToString() + " / " + KbTotal.ToString()
                            + " Kb (Speed: " + KbSpeed.ToString("0.##") + " Kb/s)");

            if (Percentage != 0)
                StaticCall.SetValue(this, ProgressBarOverall, Percentage / 2);
        }

        /* Occurs on download complete */
        async void DownloadComplete()
        {
            /* Clear step bar */
            StaticCall.SetValue(this, ProgressBarStep, 0);
            StaticCall.SetText(this, LabelStep, "");

            /* Set total bar */
            StaticCall.SetValue(this, ProgressBarOverall, 50);
            StaticCall.SetText(this, LabelOverall, "Applying Patch");

            /* Setup applier */
            await PatchSys.ApplyPatch(PatchingFileStartEvent, PatchingFileProgressEvent,
                PatchingFileEndEvent, PatchingFinishedEvent).ConfigureAwait(false);
        }

        #endregion

        #region Patch Apply Step

        /* Apply done, cleanup time! */
        private void PatchingFinishedEvent()
        {
            /* Update */
            StaticCall.SetText(this, LabelStep, "");
            StaticCall.SetValue(this, ProgressBarStep, 100);

            /* Done */
            StaticCall.SetText(this, LabelOverall, "Patching is complete");
            StaticCall.SetValue(this, ProgressBarOverall, 100);

            /* Sleep */
            Thread.Sleep(1 * 1000);

            /* Close */
            this.Close();
        }

        /* File has been patched */
        private void PatchingFileEndEvent(String pFile, Int64 FilesPatched, Int64 FilesTotal)
        {
            /* Update */
            StaticCall.SetText(this, LabelStep, "Patching of " + pFile + " is done!");
            StaticCall.SetValue(this, ProgressBarStep, 100);

            /* Update main */
            int Percentage = ((int)(((double)FilesPatched / FilesTotal) * 100) / 2);
            StaticCall.SetValue(this, ProgressBarOverall, 50 + Percentage);
        }

        /* File patch progress */
        private void PatchingFileProgressEvent(Int64 BytesPatched, Int64 TotalBytesToPatch)
        {
            /* Update */
            int Percentage = (int)(((double)BytesPatched / TotalBytesToPatch) * 100);
            StaticCall.SetValue(this, ProgressBarStep, Percentage);
        }

        /* New file patching */
        private void PatchingFileStartEvent(String pFile)
        {
            /* Update */
            StaticCall.SetValue(this, ProgressBarStep, 0);
            StaticCall.SetText(this, LabelStep, "Patching " + pFile);
        }

        #endregion

        private void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            /* Fake drag event */
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void ClosePicture_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /* We might need to cancel or do stuff */
        private void Patcher_FormClosing(object sender, FormClosingEventArgs e)
        {
            /* Cancel if update in progress */
            if (UpdateInProgress)
            {
                e.Cancel = true;
                return;
            }

            /* Start main executable */
            if (ApiExecPath != "" && File.Exists(ApiExecPath))
            {
                Process ProcStart = new Process();
                ProcStart.StartInfo = new ProcessStartInfo();
                ProcStart.StartInfo.FileName = Path.GetFullPath(ApiExecPath);
                ProcStart.StartInfo.Arguments = ApiArgs;
                ProcStart.Start();
            }
        }
    }

    /* Class to help update stuff on UI Thread */
    public static class StaticCall
    {
        /* Update Text */
        delegate void SetTextCallback(Form pForm, Control pCtrl, String pText);
        delegate void SetProgressValue(Form pForm, ProgressBar pCtrl, int pValue);

        /// <summary>
        /// Set text property of various controls
        /// </summary>
        /// <param name="pForm">The calling form</param>
        /// <param name="pCtrl"></param>
        /// <param name="pText"></param>
        public static void SetText(Form pForm, Control pCtrl, String pText)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (pCtrl.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                pForm.Invoke(d, new object[] { pForm, pCtrl, pText });
            }
            else
            {
                pCtrl.Text = pText;
            }
        }

        /// <summary>
        /// Sets value of a progress-bar
        /// </summary>
        /// <param name="pForm">The calling form</param>
        /// <param name="pBar"></param>
        /// <param name="pText"></param>
        public static void SetValue(Form pForm, ProgressBar pBar, int pValue)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (pBar.InvokeRequired)
            {
                SetProgressValue d = new SetProgressValue(SetValue);
                pForm.Invoke(d, new object[] { pForm, pBar, pValue });
            }
            else
            {
                /* Sanity */
                int fVal = pValue;

                if (fVal > 100)
                    fVal = 100;
                if (fVal < 0)
                    fVal = 0;

                pBar.Value = pValue;
            }
        }
    }

    /* Custom sort class for a list of product versions */
    public class VersionTools
    {
        /* Auto Extend */
        public static String[] Extend(String[] Original)
        {
            String[] NewArray = new String[4];

            if (Original.Length == 1)
            {
                NewArray[0] = Original[0];
                NewArray[1] = "0";
                NewArray[2] = "0";
                NewArray[3] = "0";
            }
            else if (Original.Length == 2)
            {
                NewArray[0] = Original[0];
                NewArray[1] = Original[1];
                NewArray[2] = "0";
                NewArray[3] = "0";
            }
            else if (Original.Length == 3)
            {
                NewArray[0] = Original[0];
                NewArray[1] = Original[1];
                NewArray[2] = Original[2];
                NewArray[3] = "0";
            }
            else if (Original.Length == 4)
            {
                NewArray[0] = Original[0];
                NewArray[1] = Original[1];
                NewArray[2] = Original[2];
                NewArray[3] = Original[3];
            }

            return NewArray;
        }
    }
}
