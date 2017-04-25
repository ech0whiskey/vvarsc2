using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vvarscNET.Model.ResponseModels.Modules
{
    public class ListLibraryModulesForShell_QRM
    {
        public int ShellID;
        public int DocLibraryID;
        public string LibraryPID;
        public string LibraryInstanceID;
        public string DocLibraryTitle;
        public DateTime CreatedOn;
        public DateTime ModifiedOn;
    }
}
