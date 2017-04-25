using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vvarscNET.Model.ResponseModels.Groups
{
    public class ListGroupsByShellAndGroupType_QRM
    {
        public int ShellID;
        public string GroupID;
        public string InstanceID;
        public string GroupPID;
        public string GroupName;
        public string DisplayGroupName;
        public int Status;
        public string GroupType;
        public string Cust_GroupID;
        public DateTime CreatedOn;
    }
}
