using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Model.Objects.People;
using vvarscNET.Model.Result;

namespace vvarscNET.Core.Service.Interfaces
{
    public interface IPeopleCommandService
    {
        Result CreateMember(UserContext context, Member member);

        Result UpdateMember(UserContext context, Member newMember);
    }
}
