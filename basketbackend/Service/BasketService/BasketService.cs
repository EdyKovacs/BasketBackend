using AutoMapper;
using basketbackend.Data.UnitOfWork;
using basketbackend.Presentation.Models;
using basketbackend.Service.Exceptions;
using Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace basketbackend.Service.BasketService
{
    public class BasketService : IBasketService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BasketService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BasketResponse> AddItemToBasket(int basketId, string name, double price)
        {
            var basket = await _unitOfWork.BasketRepository.GetWithItemsIncluded(basketId);
            if (basket == null)
            {
                throw new BasketDoesNotExistException($"Basket with id: '{basketId}' does not exist");
            }

            if (basket.Closed)
            {
                throw new BasketAlreadyClosedException($"Basket with id: '{basket.Id}' was already closed");
            }

            var newItem = new Item()
            {
                Name = name,
                Price = price,
                Basket = basket,
                BasketId = basket.Id
            };

            if (basket.Items == null)
            {
                basket.Items = new List<Item>();
            }

            basket.Items.Add(newItem);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<BasketResponse>(basket);
        }

        public async Task<BasketResponse> CloseBasket(int basketId, bool paid)
        {
            var basket = await _unitOfWork.BasketRepository.GetWithItemsIncluded(basketId);
            if (basket == null)
            {
                throw new BasketDoesNotExistException($"Basket with id: '{basketId}' does not exist");
            }

            if (basket.Closed)
            {
                throw new BasketAlreadyClosedException($"Basket with id: '{basket.Id}' was already closed");
            }

            if (paid == true)
            {
                // Here we would probably add a component that would have the sole responsibility of verifying if the payment was indeed completed succesfully
                basket.Closed = true;
                basket.Paid = true;
                await _unitOfWork.SaveChangesAsync();
            }

            return _mapper.Map<BasketResponse>(basket);
        }

        public async Task<BasketResponse> CreateBasket(string customer, bool paysVat)
        {
            var oldBasket = _unitOfWork.BasketRepository.GetAll().SingleOrDefault(bsk => bsk.Customer == customer);
            if (oldBasket != null)
            {
                throw new BasketExistsException($"Basket with id: '{oldBasket.Id}' already exists for customer: '{customer}'");
            }

            var newBasket = new Basket(customer, paysVat);
            _unitOfWork.BasketRepository.Add(newBasket);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<BasketResponse>(newBasket);
        }

        public async Task<BasketResponse> Get(int id)
        {
            var basket = await _unitOfWork.BasketRepository.GetWithItemsIncluded(id);
            if (basket == null)
            {
                throw new BasketDoesNotExistException($"Basket with id: '{id}' does not exist");
            }

            return _mapper.Map<BasketResponse>(basket);
        }
    }
}
