namespace JobService.Domain;

/// <summary>
/// Represents the cost calculation result for an item.
/// </summary>
public class ItemCostResult
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
    /// Gets or sets item margin.
    /// </summary>
    public decimal Margin { get; set; }
}