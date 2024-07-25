using Microsoft.Extensions.DependencyInjection;
using Core;
using CommandHandlers;

namespace Domain;

public static class CommandHandlerInstaller
{
    public static void AddCommandHandlers(this IServiceCollection services)
    {
        services.InstallCommandBus<CompanyCommandHandlers>();
        //services.AddValidatorsFromAssemblyContaining<AdminCommandBase>();

    }

}