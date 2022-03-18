namespace Products.Cli.Application;

public class Command
{
    public Command(string inputData, string dataSourceName)
    {
        InputData = inputData;
        DataSourceName = dataSourceName;
    }
    public string InputData { get; set; }
    public string DataSourceName { get; set; }
}
