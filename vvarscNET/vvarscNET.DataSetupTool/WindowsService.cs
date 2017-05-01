using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace vvarscNET.DataSetupTool
{
    [RunInstaller(true)]
    public partial class Installer : System.Configuration.Install.Installer
    {
        ServiceInstaller serviceInstaller;
        ServiceProcessInstaller processInstaller;

        public Installer()
        {
            serviceInstaller = new ServiceInstaller();
            processInstaller = new ServiceProcessInstaller();

            processInstaller.Account = ServiceAccount.LocalSystem;
            serviceInstaller.StartType = ServiceStartMode.Automatic;
            serviceInstaller.ServiceName = Program.Name;
            serviceInstaller.DisplayName = Program.DisplayName;
            serviceInstaller.Description = Program.Description;

            Installers.Add(serviceInstaller);
            Installers.Add(processInstaller);
        }
    }

    public interface WorkerProcessInterface
    {
        string MainService(string[] args);

        string OnStop();

        string OnCommand(List<string> args);

        bool IsAlive();

        void LogInfo(string msg);
    }

    public partial class Service1 : ServiceBase
    {
        public WorkerProcessInterface pgm = null;

        public Service1(WorkerProcessInterface pgm)
        {
            //InitializeComponent();
            this.pgm = pgm;
        }

        protected override void OnStart(string[] args)
        {
            Thread thMain = new Thread(new ParameterizedThreadStart(Service.ServiceThread));
            thMain.Start(new Tuple<Service1, string[]>(this, args));
        }

        protected override void OnStop()
        {
            pgm.OnStop();
        }

        protected override void OnCustomCommand(int command)
        {
            // Control_128 - Control_256
            List<string> args = new List<string> { "Control_" + command };
            pgm.OnCommand(args);
        }
    }

    //===================

    class Service
    {
        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        private enum StdHandle { Stdin = -10, Stdout = -11, Stderr = -12 };

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetStdHandle(StdHandle std);

        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(IntPtr hdl);

        //===================

        public static void ServiceThread(object obj)
        {
            Tuple<Service1, string[]> param = (Tuple<Service1, string[]>)obj;

            try
            {
                string path = System.AppDomain.CurrentDomain.BaseDirectory;
                Directory.SetCurrentDirectory(path);

                param.Item1.pgm.MainService(param.Item2);

                ServiceBase sb = param.Item1 as ServiceBase;
                sb.Stop();
            }
            catch (Exception ex)
            {
                param.Item1.pgm.LogInfo(ex.Message);
            }
        }

        //===================

        static void KeyboardThread(object obj)
        {
            WorkerProcessInterface pgm = (WorkerProcessInterface)obj;

            pgm.LogInfo("KeyboardThread Starting...");

            Thread.Sleep(1000);

            try
            {
                while (pgm.IsAlive())
                {
                    string cmd = Console.ReadLine();
                    if (!pgm.IsAlive())
                    {
                        break;
                    }

                    List<string> args = Service.ParseLine(cmd);
                    if (args != null)
                    {
                        pgm.OnCommand(args);
                    }
                }
            }
            catch (Exception e)
            {
                pgm.LogInfo("KeyboardThread: " + e.Message);
            }

            pgm.LogInfo("KeyboardThread Stopped");
        }

        public static void StartService(string[] args, WorkerProcessInterface pgm)
        {
            try
            {
                if (Environment.UserInteractive)
                {
                    Thread thKeyboard = new Thread(new ParameterizedThreadStart(KeyboardThread));
                    thKeyboard.Start(pgm);

                    pgm.MainService(args);

                    // Force Console.ReadLine() to exit by forcing in an [ENTER] key
                    const int VK_RETURN = 0x0D;
                    const int WM_KEYDOWN = 0x100;
                    var hWnd = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
                    PostMessage(hWnd, WM_KEYDOWN, VK_RETURN, 0);

                    // Force Console.ReadLine() to exit by closing Stdin
                    IntPtr stdin = GetStdHandle(StdHandle.Stdin);
                    CloseHandle(stdin);
                }
                else
                {
                    ServiceBase.Run(new ServiceBase[] { new Service1(pgm) });
                }
            }
            catch (Exception ex)
            {
                pgm.LogInfo(ex.Message);
            }
        }

        public static List<string> ParseLine(string line)
        {
            List<string> ret = null;

            if (!string.IsNullOrEmpty(line))
            {
                string line2 = line.Trim();

                if (!string.IsNullOrEmpty(line2))
                {
                    if (line2.ElementAt(0) != '#')
                    {
                        ret = line2.Split(' ').ToList();
                    }
                }
            }

            return ret;
        }
    }
}
