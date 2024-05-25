namespace JobService.WebAPI.Dto;

/// <summary>
/// Represents an item.
/// </summary>
public sealed class ItemDto
{
    /// <summary>
    /// Gets or sets item name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets item cost.
    /// </summary>
    public decimal Cost { get; set; }

    /// <summary>
    /// Gets or sets exempt flag.
    /// </summary>
    public bool Exempt { get; set; }
}