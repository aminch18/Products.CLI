namespace Products.Cli.Application.Abstractions;

public interface IHandler<T> where T : Command
{
    Task HandleAsync(T command);
}
