namespace Products.Cli.Application.Abstractions;
public interface IRepository<T> where T : class
{
    public Task<IEnumerable<T>> GetAllAsync(int id);
    public Task<T> GetAsync(int id);
    public Task CreateAsync(T entity);
    public Task UpdateAsync(T entity);
    public Task DeleteAsync(int id);
}
