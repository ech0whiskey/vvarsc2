using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Core.Interfaces;
using vvarscNET.Model.Enums;
using vvarscNET.Model.Objects.Organizations;

namespace vvarscNET.Core.CommandModels.Organizations
{
    public class DeleteUnit_C : ICommand
    {
        public int UnitID;
    }
}
