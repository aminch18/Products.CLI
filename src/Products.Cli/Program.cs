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
                               .AddScoped<IMainManager, MainManager>()
                               .BuildServiceProvider();
#endregion
var arguments = Environment.GetCommandLineArgs();
await servicesProvider.GetService<IMainManager>()
                      .ExecuteAsync(arguments[1], arguments[2]);

return;
