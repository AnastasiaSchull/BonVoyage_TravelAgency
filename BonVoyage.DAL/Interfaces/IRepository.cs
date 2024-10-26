using System.Linq.Expressions;

namespace BonVoyage.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IQueryable<T>> GetAll();
        Task<T> Get(int id);
        Task Create(T item);
        void Update(T item);
        Task Delete(int id);

        //дефолтные методы
        Task<int> CountAsync()
        {
            return Task.FromResult(0);
        }

        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate) => Task.FromResult<IEnumerable<T>>(null);
    }
}
