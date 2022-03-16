namespace Products.Cli.Application.Abstractions;

public interface ISerializer
{
    Task<T> DeserializeAsync<T>(string input);
    T Deserialize<T>(string input);
}
