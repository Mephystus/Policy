// -------------------------------------------------------------------------------------
//  <copyright file="IPolicyApiClient.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Policy.Api.Client.Interfaces;

using Policy.Models;

/// <summary>
/// Provides the contracts for policy API.
/// </summary>
public interface IPolicyApiClient
{
    /// <summary>
    /// Gets a customer.
    /// </summary>
    /// <param name="id">The customer Id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The policy.</returns>
    Task<PolicyResponse> GetPolicyAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the customer's policies.
    /// </summary>
    /// <param name="customerId">The customer Id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The policy.</returns>
    Task<List<PolicyResponse>> GetCustomerPoliciesAsync(Guid customerId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Pings the customer API.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task<object> PingAsync(CancellationToken cancellationToken = default);
}
