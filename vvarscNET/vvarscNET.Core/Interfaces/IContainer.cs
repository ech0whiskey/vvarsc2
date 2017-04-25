using SimpleInjector;

namespace vvarscNET.Core.Interfaces
{
    public interface IContainer
    {
        TService GetInstance<TService>() where TService : class;
    }

    public class IocContainer : IContainer
    {
        private readonly Container _container;

        public IocContainer(Container container)
        {
            _container = container;
        }

        public TService GetInstance<TService>() where TService : class
        {
            return _container.GetInstance<TService>();
        }
    }
}
