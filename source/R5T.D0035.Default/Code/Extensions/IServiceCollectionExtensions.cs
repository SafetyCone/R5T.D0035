using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.D0010;
using R5T.D0036;

using R5T.Dacia;


namespace R5T.D0035.Default
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="LocalRepositoriesWithRemoteUpdatesProvider"/> implementation of <see cref="ILocalRepositoriesWithRemoteUpdatesProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddLocalRepositoriesWithRemoteUpdatesProvider(this IServiceCollection services,
            IServiceAction<IMessageSink> messageSinkAction,
            IServiceAction<ISourceControlOperator> sourceControlOperatorAction)
        {
            services
                .AddSingleton<ILocalRepositoriesWithRemoteUpdatesProvider, LocalRepositoriesWithRemoteUpdatesProvider>()
                .Run(messageSinkAction)
                .Run(sourceControlOperatorAction)
                ;

            return services;
        }

        /// <summary>
        /// Adds the <see cref="LocalRepositoriesWithRemoteUpdatesProvider"/> implementation of <see cref="ILocalRepositoriesWithRemoteUpdatesProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<ILocalRepositoriesWithRemoteUpdatesProvider> AddLocalRepositoriesWithRemoteUpdatesProviderAction(this IServiceCollection services,
            IServiceAction<IMessageSink> messageSinkAction,
            IServiceAction<ISourceControlOperator> sourceControlOperatorAction)
        {
            var serviceAction = ServiceAction.New<ILocalRepositoriesWithRemoteUpdatesProvider>(() => services.AddLocalRepositoriesWithRemoteUpdatesProvider(
                messageSinkAction,
                sourceControlOperatorAction));

            return serviceAction;
        }
    }
}
