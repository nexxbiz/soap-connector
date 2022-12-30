using Elsa.Options;
using Microsoft.Extensions.DependencyInjection;
using NexxLogic.Elsa.V2.Activities.SoapConnector.Activities;
using NexxLogic.SoapConnector.Client;

namespace NexxLogic.Elsa.V2.Activities.SoapConnector.Extensions;

public static class ServiceCollectionExtensions
{
    public static ElsaOptionsBuilder AddSoapConnector(this ElsaOptionsBuilder options)
    {
        options.Services.AddSoapConnectorServices();
        options.AddSoapConnectorActivity();
        return options;
    }

    private static ElsaOptionsBuilder AddSoapConnectorActivity(this ElsaOptionsBuilder services) =>
        services.AddActivity<SendSoapRequest>()
            .AddActivity<SoapHandler>();
    
    private static IServiceCollection AddSoapConnectorServices(this IServiceCollection services)
    {
        services
            .AddSingleton<ISoapClient, SoapClient>();
        return services;
    }
}