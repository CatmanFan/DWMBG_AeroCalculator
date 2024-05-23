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
        private const uint WM_DWMCOLORIZATIONCOLORCHANGED = 0x320;
        private const uint WM_DWMCOMPOSITIONCHANGED = 0x31E;
        private const uint WM_THEMECHANGED = 0x31A;

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern IntPtr FindWindowW(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern bool PostMessageW(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        public static void RefreshDWM()
        {
            foreach (var proc in Process.GetProcessesByName("dwmblurglass"))
                proc.Kill();

            IntPtr hWnd = FindWindowW("Dwm", null);

            if (hWnd != IntPtr.Zero)
                foreach (var status in new uint[] { WM_THEMECHANGED, WM_DWMCOMPOSITIONCHANGED, WM_DWMCOLORIZATIONCOLORCHANGED })
                    PostMessageW(hWnd, status, IntPtr.Zero, IntPtr.Zero);
        }
    }
}

