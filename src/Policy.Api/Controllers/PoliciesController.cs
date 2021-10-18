// -------------------------------------------------------------------------------------
//  <copyright file="PoliciesController.cs" company="The AA (Ireland)">
//    Copyright (c) The AA (Ireland). All rights reserved.
//  </copyright>
// -------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Policy.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Policy.Api.Controllers;

/// <summary>
/// The policies controller.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PoliciesController : ControllerBase
{
    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<PoliciesController> _logger;

    /// <summary>
    /// Initialises a new instance of the <see cref="PoliciesController" /> class.
    /// </summary>
    /// <param name="logger">An instance of <see cref="ILogger{PoliciesController}"/></param>
    public PoliciesController(
        ILogger<PoliciesController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Gets the policy.
    /// </summary>
    /// <param name="id">The policy Id.</param>
    /// <returns>The policy.</returns>
    [HttpGet("{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "The policy", typeof(PolicyResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Policy not found.")]
    public async Task<IActionResult> GetPolicyAsync(Guid id) 
    {
        _logger.LogInformation("Policy ID: {id}", id);

        await Task.Delay(50);

        var response = new PolicyResponse 
        {
            PolicyId = id,
            PolicyNumber = "PXZ-0987.00",
            Product = "Pension - LX",
            StartDate = DateTime.Now.AddYears(-1)
        };

        return Ok(response);
    }

    /// <summary>
    /// Gets the customer policies.
    /// </summary>
    /// <param name="id">The customer Id.</param>
    /// <returns>The policies.</returns>
    [HttpGet("customer/{id:guid}")]
    [SwaggerResponse(StatusCodes.Status200OK, "The customer policies", typeof(List<PolicyResponse>))]
    public async Task<IActionResult> GetCustomerPoliciesAsync(Guid id)
    {
        _logger.LogInformation("Customer ID: {id}", id);

        await Task.Delay(50);

        var response = new List<PolicyResponse>
        {
            new PolicyResponse
            {
                PolicyId = Guid.NewGuid(),
                PolicyNumber = "SVT-1234.03",
                Product = "Savings - MT",
                StartDate = DateTime.Now.AddYears(-1)
            },
            new PolicyResponse
            {
                PolicyId = Guid.NewGuid(),
                PolicyNumber = "IVT-0989.02",
                Product = "Investment - CC",
                StartDate = DateTime.Now.AddYears(-3)
            },
            new PolicyResponse
            {
                PolicyId = Guid.NewGuid(),
                PolicyNumber = "PNL-7123.01",
                Product = "Pension - LX",
                StartDate = DateTime.Now.AddYears(-4)
            }
        };

        return Ok(response);
    }

    /// <summary>
    /// Ping the controller.
    /// </summary>
    /// <returns>The current date.</returns>
    [HttpGet("ping")]
    [SwaggerResponse(StatusCodes.Status200OK, "The current date")]
    public IActionResult Ping()
    {
        return Ok(new
        {
            Date = DateTime.Now
        });
    }
}