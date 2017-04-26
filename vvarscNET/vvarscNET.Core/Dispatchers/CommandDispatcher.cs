using vvarscNET.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Model.Result;

namespace vvarscNET.Core.Dispatchers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IContainer _container;

        public CommandDispatcher(IContainer container)
        {
            _container = container;
        }

        public Result Dispatch<TCommand>(IUserContext context, TCommand command) where TCommand : ICommand
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (string.IsNullOrEmpty(context.AccessToken))
                throw new ArgumentNullException(nameof(context.AccessToken));

            var handler = _container.GetInstance<ICommandHandler<TCommand>>();
            if (handler == null)
            {
                throw new Exception("Unable to resolve command handler.");
            }
            return handler.Handle(context, command);
        }
    }
}
