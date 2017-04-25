using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vvarscNET.Core.Interfaces
{
    public interface IQuery<TResult>
    {
    }

    public interface IQueryHandler<in TQuery, out TResult> where TQuery : IQuery<TResult>
    {
        TResult Handle(string accessTokenID, TQuery query);
    }
}
