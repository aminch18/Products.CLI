namespace Products.Cli.Application.Services;

using Utils;
using FluentValidation;
using Products.Cli.Application.Abstractions;
using Products.Cli.Application.Models;
using System.Threading.Tasks;
using Products.Cli.Domain.Models;

public class Handler : IHandler<Command>
{
    private readonly IRepository<Product> _repository;
    private readonly IImportDataService _service;
    private readonly IValidator<Command> _validator;

    public Handler(IImportDataService service, IValidator<Command> validator, IRepository<Product>)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public async Task HandleAsync(Command command)
    {
        await _validator.ValidateAndThrowAsync(command);

        var products = await _service.ImportDataAsync(command.DataSourceName, command.InputData);

        foreach (var item in products)
        {
            Utils.WriteLine($"importing {item}", ConsoleColor.White);
            
            //Perfect side to ingest data on data base.
            //await _repository.CreateAsync(item);
        }
    }
}