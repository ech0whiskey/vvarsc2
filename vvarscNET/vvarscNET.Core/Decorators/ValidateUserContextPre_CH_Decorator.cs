using vvarscNET.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Core.QueryModels.People;
using vvarscNET.Core.QueryModels.Authentication;
using vvarscNET.Model.Objects.People;
using vvarscNET.Model.ResponseModels.Authentication;
using vvarscNET.Core;
using vvarscNET.Model.Result;

namespace vvarscNET.Core.Decorators
{
    public class ValidateUserContextPre_CH_Decorator<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> _decoratedCH;
        private readonly IPermissionQueryHandler<GetMemberByAccessToken_Q, Member> _getMemberByToken_QH;
        private readonly IPermissionQueryHandler<GetAccessTokenByValue_Q, GetAccessToken_QRM> _getAccessTokenByValue_QH;

        public ValidateUserContextPre_CH_Decorator(
            ICommandHandler<TCommand> commandHandler,
            IPermissionQueryHandler<GetMemberByAccessToken_Q, Member> getMemberByToken_QH,
            IPermissionQueryHandler<GetAccessTokenByValue_Q, GetAccessToken_QRM> getAccessTokenByValue_QH)
        {
            _decoratedCH = commandHandler;
            _getMemberByToken_QH = getMemberByToken_QH;
            _getAccessTokenByValue_QH = getAccessTokenByValue_QH;
        }

        public Result Handle(IUserContext userContext, TCommand command)
        {
            if (userContext == null)
                throw new ArgumentNullException(nameof(UserContext));

            if (string.IsNullOrEmpty(userContext.AccessToken))
                throw new ArgumentNullException(nameof(userContext.AccessToken));

            if (command == null)
                throw new ArgumentNullException(nameof(command));

            //Bypass validation if GlobalToken
            if (userContext.AccessToken.Equals(Globals.AuthHandlerToken))
                return _decoratedCH.Handle(userContext, command);

            //Retrieve Token
            var tokenQuery = new GetAccessTokenByValue_Q
            {
                AccessToken = userContext.AccessToken
            };
            var accessTokenResult = _getAccessTokenByValue_QH.Handle(tokenQuery);

            if (accessTokenResult == null)
                return new Result {
                    Status = System.Net.HttpStatusCode.NotFound,
                    StatusDescription = "Could not find AccessToken"
                };

            //Is Token Expired?
            if (accessTokenResult.ValidTo <= DateTime.Now)
                return new Result {
                    Status = System.Net.HttpStatusCode.Unauthorized,
                    StatusDescription = "AccessToken Expired"
                };

            //Retrive Member for Token
            var person = _getMemberByToken_QH.Handle(new GetMemberByAccessToken_Q{
                AccessToken = accessTokenResult.AccessToken.ToString()
            });
            if (person == null)
                return new Result {
                    Status = System.Net.HttpStatusCode.NotFound,
                    StatusDescription = "Could not find person by AccessToken"
                };
            
            userContext.MemberID = person.ID;
            userContext.OrganizationID = person.OrganizationID;

            return _decoratedCH.Handle(userContext, command);
        }
    }
}
