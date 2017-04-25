using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vvarscNET.Core.Interfaces
{
    public interface IUserContext
    {
        string AccessTokenId { get; set; }

        string MemberPID { get; set; }

        int ShellID { get; set; }
    }
}
