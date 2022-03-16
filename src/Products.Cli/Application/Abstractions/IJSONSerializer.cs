namespace Products.Cli.Application.Abstractions;

public interface IJSONSerializer : ISerializer
{
    Task<T> DeserializeAsync<T>(Stream input);
}