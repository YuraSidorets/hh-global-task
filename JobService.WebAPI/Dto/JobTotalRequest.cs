namespace JobService.WebAPI.Dto;

/// <summary>
/// Represents a job total request.
/// </summary>
public sealed class JobTotalRequest
{
    /// <summary>
    /// Gets or sets items
    /// </summary>
    public ItemDto[] Items { get; set; }

    /// <summary>
    /// Gets or sets extra margin flag.
    /// </summary>
    public bool ExtraMargin { get; set; }
}