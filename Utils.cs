using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace DWMBG_AeroCalculator
{
    /// <summary>
    /// Copied and converted from C++ from Maplespe's official source code: https://github.com/Maplespe/DWMBlurGlass/blob/master/DWMBlurGlass/MHostHelper.cpp
    /// </summary>

    public static class Utils
    {
        private const uint WM_DWMCOLORIZATIONCOLORCHANGED = 0x320;
        private const uint WM_DWMCOMPOSITIONCHANGED = 0x31E;
        private const uint WM_THEMECHANGED = 0x31A;

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern IntPtr FindWindowW(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern bool PostMessageW(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(IntPtr hWnd);

        private static string DWMBGApp { get => Path.GetDirectoryName(Properties.Settings.Default.ConfigFile).Replace("\\data", "\\DWMBlurGlass.exe"); }

        public static void OpenDWMBG()
        {
            foreach (Process proc in Process.GetProcessesByName("dwmblurglass"))
            {
                if (proc.MainWindowHandle != IntPtr.Zero)
                {
                    SetForegroundWindow(proc.MainWindowHandle);
                    return;
                }
            }

            var dwmbg = new Process();
            dwmbg.StartInfo.FileName = DWMBGApp;
            dwmbg.Start();
        }

        public static void RestartDWMBG()
        {
            foreach (Process proc in Process.GetProcessesByName("dwmblurglass"))
            {
                proc.Kill();
            }

            Start:
            foreach (string argument in new string[] { "runhost", " runhost" })
            {
                var dwmbg = new Process();
                dwmbg.StartInfo.FileName = DWMBGApp;
                dwmbg.StartInfo.Arguments = argument;
                dwmbg.StartInfo.Verb = "runas";
                dwmbg.Start();
                System.Threading.Thread.Sleep(500);
                dwmbg.Dispose();
            }

            if (Process.GetProcessesByName("dwmblurglass").Length == 0) goto Start;

            RestartWindhawkMods();

            for (int i = 0; i < 4; i++)
            {
                System.Threading.Thread.Sleep(100);
                RefreshDWM();
            }
        }

        private static void RestartWindhawkMods()
        {
            string windhawk = "SOFTWARE\\Windhawk\\Engine\\Mods\\";
            string[] mods = new string[]
            {
                "local@restore-vista-caption-buttons",
                "local@restore-seven-caption-buttons",
                "local@restore-eight-caption-buttons",
                "local@restore-vista-caption-buttons-fork",
                "local@restore-seven-caption-buttons-fork",
                "local@restore-eight-caption-buttons-fork",
                "restore-vista-caption-buttons",
                "restore-seven-caption-buttons",
                "restore-eight-caption-buttons",
                "restore-vista-caption-buttons-fork",
                "restore-seven-caption-buttons-fork",
                "restore-eight-caption-buttons-fork",
            };

            foreach (string mod in mods)
            {
                RegistryKey regKey = null;
                goto Search;

                Found:
                regKey.SetValue("Disabled", 1, RegistryValueKind.DWord);
                System.Threading.Thread.Sleep(400);
                regKey.SetValue("Disabled", 0, RegistryValueKind.DWord);
                goto End;

                Search:
                using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
                {
                    regKey = hklm.OpenSubKey(windhawk + mod, true);
                    if (regKey != null && (int)regKey.GetValue("Disabled") == 0) goto Found;
                }

                using (RegistryKey hkcu = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
                {
                    regKey = hkcu.OpenSubKey(windhawk + mod, true);
                    if (regKey != null && (int)regKey.GetValue("Disabled") == 0) goto Found;
                }
            }

            End:
            foreach (Process proc in Process.GetProcessesByName("VSCodium"))
                proc.Kill();
        }

        public static void RefreshSIB(double afterglow)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\StartIsBack", true))
            {
                if (key != null)  // Must check for null key
                {
                    uint alpha = Convert.ToUInt32(Math.Round(afterglow / 100 * 254d));

                    key.SetValue("StartMenuAlpha", alpha, RegistryValueKind.DWord);
                    key.SetValue("TaskbarAlpha", alpha, RegistryValueKind.DWord);
                }
            }
        }

        public static bool CheckValidity(string path)
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

        public static (double primary, double secondary, double blur) CalculateAeroIntensity(double t)
        {
            var primary = t < 103d ? 5 : t < 188d ? 0.776471d * t - 74.976471d : t < 189d ? 71 : 0.535714d * t - 31.25d;
            var secondary = t < 102d ? 0.526316d * t - 8.684211d : t < 189d ? -0.517241d * t + 97.758621d : 0d;
            var blur = t < 102d ? -0.526316d * t + 103.684211d : t < 188d ? -0.255814d * t + 76.093023d : t < 189d ? 28d : -0.535714d * t + 131.25d;
            return (primary, secondary, blur);
        }

        private static string toConfigData(double input) => (Convert.ToDecimal(input).ToString("0.000") + "000").Replace(',', '.').Replace(' ', '.');

        public static bool ChangeAeroIntensity(double value)
        {
            (double primary, double secondary, double blur) = CalculateAeroIntensity(Convert.ToDouble(value));
            return ChangeAeroIntensity(primary, secondary, blur);
        }

        public static bool ChangeAeroIntensity(double primary, double secondary, double blur)
        {
            // Write to files
            // *************
            string[] newConfig = File.ReadAllLines(Properties.Settings.Default.ConfigFile);
            int modified = 0;

            for (int i = 0; i < newConfig.Length; i++)
            {
                if (newConfig[i].StartsWith("aeroColorBalance")) { newConfig[i] = $"aeroColorBalance={toConfigData(primary / 100)}"; modified++; }
                else if (newConfig[i].StartsWith("aeroAfterglowBalance")) { newConfig[i] = $"aeroAfterglowBalance={toConfigData(secondary / 100)}"; modified++; }
                else if (newConfig[i].StartsWith("aeroBlurBalance")) { newConfig[i] = $"aeroBlurBalance={toConfigData(blur / 100)}"; modified++; }
            }

            if (modified < 3)
            {
                return false;
            }

            else
            {
                using (StreamWriter sw = new StreamWriter(Properties.Settings.Default.ConfigFile))
                    foreach (string item in newConfig)
                        sw.WriteLine(item);
                if (Properties.Settings.Default.SIB) RefreshSIB(secondary);
                return true;
            }
        }

        public static void RefreshDWM()
        {
            foreach (Process proc in Process.GetProcessesByName("dwmblurglass"))
                proc.Kill();

            List<IntPtr> hWnd = new List<IntPtr>() { FindWindowW("Dwm", null) };
            foreach (Process item in Process.GetProcessesByName("dwm"))
            {
                hWnd.Add(item.Handle);
                hWnd.Add(item.MainWindowHandle);
            }
            hWnd.RemoveAll(x => x == IntPtr.Zero);

            for (int i = 0; i < hWnd.Count; i++)
                foreach (uint status in new uint[] { WM_THEMECHANGED, WM_DWMCOMPOSITIONCHANGED, WM_DWMCOLORIZATIONCOLORCHANGED })
                    PostMessageW(hWnd[i], status, IntPtr.Zero, IntPtr.Zero);
        }

        public static void KillDWM()
        {
            foreach (Process proc in Process.GetProcessesByName("dwm"))
                proc.Kill();

            System.Threading.Thread.Sleep(800);

            RestartDWMBG();
        }
    }
}

