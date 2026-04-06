using Joaoaalves.Tiny.Abstractions.Interfaces;
using Joaoaalves.Tiny.Core.Clients;
using Joaoaalves.Tiny.Core.Http;
using Joaoaalves.Tiny.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Joaoaalves.Tiny.Core.Extensions;

/// <summary>
/// Extension methods for registering the Tiny API client library with
/// the Microsoft Dependency Injection container.
/// </summary>
public static class TinyServiceCollectionExtensions
{
    /// <summary>
    /// Registers all Tiny services using the provided API token.
    /// </summary>
    /// <param name="services">The service collection to register into.</param>
    /// <param name="apiToken">The Tiny API token obtained from the Tiny panel.</param>
    /// <returns>The same <paramref name="services"/> instance for chaining.</returns>
    public static IServiceCollection AddTiny(this IServiceCollection services, string apiToken)
        => services.AddTiny(apiToken, null);

    /// <summary>
    /// Registers all Tiny services with full control over <see cref="TinyOptions"/>.
    /// </summary>
    /// <param name="services">The service collection to register into.</param>
    /// <param name="apiToken">The Tiny API token obtained from the Tiny panel.</param>
    /// <param name="configure">Optional delegate to further configure <see cref="TinyOptions"/>.</param>
    /// <returns>The same <paramref name="services"/> instance for chaining.</returns>
    public static IServiceCollection AddTiny(
        this IServiceCollection services,
        string apiToken,
        Action<TinyOptions>? configure = null)
    {
        var options = new TinyOptions { Token = apiToken };
        configure?.Invoke(options);
        return RegisterServices(services, options);
    }

    private static IServiceCollection RegisterServices(IServiceCollection services, TinyOptions options)
    {
        services.AddSingleton(options);

        services.AddHttpClient<TinyHttpClient>(http =>
        {
            http.BaseAddress = options.BaseAddress;
        });

        services.AddTransient<TinyProductClient>();
        services.AddTransient<TinyOrderClient>();
        services.AddTransient<TinyStockClient>();

        services.AddTransient<ITinyProductService, TinyProductService>();
        services.AddTransient<ITinyOrderService, TinyOrderService>();
        services.AddTransient<ITinyStockService, TinyStockService>();

        return services;
    }
}
