using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Core.Interfaces;
using vvarscNET.Model.Enums;

namespace vvarscNET.Core.CommandModels.People
{
    public class DeleteMember_C : ICommand
    {
        public int ID;
    }
}
