using vvarscNET.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vvarscNET.Core.CommandModels.Members
{
    public class UpdateMember_C : ICommand
    {
        public string MemberPID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
