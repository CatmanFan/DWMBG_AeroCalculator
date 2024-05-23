using System;
using System.IO;
using System.Windows.Forms;

namespace DWMBG_AeroCalculator
{
    public partial class MainForm : Form
    {
        double primary;
        double secondary;
        double blur;

        public MainForm()
        {
            InitializeComponent();
            trackBar1.Value = Properties.Settings.Default.Opacity;
            SetValues();
        }

        private void trackBar1_Scroll(object sender, EventArgs e) => SetValues();

        private void SetValues()
        {
            float t = 26 + (217 * (trackBar1.Value / (float)trackBar1.Maximum));
            primary = t < 103 ? 5 : t < 188 ? 0.776471 * t - 74.976471 : t < 189 ? 71 : 0.535714 * t - 31.25;
            secondary = t < 102 ? 0.526316 * t - 8.684211 : t < 189 ? -0.517241 * t + 97.758621 : 0;
            blur = t < 102 ? -0.526316 * t + 103.684211 : t < 188 ? -0.255814 * t + 76.093023 : t < 189 ? 28 : -0.535714 * t + 131.25;
            SetText();
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

        private string ToString(double input) => Convert.ToDecimal(input).ToString("0.###");
        private string ToConfigData(double input) => Convert.ToDecimal(input / 100).ToString("0.000") + "000";

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
                {
                    foreach (string item in newConfig)
                        sw.WriteLine(item);
                }

                // MessageBox.Show("All values written to config.\nYou may need to uninstall and reinstall DWMBlurGlass from its GUI, or sign off Windows for changes to take effect.");

                Properties.Settings.Default.Opacity = trackBar1.Value;
                Properties.Settings.Default.Save();

                Utils.RestartDWMBG();
            }

        }
    }
}
