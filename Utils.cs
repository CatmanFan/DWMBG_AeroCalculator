using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DWMBG_AeroCalculator
{
    /// <summary>
    /// Copied and converted from C++ from Maplespe's official source code: https://github.com/Maplespe/DWMBlurGlass/blob/master/DWMBlurGlass/MHostHelper.cpp
    /// </summary>

    public static class Utils
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll")]
        private static extern uint GetProcessId(string processName);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessageW(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        public static string GetCurrentDir() => Environment.CurrentDirectory;

        public static string DWMBGApp { get => Path.GetDirectoryName(Properties.Settings.Default.ConfigFile).Replace("\\data", "\\DWMBlurGlass.exe"); }

        public static void RestartDWMBG()
        {
            if (MessageBox.Show("All values written to config.\nRestart DWM?", "Notice", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                KillDWM();
                RunDWMBG();
            }
        }

        public static void RunDWMBG()
        {
            Process.Start(DWMBGApp, "runhost");
        }

        public static void KillDWM()
        {
            Process[] procList = Process.GetProcessesByName("dwmblurglass");

            foreach (Process proc in procList)
            {
                proc.Kill();
            }

            procList = Process.GetProcessesByName("dwm");

            foreach (Process proc in procList)
            {
                proc.Kill();
            }
        }
    }
}

