namespace Products.Cli.Application;

using FluentValidation;
using Products.Cli.Application.Models;

public class CommandValidator : AbstractValidator<Command>
{
    private List<string> _availableDataSources = new List<string> { "CAPTERRA", "SOFTWAREADVICE" };
    public CommandValidator()
    {
        RuleFor(_ => _.InputData).NotEmpty();
        RuleFor(_ => _.DataSourceName).NotEmpty();
        RuleFor(_ => _.DataSourceName).Must(x => _availableDataSources.Contains(x))
                                      .WithMessage("Unavailable data source");
    }
}