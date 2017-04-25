namespace vvarscNET.Core.Interfaces
{
    public interface IPermissionQueryDispatcher
    {
        TResult Dispatch<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
    }
}
