using Microsoft.Win32;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace DWMBG_AeroCalculator
{
    public partial class MainForm : Form
    {
        double primary;
        double secondary;
        double blur;

        bool shownNotice = false;

        public MainForm()
        {
            InitializeComponent();
            CultureInfo.CurrentUICulture = CultureInfo.CurrentCulture = new CultureInfo("en") { NumberFormat = new NumberFormatInfo() { NumberDecimalSeparator = ".", CurrencyDecimalSeparator = "." } };

            trackBar1.Value = Properties.Settings.Default.Opacity;
            RefreshSIB.Enabled = Registry.CurrentUser.OpenSubKey("SOFTWARE\\StartIsBack", true) != null;
            if (!RefreshSIB.Enabled) { Properties.Settings.Default.SIB = RefreshSIB.Checked = false; Properties.Settings.Default.Save(); }
            else { RefreshSIB.Checked = Properties.Settings.Default.SIB; }

            SetValues();

            WarningIcon.Image = new Icon(SystemIcons.Warning, 16, 16).ToBitmap();
            string warning = "By killing DWM, the following will also automatically be done by this program:\n" +
                             "• Restarting DWMBlurGlass.\n" +
                             "• Disabling and re-enabling of affected Windhawk mod(s) which modify window caption" +
                             "\n   buttons (these stop working when DWM is restarted).\n\n" +
                             "Some affected applications may need to be restarted manually.";
            toolTip.SetToolTip(WarningIcon, warning);
            toolTip.SetToolTip(KillDWM, warning);
        }

        private void trackBar1_Scroll(object sender, EventArgs e) => SetValues();

        private void SetValues()
        {
            float t = 26 + (217 * (trackBar1.Value / (float)trackBar1.Maximum));
            primary = t < 103 ? 5 : t < 188 ? 0.776471 * t - 74.976471 : t < 189 ? 71 : 0.535714 * t - 31.25;
            secondary = t < 102 ? 0.526316 * t - 8.684211 : t < 189 ? -0.517241 * t + 97.758621 : 0;
            blur = t < 102 ? -0.526316 * t + 103.684211 : t < 188 ? -0.255814 * t + 76.093023 : t < 189 ? 28 : -0.535714 * t + 131.25;
            SetText();

            WarningIcon.Visible = RestartDWMBG.Enabled = KillDWM.Enabled = RefreshDWM.Enabled = Properties.Settings.Default.Opacity == trackBar1.Value;
        }

        private bool CheckValidity(string path)
        {
            if (!File.Exists(path)) return false;

            bool valid = false;

            foreach (string line in File.ReadAllLines(Path.Combine(path)))
            {
                if (line.Contains("aeroColorBalance") || line.Contains("aeroAfterglowBalance") || line.Contains("aeroBlurBalance"))
                {
                    valid = true;
                    Properties.Settings.Default.ConfigFile = path;
                    Properties.Settings.Default.Save();
                }
            }

            return valid;
        }

        private string ToString(double input) => Convert.ToDecimal(input).ToString("0.###").Replace(',', '.').Replace(' ', '.');
        private string ToConfigData(double input) => (Convert.ToDecimal(input).ToString("0.000") + "000").Replace(',', '.').Replace(' ', '.');

        private void SetText() => label2.Text = $"{ToString(primary)}\n{ToString(secondary)}\n{ToString(blur)}";

        private void WriteToConfig_Click(object sender, EventArgs e)
        {
            if (!CheckValidity(Properties.Settings.Default.ConfigFile))
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (!CheckValidity(openFileDialog.FileName))
                    {
                        MessageBox.Show("Not a valid configuration file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            // Write to files
            // *************
            string[] newConfig = File.ReadAllLines(Properties.Settings.Default.ConfigFile);
            int modified = 0;

            for (int i = 0; i < newConfig.Length; i++)
            {
                if (newConfig[i].StartsWith("aeroColorBalance")) { newConfig[i] = $"aeroColorBalance={ToConfigData(primary / 100)}"; modified++; }
                else if (newConfig[i].StartsWith("aeroAfterglowBalance")) { newConfig[i] = $"aeroAfterglowBalance={ToConfigData(secondary / 100)}"; modified++; }
                else if (newConfig[i].StartsWith("aeroBlurBalance")) { newConfig[i] = $"aeroBlurBalance={ToConfigData(blur / 100)}"; modified++; }
            }

            if (modified < 3)
            {
                MessageBox.Show("Failed to modify all Aero blur values.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else
            {
                using (StreamWriter sw = new StreamWriter(Properties.Settings.Default.ConfigFile))
                    foreach (string item in newConfig)
                        sw.WriteLine(item);
                if (Properties.Settings.Default.SIB) Utils.RefreshSIB(secondary);

                // MessageBox.Show("All values written to config.\nYou may need to uninstall and reinstall DWMBlurGlass from its GUI, or sign off Windows for changes to take effect.");

                Properties.Settings.Default.Opacity = trackBar1.Value;
                Properties.Settings.Default.Save();

                WarningIcon.Visible = RestartDWMBG.Enabled = KillDWM.Enabled = RefreshDWM.Enabled = true;
            }
        }

        private void openDwmBgMenuItem_Click(object sender, EventArgs e) => Utils.OpenDWMBG();
        private void openAppMenuItem_Click(object sender, EventArgs e) { Show(); WindowState = FormWindowState.Normal; Activate(); /* notifyIcon.Visible = false; */ }
        private void notifyIcon_MouseClick(object sender, MouseEventArgs e) { if (e.Button == MouseButtons.Left) { Show(); WindowState = FormWindowState.Normal; Activate(); /* notifyIcon.Visible = false; */ } }
        private void exitAppMenuItem_Click(object sender, EventArgs e) => Application.Exit();

        private void KillDWM_Click(object sender, EventArgs e) { Utils.KillDWM(); }
        private void RefreshDWM_Click(object sender, EventArgs e) { Utils.RefreshDWM(); }
        private void RestartDWMBG_Click(object sender, EventArgs e) { Utils.RestartDWMBG(); }
        private void RefreshSIB_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.SIB = RefreshSIB.Checked;
            Properties.Settings.Default.Save();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
                Hide();

                // notifyIcon.Visible = true;
                if (!shownNotice) { notifyIcon.ShowBalloonTip(5500, "Minimized to taskbar", "You can exit the application by right-clicking this icon and selecting Exit from the menu.", ToolTipIcon.None); shownNotice = true; }
            }
        }
    }
}
