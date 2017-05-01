using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Core.Interfaces;

namespace vvarscNET.Core.CommandModels.Organizations
{
    public class CreateOrganization_C : ICommand
    {
        public string OrganizationName;
        public string OrganizationSpectrumID;
        public string OrganizationWebsiteURL;
        public bool IsActive;
    }
}
