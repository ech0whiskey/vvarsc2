using Microsoft.SqlServer.Dac;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vvarscNET.Test.Helpers.Data
{
    public class TestDb
    {
        public static void DeployDacpac(string dbName, string connString, string dacpacName, bool deployExtras = false)
        {
            Console.WriteLine("Configuring DAC services...");

            connString = string.Format(connString, dbName);

            var dacServices = new DacServices(connString);
            dacServices.Message += DacServices_Message;
            dacServices.ProgressChanged += DacServices_ProgressChanged;

            var parent = Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName);

            var rootDir = Directory.GetDirectoryRoot(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName);

            var filePath = parent.FullName;
            if (!parent.FullName.EndsWith("_output"))
                filePath = string.Format("{0}\\_output", parent.FullName);

            filePath = string.Format("{0}\\{1}", filePath, dacpacName);

            if (!File.Exists(filePath))
                throw new Exception("Could not find dacpac");

            var dacPackage = DacPackage.Load(filePath);

            var dacDeployOpts = new DacDeployOptions();
            dacDeployOpts.BlockOnPossibleDataLoss = false;
            if (!deployExtras)
                dacDeployOpts.ExcludeObjectTypes = new ObjectType[] { ObjectType.StoredProcedures, ObjectType.UserDefinedTableTypes, ObjectType.Views };


            Console.WriteLine("Deploying dacpac...");
            dacServices.Deploy(dacPackage, dbName, true, dacDeployOpts);

        }

        public static void DropTestDB(string dbName, string connString)
        {
            connString = string.Format(connString, "Master");

            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                String sqlCommandText = string.Format("if exists(select * from sys.databases where name='{0}') BEGIN ALTER DATABASE[{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE[{0}] END", dbName);
                SqlCommand sqlCommand = new SqlCommand(sqlCommandText, con);
                sqlCommand.ExecuteNonQuery();
            }
        }

        private static void DacServices_Message(object sender, DacMessageEventArgs e)
        {
            Console.WriteLine("DAC Message: {0}", e.Message);
        }

        private static void DacServices_ProgressChanged(object sender, DacProgressEventArgs e)
        {
            Console.WriteLine("{0}: {1}", e.Status, e.Message);
        }
    }
}
