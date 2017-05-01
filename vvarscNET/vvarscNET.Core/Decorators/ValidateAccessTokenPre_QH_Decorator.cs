using vvarscNET.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Core.QueryModels.People;
using vvarscNET.Core.QueryModels.Authentication;
using vvarscNET.Model.ResponseModels.People;
using vvarscNET.Model.ResponseModels.Authentication;
using vvarscNET.Core;
using vvarscNET.Model.Result;

namespace vvarscNET.Core.Decorators
{
    public class ValidateAccessTokenPre_QH_Decorator<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        private readonly IQueryHandler<TQuery, TResult> _decoratedQH;
        private readonly IPermissionQueryHandler<GetAccessTokenByValue_Q, GetAccessToken_QRM> _getAccessTokenByValue_QH;

        public ValidateAccessTokenPre_QH_Decorator(
            IQueryHandler<TQuery, TResult> queryHandler,
            IPermissionQueryHandler<GetAccessTokenByValue_Q, GetAccessToken_QRM> getAccessTokenByValue_QH)
        {
            _decoratedQH = queryHandler;
            _getAccessTokenByValue_QH = getAccessTokenByValue_QH;
        }

        public TResult Handle(string accessToken, TQuery query)
        {

            if (string.IsNullOrEmpty(accessToken))
                throw new ArgumentNullException(nameof(accessToken));

            if (query == null)
                throw new ArgumentNullException(nameof(query));

            //Bypass validation if GlobalToken
            if (accessToken.Equals(Globals.AuthHandlerToken))
                return _decoratedQH.Handle(accessToken, query);

            //Retrieve Token
            var tokenQuery = new GetAccessTokenByValue_Q
            {
                AccessToken = accessToken
            };
            var accessTokenResult = _getAccessTokenByValue_QH.Handle(tokenQuery);

            if (accessTokenResult == null)
                throw new UnauthorizedAccessException("Unable to retrive AccessToken");

            //Is Token Expired?
            if (accessTokenResult.ValidTo <= DateTime.Now)
                throw new UnauthorizedAccessException("AccessToken Expired");

            return _decoratedQH.Handle(accessToken, query);
        }
    }
}
