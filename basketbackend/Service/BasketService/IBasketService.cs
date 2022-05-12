using basketbackend.Presentation.Models;
using System.Threading.Tasks;

namespace basketbackend.Service.BasketService
{
    public interface IBasketService
    {
        public Task<BasketResponse> CreateBasket(string customer, bool paysVat);
        public Task<BasketResponse> AddItemToBasket(int basketId, string name, double price);
        public Task<BasketResponse> CloseBasket(int id, bool paid);
        public Task<BasketResponse> Get(int id);
    }
}
