namespace Products.Cli.Application;

using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Products.Cli.Application.Utils;

public class CommandValidator : AbstractValidator<Command>
{
    public CommandValidator()
    {
        RuleFor(_ => _.InputData).NotEmpty();
        RuleFor(_ => _.InputData).Must(x => IsValidJson(x))
                                 .When(x => x.DataSourceName == Constants.CAPTERRA_NAME);
        RuleFor(_ => _.DataSourceName).NotEmpty();
        RuleFor(_ => _.DataSourceName).Must(x => Constants.AVAILABLE_DATA_SOURCES.Contains(x))
                                      .WithMessage("Unavailable data source");
    }

    private bool IsValidJson(string strInput)
    {
        strInput = strInput.Trim();

        if (!(strInput.StartsWith("{") && strInput.EndsWith("}")) && !(strInput.StartsWith("[") && strInput.EndsWith("]")))
            return false;
        
        try
        {
            var obj = JToken.Parse(strInput);
            return true;
        }
        catch (JsonReaderException jex)
        {
            Console.WriteLine(jex.Message);
            return false;
        }
    }
}