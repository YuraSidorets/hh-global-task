using JobService.Domain;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace JobService.ApplicationServices;

/// <summary>
/// Provides logc for calculating the cost of a job.
/// </summary>
public class JobCostCalculatorService : IJobCostCalculatorService<Job>
{
    private readonly JobCostSettings _jobCostSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="JobCostCalculatorService"/> class.
    /// </summary>
    /// <param name="jobCostSettings">The job cost settings.</param>
    public JobCostCalculatorService(IOptions<JobCostSettings> jobCostSettings)
    {
        ArgumentNullException.ThrowIfNull(jobCostSettings);
        _jobCostSettings = jobCostSettings.Value;
    }

    /// <inheritdoc />
    public JobCostResult CalculateCost(Job job)
    {
        var margin = _jobCostSettings.BaseMargin + (job.ExtraMargin ? _jobCostSettings.ExtraMargin : 0);
        var itemCosts = new List<ItemCostResult>();

        foreach (var item in job.Items)
        {
            var baseCost = item.Cost;
            var costWithTax = item.Exempt ? baseCost : baseCost * (1 + _jobCostSettings.SalesTax);
            var marginAmount = baseCost * margin;

            itemCosts.Add(new ItemCostResult
            {
                Name = item.Name,
                Cost = Math.Round(costWithTax, 2),
                Margin = marginAmount
            });
        }

        return new JobCostResult
        {
            Items = [.. itemCosts]
        };
    }
}