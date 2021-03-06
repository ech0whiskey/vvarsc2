﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Model.Objects.People;
using vvarscNET.Model.ResponseModels.People;

namespace vvarscNET.Core.Service.Interfaces
{
    public interface IPeopleQueryService
    {
        List<Member> ListMembersForOrganization(string accessToken, int organizationID);

        List<ListRanks_QRM> ListRanks(string accessToken);

        List<PayGrade> ListPayGrades(string accessToken);

        Member GetMemberByID(string accessToken, int memberID);
    }
}
