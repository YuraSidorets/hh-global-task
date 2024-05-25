using AutoMapper;
using JobService.ApplicationServices;
using JobService.Domain;
using JobService.WebAPI.Dto;
using Microsoft.AspNetCore.Mvc;
using System;

namespace JobService.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobController : ControllerBase
{
    private readonly IJobCostCalculatorService<Job> _jobCostCalculatorService;
    private readonly IMapper _mapper;

    public JobController(IJobCostCalculatorService<Job> jobCostCalculatorService, IMapper mapper)
    {
        ArgumentNullException.ThrowIfNull(jobCostCalculatorService);
        ArgumentNullException.ThrowIfNull(mapper);

        _jobCostCalculatorService = jobCostCalculatorService;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("total")]
    public IActionResult CalculateTotal([FromBody] JobTotalRequest jobRequest)
    {
        var job = _mapper.Map<JobTotalRequest, Job>(jobRequest);

        var jobCost = _jobCostCalculatorService.CalculateCost(job);

        var response = _mapper.Map<JobCostResult, JobTotalResponse>(jobCost);
        return Ok(response);
    }
}