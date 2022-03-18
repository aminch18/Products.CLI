namespace Unit.Tests.Application;

using FluentAssertions;
using Moq;
using Products.Cli.Application.Abstractions;
using Products.Cli.Application.Dtos;
using Products.Cli.Application.Services;
using Products.Cli.Application.Utils;
using Products.Cli.Domain.Models;
using Xunit;

public class ImportDataServiceShould
{
    public const string ValidJson = @"
    {
        ""products"": [
            {
                ""categories"": [
                    ""Customer Service"",
                    ""Call Center""
                ],
                ""twitter"": ""@freshdesk"",
                ""title"": ""Freshdesk""
            }
          ]
    }";
    public const string ValidYml = @"---
    -
      tags: ""Bugs & Issue Tracking,Development Tools""
      name: ""GitGHub""
      twitter: ""github""
    ";
    public const string InvalidJson = MockedData.InvalidJson;
    public const string InvalidYml = MockedData.InvalidYml;

    private readonly Mock<IYmlSerializer> _mockedYmlSerializer;
    private readonly Mock<IJSONSerializer> _mockedJSONSerializer;
    private IImportDataService _importDataService;
    public ImportDataServiceShould()
    {
        _mockedYmlSerializer = new Mock<IYmlSerializer>();
        _mockedJSONSerializer = new Mock<IJSONSerializer>();

        _mockedYmlSerializer.Setup(x => x.DeserializeAsync<List<CapterraDTO>>(It.IsAny<string>()))
                            .ReturnsAsync(new List<CapterraDTO>
                            {
                                new CapterraDTO
                                {
                                    Name = "GitGHub",
                                    Twitter = "github",
                                    Tags = "Bugs & Issue Tracking,Development Tools"
                                }
                            });

        _mockedJSONSerializer.Setup(x => x.DeserializeAsync<List<SoftwareAdviceDTO>>(It.IsAny<string>()))
                              .ReturnsAsync(new List<SoftwareAdviceDTO>
                              {
                                  new SoftwareAdviceDTO
                                  {
                                      Title = "Freshdesk",
                                      Twitter = "@freshdesk",
                                      Categories = new List<string>{ "Customer Service", "Call Center" }
                                  }
                              });

        _importDataService = new ImportDataService(_mockedYmlSerializer.Object, _mockedJSONSerializer.Object);
    }

    [Theory]
    [InlineData("CAPTERRA", ValidJson)]
    [InlineData("SOFTWAREADVICE", ValidYml)]
    public void Given_valid_input_data_when_importing_data_then_service_must_return_list_products(string dataProvider, string inputData)
    {
        var expectedResult = GetExpectedResult(dataProvider);
        var func = async () => await _importDataService.ImportDataAsync(dataProvider, inputData);

        var result = (func.Invoke()).Result;

        result.Should().HaveCount(expectedResult.Count);
        result.Should().BeOfType<List<Product>>();
        result[0].Name.Should().Be(expectedResult[0].Name);
        result[0].Twitter.Should().Be(expectedResult[0].Twitter);
        result[0].Categories.Should().HaveCount(expectedResult[0].Categories.Count);
        result[0].Categories.All(x => expectedResult[0].Categories.Contains(x)).Should().BeTrue();
    }

    [Theory]
    [InlineData("CSV", ValidJson)]
    [InlineData("xxxx", ValidYml)]
    public void Given_invalid_data_provider_when_importing_data_then_service_must_throw_not_implemented_exception(string dataProvider, string inputData)
    {
        var expectedResult = GetExpectedResult(dataProvider);
        var func = async () => await _importDataService.ImportDataAsync(dataProvider, inputData);
        func.Should().ThrowAsync<NotImplementedException>();
    }

    private static List<Product> GetExpectedResult(string dataProvider)
        => (dataProvider == Constants.CAPTERRA_NAME)
            ?
            new List<Product>
            {
                new Product("GitGHub", new List<string> { "Bugs & Issue Tracking", "Development Tools" },"github", Source.CAPTERRA)
            }
            :
            new List<Product>
            {
                new Product("Freshdesk",  new List<string>{ "Customer Service", "Call Center" },"@freshdesk", Source.SOFTWAREADVICE)
            };
    
}
