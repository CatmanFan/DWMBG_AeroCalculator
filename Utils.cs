using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.TaskScheduler;

namespace AeroIntensityCalculator
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

        #region --- Restart functions ---
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
                foreach (Process proc in Process.GetProcessesByName("VSCodium"))
                    proc.Kill();
                return;

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
        }

        public static void RestartDWMBG()
        {
            if (!File.Exists(DWMBGApp))
            {
                ToggleDWMTask(2, false);
                System.Threading.Thread.Sleep(250);
                ToggleDWMTask(2, true);
                System.Threading.Thread.Sleep(250);

                goto End;
            }

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
                System.Threading.Thread.Sleep(400);
                dwmbg.Dispose();
            }

            if (Process.GetProcessesByName("dwmblurglass").Length == 0) goto Start;

            End:
            RestartWindhawkMods();
            
            for (int i = 0; i < 2; i++)
            {
                System.Threading.Thread.Sleep(200);
                RefreshDWM();
            }
        }

        public static void RestartOpenGlass()
        {
            ToggleDWMTask(1, false);
            System.Threading.Thread.Sleep(800);
            ToggleDWMTask(1, true);
            System.Threading.Thread.Sleep(800);
            RefreshDWM();
        }

        public static void RefreshDWM()
        {
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
            ToggleDWMTask(2, false);
            ToggleDWMTask(1, false);
            ToggleDWMTask(0, false);

            foreach (Process proc in Process.GetProcessesByName("dwm"))
                proc.Kill();

            System.Threading.Thread.Sleep(750);

            ToggleDWMTask(0, true);
            if (InstalledOpenGlass) RestartOpenGlass();
            else if (InstalledDWMBG) RestartDWMBG();
        }
        #endregion

        #region --- DWMBlurGlass-related functions ---
        private static string _dwmbgApp = null;
        public static string DWMBGApp
        {
            get
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(_dwmbgApp) || !File.Exists(_dwmbgApp))
                    {
                        using (var tS = new TaskService())
                        using (var tC = tS.RootFolder.GetTasks())
                        {
                            if (tC.Exists(taskName_DWMBG) && tC[taskName_DWMBG].IsActive)
                            {
                                _dwmbgApp = (tC[taskName_DWMBG].Definition.Actions[0] as ExecAction).Path;
                                // if (toggle) tC[taskName].Run();
                                // else tC[taskName].Stop();
                            }
                        }
                    }

                    return _dwmbgApp;
                }
                catch
                {
                    return _dwmbgApp;
                }
            }
        }

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

            if (!File.Exists(DWMBGApp)) return;

            var dwmbg = new Process();
            dwmbg.StartInfo.FileName = DWMBGApp;
            dwmbg.Start();
        }

        public static bool WriteToDWMBG(double value)
        {
            (double primary, double secondary, double blur) = CalculateAeroIntensity(Convert.ToDouble(value));
            return WriteToDWMBG(primary, secondary, blur);
        }

        public static bool WriteToDWMBG(double primary, double secondary, double blur)
        {
            if (!File.Exists(DWMBGApp)) return true;

            // Write to files
            // *************
            string[] newConfig = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(DWMBGApp), "data\\config.ini"));
            int modified = 0;

            for (int i = 0; i < newConfig.Length; i++)
            {
                if (newConfig[i].StartsWith("aeroColorBalance")) { newConfig[i] = $"aeroColorBalance={toDwmbgData(primary / 100)}"; modified++; }
                else if (newConfig[i].StartsWith("aeroAfterglowBalance")) { newConfig[i] = $"aeroAfterglowBalance={toDwmbgData(secondary / 100)}"; modified++; }
                else if (newConfig[i].StartsWith("aeroBlurBalance")) { newConfig[i] = $"aeroBlurBalance={toDwmbgData(blur / 100)}"; modified++; }
            }

            if (modified < 3)
            {
                return false;
            }

            else
            {
                using (StreamWriter sw = new StreamWriter(Path.Combine(Path.GetDirectoryName(DWMBGApp), "data\\config.ini")))
                    foreach (string item in newConfig)
                        sw.WriteLine(item);
                if (Properties.Settings.Default.SIB) RefreshSIB(secondary);
                return true;
            }
        }

        private static string toDwmbgData(double input) => (Convert.ToDecimal(input).ToString("0.000") + "000").Replace(',', '.').Replace(' ', '.');
        #endregion
        public static void RefreshSIB(double value, bool forceColor = false)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\StartIsBack", true))
            {
                if (key != null)  // Must check for null key
                {
                    if (value < 0)
                    {
                        key.DeleteValue("StartMenuAlpha", false);
                        key.DeleteValue("TaskbarAlpha", false);
                        key.DeleteValue("StartMenuColor", false);
                        key.DeleteValue("TaskbarColor", false);
                    }
                    else
                    {
                        uint alpha = Convert.ToUInt32(Math.Round(value / 100 * 255d));

                        if (forceColor)
                        {
                            try
                            {
                                using (var dwm = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\DWM", true))
                                {
                                    var origColor = (dwm.GetValue("AccentColor") as int?)?.ToString("X");
                                    if (origColor?.Length == 8)
                                    {
                                        origColor = "00" + origColor.Substring(2);
                                        var color = Convert.ToUInt32(origColor, 16);
                                        key.SetValue("StartMenuColor", color, RegistryValueKind.DWord);
                                        key.SetValue("TaskbarColor", color, RegistryValueKind.DWord);
                                    }
                                }
                            }
                            catch
                            {

                            }
                        }

                        key.SetValue("StartMenuAlpha", alpha, RegistryValueKind.DWord);
                        key.SetValue("TaskbarAlpha", alpha, RegistryValueKind.DWord);
                    }
                }
            }
        }

        public static (double primary, double secondary, double blur) CalculateAeroIntensity(double t)
        {
            var primary = t < 103d ? 5 : t < 188d ? 0.776471d * t - 74.976471d : t < 189d ? 71 : 0.535714d * t - 31.25d;
            var secondary = t < 102d ? 0.526316d * t - 8.684211d : t < 189d ? -0.517241d * t + 97.758621d : 0d;
            var blur = t < 102d ? -0.526316d * t + 103.684211d : t < 188d ? -0.255814d * t + 76.093023d : t < 189d ? 28d : -0.535714d * t + 131.25d;
            return (primary, secondary, blur);
        }

        public static bool WriteToOpenGlass(double value)
        {
            (double primary, double secondary, double blur) = CalculateAeroIntensity(Convert.ToDouble(value));
            return WriteToOpenGlass(primary, secondary, blur);
        }

        public static bool WriteToOpenGlass(double primary, double secondary, double blur)
        {
            try
            {
                using (var reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\DWM", true))
                {
                    reg.SetValue("ColorizationColorBalanceOverride", Convert.ToUInt32(Math.Round(primary)), RegistryValueKind.DWord);
                    reg.SetValue("ColorizationAfterglowBalanceOverride", Convert.ToUInt32(Math.Round(secondary)), RegistryValueKind.DWord);
                    reg.SetValue("GlassOpacity", Convert.ToUInt32(Math.Round(blur)), RegistryValueKind.DWord);

                    if (Properties.Settings.Default.SIB)
                    {
                        reg.SetValue("GlassOverrideAccent", 1, RegistryValueKind.DWord);
                        RefreshSIB(primary);
                    }

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private const string taskName_AWM = "AWM";
        private const string taskName_OpenGlass = "OpenGlass Host";
        private const string taskName_DWMBG = "DWMBlurGlass_Extend";

        public static bool InstalledAWM
        {
            get
            {
                using (var tS = new TaskService())
                using (var tC = tS.RootFolder.GetTasks())
                {
                    return tC.Exists(taskName_AWM) && tC[taskName_AWM].Enabled;
                }
            }
        }

        public static bool InstalledOpenGlass
        {
            get
            {
                using (var tS = new TaskService())
                using (var tC = tS.RootFolder.GetTasks())
                {
                    return tC.Exists(taskName_OpenGlass) && tC[taskName_OpenGlass].Enabled;
                }
            }
        }

        public static bool InstalledDWMBG
        {
            get
            {
                using (var tS = new TaskService())
                using (var tC = tS.RootFolder.GetTasks())
                {
                    return tC.Exists(taskName_DWMBG) && tC[taskName_DWMBG].Enabled;
                }
            }
        }

        public static void ToggleDWMTask(int task, bool toggle)
        {
            string taskName = task == 1 ? taskName_OpenGlass : task == 2 ? taskName_DWMBG : taskName_AWM;

            using (var tS = new TaskService())
            using (var tC = tS.RootFolder.GetTasks())
            {
                if (tC.Exists(taskName) && tC[taskName].IsActive)
                {
                    if (toggle && tC[taskName].Enabled) tC[taskName].Run();
                    else tC[taskName].Stop();
                }
            }
        }
    }
}

