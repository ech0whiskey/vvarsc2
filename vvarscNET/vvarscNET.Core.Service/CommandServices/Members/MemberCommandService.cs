using vvarscNET.Core.CommandModels.Members;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Service.Interfaces.CommandServices;
using vvarscNET.Model.RequestModels.Members;
using vvarscNET.Model.Result;
using System;

namespace vvarscNET.Core.Service.CommandServices.Members
{
    public class MemberCommandService : IMemberCommandService
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public MemberCommandService(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        public Result UpdateMember(UserContext context, UpdateMemberRequestModel member)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));

            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var memberCommand = new UpdateMember_C
            {
                MemberPID = member.MemberPID,
                FirstName = member.FirstName,
                LastName = member.LastName
            };

            return _commandDispatcher.Dispatch<UpdateMember_C>(context, memberCommand);
        }
    }
}
