using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using vvarscNET.Test.Helpers.Data;

namespace vvarscNET.DataSetupTool
{
    class Program : WorkerProcessInterface
    {

        public const string Name = "vvarscNETDataSetupTool";
        public const string DisplayName = "vvarSCNET Data Setup Tool";
        public const string Description = "Setup data for vvarSCNET";

        static void Main(string[] args)
        {
            Service.StartService(args, new Program());
        }

        bool bQuit = false;

        public string MainService(string[] args)
        {
            while (!bQuit)
            {
                Thread.Sleep(1000);
            }
            return "OK";
        }

        public string OnCommand(List<string> args)
        {
            LogInfo("Starting...");

            if (args != null && args.Any())
            {
                string coreConnString = ConfigurationManager.AppSettings["CoreDB_ConnectionString"];

                switch (args[0].ToLower())
                {
                    case "createdbs":
                        TestDb.DeployDacpac("scNET_Core", coreConnString, "scNET_Core.dacpac", true);
                        break;
                    case "deletedbs":
                        TestDb.DropTestDB("scNET_Core", coreConnString);
                        break;
                    case "deletecoredb":
                        TestDb.DropTestDB("scNET_Core", coreConnString);
                        break;
                    case "createandinitdbs":
                        TestDb.DeployDacpac("scNET_Core", coreConnString, "scNET_Core.dacpac", true);
                        InitializeCoreTestDb.Initialize(string.Format(coreConnString, "scNET_Core"));
                        break;
                    case "createandinitcoredb":
                        TestDb.DeployDacpac("scNET_Core", coreConnString, "scNET_Core.dacpac", true);
                        InitializeCoreTestDb.Initialize(string.Format(coreConnString, "scNET_Core"));
                        break;
                    case "initdbs":
                    case "control_130":
                        InitializeCoreTestDb.Initialize(string.Format(coreConnString, "scNET_Core"));
                        break;
                    case "deleteandinit":
                    case "control_129":
                        DateTime start = DateTime.UtcNow;
                        TestDb.DropTestDB("scNET_Core", coreConnString);
                        TestDb.DeployDacpac("scNET_Core", coreConnString, "scNET_Core.dacpac", true);
                        LogInfo("Initializing scNET_Core Database Please Wait.....");
                        InitializeCoreTestDb.Initialize(string.Format(coreConnString, "scNET_Core"));
                        LogInfo("DONE completed in: " + (DateTime.UtcNow - start).TotalSeconds + " seconds");
                        break;
                    case "exit":
                    case "control_128":
                        bQuit = true;
                        break;

                    default:
                        LogInfo("Syntax Error");
                        break;
                }

                LogInfo(args[0].ToLower() + " Complete");
            }

            return "OK";
        }

        public string OnStop()
        {
            LogInfo("Stopping...");
            bQuit = true;

            return "OK";
        }

        public bool IsAlive()
        {
            return !bQuit;
        }

        public void LogInfo(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
