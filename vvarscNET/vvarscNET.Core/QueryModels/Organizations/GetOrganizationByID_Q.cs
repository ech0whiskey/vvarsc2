﻿using vvarscNET.Core.Interfaces;
using vvarscNET.Model.Objects.Organizations;

namespace vvarscNET.Core.QueryModels.Organizations
{
    public class GetOrganizationByID_Q : IQuery<Organization>
    {
        public int ID;
    }
}
