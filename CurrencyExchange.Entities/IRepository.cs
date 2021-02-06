namespace CurrencyExchange.Entities
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRepository<T> where T: class
    {
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAll();
        Task Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
