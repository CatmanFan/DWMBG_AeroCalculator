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
            (primary, secondary, blur) = Utils.CalculateAeroIntensity(t);
            
            SetText();

            WarningIcon.Visible = RestartDWMBG.Enabled = KillDWM.Enabled = RefreshDWM.Enabled = Utils.DWMBGApp == null ? false : Properties.Settings.Default.Opacity == trackBar1.Value;
        }

        private string ToString(double input) => Convert.ToDecimal(input).ToString("0.###").Replace(',', '.').Replace(' ', '.');

        private void SetText() => label2.Text = $"{ToString(primary)}\n{ToString(secondary)}\n{ToString(blur)}";

        private void WriteToConfig_Click(object sender, EventArgs e)
        {
            if (!Utils.CheckValidity(Properties.Settings.Default.ConfigFile))
            {
                using (var openFileDialog = new OpenFileDialog() { Filter = "INI files|*.ini|All files|*.*", SupportMultiDottedExtensions = true, Title = "Select a valid DWMBlurGlass configuration file..." })
                {
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        if (!Utils.CheckValidity(openFileDialog.FileName))
                        {
                            MessageBox.Show("Not a valid configuration file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                            WarningIcon.Visible = RestartDWMBG.Enabled = KillDWM.Enabled = RefreshDWM.Enabled = Properties.Settings.Default.Opacity == trackBar1.Value;
                    }
                    else return;
                }
            }

            if (Utils.ChangeAeroIntensity(primary, secondary, blur))
            {
                // MessageBox.Show("All values written to config.\nYou may need to uninstall and reinstall DWMBlurGlass from its GUI, or sign off Windows for changes to take effect.");

                Properties.Settings.Default.Opacity = trackBar1.Value;
                Properties.Settings.Default.Save();

                WarningIcon.Visible = RestartDWMBG.Enabled = KillDWM.Enabled = RefreshDWM.Enabled = true;
            }
            else
            {
                MessageBox.Show("Failed to modify all Aero blur values.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void restartAsConsoleApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.GUI = false;
            Properties.Settings.Default.Save();

            using (System.Diagnostics.Process process = new System.Diagnostics.Process()
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo()
                {
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal,
                    FileName = Application.ExecutablePath,
                    WorkingDirectory = Application.StartupPath,
                    Verb = "runas",
                    UseShellExecute = true
                }
            })
                process.Start();
            Application.Exit();
        }
    }
}
