using System.Collections.Generic;

namespace basketbackend.Data.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Add(T entity);
    }
}
