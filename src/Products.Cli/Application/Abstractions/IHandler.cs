namespace Products.Cli.Application.Abstractions;

using Products.Cli.Application.Models;

public interface IHandler<T> where T : Command
{
    Task HandleAsync(T command);
}
