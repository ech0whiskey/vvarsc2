using System;
using vvarscNET.Core.Interfaces;

namespace vvarscNET.Core.Dispatchers
{
    public class PermissionQueryDispatcher : IPermissionQueryDispatcher
    {
        private readonly IContainer _container;

        public PermissionQueryDispatcher(IContainer container)
        {
            _container = container;
        }

        public TResult Dispatch<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            var handler = _container.GetInstance<IPermissionQueryHandler<TQuery, TResult>>();
            if (handler == null)
            {
                throw new Exception("Unable to resolve query handler.");
            }
            return handler.Handle(query);
        }
    }
}
