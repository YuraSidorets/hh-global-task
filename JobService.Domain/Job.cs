namespace JobService.Domain;

/// <summary>
/// Contains data of a job.
/// </summary>
public class Job
{
    /// <summary>
    /// Gets or sets job items.
    /// </summary>
    public Item[] Items { get; set; }

    /// <summary>
    /// Gets or sets extra margin flag.
    /// </summary>
    public bool ExtraMargin { get; set; }
}