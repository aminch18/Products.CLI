namespace Unit.Tests.Application;

using FluentAssertions;
using FluentValidation;
using Products.Cli.Application;
using Xunit;

public partial class ValidatorShould
{
    public static IEnumerable<object[]> ValidData => MockedData.ValidData;
    public static IEnumerable<object[]> InvalidData => MockedData.InvalidData;
    
    private readonly CommandValidator _commandValidator;
    public ValidatorShould()
    {
        _commandValidator = new CommandValidator();
    }

    [Theory]
    [MemberData(nameof(InvalidData))]
    public void Give_invalid_input_data_when_validator_is_validating_command_then_should_throw_validation_exception(string dataSourceName, string inputData)
    {
        var command = new Command(inputData, dataSourceName);

        var func = async () => await _commandValidator.ValidateAndThrowAsync(command);

        func.Should().ThrowAsync<ValidationException>();
    }


    [Theory]
    [MemberData(nameof(ValidData))]
    public void Give_invalid_input_data_when_validator_is_validating_command_then_should_not_throw_validation_exception(string dataSourceName, string inputData)
    {
        var command = new Command(inputData, dataSourceName);

        var func = async () => await _commandValidator.ValidateAndThrowAsync(command);

        func.Should().NotThrowAsync<ValidationException>();
    }
    
}
