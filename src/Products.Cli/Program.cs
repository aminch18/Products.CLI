using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Products.Cli.Application;
using Products.Cli.Application.Abstractions;
using Products.Cli.Application.Models;
using Products.Cli.Application.Services;
using Products.Cli.Application.Utils;
using Products.Cli.Services;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

#region Registering services on service collection and building service provider.

IDeserializer CreateYamlDeserializer() => new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance)
                                                                   .Build();
var services = new ServiceCollection();
var servicesProvider = services.AddSingleton<IDeserializer>(CreateYamlDeserializer())
                               .AddSingleton<IYmlSerializer, YmlSerializer>()
                               .AddSingleton<IJSONSerializer, JSONSerializer>()
                               .AddSingleton<IValidator<Command>, CommandValidator>()
                               .AddSingleton<IImportDataService, ImportDataService>()
                               .AddScoped<IHandler<Command>, Handler>()
                               .BuildServiceProvider();
#endregion

try
{
    var arguments = Environment.GetCommandLineArgs();
    var dataSource = arguments[1];
    var inputFilePath = arguments[2];
    var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", inputFilePath);

    if (!File.Exists(filePath))
    {
        Utils.WriteLine("ERROR => Unexpected file path", ConsoleColor.Red);
        return;
    }

    var inputData = await File.ReadAllTextAsync(filePath);
    await servicesProvider.GetService<IHandler<Command>>()
                          .HandleAsync(new Command(inputData, dataSource.ToUpper()));

    return;
}
catch (Exception ex)
{
    Utils.WriteLine(ex.Message, ConsoleColor.Red);
}