namespace Products.Cli.Application.Abstractions;

using Products.Cli.Domain.Models;

public interface IImportDataService
{
    public List<Product> ImportData(string dataProviderName, string dataToImport);
    public Task<List<Product>> ImportDataAsync(string dataProviderName, string dataToImport);
}
