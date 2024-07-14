using Microsoft.Win32;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace AeroIntensityCalculator
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
            button1.Enabled = menu_write_dwmbg.Enabled = menu_restart_dwmbg.Enabled = Utils.InstalledDWMBG;
            button2.Enabled = menu_write_openglass.Enabled = menu_restart_openglass.Enabled = Utils.InstalledOpenGlass;

            menu_sib_change.Enabled = Registry.CurrentUser.OpenSubKey("SOFTWARE\\StartIsBack", true) != null;
            if (!menu_sib_change.Enabled) { Properties.Settings.Default.SIB = menu_sib_change.Checked = false; Properties.Settings.Default.Save(); }
            else { menu_sib_change.Checked = Properties.Settings.Default.SIB; }

            menu_autorestart_dwm.Checked = Properties.Settings.Default.AutoRestartDWM;

            SetValues();
        }

        private void trackBar1_Scroll(object sender, EventArgs e) => SetValues();

        private void SetValues()
        {
            float t = 26 + (217 * (trackBar1.Value / (float)trackBar1.Maximum));
            (primary, secondary, blur) = Utils.CalculateAeroIntensity(t);

            label2.Text = $"{toString(primary)}\n{toString(secondary)}\n{toString(blur)}";
        }
        private string toString(double input) => Convert.ToDecimal(input).ToString("0.###").Replace(',', '.').Replace(' ', '.');

        private void openDwmBgMenuItem_Click(object sender, EventArgs e) => Utils.OpenDWMBG();
        private void openAppMenuItem_Click(object sender, EventArgs e) { Show(); WindowState = FormWindowState.Normal; Activate(); /* notifyIcon.Visible = false; */ }
        private void notifyIcon_MouseClick(object sender, MouseEventArgs e) { if (e.Button == MouseButtons.Left) { Show(); WindowState = FormWindowState.Normal; Activate(); /* notifyIcon.Visible = false; */ } }
        private void exitAppMenuItem_Click(object sender, EventArgs e) => Application.Exit();

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

        private void menu_save_calculation_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Opacity = trackBar1.Value;
            Properties.Settings.Default.Save();
        }

        private void menu_write_dwmbg_Click(object sender, EventArgs e)
        {
            if (Utils.WriteToDWMBG(primary, secondary, blur))
            {
                Properties.Settings.Default.Opacity = trackBar1.Value;
                Properties.Settings.Default.Save();

                if (Properties.Settings.Default.AutoRestartDWM) Utils.KillDWM();
            }
            else
            {
                MessageBox.Show("Failed to modify all Aero blur values.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void menu_write_openglass_Click(object sender, EventArgs e)
        {
            if (Utils.WriteToOpenGlass(primary, secondary, blur))
            {
                Properties.Settings.Default.Opacity = trackBar1.Value;
                Properties.Settings.Default.Save();

                if (Properties.Settings.Default.AutoRestartDWM) Utils.RefreshDWM();
            }
            else
            {
                MessageBox.Show("Failed to modify all Aero blur values.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void menu_restart_dwmbg_Click(object sender, EventArgs e) => Utils.RestartDWMBG();

        private void menu_restart_openglass_Click(object sender, EventArgs e) => Utils.RestartOpenGlass();

        private void menu_sib_change_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.SIB = menu_sib_change.Checked;
            Properties.Settings.Default.Save();
        }

        private void menu_autorestart_dwm_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.AutoRestartDWM = menu_autorestart_dwm.Checked;
            Properties.Settings.Default.Save();
        }

        private void menu_refresh_dwm_Click(object sender, EventArgs e) => Utils.RefreshDWM();

        private void menu_kill_dwm_Click(object sender, EventArgs e) => Utils.KillDWM();
    }
}
