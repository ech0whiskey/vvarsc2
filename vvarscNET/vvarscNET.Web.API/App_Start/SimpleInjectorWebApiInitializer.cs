using vvarscNET.Core.Dispatchers;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.Logger;
using log4net;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(vvarscNET.Web.API.App_Start.SimpleInjectorWebApiInitializer), "Initialize")]

namespace vvarscNET.Web.API.App_Start
{
    using Core.Data.QueryHandlers.Authentication;
    using Core.Decorators;
    using Core.Factories;
    using Core.QueryModels.Authentication;
    using Core.Service.Interfaces;
    using Core.Service.Security;
    using Core.Service.QueryServices;
    using Model.ResponseModels.Authentication;
    using SimpleInjector;
    using System;
    using System.Configuration;
    using vvarscNET.Core.Service.CommandServices;

    /// <summary>
    /// Class for SimpleInjector Initialization of Dependant Components (Commands, Queries, etc).
    /// </summary>
    public static class SimpleInjectorWebApiInitializer
    {
        /// <summary>
        /// Initialize Container for SimpleInjector
        /// </summary>
        /// <param name="container"></param>
        public static void InitializeContainer(Container container)
        {
            // force the load of the query dll using ONE specific queryHandler so that getassemblies finds it
            container.Options.AllowOverridingRegistrations = true;

            container.RegisterSingleton<ILog>(LogManager.GetLogger("vvarscnet-Web-Api"));
            container.Register<ILogWriter, LogWriter>();

            // for integration Nunit testing. Otherwise pulls from web.config
            string connString = string.Format(ConfigurationManager.AppSettings["CoreDB_ConnectionString"], Core.Globals.CoreDBName);
            container.Register<SQLConnectionFactory>(() => new SQLConnectionFactory(connString), Lifestyle.Scoped);

            container.Register<IContainer, IocContainer>(Lifestyle.Scoped);

            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(ValidateUserContextPre_CH_Decorator<>));
            container.RegisterDecorator(typeof(IQueryHandler<,>), typeof(ValidateAccessTokenPre_QH_Decorator<,>));

            RegisterQueries(container);

            RegisterCommands(container);

            // Other Services
            container.Register<IAuthenticationService, AuthenticationService>(Lifestyle.Scoped);
        }

        private static void RegisterCommands(Container container)
        {
            // Command
            container.RegisterCollection(typeof(ICommand), new[] { typeof(ICommand).Assembly });
            container.Register(typeof(ICommandHandler<>), AppDomain.CurrentDomain.GetAssemblies());

            // Command Dispatcher
            container.Register(typeof(ICommandDispatcher), typeof(CommandDispatcher), Lifestyle.Scoped);

            // Command Handlers
            //container.Register<ICommandHandler<UpdateMember_C>, UpdateMember_CH>();

            // Command Services
            container.Register<IOrganizationCommandService, OrganizationCommandService>(Lifestyle.Scoped);
            container.Register<IPeopleCommandService, PeopleCommandService>(Lifestyle.Scoped);
            container.Register<IUnitCommandService, UnitCommandService>(Lifestyle.Scoped);
        }

        private static void RegisterQueries(Container container)
        {
            // Query
            container.RegisterCollection(typeof(IQuery<>), new[] { typeof(IQuery<>).Assembly });
            container.Register(typeof(IQueryHandler<,>), AppDomain.CurrentDomain.GetAssemblies());
            container.Register(typeof(IPermissionQueryHandler<,>), AppDomain.CurrentDomain.GetAssemblies());

            // Query Dispatcher
            container.Register(typeof(IQueryDispatcher), typeof(QueryDispatcher), Lifestyle.Scoped);
            container.Register(typeof(IPermissionQueryDispatcher), typeof(PermissionQueryDispatcher), Lifestyle.Scoped);

            // Permission Query Handlers
            container.Register<IPermissionQueryHandler<GetAccessTokenByValue_Q, GetAccessToken_QRM>, GetAccessTokenByValue_QH>();

            // Query Handlers
            //container.Register<IQueryHandler<GetMemberByPID_Q, GetMemberByPID_QRM>, GetMemberByPID_QH>();
            //container.Register<IQueryHandler<ListActiveShells_Q, List<ListShells_QRM>>, ListActiveShells_QH>();
            //container.Register<IQueryHandler<ListFeedModulesForShell_Q, List<ListFeedModulesForShell_QRM>>, ListFeedModulesForShell_QH>();
            //container.Register<IQueryHandler<ListLibraryModulesForShell_Q, List<ListLibraryModulesForShell_QRM>>, ListLibraryModulesForShell_QH>();
            //container.Register<IQueryHandler<ListGroupsByShellAndGroupType_Q, List<ListGroupsByShellAndGroupType_QRM>>, ListGroupsByShellAndGroupType_QH>();
            //container.Register<IQueryHandler<AuthenticateMember_Q, AuthenticateMember_QRM>, AuthenticateMember_QH>();

            // Query Services
            container.Register<IOrganizationQueryService, OrganizationQueryService>(Lifestyle.Scoped);
            container.Register<IPeopleQueryService, PeopleQueryService>(Lifestyle.Scoped);
            container.Register<IUnitQueryService, UnitQueryService>(Lifestyle.Scoped);
        }

        /// <summary>Initialize the container and register it as Web API Dependency Resolver.</summary>
        public static void Initialize()
        { }
    }
}