namespace Unit.Tests.Application;

using FluentAssertions;
using FluentValidation;
using Moq;
using Products.Cli.Application;
using Products.Cli.Application.Abstractions;
using Products.Cli.Application.Services;
using Products.Cli.Domain.Models;
using Xunit;

public class HandlerShould
{
    public const string ValidJson = MockedData.ValidJson;
    public const string ValidYml = MockedData.ValidYml;
    public const string InvalidJson = MockedData.InvalidJson;
    public const string InvalidYml = MockedData.InvalidYml;

    private readonly Mock<IImportDataService> _mockService;
    private readonly IHandler<Command> _handler;
    public HandlerShould()
    {
        _mockService = new Mock<IImportDataService>();
        _handler = new Handler(_mockService.Object, new CommandValidator());
    }

    [Fact]
    public void Given_null_parameters_when_building_handler_then_argument_null_exception_must_be_thrown()
    {
        Action act = () => new Handler(null, null);
        act.Should().Throw<ArgumentNullException>();
    }

    [Theory]
    [InlineData(InvalidYml, "CAPTERRA")]
    [InlineData(ValidJson, "SOFTWAREADVICE")]
    [InlineData(InvalidJson, "")]
    [InlineData(InvalidYml, "")]
    public void Given_invalid_command_when_handling_then_execution_must_throw_validation_exception(string input, string provider)
    {
        var command = new Command(input, provider);
        var func = async () => await _handler.HandleAsync(command);
        func.Should().ThrowAsync<ValidationException>();
    }

    [Theory]
    [InlineData(ValidYml, "CAPTERRA")]
    [InlineData(ValidJson, "SOFTWAREADVICE")]
    public void Given_valid_command_when_handling_then_execution_must_be_succesffull(string input, string provider)
    {
        _mockService.Setup(x => x.ImportDataAsync(It.IsAny<string>(), It.IsAny<string>()))
                    .ReturnsAsync(new List<Product>());

        var command = new Command(input, provider);
        var func = async () => await _handler.HandleAsync(command);

        func.Invoke().IsCompletedSuccessfully.Should().BeTrue();
    }

    [Theory]
    [InlineData(ValidYml, "CAPTERRA")]
    [InlineData(ValidJson, "SOFTWAREADVICE")]
    public void Given_valid_command_when_handling_then_execution_verify_service_is_called_one_time(string input, string provider)
    {
        _mockService.Setup(x => x.ImportDataAsync(It.IsAny<string>(), It.IsAny<string>()))
                    .ReturnsAsync(new List<Product>());

        var command = new Command(input, provider);
        var func = async () => await _handler.HandleAsync(command);
        func.Invoke();
        _mockService.Verify(x => x.ImportDataAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }
}