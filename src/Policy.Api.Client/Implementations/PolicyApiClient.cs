// -------------------------------------------------------------------------------------
//  <copyright file="PolicyApiClient.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Policy.Api.Client.Implementations;

using System;
using System.Threading.Tasks;
using Policy.Api.Client.Interfaces;
using Policy.Models;
using SharedLibrary.Api.Client;

/// <summary>
/// Direct implementation of the <see cref="IPolicyApiClient"/>.
/// </summary>
public class PolicyApiClient : ApiClientBase, IPolicyApiClient
{
    /// <summary>
    /// The base endpoint for the policy API.
    /// </summary>
    private const string BaseEnpoint = "api/policies";

    /// <summary>
    /// Initialises a new instance of the <see cref="PolicyApiClient"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client</param>
    public PolicyApiClient(HttpClient httpClient)
        : base(httpClient)
    {
    }

    /// <summary>
    /// Gets a policy.
    /// </summary>
    /// <param name="id">The policy Id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The policy.</returns>
    public async Task<PolicyResponse> GetPolicyAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await GetAsync<PolicyResponse>($"{BaseEnpoint}/{id}", cancellationToken);
    }

    /// <summary>
    /// Gets the customer's policies.
    /// </summary>
    /// <param name="customerId">The customer Id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The policy.</returns>
    public async Task<List<PolicyResponse>> GetCustomerPoliciesAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        return await GetAsync<List<PolicyResponse>>($"{BaseEnpoint}/customer/{customerId}", cancellationToken);
    }

    /// <summary>
    /// Pings the customer API.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task<object> PingAsync(CancellationToken cancellationToken = default)
    {
        return await GetAsync<object>($"{BaseEnpoint}/ping", cancellationToken);
    }
}
