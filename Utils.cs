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
                goto DWMBG_Restart;

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

            DWMBG_Restart:
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

            for (int i = 0; i < 4; i++)
            {
                System.Threading.Thread.Sleep(100);
                RefreshDWM();
            }
        }
    }
}

