using Products.Cli;
using Products.Cli.Application;
using Microsoft.Extensions.DependencyInjection;

var servicesProvider = new ServiceCollection()
                               .AddApplicationServices()
                               .BuildServiceProvider();

var arguments = Environment.GetCommandLineArgs();
await servicesProvider.GetService<IMainManager>()
                      .ExecuteAsync(arguments[1], arguments[2]);

return;
