using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vvarscNET.Core.Interfaces
{
    public interface IUserContext
    {
        string AccessToken { get; set; }

        int MemberID { get; set; }

        int OrganizationID { get; set; }
    }
}
