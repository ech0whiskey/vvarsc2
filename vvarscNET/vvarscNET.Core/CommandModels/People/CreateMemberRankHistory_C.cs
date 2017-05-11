using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Core.Interfaces;

namespace vvarscNET.Core.CommandModels.People
{
    public class CreateMemberRankHistory_C : ICommand
    {
        public int MemberID;
        public int PreviousRankID;
        public int NewRankID;
    }
}
