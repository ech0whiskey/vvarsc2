using vvarscNET.Core.Interfaces;
using System;

namespace vvarscNET.Core.Dispatchers
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IContainer _container;

        public QueryDispatcher(IContainer container)
        {
            _container = container;
        }

        public TResult Dispatch<TQuery, TResult>(string accessTokenId, TQuery query) where TQuery : IQuery<TResult>
        {
            if (query == null) throw new ArgumentNullException(nameof(query));
            if (string.IsNullOrEmpty(accessTokenId)) throw new ArgumentNullException(nameof(accessTokenId));

            var handler = _container.GetInstance<IQueryHandler<TQuery, TResult>>();
            if (handler == null)
            {
                throw new Exception("Unable to resolve query handler.");
            }
            return handler.Handle(accessTokenId, query);
        }
    }
}
