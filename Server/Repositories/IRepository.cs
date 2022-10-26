using StoreBlazor.Client.Aggregates;
using System.Linq.Expressions;

namespace StoreBlazor.Server.Repositories
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate);
        void Add(T item);
        T Remove(T item);
        Task<bool> Any(Expression<Func<T, bool>> predicate);
    }
}
