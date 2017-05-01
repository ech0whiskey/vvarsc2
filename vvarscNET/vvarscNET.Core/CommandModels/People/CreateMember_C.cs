using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Core.Interfaces;
using vvarscNET.Model.Enums;

namespace vvarscNET.Core.CommandModels.People
{
    public class CreateMember_C : ICommand
    {
        public string UserName;
        public string RSIHandle;
        public int OrganizationID;
        public UserTypeEnum UserType;
        public bool IsActive;
    }
}
