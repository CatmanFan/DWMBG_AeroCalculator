using System;
using System.Windows.Forms;

namespace DWMBG_AeroCalculator
{
    static class Program
    {
        static private bool? isConsole;
        static public bool IsConsole
        {
            get
            {
                if (isConsole == null)
                {
                    isConsole = true;
                    try { var verify = Console.WindowHeight; }
                    catch { isConsole = false; }
                }
                return isConsole.Value;
            }
        }

        static public void Help(bool continuable = false)
        {
            Console.WriteLine("Aero intensity calculator for DWMBlurGlass");
            Console.WriteLine("----------------------------");
            Console.WriteLine("Usage:");
            Console.WriteLine("  dwmbgcalc.exe [-set (value)] [-refreshdwm] [-killdwm] [-restartdwmbg]");
            Console.WriteLine();
            Console.WriteLine("Commands:");
            Console.WriteLine("  -set (value)   Changes the Aero glass intensity with a calculation similar to Windows 7's.");
            Console.WriteLine("                 The value must be a number between 0 and 100.");
            Console.WriteLine();
            Console.WriteLine("  -refreshdwm    After changing Aero settings, refreshes DWM by triggering a 'theme changed' status.");
            Console.WriteLine("  -killdwm       After changing Aero settings, kills DWM.");
            Console.WriteLine();
            Console.WriteLine("  -restartdwmbg  Starts DWMBlurGlass again if DWM has already been restarted (e.g. due to a crash).");
            Console.WriteLine();
            Console.WriteLine("  -gui           Runs the GUI version.");
            Console.WriteLine("  -help          Displays this prompt.");
            Console.WriteLine("----------------------------");
            if (!continuable)
            {
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey(false);
                Environment.Exit(0);
            }
        }

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern bool FreeConsole();

        static public void GUI()
        {
            FreeConsole();
            Properties.Settings.Default.GUI = true;
            Properties.Settings.Default.Save();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                var isGUI = Properties.Settings.Default.GUI;
            }
            catch
            {
                Properties.Settings.Default.GUI = true;
                Properties.Settings.Default.Save();
            }

            if (!IsConsole || (Properties.Settings.Default.GUI && args.Length == 0))
            {
                GUI();
            }
            else
            {
                if (args.Length == 0)
                {
                    Help(true);
                    Console.WriteLine("Type commands here:");
                    args = Console.ReadLine().Split(' ');
                }

                if (args[0].ToLower() == "-help")
                {
                    Console.Clear();
                    Help();
                }
                else if (args[0].ToLower() == "-gui") GUI();
                else
                {
                    bool invalid = true;

                    if (args[0].ToLower() == "-set")
                    {
                        if (!Utils.CheckValidity(Properties.Settings.Default.ConfigFile))
                        {
                            using (var openFileDialog = new OpenFileDialog()
                            {
                                FileName = "",
                                Filter = "INI files|*.ini|All files|*.*",
                                SupportMultiDottedExtensions = true,
                                Title = "Select a valid DWMBlurGlass configuration file...",
                            })
                                if (openFileDialog.ShowDialog() == DialogResult.OK)
                                {
                                    if (!Utils.CheckValidity(openFileDialog.FileName))
                                    {
                                        Console.WriteLine("DWMBlurGlass configuration file was not found. Please use the GUI for this function first.");
                                        Console.WriteLine("Cannot continue.");
                                        Environment.Exit(1);
                                        return;
                                    }
                                }
                        }

                        if (int.TryParse(args[1], out int opacity))
                        {
                            if (Utils.ChangeAeroIntensity(opacity))
                            {
                                Properties.Settings.Default.Opacity = opacity;
                                Properties.Settings.Default.Save();

                                invalid = false;
                            }
                            else
                            {
                                Console.WriteLine("Failed to modify all Aero blur values.");
                                Console.WriteLine("Cannot continue.");
                                Environment.Exit(1);
                                return;
                            }
                        }
                        else goto Invalid;
                    }

                    foreach (var item in args)
                    {
                        switch (item.ToLower())
                        {
                            case "-set":
                                invalid = false;
                                Utils.RestartDWMBG();
                                break;
                            case "-refreshdwm":
                                invalid = false;
                                Utils.RefreshDWM();
                                break;
                            case "-killdwm":
                                invalid = false;
                                Utils.KillDWM();
                                break;
                            case "-restartdwmbg":
                                invalid = false;
                                Utils.RestartDWMBG();
                                break;
                        }
                    }

                    Invalid:
                    if (invalid) Console.WriteLine("Invalid command(s). Please type 'dwmbgcalc.exe -help' for details.");
                    Environment.Exit(invalid ? 1 : 0);
                }
            }
        }
    }
}
