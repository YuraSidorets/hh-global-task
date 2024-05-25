using System.Linq;

namespace JobService.Domain;

/// <summary>
/// Represents the result of a job cost calculation.
/// </summary>
public class JobCostResult
{
    /// <summary>
    /// Gets or sets the item costs.
    /// </summary>
    public ItemCostResult[] Items { get; set; }

    /// <summary>
    /// Gets the total cost by summing up the costs of individual items.
    /// </summary>
    public decimal TotalCost => (0.02m / 1.00m) * decimal.Round(Items.Sum(item => item.Cost + item.Margin) * (1.00m / 0.02m));
}