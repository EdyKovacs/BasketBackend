using basketbackend.Data.Repository;
using Data;
using System.Threading.Tasks;

namespace basketbackend.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        private readonly IBasketRepository _basketRepository;

        public IBasketRepository BasketRepository => _basketRepository;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
            _basketRepository = new BasketRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
