using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using vvarscNET.Model.Objects.Organizations;

namespace vvarscNET.Test.Helpers.Data
{
    public static class SetupData
    {
        #region Organizations
        public static List<Organization> _organizations = new List<Organization>()
        {
            new Organization
            {
                OrganizationName = "VVarMachine Navy",
                OrganizationSpectrumID = "VVAR",
                OrganizationWebsiteURL = "http://www.vvarmachine.com",
                IsActive = true
            }
        };

        #endregion

    }
}
