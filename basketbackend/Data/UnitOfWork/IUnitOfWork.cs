using basketbackend.Data.Repository;
using System;
using System.Threading.Tasks;

namespace basketbackend.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBasketRepository BasketRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
