using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

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

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern bool CreateProcessW(string lpApplicationName, string lpCommandLine, IntPtr lpProcessAttributes, IntPtr lpThreadAttributes, bool bInheritHandles, uint dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory, [In] ref STARTUPINFO lpStartupInfo, out PROCESS_INFORMATION lpProcessInformation);

        [DllImport("kernel32.dll")]
        private static extern uint GetProcessId(string processName);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessageW(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        public static string GetCurrentDir() => Environment.CurrentDirectory;

        private struct STARTUPINFO
        {
            public int cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public uint dwX;
            public uint dwY;
            public uint dwXSize;
            public uint dwYSize;
            public uint dwXCountChars;
            public uint dwYCountChars;
            public uint dwFillAttribute;
            public uint dwFlags;
            public short wShowWindow;
            public short cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        private struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public uint dwProcessId;
            public uint dwThreadId;
        }

        public static string DWMBGApp { get => System.IO.Path.GetDirectoryName(Properties.Settings.Default.ConfigFile).Replace("\\data", "\\DWMBlurGlass.exe"); }

        public static void RunDWMBG()
        {
            Process.Start(DWMBGApp, "runhost");
        }

        public static void RunMHostProcess()
        {
            STARTUPINFO startupInfo = new STARTUPINFO();
            PROCESS_INFORMATION processInformation = new PROCESS_INFORMATION();

            string param = " runhost";
            if (CreateProcessW(DWMBGApp, param, IntPtr.Zero, IntPtr.Zero, false, 0, IntPtr.Zero, null, ref startupInfo, out processInformation))
            {
                CloseHandle(processInformation.hProcess);
                CloseHandle(processInformation.hThread);
            }
        }

        public static void StopMHostProcess()
        {
            foreach (var proc in Process.GetProcessesByName("dwmblurglass"))
            {
                uint pid = Convert.ToUInt32(proc.Id);

                if (pid != 0)
                {
                    IntPtr hProcess = OpenProcess(0x1F0FFF, false, pid);
                    if (hProcess != IntPtr.Zero)
                    {
                        TerminateProcess(hProcess, 0);
                        CloseHandle(hProcess);
                    }
                }
            }
        }
    }
}

