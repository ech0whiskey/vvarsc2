using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Core.Interfaces;
using vvarscNET.Model.Enums;

namespace vvarscNET.Core.CommandModels.People
{
    public class UpdateMember_C : ICommand
    {
        public int ID;
        public string UserName;
        public string RSIHandle;
        public string UserType;
        public int? RankID;
        public bool IsActive;
    }
}
