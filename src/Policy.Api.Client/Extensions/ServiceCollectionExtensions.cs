// -------------------------------------------------------------------------------------
//  <copyright file="ServiceCollectionExtensions.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Policy.Api.Client;

using Policy.Api.Client.Implementations;
using Policy.Api.Client.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.Api.Client.Configuration;
using SharedLibrary.Api.Client.Extensions;

/// <summary>
/// Provides extension methods for the <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the policy API client into the DI pipeline.
    /// </summary>
    /// <param name="services">The service collections.</param>
    /// <param name="clientSettingsDictionary">The collection of HTTP client settings.</param>
    /// <returns>An instance of <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddPolicyHttpClient(
        this IServiceCollection services,
        Dictionary<string, HttpClientSettings> clientSettingsDictionary)
    {
        var key = nameof(IPolicyApiClient);

        if (!clientSettingsDictionary.ContainsKey(key))
        {
            throw new ArgumentException($"Could not find the key '{key}' in the dictionary '{nameof(clientSettingsDictionary)}'", key);
        }

        var clientSettings = clientSettingsDictionary[key];

        services.AddHttpClient<IPolicyApiClient, PolicyApiClient>(clientSettings);

        return services;
    }    
}