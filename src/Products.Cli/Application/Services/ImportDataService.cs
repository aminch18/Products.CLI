namespace Products.Cli.Application.Services;

using Products.Cli.Application.Abstractions;
using Products.Cli.Application.Dtos;
using Products.Cli.Application.Dtos.Extensions;
using Products.Cli.Domain.Models;
using System.Threading.Tasks;

public class ImportDataService : IImportDataService
{
    private Dictionary<Source, ISerializer> _strategy;

    public ImportDataService(IYmlSerializer ymlDeserializer, IJSONSerializer jsonSerializer)
    {
        _strategy = new Dictionary<Source, ISerializer>
        {
            { Source.CAPTERRA, ymlDeserializer  },
            { Source.SOFTWAREADVICE, jsonSerializer },
        };
    }

    public List<Product> ImportData(string dataProviderName, string dataToImport)
    {
        var source = (Source)Enum.Parse(typeof(Source), dataProviderName);
        var dtos = source switch
        {
            Source.CAPTERRA => _strategy[source].Deserialize<List<CapterraDTO>>(dataToImport)
                                                .Select(x => x.ToProductDTO()),

            Source.SOFTWAREADVICE => _strategy[source].Deserialize<List<SoftwareAdviceDTO>>(dataToImport)
                                                      .Select(x => x.ToProductDTO()),
            _ => throw new NotImplementedException()
        };

        return dtos.Select(x => Product.Build(x.Name, x.Categories, x.Twitter, source)).ToList();
    }

    public async Task<List<Product>> ImportDataAsync(string dataProviderName, string dataToImport)
    {
        var source = (Source)Enum.Parse(typeof(Source), dataProviderName);
        var dtos = source switch
        {
            Source.CAPTERRA => (await _strategy[source].DeserializeAsync<List<CapterraDTO>>(dataToImport))
                                                       .Select(x => x.ToProductDTO()),

            Source.SOFTWAREADVICE => (await _strategy[source].DeserializeAsync<List<SoftwareAdviceDTO>>(dataToImport))
                                                             .Select(x => x.ToProductDTO()),
            _ => throw new NotImplementedException()
        };

        return dtos.Select(x => Product.Build(x.Name, x.Categories, x.Twitter, source)).ToList();
    }
}