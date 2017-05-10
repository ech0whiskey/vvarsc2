using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Service.Interfaces;
using vvarscNET.Model.Objects.People;
using vvarscNET.Model.Objects.Organizations;
using vvarscNET.Core.QueryModels.Organizations;
using vvarscNET.Core.CommandModels.People;
using vvarscNET.Model.Result;
using vvarscNET.Model.Enums;
using System.Security.Cryptography;
using vvarscNET.Core.CommandModels.Authentication;

namespace vvarscNET.Core.Service.CommandServices
{
    public class PeopleCommandService : IPeopleCommandService
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public PeopleCommandService(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        public Result CreateMember(UserContext context, Member member)
        {
            //Confirm OrganizationID is Valid
            var orgQuery = new GetOrganizationByID_Q
            {
                ID = member.OrganizationID
            };

            var orgResult = _queryDispatcher.Dispatch<GetOrganizationByID_Q, Organization>(context.AccessToken, orgQuery);
            if (orgResult == null)
                throw new ArgumentException("Unable to Locate Organization when Creating Member");

            //Create Member
            var memCmd = new CreateMember_C
            {
                UserName = member.UserName,
                RSIHandle = member.RSIHandle,
                OrganizationID = member.OrganizationID,
                UserType = member.UserType,
                RankID = member.RankID,
                IsActive = true
            };

            var memResult = _commandDispatcher.Dispatch<CreateMember_C>(context, memCmd);

            if (memResult.Status != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Error creating Member in DB");
            }
            else
            {
                int memID = Convert.ToInt32(memResult.ItemIDs.FirstOrDefault());

                //Create Credential for Member
                SHA256Managed hashalgo = new SHA256Managed();
                byte[] hash = hashalgo.ComputeHash(System.Text.Encoding.UTF8.GetBytes(member.UserName.ToLower()));
                string passwordHash = BitConverter.ToString(hash);
                passwordHash = passwordHash.Replace("-", "");

                var credCmd = new CreateCredential_C
                {
                    MemberID = memID,
                    UserName = member.UserName,
                    PasswordHash = passwordHash,
                    OrganizationID = member.OrganizationID
                };

                var credResult = _commandDispatcher.Dispatch<CreateCredential_C>(context, credCmd);

                if (credResult.Status != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception("Error creating Member Credential in DB");
                }
            }

            return memResult;
                
        }
    }
}
