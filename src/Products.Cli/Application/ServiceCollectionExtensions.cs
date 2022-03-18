namespace Products.Cli.Application;

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Products.Cli.Application.Abstractions;
using Products.Cli.Application.Services;
using Products.Cli.Services.Serializers;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public static class ServiceCollectionExtensions
{
    private static IDeserializer CreateYamlDeserializer() => new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance)
                                                                   .Build();
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        => services.AddSingleton<IDeserializer>(CreateYamlDeserializer())
                               .AddSingleton<IYmlSerializer, YmlSerializer>()
                               .AddSingleton<IJSONSerializer, JSONSerializer>()
                               .AddSingleton<IValidator<Command>, CommandValidator>()
                               .AddSingleton<IImportDataService, ImportDataService>()
                               .AddScoped<IHandler<Command>, Handler>()
                               .AddScoped<IMainManager, MainManager>();
}

