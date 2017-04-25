using vvarscNET.Model.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vvarscNET.Core.Interfaces
{
    public interface ICommandDispatcher
    {
        Result Dispatch<TCommand>(IUserContext context, TCommand command) where TCommand : ICommand;
    }
}
