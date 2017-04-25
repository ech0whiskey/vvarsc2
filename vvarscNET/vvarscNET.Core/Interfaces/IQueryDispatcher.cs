namespace vvarscNET.Core.Interfaces
{
    public interface IQueryDispatcher
    {
        TResult Dispatch<TQuery, TResult>(string accessTokenId, TQuery query) where TQuery : IQuery<TResult>;
    }
}
