using vvarscNET.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Core.QueryModels.Members;
using vvarscNET.Core.QueryModels.Authentication;
using vvarscNET.Model.ResponseModels.Members;
using vvarscNET.Model.ResponseModels.Authentication;
using vvarscNET.Core;
using vvarscNET.Model.Result;

namespace vvarscNET.Core.Decorators
{
    public class ValidateUserContextPre_CH_Decorator<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> _decoratedCH;
        private readonly IPermissionQueryHandler<GetMemberByAccessToken_Q, GetMemberByAccessToken_QRM> _getMemberByToken_QH;
        private readonly IPermissionQueryHandler<GetAccessTokenByPID_Q, GetAccessToken_QRM> _getAccessTokenByPID_QH;

        public ValidateUserContextPre_CH_Decorator(
            ICommandHandler<TCommand> commandHandler,
            IPermissionQueryHandler<GetMemberByAccessToken_Q, GetMemberByAccessToken_QRM> getMemberByToken_QH,
            IPermissionQueryHandler<GetAccessTokenByPID_Q, GetAccessToken_QRM> getAccessTokenByPID_QH)
        {
            _decoratedCH = commandHandler;
            _getMemberByToken_QH = getMemberByToken_QH;
            _getAccessTokenByPID_QH = getAccessTokenByPID_QH;
        }

        public Result Handle(IUserContext userContext, TCommand command)
        {
            if (userContext == null)
                throw new ArgumentNullException(nameof(UserContext));

            if (string.IsNullOrEmpty(userContext.AccessTokenId))
                throw new ArgumentNullException(nameof(userContext.AccessTokenId));

            if (command == null)
                throw new ArgumentNullException(nameof(command));

            //Bypass validation if GlobalToken
            if (userContext.AccessTokenId.Equals(Globals.AuthHandlerToken))
                return _decoratedCH.Handle(userContext, command);

            //Retrieve Token
            var tokenQuery = new GetAccessTokenByPID_Q
            {
                AccessTokenID = userContext.AccessTokenId
            };
            var accessTokenResult = _getAccessTokenByPID_QH.Handle(tokenQuery);

            if (accessTokenResult == null)
                return new Result {
                    Status = System.Net.HttpStatusCode.NotFound,
                    StatusDescription = "Could not find AccessToken"
                };

            //Is Token Expired?
            if (accessTokenResult.ExpiryDate <= DateTime.Now)
                return new Result {
                    Status = System.Net.HttpStatusCode.Unauthorized,
                    StatusDescription = "AccessToken Expired"
                };

            //Retrive Member for Token
            var person = _getMemberByToken_QH.Handle(new GetMemberByAccessToken_Q{
                AccessTokenID = accessTokenResult.AccessTokenID.ToString()
            });
            if (person == null)
                return new Result {
                    Status = System.Net.HttpStatusCode.NotFound,
                    StatusDescription = "Could not find person by AccessToken"
                };
            
            userContext.MemberPID = person.MemberPID;
            userContext.ShellID = person.ShellID;

            return _decoratedCH.Handle(userContext, command);
        }
    }
}
