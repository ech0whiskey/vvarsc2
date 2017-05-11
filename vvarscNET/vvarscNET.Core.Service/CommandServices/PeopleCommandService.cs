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
using vvarscNET.Core.QueryModels.People;
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

            //Create RankHistory for Member
            var rhCmd = new CreateMemberRankHistory_C
            {
                MemberID = memID,
                PreviousRankID = member.RankID,
                NewRankID = member.RankID
            };

            var rhResult = _commandDispatcher.Dispatch<CreateMemberRankHistory_C>(context, rhCmd);

            if (rhResult.Status != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Error creating MemberRankHistory in DB");
            }

            return memResult;
                
        }

        public Result UpdateMember(UserContext context, Member newMember)
        {
            //Get Existing Member
            var memQuery = new GetMemberByID_Q
            {
                MemberID = newMember.ID
            };

            var existingMember = _queryDispatcher.Dispatch<GetMemberByID_Q, Member>(context.AccessToken, memQuery);
            if (existingMember == null)
                throw new Exception("Unable to retrive Member for Update Request");

            var memCmd = new UpdateMember_C
            {
                ID = existingMember.ID,
                UserName = existingMember.UserName, //THIS FIELD WON'T BE UPDATED
                RSIHandle = newMember.RSIHandle,
                UserType = newMember.UserType,
                RankID = newMember.RankID,
                IsActive = newMember.IsActive
            };

            var memResult = _commandDispatcher.Dispatch<UpdateMember_C>(context, memCmd);
            if (memResult.Status != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Error updating Member in DB");
            }

            //If Rank Changed, Insert Row into History
            if (existingMember.RankID != newMember.RankID)
            {
                var rhCmd = new CreateMemberRankHistory_C
                {
                    MemberID = existingMember.ID,
                    PreviousRankID = existingMember.RankID,
                    NewRankID = newMember.RankID
                };

                var rhResult = _commandDispatcher.Dispatch<CreateMemberRankHistory_C>(context, rhCmd);
                if (rhResult.Status != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception("Error creating MemberRankHistory in DB");
                }
            }

            return memResult;
        }
    }
}
