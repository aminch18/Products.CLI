namespace Products.Cli;

using Products.Cli.Application;
using Products.Cli.Application.Abstractions;
using Products.Cli.Application.Utils;

public interface IMainManager
{
    Task ExecuteAsync(string dataSource, string inputFilePath);
}
public class MainManager : IMainManager
{
    private readonly IHandler<Command> _handler;

    public MainManager(IHandler<Command> handler)
    {
        _handler = handler ?? throw new ArgumentNullException(nameof(handler));
    }

    public async Task ExecuteAsync(string dataSource, string inputFilePath)
    {
        try
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Infrastructure/Data", inputFilePath);

            if (!File.Exists(filePath))
            {
                Utils.WriteLine("ERROR => Unexpected file path", ConsoleColor.Red);
                return;
            }

            if (!filePath.Contains(dataSource))
            {
                Utils.WriteLine("ERROR => File to import is not related to the data source introduced", ConsoleColor.Red);
                return;
            }

            var inputData = await File.ReadAllTextAsync(filePath);
            await _handler.HandleAsync(new Command(inputData, dataSource.ToUpper()));

            return;
        }
        catch (Exception ex)
        {
            Utils.WriteLine(ex.Message, ConsoleColor.Red);
        }
    }
}

