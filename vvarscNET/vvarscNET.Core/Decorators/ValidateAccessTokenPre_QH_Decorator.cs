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
    public class ValidateAccessTokenPre_QH_Decorator<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        private readonly IQueryHandler<TQuery, TResult> _decoratedQH;
        private readonly IPermissionQueryHandler<GetAccessTokenByPID_Q, GetAccessToken_QRM> _getAccessTokenByPID_QH;

        public ValidateAccessTokenPre_QH_Decorator(
            IQueryHandler<TQuery, TResult> queryHandler,
            IPermissionQueryHandler<GetAccessTokenByPID_Q, GetAccessToken_QRM> getAccessTokenByPID_QH)
        {
            _decoratedQH = queryHandler;
            _getAccessTokenByPID_QH = getAccessTokenByPID_QH;
        }

        public TResult Handle(string accessTokenID, TQuery query)
        {

            if (string.IsNullOrEmpty(accessTokenID))
                throw new ArgumentNullException(nameof(accessTokenID));

            if (query == null)
                throw new ArgumentNullException(nameof(query));

            //Bypass validation if GlobalToken
            if (accessTokenID.Equals(Globals.AuthHandlerToken))
                return _decoratedQH.Handle(accessTokenID, query);

            //Retrieve Token
            var tokenQuery = new GetAccessTokenByPID_Q
            {
                AccessTokenID = accessTokenID
            };
            var accessTokenResult = _getAccessTokenByPID_QH.Handle(tokenQuery);

            if (accessTokenResult == null)
                throw new UnauthorizedAccessException("Unable to retrive AccessToken");

            //Is Token Expired?
            if (accessTokenResult.ExpiryDate <= DateTime.Now)
                throw new UnauthorizedAccessException("AccessToken Expired");

            return _decoratedQH.Handle(accessTokenID, query);
        }
    }
}
