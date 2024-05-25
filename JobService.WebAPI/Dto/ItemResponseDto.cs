namespace JobService.WebAPI.Dto;

/// <summary>
/// Represents an item response.
/// </summary>
public sealed class ItemResponseDto
{
    /// <summary>
    /// Gets or sets item name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets item cost.
    /// </summary>
    public decimal Cost { get; set; }
}