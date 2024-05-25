namespace JobService.WebAPI.Dto;

/// <summary>
/// Represents a job total response.
/// </summary>
public sealed class JobTotalResponse
{
    /// <summary>
    /// Gets or sets items
    /// </summary>
    public ItemResponseDto[] Items { get; set; }

    /// <summary>
    /// Gets or sets job total.
    /// </summary>
    public decimal TotalCost { get; set; }
}