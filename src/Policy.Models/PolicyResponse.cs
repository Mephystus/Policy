// -------------------------------------------------------------------------------------
//  <copyright file="PolicyResponse.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

namespace Policy.Models;

public class PolicyResponse
{
    /// <summary>
    /// Gets or sets the policy Id.
    /// </summary>
    public Guid PolicyId { get; set; }

    /// <summary>
    /// Gets or sets the policy number.
    /// </summary>
    public string PolicyNumber { get; set; } = default!;

    /// <summary>
    /// Gets or sets the product.
    /// </summary>
    public string Product { get; set; } = default!;

    /// <summary>
    /// Gets or sets the start date.
    /// </summary>
    public DateTime StartDate { get; set; }
}