using FluentValidation;
using JobService.WebAPI.Dto;

namespace JobService.WebAPI.Validators;

/// <summary>
/// This class is responsible for validating <see cref="JobTotalRequest"/> classes.
/// </summary>
/// <seealso cref="FluentValidation.AbstractValidator{T}" />
/// <seealso cref="CreatePatientHttp"/>
public class JobTotalRequestValidator : AbstractValidator<JobTotalRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="JobTotalRequestValidator"/> class.
    /// </summary>
    public JobTotalRequestValidator()
    {
        RuleFor(req => req).NotNull();
        RuleFor(req => req.Items).NotEmpty();
        RuleForEach(req => req.Items)
            .ChildRules(i =>
            {
                i.RuleFor(x => x.Name).NotEmpty();
                i.RuleFor(x => x.Cost).GreaterThan(0);
            });
    }
}