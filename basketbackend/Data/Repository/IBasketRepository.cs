using Data;
using System.Threading.Tasks;

namespace basketbackend.Data.Repository
{
    public interface IBasketRepository : IRepository<Basket>
    {
        public Task<Basket> GetWithItemsIncluded(int id);
    }
}
