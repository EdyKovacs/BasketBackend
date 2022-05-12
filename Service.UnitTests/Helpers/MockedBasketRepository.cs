using basketbackend.Data.Repository;
using Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.UnitTests.Helpers
{
    public class MockedBasketRepository : IBasketRepository
    {
        private readonly List<Basket> _baskets = new();

        public Basket Add(Basket entity)
        {
            _baskets.Add(entity);
            return entity;
        }

        public IEnumerable<Basket> GetAll() => _baskets;

        public Task<Basket> GetWithItemsIncluded(int id) => Task.FromResult(_baskets.FirstOrDefault(basket => basket.Id == id));
    }
}
