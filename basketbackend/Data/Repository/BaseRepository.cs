using Data;
using System.Collections.Generic;
using System.Linq;

namespace basketbackend.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly DatabaseContext _context;

        public BaseRepository(DatabaseContext context)
        {
            _context = context;
        }

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
    }
}
