using Microsoft.Extensions.Options;

namespace JobService.ApplicationServices;

/// <summary>
/// Provides job cost settings.
/// </summary>
public sealed class JobCostSettings : IOptions<JobCostSettings>
{
    /// <summary>
    /// The name of the configuration section.
    /// </summary>
    public const string SectionName = "JobCost";

    /// <summary>
    /// Gets or sets the sales tax.
    /// </summary>
    public decimal SalesTax { get; set; }

    /// <summary>
    /// Gets or sets the base margin.
    /// </summary>
    public decimal BaseMargin { get; set; }

    /// <summary>
    /// Gets or sets the extra margin.
    /// </summary>
    public decimal ExtraMargin { get; set; }

    /// <inheritdoc />
    JobCostSettings IOptions<JobCostSettings>.Value => this;
}