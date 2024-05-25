using FluentAssertions;
using JobService.ApplicationServices;
using JobService.Domain;
using JobService.WebAPI.Configuration;
using JobService.WebAPI.Dto;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace JobService.Tests.ComponentTests;

/// <summary>
/// Provides tests for the <see cref="JobService.WebAPI.Controllers.JobController"/> class.
/// </summary>
public class JobControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    /// <summary>
    /// Intitializes a new instance of the <see cref="JobControllerTests"/> class.
    /// </summary>
    /// <param name="factory"></param>
    public JobControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task CalculateTotal_ValidInput_ReturnsExpectedResult()
    {
        // arrange
        var client = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                config
                    .AddInMemoryCollection(new Dictionary<string, string>
                    {
                        ["JobCost:SalesTax"] = "0.07",
                        ["JobCost:BaseMargin"] = "0.11",
                        ["JobCost:ExtraMargin"] = "0.05",
                    });
            });
            builder.ConfigureServices((builder, services) =>
            {
                services.ConfigureServices(builder.Configuration);

                var jobCostResult = new JobCostResult
                {
                    Items =
                    [
                            new ItemCostResult { Name = "envelopes", Cost = 556.40m, Margin = 83.2m },
                            new ItemCostResult { Name = "letterhead", Cost = 1983.37m, Margin = 317.3392m }
                    ]
                };

                var jobCostCalculatorServiceMock = Substitute.For<IJobCostCalculatorService<Job>>();
                jobCostCalculatorServiceMock.CalculateCost(Arg.Any<Job>()).Returns(jobCostResult);

                services.AddSingleton(jobCostCalculatorServiceMock);
            });
        }).CreateClient();

        var jobRequest = new JobTotalRequest
        {
            ExtraMargin = true,
            Items =
            [
                    new ItemDto { Name = "envelopes", Cost = 520.00m, Exempt = false },
                    new ItemDto { Name = "letterhead", Cost = 1983.37m, Exempt = true }
            ]
        };

        var expectedResponse = new JobTotalResponse
        {
            Items =
            [
                    new ItemResponseDto { Name = "envelopes", Cost = 556.40m },
                    new ItemResponseDto { Name = "letterhead", Cost = 1983.37m }
            ],
            TotalCost = 2940.30m
        };

        // act
        var response = await client.PostAsJsonAsync("/api/job/total", jobRequest);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<JobTotalResponse>();

        // assert
        result.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task CalculateTotal_NullRequest_ReturnsBadRequest()
    {
        // arrange
        var client = _factory.CreateClient();

        // act
        var response = await client.PostAsJsonAsync("/api/job/total", (JobTotalRequest)null);

        // assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }
}