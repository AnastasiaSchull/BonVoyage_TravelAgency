namespace BonVoyage.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IQueryable<T>> GetAll();
        Task<T> Get(int id);
        Task Create(T item);
        void Update(T item);
        Task Delete(int id);

        //дефолтный метод
        Task<int> CountAsync()
        {
            return Task.FromResult(0);
        }
    }
}
