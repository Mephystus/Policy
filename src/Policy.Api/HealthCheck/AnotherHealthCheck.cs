// -------------------------------------------------------------------------------------
//  <copyright file="AnotherHealthCheck.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Policy.Api.HealthChecks;

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

/// <summary>
/// Defines the health check implementation for the dependency: 'Another'
/// </summary>
public class AnotherHealthCheck : IHealthCheck
{
    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<AnotherHealthCheck> _logger;

    /// <summary>
    /// Initialises a new instance of the <see cref="AnotherHealthCheck" /> class.
    /// </summary>
    /// <param name="logger">An instance of <see cref="ILogger{AnotherHealthCheck}"/></param>
    public AnotherHealthCheck(ILogger<AnotherHealthCheck> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Runs the health check, returning the status of the component being checked.
    /// </summary>
    /// <param name="context">A context object associated with the current execution.</param>
    /// <param name="cancellationToken">The token that can be used to cancel the health check.</param>
    /// <returns>A <see cref="Task"/> that completes when the health check has finished,
    /// yielding the status of the component being checked.</returns>
    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            //// TODO: implement health check against dependencies

            var rnd = new Random();
            var val = rnd.Next(0, 200);

            if (val % 2 == 0)
            {
                throw new Exception("Failed another health check!");
            }

            return Task.FromResult(HealthCheckResult.Healthy());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed another health check!");

            return Task.FromResult(new HealthCheckResult(
                context.Registration.FailureStatus,
                description: "Failed health check!",
                exception: ex,
                data: null));
        }
    }
}