// -------------------------------------------------------------------------------------
//  <copyright file="ServiceCollectionExtensions.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Policy.Api.Extensions;

using Microsoft.Extensions.DependencyInjection;
using Policy.Api.HealthChecks;

/// <summary>
/// Provides extension methods for the <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the API health checks into the pipeline.
    /// </summary>
    /// <param name="services">The service collections.</param>
    /// <returns>An instance of <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddApiHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks().AddCheck<AnotherHealthCheck>("Another");

        return services;
    }     
}