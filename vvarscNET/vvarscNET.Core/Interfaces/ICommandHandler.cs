using vvarscNET.Model.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vvarscNET.Core.Interfaces
{
    public interface ICommandHandler<in TCommand>
    {
        Result Handle(IUserContext context, TCommand command);
    }
}
