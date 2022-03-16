namespace Products.Cli.Services;

using YamlDotNet.Serialization;
using Products.Cli.Application.Abstractions;

public class YmlSerializer : IYmlSerializer
{
    IDeserializer _ymlDeserializer;

    public YmlSerializer(IDeserializer ymlDeserializer)
    {
        _ymlDeserializer = ymlDeserializer ?? throw new ArgumentNullException(nameof(IDeserializer));
    }

    public async Task<T> DeserializeAsync<T>(string input)
        => await Task.FromResult(_ymlDeserializer.Deserialize<T>(input));
    
    public T Deserialize<T>(string input)
        => _ymlDeserializer.Deserialize<T>(input);
}
