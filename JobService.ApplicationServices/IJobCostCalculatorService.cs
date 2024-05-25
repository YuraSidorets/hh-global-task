using JobService.Domain;

namespace JobService.ApplicationServices;

/// <summary>
/// Describes the functionality of a service that calculates the cost of a job.
/// </summary>
public interface IJobCostCalculatorService<in TJob>
{
    /// <summary>
    /// Calculates the cost for the given job.
    /// </summary>
    /// <param name="job">The job to calculate the cost for.</param>
    JobCostResult CalculateCost(TJob job);
}