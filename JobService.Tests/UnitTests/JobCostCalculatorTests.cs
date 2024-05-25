using FluentAssertions;
using JobService.ApplicationServices;
using JobService.Domain;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace JobService.Tests.UnitTests;

/// <summary>
/// Provides unit tests for the <see cref="JobCostCalculatorService"/> class.
/// </summary>
public class JobCostCalculatorTests
{
    private readonly IJobCostCalculatorService<Job> _calculator;

    /// <summary>
    /// Initializes a new instance of the <see cref="JobCostCalculatorTests"/> class.
    /// </summary>
    public JobCostCalculatorTests()
    {
        var options = Options.Create(new JobCostSettings
        {
            SalesTax = 0.07m,
            BaseMargin = 0.11m,
            ExtraMargin = 0.05m
        });
        _calculator = new JobCostCalculatorService(options);
    }

    [Theory]
    [MemberData(nameof(CalculateJobCostTestData))]
    public void CalculateJobCost_ValidInput_ReturnsCorrectResult(Job job, Dictionary<string, decimal> expectedItemCosts, decimal totalExpectedCost)
    {
        // act
        var result = _calculator.CalculateCost(job);

        // assert
        result.Items.Should().NotBeNullOrEmpty();
        foreach (var expectedItemCost in expectedItemCosts)
        {
            var itemResult = Array.Find(result.Items, i => i.Name == expectedItemCost.Key);
            itemResult.Should().NotBeNull();
            itemResult.Cost.Should().Be(expectedItemCost.Value);
        }
        result.TotalCost.Should().Be(totalExpectedCost);
    }

    public static IEnumerable<object[]> CalculateJobCostTestData()
    {
        yield return new object[]
        {
            new Job
            {
                ExtraMargin = true,
                Items =
                [
                    new Item { Name = "envelopes", Cost = 520.00m, Exempt = false },
                    new Item { Name = "letterhead", Cost = 1983.37m, Exempt = true }
                ]
            },
            new Dictionary<string, decimal>
            {
               { "envelopes", 556.40m },
               { "letterhead", 1983.37m }
            },
            2940.30m
        };

        yield return new object[]
        {
            new Job
            {
                ExtraMargin = false,
                Items =
                [
                    new Item { Name = "t-shirts", Cost = 294.04m, Exempt = false },
                ]
            },
            new Dictionary<string, decimal>
            {
               { "t-shirts", 314.62m },
            },
            346.96m
        };

        yield return new object[]
        {
            new Job
            {
                ExtraMargin = true,
                Items =
                [
                    new Item { Name = "frisbees", Cost = 19385.38m, Exempt = true },
                    new Item { Name = "yo-yos", Cost = 1829m, Exempt = true },
                ]
            },
            new Dictionary<string, decimal>
            {
               { "frisbees", 19385.38m },
               { "yo-yos", 1829.00m },
            },
            24608.68m
        };
    }
}